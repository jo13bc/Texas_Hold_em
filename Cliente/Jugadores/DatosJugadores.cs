using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Jugadores
{
    abstract class DatosJugadores: Jugador 
    {
        public abstract string nombre { get; }

        public abstract int buyin { get; }
    }
}
