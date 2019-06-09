using Servidor.Cartas;
using Servidor.Configurations;
using Servidor.Utility.TexasHoldem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Jugador
{
    class GameInfo <P> : PlayerInfo
    {
        private int round;
        private int dealer;
        private int playerTurn;
        private TexasHoldEmUtil.GameState gameState;
        private readonly List<Card> communityCards = new List<Card>(TexasHoldEmUtil.COMMUNITY_CARDS);
        private List<P> players;
        private Settings settings;
        public Settings getSettings()
        {
            return settings;
        }
        public void setSettings(Settings settings)
        {
            this.settings = settings;
        }
        public int getRound()
        {
            return round;
        }
        public void setRound(int round)
        {
            this.round = round;
        }
        public int getDealer()
        {
            return dealer;
        }
        public void setDealer(int dealer)
        {
            this.dealer = dealer;
        }
        public int getPlayerTurn()
        {
            return playerTurn;
        }
        public void setPlayerTurn(int playerTurn)
        {
            this.playerTurn = playerTurn;
        }
        public TexasHoldEmUtil.GameState setGameState()
        {
            return gameState;
        }
        public void setGameState(TexasHoldEmUtil.GameState gameState)
        {
            this.gameState = gameState;
        }
        public List<Card> getCommunityCards()
        {
            return new List<Card>(communityCards);
        }
        public void setCommunityCards(List<Card> communityCards)
        {
            this.communityCards.Clear();
            this.communityCards.AddRange(communityCards);
        }
        public List<P> getPlayers()
        {
            return new List<P>(players);
        }
        public void setPlayers(List<P> players)
        {
            this.players = new List<P>(players);
        }
        public P getPlayer(int index)
        {
            return players[index];
        }
        public int getNumPlayer()
        {
            return players.Count;
        }
        public void addPlayer(P player)
        {
            players.Add(player);
        }
        public void removePlayer(P player)
        {
            players.Remove(player);
        }
        public bool addCommunityCard(Card card)
        {
            if(communityCards.Count < TexasHoldEmUtil.COMMUNITY_CARDS)
            {
                communityCards.Add(card);
                return true;
            }
            return false;   
        }
        public void clearCommunityCard()
        {
            this.communityCards.Clear();
        }
    }
}
