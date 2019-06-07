using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Servidor.Events;

namespace Servidor.GameMechanics.Timer
{
    class GameTimer : IGameTimer
    {
    //    private static readonly Log  LOGGER;
    //    private static Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly string TIMEOUT_EVENT_TYPE = "timeOutCommand";
        private readonly string source;
        private long time;
        private IGameEventDispatcher dispatcher;
        private bool reset = false;
        private volatile bool exitt = false;
        //private readonly Task.Factory.StartNew = executors;
        private long timeoutId;

        public GameTimer(String source/*, ExecutorService executors*/ )
        {
            this.source = source;
            //this.executors = executors
        }
        public /*syscro*/ void exit()
        {
            this.exitt = true;
            this.reset = true;
            //this.player = null
            //notify();

        }

        public IGameEventDispatcher GetDispatcher()
        {
            return dispatcher;
        }

        public long getTime()
        {
            return time;
        }

        public void resetTimer(long timerountId)
        {
            this.timeoutId = timerountId;
            this.reset = false;
            //this.player = null
            //notify();
        }

        public void setDispatcher(IGameEventDispatcher dispatcher)
        {
            throw new NotImplementedException();
        }

        public void setTime(long time)
        {
            this.time = time;
        }
    }
}
