using System;
using System.Collections.Generic;
using System.Text;


namespace Servidor.Events
{
    class GameEventDispatcher<T> : IGameEventDispatcher
    {


        public readonly String EXIT_EVENT_TYPE = "exit";
        private Dictionary<String, IGameEventProcessor<T>> processors;
        public readonly T target;
        private List<GameEvent> events = new List<GameEvent>();
        private bool exxit = false;
        //private ExecutorService executors;


        public GameEventDispatcher(T t, Dictionary<String, IGameEventProcessor<T>> p//, ExecutorService e
            ) {
            target = t;
            processors = p;
           // executors = e;
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


    }

}
