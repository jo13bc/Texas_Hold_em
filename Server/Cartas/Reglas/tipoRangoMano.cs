﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Reglas
{
    public enum tipoRangoMano
    {
        HighCard = 0,
        Pair = 1000,
        TwoPairs = 2000,
        ThreeOfAKind = 3000,
        Straight = 4000,
        Flush = 5000,
        FullHouse = 6000,
        FourOfAKind = 7000,
        StraightFlush = 8000
    }
}
