using System;
using System.Collections.Generic;
using System.Text;
using static Servidor.Utility.TexasHoldem.TexasHoldEmUtil;

namespace Servidor.Jugador
{
    class BetCommand
    {
        private readonly BetCommandType type;
        private long chips;

        public BetCommand(BetCommandType type, long chips)
        {
            this.type = type;
            this.chips = chips;
        }
        public BetCommand(BetCommandType type)
        {
            //this(type,0);
        }

        public BetCommandType getType()
        {
            return type;
        }

        public long getChips()
        {
            return chips;
        }

        public void setChips(long chips)
        {
            this.chips = chips;
        }
    }
}
