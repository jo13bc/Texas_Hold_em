using Servidor.Cartas;
using Servidor.Configurations;
using Servidor.Events;
using Servidor.GameMechanics.MachineState;
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
        private long timeoutId = 0;

        public StateMachineConnector(Dictionary<string, IGameEventDispatcher> pd)
        {
            this.playerDispatcher = pd;
        }

        private static StateMachine<ModelContext> buildStateMachine()
        {
            throw new NotImplementedException();
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

        }

        private void notifyCheck()
        {

        }

        private void notifyPlayerTurn()
        {

        }

        private void notifiEndHand()
        {

        }

        private void notifyEndGame()
        {
            throw new NotImplementedException();
        }

        private void notifyEvent(string type)
        {

        }

        private StateMachine<ModelContext> buildStateMachine()
        {


            return null;
        }
    }
}
