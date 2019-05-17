using Servidor;
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
            new Servidor();

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
