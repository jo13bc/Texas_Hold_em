using Servidor.Cartas;
using Servidor.Configurations;
using Servidor.Controller;
using Servidor.Events;
using Servidor.GameMechanics.MachineState;
using Servidor.GameMechanics.State;
using Servidor.Jugador;
using Servidor.Model;
using Servidor.Utility.TexasHoldem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.GameMechanics
{
    class StateMachineConnector
    {
        private static readonly int END_HAND_SLEEP_TIME = 1000;
        public static readonly string NEXT_PLAYER_TURN = "nextPlayerTurn";
        private readonly StateMachine<ModelContext> texasStateMachine = buildStateMachine();
        private readonly Dictionary<string, IGameEventDispatcher> playerDispatcher;
        private ModelContext model;
        private IGameEventDispatcher system;
        private StateMachineInstance<ModelContext> instance;

        public StateMachineConnector(Dictionary<string, IGameEventDispatcher> pd)
        {
            this.playerDispatcher = pd;
        }

        public void createGame(Settings settings)
        {
            if (model == null)
            {
                model = new ModelContext(settings);
                model.setDealer(-1);
            }
        }
        public void addPlayer(String playerName)
        {
            if (model != null)
            {
                model.addPlayer(playerName);
            }
        }

        public void startGame()
        {
            if (instance == null && model != null )
            {
                model.setDeck(new Deck());
                instance = texasStateMachine.startInstance(model);
                model.setDealer(0);
                execute();
            }
        }

        public void betCommand(string playerName, BetCommand command)
        {
            if (instance != null && playerName.Equals(model.getPlayerTurnName()))
            {
                BetCommand betCommand = command;
                if (betCommand == null)
                {
                    betCommand = new BetCommand(TexasHoldEmUtil.BetCommandType.ERROR);
                }
                model.getPlayerByName(playerName).setBetCommand(betCommand);
                execute();
            }
        }

        private void execute()
        {
            if (instance.execute().isFinish())
            {
                model.setGameState(TexasHoldEmUtil.GameState.END);
                model.setCommunityCars(new List<Card>());
                notifyEndGame();
                instance = null;
            }
        }
       private void notifyInitHand()
        {
            notifyEvent(GameController.INIT_HAND_EVENT_TYPE);
        }

        private void notifyBetCommand()
        {
            String playerTurn = model.getLastPlayerBet().getName();
            BetCommand lbc = model.getLastBetCommand();
            foreach (string playerName in playerDispatcher.Keys)
            {
                playerDispatcher[playerName].dispatch(
                    new GameEvent(GameController.BET_COMMAND_EVENT_TYPE, playerTurn,
                    new BetCommand(lbc.getType(), lbc.getChips())));
            }
        }

        private void notifyCheck()
        {
            foreach (string playerName in playerDispatcher.Keys)
            {
                playerDispatcher[playerName].dispatch(
                    new GameEvent(GameController.CHECK_PLAYER_EVENT_TYPE, GameController.SYSTEM_CONTROLLER,
                    model.getCommunityCards()));
            }
        }

        private void notifyPlayerTurn()
        {
            String playerTurn = model.getPlayerTurnName();
            if (playerTurn != null)
            {
                playerDispatcher[playerTurn].dispatch(
                    new GameEvent(GameController.GET_COMMAND_PLAYER_EVENT_TYPE,
                    GameController.SYSTEM_CONTROLLER,
                    PlayerAdapter.toTableState(model, playerTurn)));
            }
        }

        private void notifiEndHand()
        {
            notifyEvent(GameController.END_HAND_PLAYER_EVENT_TYPE);
            //THREAD.sleep(GameController.END_HAND_SLEEP_TIME);
        }

        private void notifyEndGame()
        {
            notifyEvent(GameController.END_GAME_PLAYER_EVENT_TYPE);
            system.dispatch(new GameEvent(GameController.EXIT_CONNECTOR_EVENT_TYPE,
                GameController.SYSTEM_CONTROLLER));
            notifyEvent(GameController.EXIT_CONNECTOR_EVENT_TYPE);
        }

        private void notifyEvent(string type)
        {
            foreach (string playerName in playerDispatcher.Keys)
            {
                //playerDispatcher[playerName].dispatch(
                //    new GameEvent(GameController.SYSTEM_CONTROLLER,
                //    PlayerAdapter.toTableState(model, playerName)));
            }
        }

        private static StateMachine<ModelContext> buildStateMachine()
        {
            StateMachine<ModelContext> sm = new StateMachine<ModelContext>();
            //IState<ModelContext> initHandState = StateDecoratorBuilder<ModelContext>.after(
            //    new InitHandState(), () => notifyInitHand());

            return null;
        }

        public void setSystem(GameEventDispatcher<StateMachineConnector> g) {  }
    }
}
