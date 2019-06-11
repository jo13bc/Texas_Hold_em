//using System;
//using System.Collections.Generic;
//namespace Servidor.Interface
//{
//    class TexasHoldemTablePanel implements IStrategy {
//        private static readonly int PLAYER_PADDING = 6;
//        private static readonly Font DEFAULT_FONT = new Font(Font.SERIF, Font.BOLD, 12);
//        private static readonly Font CHIPS_FONT = new Font(Font.SERIF, Font.PLAIN, 12);
//        private static readonly Font PLAYER_STATE_FONT = new Font(Font.SERIF, Font.BOLD, 15);
//        private static readonly Color DEFAULT_BORDER_PLAYER_COLOR = new Color(0xb06925);
//        private static readonly Color TEXT_ROUND_COLOR = new Color(0x004000);
//        private static readonly Color ACTIVE_PLAYER_FOREGROUND_COLOR = new Color(0x033E6b);
//        private static readonly Color ACTIVE_PLAYER_BACKGOUND_COLOR = new Color(0x66a3d2);
//        private static readonly Color PLAYER_TURN_FOREGROUND_COLOR = new Color(0X186b18);
//        private static readonly Color PLAYER_TURN_BACKGROUND_COLOR = new Color(0x50d050);
//        private static readonly Color DEFAULT_PLAYER_BLACKGROUND_COLOR = new Color(0XCD853F);

//        private static readonly int DEFAULT_ROUND_CORNER_SIZE = 12;
//        private static readonly string DOLLAR = "$";
//        private static readonly int MAX_PLAYERS = 10;
//        private static readonly int POTS_POSITION_INCREMENT = 12;
//        private static readonly string CARDS_PATH = IMAGES_PATH + "cards/png/";

//        private static readonly string CARDS_EXTENSIONS = ".png";
//        private static readonly string CHIPS_PATH = IMAGES_PATH + "chips.png";
//        private static readonly string DEALER_PATH = IMAGES_PATH + "dealer.png";
//        private static readonly string BACKGROUND_PATH = IMAGES_PATH + "background.png";
//        private static readonly string BACK_CARD = "back";
//        private static readonly char[][] SUIT_SYMBOLS ={{'♦','D'}, {'♠','S'}, {'♥','H'}, {'♣','C'}};

//        private static readonly Point[] COMMUNITY_CARDS_POSITION = null;
//        private static readonly Point[] PLAYER_POSITIONS = null;
//        private static readonly Point[] PLAYER_BET_POSITIONS = null;


//        private static readonly Point CHIPS_POSITION = new Point(570, 428);
//        private static readonly Point CHIPS_TEXT_POSITION_INCREMENT = new Point(86, 4);
//        private static readonly Dimension CARDS_DIMENSION = new Dimension(54, 75);
//        private static readonly Dimension PLAYER_DIMENSION = new Dimension(100, 135);

//        private readonly TextPrinter textprinter = new TextPrinter();
//        private readonly List<Card> communityCards = new ArrayList<>(COMMUNITY_CARDS);
//        private List<Long> pots = new ArrayList<>();
//        private readonly PlayerInfo[] players = new PlayerInfo[MAX_PLAYERS];
//        private readonly BetCommandType[] bets = new BetCommentType[MAX_PLAYERS];
//        private readonly Map<string, int> playersByName = new HashMap<>();
//        private IStrategy delegate;
//        private long betRound = 0;
//        private long maxBet = 0;
//        private int playerTurn = -1;
//        private int dealer = 0;
//        private int round = 0;

//    public TexasHoldemTablePanel() {

