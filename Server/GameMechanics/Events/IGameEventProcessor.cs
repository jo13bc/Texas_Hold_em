using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Events
{
    interface IGameEventProcessor<T>
    {
        void process(T target , GameEvent @event);
    }
}
