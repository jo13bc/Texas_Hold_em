using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Cartas
{

    public class Carta
    {

        private Rango valorCarta;
        private Palo paloCarta;
        private bool bocaArriba;
        public enum Rango
        {
            Dos = 2, Tres = 3, Cuatro = 4, Cinco = 5, Seis = 6, Siete = 7, Ocho = 8, Nueve = 9, Diez = 10,
            Jota = 11, Reina = 12, Rey = 13, As = 14,
        }

        public enum Palo
        {
            Trebol = 0, // ♣
            Diamante = 1, // ♦
            Corazon = 2, // ♥
            Espada = 3 // ♠
        }

        public Carta() {
            valorCarta = Rango.As ;
            paloCarta = Palo.Corazon;
            bocaArriba = false;
        }
        public  Carta(Rango r, Palo p, bool b)
        {
            valorCarta = r;
            paloCarta = p;
            bocaArriba = b;
        }

        ~Carta(){ }


        public Rango getValor() { return valorCarta; }
        public Palo getPalo() { return paloCarta; }
        public bool estaBocaArriba() { return bocaArriba; }

        public void setValor(Rango r) { valorCarta = r; }
        public void setPalo(Palo p) { paloCarta = p; }
        public void voltear() { bocaArriba = !bocaArriba; }
    }
 }

      
   



     
	

