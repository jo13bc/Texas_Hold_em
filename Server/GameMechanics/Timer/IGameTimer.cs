using Servidor.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.GameMechanics.Timer
{
    interface IGameTimer
    {
        void exit();
        long getTime();
        void resetTimer(long timeoutId);
        void setTime(long time);
        IGameEventDispatcher GetDispatcher();
        void setDispatcher(IGameEventDispatcher dispatcher);
    }
}
