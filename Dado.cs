using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoLineal_Simulacion
{
    internal class Dado
    {
        string sResultPruebas = "";
        List<float> r;
        public Dado(int x0, int c, int k, int g)
        {
            NumPseudoaleatorios oNumeros = new NumPseudoaleatorios(x0, c, k, g);
            r = oNumeros.getNumerosPseudoaleatrios();
        }
        public void Generar()
        {
            sResultPruebas = sResultPruebas + "\n**********************************************************************************";
            sResultPruebas = sResultPruebas + "\nSimulacion de la tirada de un dado";
            Console.WriteLine("\n**********************************************************************************");
            Console.WriteLine("\nSimulacion de la tirada de un dado");

            List<double> corrida = new List<double>();
            List<int> numeros = new List<int>();
            int n = 50, i = 0, corridas = 1;

            while (corridas <= 50)
            {

                sResultPruebas = sResultPruebas + "\nCorrida <" + corridas + ">:\n";
                Console.WriteLine("Corrida <" + corridas + ">:\n");

                corrida.RemoveRange(0, corrida.Count());
                numeros.RemoveRange(0, corrida.Count());

                while (corrida.Count() != 50)
                {
                    corrida.Add(r[i]);
                    i++;
                }

                foreach (double d in corrida)
                {
                    if (d >= 0 && d < 0.166666666)
                    {
                        numeros.Add(1);
                    }
                    else if (d >= 0.16666666 && d < 0.33333333)
                    {
                        numeros.Add(2);
                    }
                    else if (d >= 0.33333333 && d < 0.5)
                    {
                        numeros.Add(3);
                    }
                    else if (d >= 0.5 && d < 0.66666666)
                    {
                        numeros.Add(4);
                    }
                    else if (d >= 0.66666666 && d < 0.83333333)
                    {
                        numeros.Add(5);
                    }
                    else if (d >= 0.833333333 && d <= 1)
                    {
                        numeros.Add(6);
                    }
                }

                //Media
                float media = 0;

                foreach (int d in numeros)
                {
                    media = media + d;
                }
                media = media / n;

                numeros.Sort();

                //Imprimir las listas
                sResultPruebas = sResultPruebas + "\nLista de numeros: ";
                sResultPruebas = sResultPruebas + "\n[";
                Console.WriteLine("Lista de numeros: ");
                Console.Write("[");
                foreach (int x in numeros)
                {
                    Console.Write(x + ",");
                    sResultPruebas = sResultPruebas + x + ",";
                }
                sResultPruebas = sResultPruebas + "]\n";
                sResultPruebas = sResultPruebas + "\nLista de numeros ri:";
                sResultPruebas = sResultPruebas + "\n[";

                Console.Write("]\n");
                Console.WriteLine("\nLista de numeros ri:");
                Console.Write("[");
                foreach (double x in corrida)
                {
                    sResultPruebas = sResultPruebas + x + ",";

                }
                sResultPruebas = sResultPruebas + "]\n";
                sResultPruebas = sResultPruebas + "\nLa media de la corrida es: " + media;
                Console.Write("]\n");
                Console.WriteLine("\nLa media de la corrida es: " + media);

                //Moda
                int unos = 0, dos = 0, tres = 0, cuatro = 0, cinco = 0, seis = 0;
                int moda = 0;

                foreach (int x in numeros)
                {
                    if (x == 1) unos++;
                    if (x == 2) dos++;
                    if (x == 3) tres++;
                    if (x == 4) cuatro++;
                    if (x == 5) cinco++;
                    if (x == 6) seis++;
                }
                int max = 0, aux = 0;
                while (aux < 6)
                {
                    if (unos > max)
                    {
                        max = unos;
                        moda = 1;
                    }
                    else if (dos > max)
                    {
                        max = dos;
                        moda = 2;
                    }
                    else if (tres > max)
                    {
                        max = tres;
                        moda = 3;
                    }
                    else if (cuatro > max)
                    {
                        max = cuatro;
                        moda = 4;
                    }
                    else if (cinco > max)
                    {
                        max = cinco;
                        moda = 5;
                    }
                    else if (seis > max)
                    {
                        max = seis;
                        moda = 6;
                    }
                    aux++;
                }
                sResultPruebas = sResultPruebas + "\nLa moda de la corrida es: " + moda;
                Console.WriteLine("La moda de la corrida es: " + moda);

                //Desviacion estandar
                double desv = 0;
                foreach (int x in numeros)
                {
                    desv = desv + Math.Pow((x - media), 2);
                }
                desv = desv / n;
                sResultPruebas = sResultPruebas + "\nLa desviacion estandar de la corrida es: " + desv;
                Console.WriteLine("La desviacion estandar de la corrida es: " + desv);
                sResultPruebas = sResultPruebas + "\nLa varianza es: " + Math.Pow(desv, 2);
                Console.WriteLine("La varianza es: " + Math.Pow(desv, 2));

                //Mediana
                float mediana = 0;
                while (numeros.Count() != 2)
                {
                    numeros.RemoveAt(0);
                    numeros.RemoveAt(numeros.Count() - 1);
                }
                mediana = (numeros[0] + numeros[1]) / 2;
                Console.WriteLine("\nLa mediana es: " + mediana);
                sResultPruebas = sResultPruebas + "\nLa mediana es: " + mediana;
                corridas++;
            }
            Console.WriteLine("\n**********************************************************************************");
        }

        public void EscribirArchivo()
        {
            TextWriter Escribir = new StreamWriter("C:\\Mis archivos\\Quinto semestre\\Simulación\\Dado.txt");

            Escribir.WriteLine("Programa de DADO\n");
            Escribir.WriteLine(sResultPruebas);
            Escribir.Close();
        }

    }
}
