using Jint.Native;
using MonoBrickFirmware.UserInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSRL.Robotics.UserInput
{
    public class Buttons : IDisposable
    {
        ButtonEvents _events;
        List<ButtonEntry> _entries = new List<ButtonEntry>();
        public Buttons()
        {
            _events = new ButtonEvents();
            _events.EnterReleased += () => HandleKey(ButtonTypes.Enter);
            _events.EscapeReleased += () => HandleKey(ButtonTypes.Escape);
            _events.UpReleased += () => HandleKey(ButtonTypes.Up);
            _events.DownReleased += () => HandleKey(ButtonTypes.Down);
            _events.LeftReleased += () => HandleKey(ButtonTypes.Left);
            _events.RightReleased += () => HandleKey(ButtonTypes.Right);
        }

        ~Buttons()
        {
            Dispose();
        }

        public int Enter(JsValue callback)
        {
            var entrie = new ButtonEntry(callback, ButtonTypes.Enter);
            _entries.Add(entrie);
            return entrie.Id;
        }

        public int Escape(JsValue callback)
        {
            var entrie = new ButtonEntry(callback, ButtonTypes.Escape);
            _entries.Add(entrie);
            return entrie.Id;
        }

        public int Up(JsValue callback)
        {
            var entrie = new ButtonEntry(callback, ButtonTypes.Up);
            _entries.Add(entrie);
            return entrie.Id;
        }

        public int Down(JsValue callback)
        {
            var entrie = new ButtonEntry(callback, ButtonTypes.Down);
            _entries.Add(entrie);
            return entrie.Id;
        }

        public int Left(JsValue callback)
        {
            var entrie = new ButtonEntry(callback, ButtonTypes.Left);
            _entries.Add(entrie);
            return entrie.Id;
        }

        public int Right(JsValue callback)
        {
            var entrie = new ButtonEntry(callback, ButtonTypes.Right);
            _entries.Add(entrie);
            return entrie.Id;
        }

        public void Remove(int id)
        {
            _entries.RemoveAll(x => x.Id == id);
        }

        public void RemoveAll()
        {
            _entries.Clear();
        }

        private void HandleKey(ButtonTypes type)
        {
            foreach (var e in _entries.Where(x => x.Type == type))
                e.Callback.Invoke();
        }

        public void Dispose()
        {
            _events.Kill();
            _events.Dispose();
        }

        private class ButtonEntry
        {
            public JsValue Callback { get; private set; }
            public int Id { get; private set; }
            public ButtonTypes Type { get; private set; }
            private static int _id = 0;

            public ButtonEntry(JsValue callback, ButtonTypes type)
            {
                Callback = callback;
                Type = type;
                Id = _id++;
            }
        }
    }
}
