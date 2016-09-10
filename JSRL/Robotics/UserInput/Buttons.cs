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
        public Buttons(ButtonEvents events)
        {
            _events = events;
            _events.EnterReleased += HandleEnter;
            _events.EscapeReleased += HandleEscape;
            _events.UpReleased += HandleUp;
            _events.DownReleased += HandleDown;
            _events.LeftReleased += HandleLeft;
            _events.RightReleased += HandleRight;
        }

        ~Buttons()
        {
            Dispose();
        }

        #region Register

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
        #endregion

        public void Remove(int id)
        {
            _entries.RemoveAll(x => x.Id == id);
        }

        public void RemoveAll()
        {
            _entries.Clear();
        }

        #region Handler
        private void HandleEnter() => HandleKey(ButtonTypes.Enter);
        private void HandleEscape() => HandleKey(ButtonTypes.Escape);
        private void HandleUp() => HandleKey(ButtonTypes.Up);
        private void HandleDown() => HandleKey(ButtonTypes.Down);
        private void HandleLeft() => HandleKey(ButtonTypes.Left);
        private void HandleRight() => HandleKey(ButtonTypes.Right);

        private void HandleKey(ButtonTypes type)
        {
            foreach (var e in _entries.Where(x => x.Type == type))
                e.Callback.Invoke();
        }
        #endregion  
        public void Dispose()
        {
            _events.EnterReleased -= HandleEnter;
            _events.EscapeReleased -= HandleEscape;
            _events.UpReleased -= HandleUp;
            _events.DownReleased -= HandleDown;
            _events.LeftReleased -= HandleLeft;
            _events.RightReleased -= HandleRight;
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