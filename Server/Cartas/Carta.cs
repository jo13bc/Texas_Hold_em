using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Cartas
{

    class Carta
    {

        Rango valorCarta;
        Palo paloCarta;
        bool bocaArriba;
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

        Carta() {
            valorCarta = Rango.As ;
            paloCarta = Palo.Corazon;
            bocaArriba = false;
        }
        Carta(Rango r, Palo p, bool b)
        {
            valorCarta = r;
            paloCarta = p;
            bocaArriba = b;
        }

        ~Carta(){ }


        Rango getValor() { return valorCarta; }
        Palo getPalo() { return paloCarta; }
        bool estaBocaArriba() { return bocaArriba; }

        void setValor(Rango r) { valorCarta = r; }
        void setPalo(Palo p) { paloCarta = p; }
        void voltear() { bocaArriba = !bocaArriba; }
    }
 }

      
   



     
	

