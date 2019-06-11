using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Servidor.Configurations;
using Servidor.Jugador;

namespace Servidor.Controller
{
    class GameController:IGameController
        //,Runnable
    {
        private static readonly int DISPATCHER_THREADS = 1;
        private static readonly int EXTRA_THREADS = 2;
        public static readonly String SYSTEM_CONTROLLER = "system";
   
    private readonly Dictionary<String, IGameEventDispatcher<PokerEventType>> players = new HashMap<>();
        private readonly List<String> playersByName = new ArrayList<>();
        private readonly List<ExecutorService> subExecutors = new ArrayList<>();
        private readonly Map<PokerEventType, IGameEventProcessor<PokerEventType, IStrategy>> playerProcessors;
        private readonly GameEventDispatcher<ConnectorGameEventType, StateMachineConnector> connectorDispatcher;
        private readonly StateMachineConnector stateMachineConnector;
    private readonly IGameTimer timer;
    private Settings settings;
        private ExecutorService executors;
        private boolean finish = false;


        public GameController()
           
        {


        }

        public bool addStrategy(IStrategy strategy)
        {
            throw new NotImplementedException();
        }

        public void setSettings(Settings settings)
        {
            throw new NotImplementedException();
        }

        public void start()
        {
            throw new NotImplementedException();
        }

        public void waitFinish()
        {
            throw new NotImplementedException();
        }
    }
}
