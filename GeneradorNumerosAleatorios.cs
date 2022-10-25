using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoLineal_Simulacion
{
    internal class GeneradorNumerosAleatorios
    {
        int n;
        float t;
        float tPrima;
        List<float> PSE;
        public GeneradorNumerosAleatorios()
        {
            n = 0;
            t = 1;
            NumPseudoaleatorios oNumeros = new NumPseudoaleatorios(6, 8192, 15, 13);
            PSE = oNumeros.getNumerosPseudoaleatrios();
        }

        public float GenerarNum(float landa)
        {
            float e = (float)Math.Exp(-landa);
            foreach (var num in PSE)
            {
                tPrima = t * num;
                if(tPrima >= e)
                {
                    n++;
                    t = tPrima;
                }
                else
                {
                    Console.WriteLine(n);
                    return n;
                }
            }

            return 0;
        }
    }
}
