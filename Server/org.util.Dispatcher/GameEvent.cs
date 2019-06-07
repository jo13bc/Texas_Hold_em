using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.org.util.Dispatcher
{
    class GameEvent
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

        public void getType(String type)
        {
            this.type = type;
        }

        public void getPayload(Object payload)
        {

            this.payload = payload;
        }

    }
}
