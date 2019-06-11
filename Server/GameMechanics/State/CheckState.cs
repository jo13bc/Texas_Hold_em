using Servidor.Cartas;
using Servidor.Configurations;
using Servidor.GameMechanics.MachineState;
using Servidor.Logic.Player;
using Servidor.Model;
using System;
using System.Collections.Generic;
using System.Text;
using static Servidor.Utility.TexasHoldem.TexasHoldEmUtil;

namespace Servidor.GameMechanics.State
{
    class CheckState : IState<ModelContext>
    {
        private static readonly String NAME = "Next";
        private static readonly GameState[] GAME_STATE = (GameState[])Enum.GetValues(GameState.END.GetType());
        private static readonly int[] OBATIN_CARDS = { 3, 1, 1, 0, 0 };
        public bool execute(ModelContext model)
        {
            int indexGameState = indexByGameState(model.getGameState());
            if (OBATIN_CARDS[indexGameState] > 0)
            {
                model.addCommunityCard(OBATIN_CARDS[indexGameState]);
            }
            model.setGameState(GAME_STATE[indexGameState + 1]);
            //Analizar con setActiveLastPlayers
            model.setActivePlayers(model.getActivePlayers());
            model.setBets(0);

            foreach (PlayerEntity player in model.getPlayers())
            {
                if (player.isActive())
                {
                    player.setState(PlayerState.READY);
                }
            }
            model.setPlayerTurn(ModelUtil.nextPalyer(model, model.getDealer()));
            model.setLastBetCommand(null);
            //Analizar esta parte
            model.setPlayerBet(null);
            return true;
        }

        public string getName()
        {
            return NAME;
        }

        private int indexByGameState(GameState gameState)
        {
            int i = 0;
            while (i < GAME_STATE.Length && GAME_STATE[i] != gameState)
            {
                i++;
            }
            return i;
        }
    }
}
