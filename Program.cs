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
            //int x0, int c, int k, int g


            //NumPseudoaleatorios oNumeros = new NumPseudoaleatorios(7, 239, 8, 11);
            //oNumeros.PruebaUniformidad(252, 113.145);
            //oNumeros.PruebaDeMedias();
            ////public void PruebaVarianza(int nvlConfianza, double chiNormal, double chiComplemento)
            ////95, 7942.039569, 8443.748976
            //oNumeros.PruebaVarianza(95);
            //oNumeros.PruebaDeIndependencia(252, 1.96);
            //oNumeros.EscribirArchivo();

            //Dado nuevoDado = new Dado(7, 252, 3, 8);
            //nuevoDado.Generar();
            //nuevoDado.EscribirArchivo();


            //NumPseudoaleatorios oNumeros = new NumPseudoaleatorios(6, 8192, 15, 13);
            //oNumeros.PruebaUniformidad(8192);
            //oNumeros.PruebaDeMedias();
            //oNumeros.PruebaVarianza(95);
            //oNumeros.PruebaDeIndependencia(8192, 1.96);
            //oNumeros.EscribirArchivo();

            //TeoriaColas oCola = new TeoriaColas(3, 8, 25, 37, 100, 500);
            //oCola.Implementar(10);
            //oCola.EscribirArchivo();
            GeneradorVariablesAleatorias generador = new GeneradorVariablesAleatorias();
            generador.GeneradorVariablesNormal(100);
            generador.TablaFrecuencia();




        }
    }
}