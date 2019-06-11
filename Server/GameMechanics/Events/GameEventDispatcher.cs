using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Servidor.Events
{
    class GameEventDispatcher<T> : IGameEventDispatcher
    {
        private readonly Thread thread;

        public readonly String EXIT_EVENT_TYPE = "exit";
        private Dictionary<String, IGameEventProcessor<T>.process> processors;
        public readonly T target;
        private List<GameEvent> events = new List<GameEvent>();
        private bool exxit = false;
        //private ExecutorService executors;


        public GameEventDispatcher(T t, Dictionary<String, IGameEventProcessor<T>.process> p, ExecutorService e
         ) {

        
            target = t;
            processors = p;
            executors = e;
        }

        public void dispatch(GameEvent gameEvent)
        {
            events.Add(gameEvent);
            //this.notify();
        }

        public void exit()
        {
            exxit = true;
            //this.notify();
        }

        private void doTask()
        {
            List<GameEvent> lastsEvents;
          //  Thread.SpinWait();
        }
    }

}
