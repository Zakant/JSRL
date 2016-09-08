using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace JSRL.Robotics.Network
{
    public class NetworkService
    {
        public static NetworkService Instance { get; } = new NetworkService();

        public bool isRunning { get; protected set; } = false;

        private TcpListener _listener;
        private TcpClient _client;

        private NetworkStream _stream;

        public void StartService()
        {
            if (isRunning) throw new InvalidOperationException("Trying to start network service, but it was allready running!");
            _listener = new TcpListener(IPAddress.Any, 8000);
            _listener.Start();

            _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, null);
        }



        public void StopService()
        {
            if (!isRunning) return;
            _listener.Stop();
            _listener = null;
            _client.Close();
            _client = null;
        }

        private void HandleAcceptTcpClient(IAsyncResult ar)
        {
            _client = _listener.EndAcceptTcpClient(ar);
            _stream = _client.GetStream();
        }
    }
}
