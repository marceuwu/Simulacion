using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;



namespace AlgoritmoLineal_Simulacion
{
    public class AlgoritmoLineal_Simulacion
    {
        
        static void Main(string[] args)
        {
            //g = 13, k= 15
            //NumPseudoaleatorios oNumeros = new NumPseudoaleatorios(6, 8192, 15, 13);
            //oNumeros.PruebaUniformidad(8192, 113.145);
            //oNumeros.PruebaDeMedias();
            //oNumeros.PruebaVarianza(95, 7942.039569, 8443.748976);
            //oNumeros.PruebaDeIndependencia(8192, 1.96);
            //oNumeros.EscribirArchivo();

            //Dado nuevoDado = new Dado(6, 8192, 15, 13);
            TeoriaColas oCola = new TeoriaColas(3,8,25,37,100,500);
            oCola.Implementar(10);
            
        }
    }
}