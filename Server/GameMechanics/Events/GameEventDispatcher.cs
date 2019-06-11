using Servidor.GameMechanics.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Servidor.Events
{
    class GameEventDispatcher<T> : IGameEventDispatcher, IDisposable, INotifyPropertyChanged
    {
        private readonly Thread thread;

        public readonly String EXIT_EVENT_TYPE = "exit";
        private Dictionary<String, IGameEventProcessor<T>.process> processors;
        public readonly T target;
        private List<GameEvent> events = new List<GameEvent>();
        private bool _exit = false;
        private Dispatcher dispatcher;

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propName) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        #endregion

        public GameEventDispatcher(T t, Dictionary<String, IGameEventProcessor<T>.process> p, Dispatcher d
           ,Thread th) {

            thread = th;
            target = t;
            processors = p;
            dispatcher = d;
        }

        public void dispatch(GameEvent gameEvent)
        {
            events.Add(gameEvent);
            this.NotifyPropertyChanged("Add GameEvent");
        }

        public void exit()
        {
            _exit = true;
            this.NotifyPropertyChanged("Exit");
        }

        private void doTask()
        {
            List<GameEvent> lastEvent;
            //Sincronized
            if (events.Count == 0)
            {
                //this.wait();
            }
            lastEvent = events;
            events = new List<GameEvent>();
            for (int i = 0; i < lastEvent.Count && !_exit; i++)
            {
                GameEvent _event = lastEvent[i];
                if (EXIT_EVENT_TYPE.Equals(_event.getType()))
                {
                    _exit = true;
                }
                else
                {
                    //process event();
                }
            }
        }

        private void run()
        {
            while (!_exit)
            {
                doTask();
            }
            //dispatcher.shutdown();
        }

        public Socket Socket { get; set; }
        public Thread Thread { get; set; }

        public void SendMessage(string message)
        {
            try
            {
                this.Socket.Send(Encoding.Unicode.GetBytes(message));
            }
            catch (ArgumentNullException)
            {
                //throw;
            }
        }

        public bool IsSocketConnected()
        {
            return IsSocketConnected(Socket);
        }

        public static bool IsSocketConnected(Socket s)
        {
            if (!s.Connected)
                return false;

            if (s.Available == 0)
                if (s.Poll(1000, SelectMode.SelectRead))
                    return false;

            return true;
        }

        #region IDisposable implementation
        private bool _isDisposed = false;
        public void Dispose()
        {
            if (!_isDisposed)
            {
                if (this.Socket != null)
                {
                    this.Socket.Shutdown(SocketShutdown.Both);
                    this.Socket.Dispose();
                    this.Socket = null;
                }
                if (this.Thread != null)
                    this.Thread = null;
                _isDisposed = true;
            }
        }
        #endregion
    }

}
