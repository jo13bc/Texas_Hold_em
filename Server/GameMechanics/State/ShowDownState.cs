using Servidor.Cartas;
using Servidor.Configurations;
using Servidor.GameMechanics.MachineState;
using Servidor.Hand;
using Servidor.Jugador;
using Servidor.Logic.Player;
using Servidor.Model;
using System;
using System.Collections.Generic;
using System.Text;
using static Servidor.Utility.TexasHoldem.TexasHoldEmUtil;

namespace Servidor.GameMechanics.State
{
    class ShowDownState : IState<ModelContext>
    {
        public static readonly String NAME = "ShowDown";

        public bool execute(ModelContext model)
        {
            List<PlayerEntity> players = calculateHandValue(model.getCommunityCars(), model.getPlayers());
            Dictionary<long, List<PlayerEntity>> indexByBet = new Dictionary<long, List<PlayerEntity>>();
            foreach (PlayerEntity player in players)
            {
                
                if (player.getBet() > 0)
                {
                    //indexByBet.Add(player.getBet());
                }
            }
            return false;
        }

        public string getName()
        {
            return NAME;
        }

        private List<PlayerEntity> calculateHandValue(List<Card> cc, List<PlayerEntity> players)
        {
            Hand7Evaluator evaluator = new Hand7Evaluator(new HandEvaluator());
            evaluator.setCommunityCards(cc);
            foreach (PlayerEntity p in players)
            {
                p.setHandValue(evaluator.eval(p.getCard(0), p.getCard(1)));
            }
            return players;
        }
    }
}
