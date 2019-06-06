using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Hand
{
    class HandEvaluatorSteps
    {
        private static readonly String[] VALORES = { "mano0", "iguales", "mano1" };
        private IHandEvaluator handEvaluator;
        private String resultado;

        public void unIHandEvaluator(){
            handEvaluator = new HandEvaluator();
        }

        public void calculamos_la_comparacion(String h0, String h1)
        {
        }

        public void el_resultado_esperado_es(String exResult)
        {
            
        }

    }
}
