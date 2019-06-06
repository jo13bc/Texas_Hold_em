using Servidor.Cartas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Hand
{
    interface IHandEvaluator
    {
        int eval(Card[] cards);
    }
}
