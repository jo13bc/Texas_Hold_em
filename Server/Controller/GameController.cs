using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Servidor.Cartas;
using Servidor.Configurations;
using Servidor.Events;
using Servidor.GameMechanics;
using Servidor.Jugador;
using static Servidor.GameMechanics.Events.Dispactcher;

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
        private readonly Dictionary<String, IGameEventProcessor<IStrategy>.process> playerProcessors = new Dictionary<String, IGameEventProcessor<IStrategy>.process>();

        private readonly GameEventDispatcher<StateMachineConnector> connectorDispatcher;
        private readonly StateMachineConnector stateMachineConnector;

        private Settings settings;

        private Dispatcher executors;        
        private List<Dispatcher> subExecutors = new List<Dispatcher>();


        public GameController()
        {
            stateMachineConnector = new StateMachineConnector(players);
            connectorDispatcher = new GameEventDispatcher<StateMachineConnector>(StateMachineConnector,buildConnectorProcessors(),buildExecutor(1));
            stateMachineConnector.setSystem(connectorDispatcher);
            playerProcessors = buildPlayerProcessors();
        }


        private Dispatcher buildExecutor(int threads) {
            ExecuteService result = Executors.newFixedThreadPool(threads);
            subExecutors.Add(result);
            return result;
        }

        

        private static Dictionary<String, IGameEventProcessor<StateMachineConnector>.process> buildConnectorProcessors(){

            Dictionary<String, IGameEventProcessor<StateMachineConnector>.process> 
                cpm = new Dictionary<String, IGameEventProcessor<StateMachineConnector>.process>();
            StateMachineConnector smc = new StateMachineConnector(null);
            cpm.Add(CREATE_GAME_CONNECTOR_EVENT_TYPE,(c ,e) => c.createGame((Settings )e.getPayload()));

            cpm.Add(ADD_PLAYER_CONNECTOR_EVENT_TYPE,(c,e)=>c.addPlayer(e.getSource()));

            cpm.Add(INIT_HAND_EVENT_TYPE,(c,e)=>c.startGame());

            cpm.Add(BET_COMMAND_EVENT_TYPE,(c,e)=>c.betCommand(e.getSource(),(BetCommand)e.getPayload()));

            
            return cpm;
        }

        private Dictionary<String, IGameEventProcessor<IStrategy>.process> buildPlayerProcessors() {
            Dictionary<String, IGameEventProcessor<IStrategy>.process> ppm = new Dictionary<string, IGameEventProcessor<IStrategy>.process>();
            IGameEventProcessor<IStrategy>.process defaultProcessor = (s, e) => s.updateState((GameInfo<PlayerInfo>)e.getPayload());

            ppm.Add(INIT_HAND_EVENT_TYPE,defaultProcessor);
            ppm.Add(END_GAME_PLAYER_EVENT_TYPE,defaultProcessor);
            ppm.Add(BET_COMMAND_EVENT_TYPE,(s,e)=>s.onPlayerCommand(e.getSource(),(BetCommand)e.getPayload()));
            ppm.Add(CHECK_PLAYER_EVENT_TYPE,(s,e)=>s.check((List<Card>) e.getPayload()));
            ppm.Add(GET_COMMAND_PLAYER_EVENT_TYPE,(s,e)=> {

                GameInfo<PlayerInfo> gi = (GameInfo < PlayerInfo >) e.getPayload();
                String playerTurn = gi.getPlayers()[(gi.getPlayerTurn())].getName();
                BetCommand cmd = s.GetCommand(gi);
                connectorDispatcher.dispatch(new GameEvent(BET_COMMAND_EVENT_TYPE,playerTurn,cmd));

            });

            return ppm;
        }

        public void setSettings(Settings s)
        {
            settings = s;
        }
        public bool addStrategy(IStrategy strategy)
        {
            bool result = false;
            String name = strategy.getName();
            if (!players.ContainsKey(name)&&!SYSTEM_CONTROLLER.Equals(name)) {
                players.Add(name,, new GameEventDispatcher<IGameEventDispatcher>(strategy,playerProcessors,buildExecutor(DISPATCHER_THREADS)));
            }
            return true;
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
