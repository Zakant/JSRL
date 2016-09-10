using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace JSRL.Robotics.Network
{
    public class NetworkService
    {
        public static NetworkService Instance { get; } = new NetworkService();

        public event EventHandler<DataReceivedEventArgs> DataReceived;
        public event EventHandler<ConnectionChangedEventArgs> ConnectionChanged;


        public bool isRunning { get; protected set; } = false;

        private ConnectionStatus _status;
        public ConnectionStatus Status
        {
            get { return _status; }
            set
            {
                if(value != _status)
                {
                    var old = _status;
                    _status = value;
                    ConnectionChanged?.Invoke(this, new ConnectionChangedEventArgs(old, _status));
                }
            }
        }

        private TcpListener _listener;
        private TcpClient _client;

        private NetworkStream _stream;
        private JsonReader _reader;
        private JsonWriter _writer;
        private JsonSerializer _serializer;

        private Thread _readThread;
        public void StartService()
        {
            if (isRunning) throw new InvalidOperationException("Trying to start network service, but it was allready running!");
            isRunning = true;
            _listener = new TcpListener(IPAddress.Any, 8000);
            _listener.Start();
            _serializer = JsonSerializer.Create();

            _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, null);
        }



        public void StopService()
        {
            if (!isRunning) return;
            Status = ConnectionStatus.Disconnected;
            _listener.Stop();
            _listener = null;
            _client.Close();
            _client = null;
            _readThread.Abort();
        }

        public void Send(dynamic data)
        {
            if (!isRunning) throw new InvalidOperationException("Cant send data if not running!");
            if (Status != ConnectionStatus.Connected) return;
            _serializer.Serialize(_writer, data);
        }

        private void HandleAcceptTcpClient(IAsyncResult ar)
        {
            _client = _listener.EndAcceptTcpClient(ar);
            _stream = _client.GetStream();
            _reader = new JsonTextReader(new StreamReader(_stream, Encoding.UTF8));
            _writer = new JsonTextWriter(new StreamWriter(_stream, Encoding.UTF8));
            _readThread = new Thread(Read);
            _readThread.Start();
            Status = ConnectionStatus.Connected;
        }

        private void Read()
        {
            try
            {
                while(isRunning)
                {
                    var data = _serializer.Deserialize(_reader);
                    DataReceived?.Invoke(this, new DataReceivedEventArgs(data));
                }
            }
            catch(Exception ex)
            {
                new Thread(() =>
                {
                    StopService();
                    StartService();
                }).Start();
            }
        }
    }
}