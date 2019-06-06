using Servidor;
using Servidor.Cartas;
using Servidor.Hand;
using Servidor.Jugador;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Servidor
{
    class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // new Servidor();
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Card carta = new Card(Card.Suit.CLUB, Card.Rank.ACE);

            //KA
            Deck deck = new Deck();

            deck.shuffle();

            Card c1 = deck.obtainCard();
            Card c2 = deck.obtainCard();
            Card c3 = deck.obtainCard();
            Card c4 = deck.obtainCard();
            Card c5 = deck.obtainCard();

            //SA
            Card s1 = deck.obtainCard();
            Card s2 = deck.obtainCard();

            //SA
            Card s11 = deck.obtainCard();
            Card s21 = deck.obtainCard();
            IHandEvaluator handEvaluator = new HandEvaluator();

            List<Card> cards = new List<Card>();

            Card[] cs = { c1, c2, c3, c4, c5 };
            Card[] ss = { s1, s2 };
            Card[] ss1 = { s11, s21 };

            cards.AddRange(cs);


            Hand7Evaluator eval = new Hand7Evaluator(handEvaluator);

            Console.WriteLine("\n\nCartas Comunitarias\n");
            foreach (Card card in cs)
            {
                Console.WriteLine(card.ToString());
            }

            Console.WriteLine("\n\nCartas del Jugador A\n");
            foreach (Card card in ss)
            {
                Console.WriteLine(card.ToString());
            }

            eval.setCommunityCards(cards);

            Console.WriteLine("\nPuntaje obtenido: ");

            Console.WriteLine(eval.eval(s1, s2));


            Console.WriteLine("\n\nCartas del Jugador B\n");
            foreach (Card card in ss1)
            {
                Console.WriteLine(card.ToString());
            }

            Console.WriteLine("\nPuntaje obtenido : ");

            Console.WriteLine(eval.eval(s11, s21));

            //IHandEvaluator handEvaluator = new HandEvaluator();

            //Card[] cc = { new Card(Card.Suit.CLUB, Card.Rank.NINE),
            //     new Card(Card.Suit.HEART, Card.Rank.KING),
            //     new Card(Card.Suit.DIAMOND, Card.Rank.TRHEE),
            //     new Card(Card.Suit.HEART, Card.Rank.FOUR),
            //     new Card(Card.Suit.SPADE, Card.Rank.FIVE)};

            //foreach (Card card in cc)
            //{
            //    Console.WriteLine(card.ToString());
            //}
            //Console.WriteLine(handEvaluator.eval(cc));

            //Console.WriteLine(carta);

            //TcpListener listen = new TcpListener(IPAddress.Any, 1200);
            //Console.WriteLine("Escuchando.........");
            //listen.Start();
            //TcpClient client = listen.AcceptTcpClient();
            //Console.WriteLine("Cliente Conectdo ");
            //NetworkStream stream = client.GetStream();
            //byte[] byffer = new byte[client.ReceiveBufferSize];
            //int data = stream.Read(byffer, 0, client.ReceiveBufferSize);
            //string ch = Encoding.Unicode.GetString(byffer, 0, data);
            //int NumeroServidor = 5;
            //int NumerCliente = Convert.ToInt32(ch);
            //int resultado = NumeroServidor + NumerCliente;
            //Console.WriteLine("Mensaje Recivido: " + resultado);
            //client.Close();
            //Console.ReadLine();
        }
    }
}
