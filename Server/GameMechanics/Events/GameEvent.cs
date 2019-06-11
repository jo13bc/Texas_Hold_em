using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Servidor.Events
{
    class GameEvent : IDisposable, INotifyPropertyChanged
    {
        private String type;
        private String source;
        private Object payload;

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propName) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        #endregion

        public GameEvent() { }
        public GameEvent(String type, String source)
        {
            this.source = source;
            this.type = type;
            this.NotifyPropertyChanged("Source and type.");
        }

        public GameEvent(String type, String source, Object payload)
        {
            this.source = source;
            this.type = type;
            this.payload = payload;
            this.NotifyPropertyChanged("Source, type and payload.");
        }

        public String getSource()
        {
            return source;
        }

        public String getType()
        {
            return type;
        }

        public Object getPayload()
        {
            return payload;
        }

        public void setSource(String source)
        {
            this.source = source;
            this.NotifyPropertyChanged("Source");
        }

        public void setType(String type)
        {
            this.type = type;
            this.NotifyPropertyChanged("Type");
        }

        public void setPayload(Object payload)
        {
            this.payload = payload;
            this.NotifyPropertyChanged("Payload");
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
