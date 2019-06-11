using System;
using System.Collections.Generic;
using Servidor.GameMechanics.MachineState;
using Servidor.Logic.Player;
using Servidor.Model;

using static Servidor.Utility.TexasHoldem.TexasHoldEmUtil;
namespace Servidor.GameMechanics.State
{
    class WinnerState : IState<ModelContext>
    {
        public static readonly String NAME = "Winner";

        public bool execute(ModelContext context)
        {
            PlayerEntity ptrPlayer=null;
            long suma = 0;
            List<PlayerEntity> players = context.getPlayers();
            foreach (PlayerEntity p in players) {
                if (p.isActive() || p.getState()== PlayerState.ALL_IN) {
                     ptrPlayer = p;
                    break;  
                }

            }

            foreach (PlayerEntity p in players) {
                suma += p.getBet() ;
            }

            ptrPlayer.addChips(suma);

                return true;
        }

        public string getName()
        {
            return NAME;
        }
    }
}
