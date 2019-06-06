using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.org.util.Dispatcher
{
    interface IGameEventProcessor<T>
    {
        void process(T target , GameEvent @event);
    }
}