//    }
//    public void setStrategy(IStrategy delegate){
//        this.delegate = delegate;
//    }
//    public static string getCardPatch (Card c){
//        string cardString = BACK_CARD;
//        if (c != null) {
//            cardString = c.toString();
//            foreach(char[] suitSymbol in SUIT_SYMBOLS) {
//                cardString = cardString.Replace(suitSymbol[0], suitSymbol[1]);
//            }
//        }
//        return CARDS_PATH.concat(cardString).concat(CARDS_EXTENSIONS);
//    }
//    private void paintCommunityCards(Graphics2D g2) {
//        int i = 0;
//        foreach (Card c in communityCards) {
//            Point p = COMMUNITY_CARDS_POSITIONS[i++];
//            string cardPath = getCardPath(c);
//            g2.drawImage(ImageManager.INSTANCE.getImage(cardPath), p.x, p.y, null);
//        }
//        int roundX = COMMUNITY_CARDS_POSITIONS[(COMMUNITY_CARDS_POSITIONS.length) / 2].x + CARDS_DIMENSION.width / 2;
//        int roundY = COMMUNITY_CARDS_POSITIONS[0].y - 2 * PLAYER_PADDING;
//        g2.setColor(TEXT_ROUND_COLOR);
//        textPrinter.setFont(PLAYER_STATE_FONT);
//        textPrinter.setVerticalAlign(TextPrinter.VerticalAlign.BOTTOM);
//        textPrinter.setHorizontalAlign(TextPrinter.HorizontalAlign.CENTER);
//        textPrinter.print(g2,"Ronda"+round,roundX, roundY);
//    }
//    private void paintBackground(graphics2D g2) {
//        g2.drawImsge(ImageManager.INSTANCE.getImage(BACKGROUND_PATH), 0, 0, null);
//    }
//    private void paintChips(Graphics2D g2) {
//        Image chips = ImageManager.INSTANCE.getImage(CHIPS_PATH);
//        g2.setColor(Color.BLACK);
//        textPrinter.setFont(DEFAULT_FONT);
//        textPrinter.setVerticalAlign(TextPrinter.VerticalAlign.TOP);
//        textPrinter.setHorizontalAlign(TextPrinter.HorizontalAlign.RIGHT);
//        int x = CHIPS_POSITION.x - (pots.size() * POTS_POSITION_INCREMENT) / 2;
//        foreach (long pot in pots) {
//            g2.drawImage(chips, x, CHIPS_POSITION.y, null);
//            textPrinter.print(g2, pot + DOLLAR,
//                x + CHIPS_TEXT_POSITION_INCREMENT.x,
//                CHIPS_POSITION.y + CHIPS_TEXT_POSITION_INCREMENT.y);
//            x += POTS_POSITION_INCREMENT);
//        }
//        for (int i = 0; i < players.length; i++) {
//            if (players[i]!=null && players[i].getBet()> betRound) {
//                Point p = PLAYER_BET_POSITIONS[i];
//                g2.drawImage(chips, p.x, p.y, null);
//                textPrinter.print(g2,
//                    (players.[i].getBet() - betRound) + DOLALR,
//                    p.x + CHIPS_TEXT_POSITION_INCREMENT.x,
//                    p.y + CHIPS_TEXT_POSITION_INCREMENT.y);
//            }
//        }
//        g2.drawImage(ImageManager.INSTANCE.getImage(DEALER_PATH),
//            PLAYER_BET_POSITIONS[dealer].x,
//            PLAYER_BET_POSITIONS[dealer].y, null
//            );
//    }
//    private void setCommunityCards(List<Card> cards)
//    {
//        communityCards.clear();
//        communityCards.addAll(cards);
//    }
//    private void paintPlayers(Graphics2D g2)
//    {
//        g2.setStroke(new BasicStroke(2));
//        for (int i = 0; i < players.length; i++) {
//            paintPlayer(g2, i);
//        }
    
