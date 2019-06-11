using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Events
{
    public class IGameEventProcessor<T>
    {
        public delegate void process(T target, GameEvent _event);
    }
}
