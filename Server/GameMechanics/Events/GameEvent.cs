using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Servidor.Events
{
    public class GameEvent
    {
        private String type;
        private String source;
        private Object payload;

        public GameEvent() { }
        public GameEvent(String type, String source)
        {
            this.source = source;
            this.type = type;
        }

        public GameEvent(String type, String source, Object payload)
        {
            this.source = source;
            this.type = type;
            this.payload = payload;
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
        }

        public void setType(String type)
        {
            this.type = type;
        }

        public void setPayload(Object payload)
        {
            this.payload = payload;
        }

    }
}