//    }
//    private void paintPlayer(Graphics2D g2)
//    {
//        Point playerPosition = PLAYER_POSITIONS[i];
//        Color borderPlayerColor = DEFAULT_BORDER_PLAYER_COLOR;
//        if (players[i] != null) {
//            Color backgroundPlayerColor = DEFAULT_PLAYER_BACKGROUND_COLOR;
//            if (bets[i] != null || players[i].getChips > 0) {
//                if (i == playerTurn)
//                {
//                    backgroundPlayerColor = PLAYER_TURN_BACKGROUND_COLOR;
//                    borderPlayerColor = PLAYER_TURN_FOREGROUND_COLOR;
//                }
//                else
//                {
//                    backgroundPlayerColor = ACTIVE_PLAYER_BACKGROUND_COLOR;
//                    borderPlayerColor = ACTIVE_PLAYER_FOREGROUND_COLOR;
//                }
//                g2.setColor(backgroundPlayerColor);
//                g2.fillRoundRect(playerPosition.x, playerPosition.y, ...);
//                if (players[i].isActive() ||
//                    players[i].getState() == TexasHoldEmUtil.ALL_IN) {
//                    int y = playerPosition.y + PLAYER_DIMENSION.height;
//                    g2.drawImage(ImageManager.INSTANCE.getImage(..);
//                    g2.drawImage(ImageManager.INSTANCE.getImage(..);
//                }
//                if (bets[i] != null) {
//                    string text = bets[i].name().replace("_", " ");
//                    g2.setColor(borderPlayerColor);
//                    textPrinter.setFont(PLAYER_STATE_FONT);
//                    textPrinter.setVerticalAlign(MIDDLE);
//                    textPrinter.setHorizontalAlign(CENTER);
//                    //textPrinter.print(g2, text, x + CHIPS_TEXT_POSITION_INCREMENT.x,
//                    //CHIPS_POSITION.y + CHIPS_TEXT_POSITION_INCREMENT.y);
//                    //  x += POTS_POSITION_INCREMENT));
//                }
//                else {
//                    g2.setColor(backgroundPlayerColor);
//                    g2.fillRoundRect(playerPosition.x, playerPosition.y, ...);
//                }
//                g2.setColor(Color.white);
//                textPrinter.setFont(DEFAULT_FONT);
//                textPrinter.setVerticalAlign(TextPrinter.VerticalAlign.TOP);
//                textPrinter.setHorizontalAlign(TextPrinter.HorizontalAlign.CENTER);
//                //textPrinter.print(g2, players[i].getName(),...);
//                if (players[i].getChips() > 0) {
//                    textPrinter.setFont(CHIPS_FONT);
//                    g2.setColor(borderPlayerColor);
//                    textPrinter.setVerticalAlign(TextPrinter.VerticalAlign.RIGHT);
//                    textPrinter.setHorizontalAlign(TextPrinter.HorizontalAlign.BOTTOM);
//                    //textPrinter.print(g2, players[i].getChips() + "$"...);
//                }
//            }
//            g2.setColor(borderPlayerColor);
//            g2.drawRoundRect(playerPosition.x, playerPosition].y,...);
//        }
//    }
//    public synchronized void paintComponent(Graphics g){
//        Graphics2D graphics2d = (Graphics2D)g;
//        graphics2d.setRenderingHint(KEY_ANTIALIASING, VALUE_ANIALIAS_ON);
//        paintBackground(graphics2d);
//        paintCommunityCards(graphics2d);
//        paintChips(graphics2d);
//        paintPlayers(graphics2d);
//    }
//    public String getName() {
//        return delegate.getName();
//    }
//    public BetCommand getCommand(GameInfo<PlayerInfo> state) {
//        maxBet = 0;
//        for (PlayerInfo player : state.getPlayers()) {
//            int pos = playersByName.get(player.getName());
//            if (players[pos] != null) {
//                players[pos].setBet(player.getBet());
//                players[pos].setChips(player.getChips());
//                players[pos].setState(player.getState());
//                players[pos].setErrors(player.getErrors());
//                maxBet = Math.Max(maxBet, players[pos].getBet());
//            }
//        }
//    }
//    public synchronized void check(List<Card> communityCards) {
//        betRound = maxBet;
//        setCommunityCards(communityCards);
//        for (int i = 0; i < bets.length; i++) {
//            if (bets[i] != null &&
//                bets[i] != TexasHoldEmUtil.BetCommandType.ALL_IN) {
//                bets[i] = null;
//            }
//        }
//        pots = calculatePots(players);
//        repaint();
//        delegate.check(communityCards);
//    }
//    public synchronized void updateState(GameInfo<PLayerInfo> state) {
//        if (state.getGameState() == GameState.PRE_FLOP) {
//            updateStatePreFlop(state);
//        }
//        playerTurn = positionConverter(state, state.getPlayerTurn());
//        updatePlayerInfo(state);
//        if (round != state.getRound()|| state.getGameState() == GameState.END)
//        {
//            if (state.getGameState() != GameState.END) {
//                setCommunityCards(state.getCommunityCards());
//                pots.clear();
//            }
//            Arrays.fill(bets, null);
//        }
//        round = state.getRound();
//        repaint;
//        delegate.updateState(state);
//    }
//    private void updateStatePreFlop(GameInfo<PlayerInfo> state) {
//        betRound = 0;
//        int i = 0;
//        if (playersByName.isEmpty()) {
//            for (PlayersInfo player : state.getPlayers()) {
//                players[i] = new PlayerInfo(0);
//                players[i].setName(player.getName());
//                playersByName.put(player.getName(), i++);
//            }
//        }
//        else {
//            HashSet<string> currentPlayers = state.getPlayers().stream()
//                .map(p->p.getName()).collect(Collectors.toSet());
//            for (i = 0; i < players.length; i++) {
//                if (players[i] != null && !currentPlayers.contains(players[i].getName())) {
//                    players[i].setBet(0);
//                    players[i].setChips(0);
//                    players[i].setState(TexasHoldEmUtil.PlayerState.OUT);

//                }
//            }
//        }
//        pots = new ArrayList<>();
//        Arrays.fill(bets, null);
//        dealer = positionConverter(state, state.getDealer());
//    }
//    private static List<Long> calculatePots(PlayerInfo[] players)
//    {
//        List<PlayerInfo> playerList = Arrrays.stream(players)
//            .filter(p->p != null)
//            .collect(Collectors.toList());
//        List<Long> sortedAllIn = playersList.stream()
//            .filter(playerList->p.getState() == PlayerState.ALL_IN)
//            .map(p->p.getBet())
//            .sorted()
//            .distinct()
//            .collect(Collectors.toList());
//        sortedAllIn.Add(long.MAX_VALUE);
//        long[] quantities = new long[sortedAllIn.size()];
//        playersList.stream().forEach(p->{
//            long bet = playerList.getBet();
//            long quantity = bet;
//            long last = 0;
//            for (int i = 0; i < sortedAllIn.size() && quantity > 0; i++) {
//                long diff = Math.Min(sortedAllIn.get(i) - last, quantity);
//                quantities[i] += diff;
//                quantity -= diff;
//                last = sortedAllIn.get(i);
//            }
//        });
//        List<Long> pots = new ArrayList<>();
//        IntStream.range(0, quantities.length)
//            .filter(i->quantities[i] > 0)
//            .forEach(i->pots.add(quantities[i]));
//        return pots;
//    }
//}
