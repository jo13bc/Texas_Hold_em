using System;
using System.Collections.Generic;
using System.Text;


namespace Servidor.org.util.Dispatcher
{
    class GameEventDispatcher : IGameEventDispatcher
    {

        private static const Logger LOGGER = LoggerFactory.getLogger(GameEventDispatcher.class);
    private const Map<E, IGameEventProcessor<E, T>> processors;
        private const T target;
    private const E exitEven;

        private List<GameEvent> events = new ArrayList<GameEvent>();
        private volatile bool exit = false;
        private ExecutorService executors;

        public void dispatch(GameEvent gameEvent)
        {
            throw new NotImplementedException();
        }

        public void exit()
        {
            throw new NotImplementedException();
        }
    }

}
