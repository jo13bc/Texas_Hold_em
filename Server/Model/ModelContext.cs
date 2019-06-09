using Servidor.Cartas;
using Servidor.Configurations;
using Servidor.Jugador;
using Servidor.Logic.Player;
using Servidor.Utility.TexasHoldem;
using System;
using System.Collections.Generic;
using System.Text;
using static Servidor.Utility.TexasHoldem.TexasHoldEmUtil;

namespace Servidor.Model
{
    class ModelContext
    {
        private readonly GameInfo<PlayerEntity> _gameInfo = new GameInfo<PlayerEntity>();
        private readonly Dictionary<string, PlayerEntity> _playersByName;
        private int _activePlayers;
        private long _highBet;
        private Deck _deck;
        private int _playersAllIn;
        private BetCommand _lastBetCommand;
        private PlayerEntity _lastPlayerBet;
        private int _bets = 0;

        public ModelContext(Settings _settings)
        {
            this._gameInfo.setSettings(_settings);
            this._gameInfo.setPlayers(new List<PlayerEntity>(TexasHoldEmUtil.MAX_PLAYERS));
            this._playersByName = new Dictionary<string, PlayerEntity>(_settings.getMaxPlayers());
            this._highBet = 0;
        }
        public Deck getDeck()
        {
            return _deck;
        }
        public void setDeck(Deck _deck)
        {
            this._deck = _deck;
        }

        public int getPlayersAllIn()
        {
            return _playersAllIn;
        }

        public void setPlayersAllIn(int _playersAllIn)
        {
            this._playersAllIn = _playersAllIn;
        }

        public int getNumPlayers()
        {
            return _gameInfo.getNumPlayer();
        }

        public void addPlayer(string _playerName)
        {
            PlayerEntity player = new PlayerEntity();
            player.setName(_playerName);
            player.setChips(_gameInfo.getSettings().getPlayerChip());
            this._playersByName.Add(_playerName, player);
            this._gameInfo.addPlayer(player);
        }

        public void removePlayer(string playerName)
        {
            _gameInfo.removePlayer(_playersByName[playerName]);
        }

        public long getHighBet()
        {
            return _highBet;
        }
        public void setHighBet(long _highBet)
        {
            this._highBet = _highBet;
        }
        public int getDealer()
        {
            return _gameInfo.getDealer();
        }
        public void getDealer(int _dealer)
        {
            this._gameInfo.setDealer(_dealer);
        }
        public int getRound()
        {
            return _gameInfo.getRound();
        }
        public void setRound(int _round)
        {
            this._gameInfo.setRound(_round);
        }
        public GameState getGameState()
        {
            return getGameState();
        }
        public void setGameState(GameState _gameState)
        {
            this.setGameState(_gameState);
        }
        public string getPlayerTurnName()
        {
            string result = null;
            int turnPlayer = _gameInfo.getPlayerTurn();
            if (turnPlayer >= 0)
            {
                result = _gameInfo.getPlayer(turnPlayer).getName();
            }
            return result;
        }
        public int getPlayerTurn()
        {
            return _gameInfo.getPlayerTurn();
        }
        public void setPlayerTurn(int _playerTurn)
        {
            this._gameInfo.setPlayerTurn(_playerTurn);
        }
        public Settings getSettings()
        {
            return _gameInfo.getSettings();
        }
        public List<Card> getCommunityCars()
        {
            return _gameInfo.getCommunityCards();
        }
        public void setCommunityCars(List<Card> _commynityCards)
        {
            this._gameInfo.setCommunityCards(_commynityCards);
        }
        public List<PlayerEntity> getPlayers()
        {
            return _gameInfo.getPlayers();
        }
        public PlayerEntity getPlayer(int _player)
        {
            return this._gameInfo.getPlayer(_player);
        }
        public void setPlayers(List<PlayerEntity> _players)
        {
            this._gameInfo.setPlayers(_players);
            this._playersByName.Clear();
            foreach (PlayerEntity player in _players)
            {
                this._playersByName.Add(player.getName(), player);
            }
            this._gameInfo.setPlayers(_players);
        }
        public PlayerEntity getPlayerByName(string _name)
        {
            return _playersByName[_name];
        }
        public int getActivePlayers()
        {
            return _activePlayers;
        }
        public void setActivePlayers(int _activePlayers)
        {
            this._activePlayers = _activePlayers;
        }
        public void lastResultCommand(PlayerEntity _player, BetCommand _resultCommand)
        {
            this._lastPlayerBet = _player;
            _lastBetCommand = _resultCommand;
        }
        public BetCommand getLastBetCommand()
        {
            return _lastBetCommand;
        }
        public void setLastBetCommand(BetCommand _resultLastbetCommand)
        {
            this._lastBetCommand = _resultLastbetCommand;
        }
        public PlayerEntity getLastPlayerBet()
        {
            return _lastPlayerBet;
        }
        public void setPlayerBet(PlayerEntity _lastPlayerBet)
        {
            this._lastPlayerBet = _lastPlayerBet;
        }
        public int getBets()
        {
            return _bets;
        }
        public void setBets(int _bets)
        {
            this._bets = _bets;
        }
        public int addCommunityCard(int numCards)
        {
            bool added = true;
            int i = 0;
            while (i < numCards && added)
            {
                added = _gameInfo.addCommunityCard(_deck.obtainCard());
            }
            if(added){
                i++;
            }
            return i;
        }
        public void clearCommunityCard()
        {
            _gameInfo.clearCommunityCard();
        }
    }
}
