using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Configurations
{
    public class Settings
    {
        private int maxPlayers;
        private long time;
        private int maxErrors;
        private int maxRounds;
        private long PlayerChip;
        private long smallBind;
        private int rounds4IncrementBlind;

        public Settings()
        {
            this.maxPlayers = 4;
            this.time = 0;
            this.maxErrors = 10;
            this.maxRounds = 10;
            this.PlayerChip = 0;
            this.smallBind = 10;
            this.rounds4IncrementBlind = 1;
        }

        public Settings(Settings s)
        {
            this.maxPlayers = s.maxPlayers;
            this.time = s.time;
            this.maxErrors = s.maxErrors;
            this.maxRounds = s.maxRounds;
            this.PlayerChip = s.PlayerChip;
            this.smallBind = s.smallBind;
            this.rounds4IncrementBlind = s.rounds4IncrementBlind;
    }

        public int getMaxErrors()
        {
            return maxErrors;
        }

        public void setMaxErrors(int maxErrors)
        {
            this.maxErrors = maxErrors;
        }

        public int getMaxPlayers()
        {
            return maxPlayers;
        }

        public void setMaxPlayers(int maxPlayers)
        {
            this.maxPlayers = maxPlayers;
        }

        public long getTime()
        {
            return time;
        }

        public void setTime(long time)
        {
            this.time = time;
        }

        public long getPlayerChip()
        {
            return PlayerChip;
        }

        public void setPlayerChip(long playerChip)
        {
            this.PlayerChip = playerChip;
        }

        public long getSmallBind()
        {
            return smallBind;
        }

        public void setSmallBind(long smallBind)
        {
            this.smallBind = smallBind;
        }

        public long getBigBind()
        {
            return smallBind * 2;
        }

        public int getMaxRounds()
        {
            return maxRounds;
        }

        public void setMaxRounds(int maxRounds)
        {
            this.maxRounds = maxRounds;
        }

        public int getRound4IncrementBlind()
        {
            return rounds4IncrementBlind;
        }

        public void setRounds4IncrementBlind(int rounds4IncrementBlind)
        {
            this.rounds4IncrementBlind = rounds4IncrementBlind;
        }
    }
}
