using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cliente
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ConexionTCP conexionTcp = new ConexionTCP();
        public static string IPADDRESS = "127.0.0.1";
        public const int PORT = 1982;
        public MainWindow()
        {
            InitializeComponent();
            conexionTcp.OnDataRecieved += MensajeRecibido;

            if (!conexionTcp.Connectar(IPADDRESS, PORT))
            {
                System.Windows.Forms.MessageBox.Show("Error conectando con el servidor!");
                return;
            }
        }

        internal static ConexionTCP ConexionTcp { get => conexionTcp; set => conexionTcp = value; }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            conexionTcp.OnDataRecieved += MensajeRecibido;

            if (!conexionTcp.Connectar(IPADDRESS, PORT))
            {
                System.Windows.Forms.MessageBox.Show("Error conectando con el servidor!");
                return;
            }
        }

        private void MensajeRecibido(string datos)
        {
            var paquete = new Paquete(datos);
            string comando = paquete.Comando;
            if (comando == "resultado")
            {
                string contenido = paquete.Contenido;

                //Invoke(new Action(() =>     label1.Text = string.Format("Respuesta: {0}", contenido)));
            }
        }

        private void MainWindow_Closing(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (conexionTcp.TcpClient.Connected)
            {
                var msgPack = new Paquete("login", string.Format("{0},{1}", textBoxUsuario.Text, textBoxContraseña.Text));
                conexionTcp.EnviarPaquete(msgPack);
            }
        }
    }
}
