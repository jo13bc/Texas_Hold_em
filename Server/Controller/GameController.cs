using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Servidor.Configurations;
using Servidor.Events;
using Servidor.GameMechanics;
using Servidor.Jugador;

namespace Servidor.Controller
{
    class GameController:IGameController
        //,Runnable
    {
        private static readonly int DISPATCHER_THREADS = 1;
        private static readonly int EXTRA_THREADS = 2;
        public static readonly String SYSTEM_CONTROLLER = "system";

        public static readonly String INIT_HAND_EVENT_TYPE = "initHand";
        public static readonly String BET_COMMAND_EVENT_TYPE = "initHand";

        public static readonly String END_GAME_PLAYER_EVENT_TYPE = "endGame";
        public static readonly String END_HAND_PLAYER_EVENT_TYPE = "endHand";

        public static readonly String CHECK_PLAYER_EVENT_TYPE = "check";

        public static readonly String GET_COMMAND_PLAYER_EVENT_TYPE = "getCommand";

        public static readonly String EXIT_CONNECTOR_EVENT_TYPE = "exit";
        public static readonly String ADD_PLAYER_CONNECTOR_EVENT_TYPE = "addPlayer";
        public static readonly String TIMEOUT_CONNECTOR_EVENT_TYPE = "timeOutCommand";
        public static readonly String CREATE_GAME_CONNECTOR_EVENT_TYPE = "createGame";

        private readonly Dictionary<String, IGameEventDispatcher> players = new Dictionary<string, IGameEventDispatcher>();
        private readonly List<String> playersByName = new List<string>();
        private readonly Dictionary<String, IGameEventProcessor<IStrategy>> playerProcessors = new Dictionary<String, IGameEventProcessor<IStrategy>>();

        private readonly GameEventDispatcher<StateMachineConnector> connectorDispatcher;
        private readonly StateMachineConnector stateMachineConnector;

        private Settings settings;

        private ExecutorService executors;        
        private List<ExecutorService> subExecutors = new List<ExecutorService>();


        public GameController()
        {
            stateMachineConnector = new StateMachineConnector(players);
            connectorDispatcher = new GameEventDispatcher<StateMachineConnector>(StateMachineConnector,buildConnectorProcessors(),buildExecutor());
            stateMachineConnector.setSystem(connectorDispatcher);
            playerProcessors = buildPlayerProcessors();
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
