using System;
using System.Collections.Generic;
using System.Text;
//i really do not think that this page is necessary!
namespace Servidor.juego.gui
{
    class TexasHoldemView /*extends javax.swing.JFrame*/{
        private static readonly int WINDOW_HEIGHT = 800;
        private static readonly int WINDOW_WIDTH = 1280;
        private TexasHoldemTablePanel jTablePanel;

        public void TexasHoldemView (IStrategy delegate){
            initComponents();
            setTitle();
        }
}
}
