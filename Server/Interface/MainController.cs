//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Servidor.Interface
//{
//    public sealed class MainController {
//        private static readonly int PLAYERS = 4;
//        public static void Main(string[] args) {
//            IStrategy strategyMain = new RandomStrategy("RandomStrategy-0");
//            List<IStrategy> strategies = new ArrayList<>();
//            strategies.add(strategyMain);
//            for (int i = 1; i < PLAYERS; i++) {
//                strategies.add(new RandomStrategy("RandomStrategy-" + string.valueOf(i)));
//            }
//            Collections.shuffle(strategies);
//            Settings settings = new Settings();
//            settings.setMaxErrors(3);
//            settings.setMaxPlayers(PLAYERS);
//            settings.setMaxRound(1000);
//            settings.setTime(500);
//            settings.setPlayerChip(5000L);
//            settings.setRound4IncrementBlind(20);
//            settings.setSmallBind(settings.getPlayerChip() / 100);
//            IGameController controller = new IGameController();
//            controller.setSettings(settings);
//            foreach (IStrategy strategy in strategies) {
//                controller.addStrategy(strategy);
//            }
//            controller.start();
//        }
//    }
//}