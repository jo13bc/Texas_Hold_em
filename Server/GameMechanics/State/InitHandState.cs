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
    class InitHandState : IState<ModelContext>
    {
        private static readonly string NAME = "InitHand";
        public bool execute(ModelContext model)
        {
            Deck deck = model.getDeck();
            deck.shuffle();
            Settings settings = model.getSettings();
            model.setGameState(GameState.PRE_FLOP);
            model.clearCommunityCard();
            model.setRound(model.getRound() + 1);
            //Analizar esta parte.
            if (model.getRound() % settings.getRound4IncrementBlind() == 0)
            {
                settings.setSmallBind(2 * settings.getSmallBind());
            }
            model.setPlayersAllIn(0);
            model.setHighBet(0L);
            List<PlayerEntity> players = model.getPlayers();
            foreach (PlayerEntity p in players)
            {
                p.setState(PlayerState.READY);
                p.setHandValue(0);
                p.setBet(0);
                p.showCards(false);
                p.setCards(deck.obtainCard(), deck.obtainCard());
            }
            int numPlayers = model.getNumPlayers();
            model.setActivePlayers(numPlayers);

            int dealerIndex = (model.getDealer() + 1) % numPlayers;
            model.setDealer(dealerIndex);

            model.setPlayerTurn((dealerIndex + 1) % numPlayers);
            if (numPlayers > MIN_PLAYER)
            {
                //Analizar esto
                compulsoryBet(model, settings.getSmallBind());
            }
            compulsoryBet(model, settings.getBigBind());
            //Analizar esto
            return true;
        }

        private void compulsoryBet(ModelContext model, long chips)
        {
            int turn = model.getPlayerTurn();
            PlayerEntity player = model.getPlayer(turn);
            if (player.getChips() <= chips)
            {
                player.setState(PlayerState.ALL_IN);
                ModelUtil.playerBet(model, player, BetCommandType.ALL_IN, player.getChips());
            }
            else
            {
                ModelUtil.playerBet(player, chips);
            }
            model.setHighBet(chips);
            model.setPlayerTurn((turn + 1) % model.getNumPlayers());
        }

        public string getName()
        {
            return NAME;
        }
    }
}
