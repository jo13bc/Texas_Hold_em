using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Servidor.Conection
{
    class Servidor
    {
        public delegate void ClientCarrier(ConexionTCP conexionTcp);
        public event ClientCarrier OnClientConnected;
        public event ClientCarrier OnClientDisconnected;
        public delegate void DataRecieved(ConexionTCP conexionTcp, string data);
        public event DataRecieved OnDataRecieved;

        private TcpListener _tcpListener;
        private Thread _acceptThread;
        private List<ConexionTCP> connectedClients = new List<ConexionTCP>();

        public Servidor()
        {
            Console.WriteLine("Servidor iniciado");
            cargarServidor();
        }

        private void cargarServidor()
        {
            OnDataRecieved += MensajeRecibido;
            OnClientConnected += ConexionRecibida;
            OnClientDisconnected += ConexionCerrada;

            EscucharClientes("127.0.0.1", 1982);
        }

        private void MensajeRecibido(ConexionTCP conexionTcp, string datos)
        {
            var paquete = new Paquete(datos);
            string comando = paquete.Comando;
            if (comando == "login")
            {
                string contenido = paquete.Contenido;
                List<string> valores = Mapa.Deserializar(contenido);

                Console.WriteLine(string.Format("Valor 1 : {0}", valores[0]));
                Console.WriteLine(string.Format("Valor 2 : {0}", valores[1]));

                var msgPack = new Paquete("resultado", "OK");
                conexionTcp.EnviarPaquete(msgPack);
            }
        }

        private void ConexionRecibida(ConexionTCP conexionTcp)
        {
            lock (connectedClients)
                if (!connectedClients.Contains(conexionTcp))
                    connectedClients.Add(conexionTcp);
            Console.WriteLine(string.Format("Clientes: {0}", connectedClients.Count));
        }

        private void ConexionCerrada(ConexionTCP conexionTcp)
        {
            lock (connectedClients)
                if (connectedClients.Contains(conexionTcp))
                {
                    int cliIndex = connectedClients.IndexOf(conexionTcp);
                    connectedClients.RemoveAt(cliIndex);
                }
            Console.WriteLine(string.Format("Clientes: {0}", connectedClients.Count));
        }

        private void EscucharClientes(string ipAddress, int port)
        {
            try
            {
                _tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
                _tcpListener.Start();
                _acceptThread = new Thread(AceptarClientes);
                _acceptThread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Error: {0}", e.Message.ToString()));
            }
        }
        private void AceptarClientes()
        {
            do
            {
                try
                {
                    var conexion = _tcpListener.AcceptTcpClient();
                    var srvClient = new ConexionTCP(conexion)
                    {
                        ReadThread = new Thread(LeerDatos)
                    };
                    srvClient.ReadThread.Start(srvClient);

                    if (OnClientConnected != null)
                        OnClientConnected(srvClient);
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Error: {0}", e.Message.ToString()));
                }

            } while (true);
        }

        private void LeerDatos(object client)
        {
            var cli = client as ConexionTCP;
            var charBuffer = new List<int>();

            do
            {
                try
                {
                    if (cli == null)
                        break;
                    if (cli.StreamReader.EndOfStream)
                        break;
                    int charCode = cli.StreamReader.Read();
                    if (charCode == -1)
                        break;
                    if (charCode != 0)
                    {
                        charBuffer.Add(charCode);
                        continue;
                    }
                    if (OnDataRecieved != null)
                    {
                        var chars = new char[charBuffer.Count];
                        //Convert all the character codes to their representable characters
                        for (int i = 0; i < charBuffer.Count; i++)
                        {
                            chars[i] = Convert.ToChar(charBuffer[i]);
                        }
                        //Convert the character array to a string
                        var message = new string(chars);

                        //Invoke our event
                        OnDataRecieved(cli, message);
                    }
                    charBuffer.Clear();
                }
                catch (IOException)
                {
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Error: {0}", e.Message.ToString()));

                    break;
                }
            } while (true);

            if (OnClientDisconnected != null)
                OnClientDisconnected(cli);
        }

    }
}

