using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;

namespace AlgoritmoLineal_Simulacion
{
    internal class GeneradorVariablesAleatorias
    {
        int n;
        int cont;
        float t;
        float tPrima;
        List<float> PSE;
        List<float> varGenerados;
        float media;
        float desv;
        float varianza;
        public GeneradorVariablesAleatorias()
        {
            cont = 0;
            n = 0;
            t = 1;
            media = 10;
            varianza =(float) 42.35;
            desv = (float)Math.Sqrt(varianza);
            NumPseudoaleatorios oNumeros = new NumPseudoaleatorios(6, 8192, 15, 13);
            PSE = oNumeros.getNumerosPseudoaleatrios();
            varGenerados = new List<float>();
        }

        public float EcuacionRecursivaPoisson(float landa)
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

        public float EcuacionRecursivaNormal()
        {
            float N = 0;
            for (int j=0; j<12; j++)
            {
                N = N + PSE[cont];
                cont++;     
            }
            N = N - 6;
            N = N * desv + media;
            if (N <= 0)
            {
                return EcuacionRecursivaNormal();
            }
            return N;
        }
        public float ObtenerDatoMenor()
        {
            float menor = varGenerados[0];
            foreach (var num in varGenerados)
            {
                if(num < menor)
                {
                    menor = num;
                }
            }
            return menor;
        }
        public float ObtenerDatoMayor()
        {
            float mayor = varGenerados[0];
            foreach (var num in varGenerados)
            {
                if (num > mayor)
                {
                    mayor = num;
                }
            }
            return mayor;
        }
        public double F(double x)
        {
            MathNet.Numerics.Distributions.Normal result = new MathNet.Numerics.Distributions.Normal();
            return result.CumulativeDistribution(x);
        }
        public double ChiCuadrada(int parametros, int gradosLib)
        {
            var chi = new ChiSquared(gradosLib-parametros-1);
            return chi.InverseCumulativeDistribution(0.95);
        }
        public void TablaFrecuencia()
        {
            float menor = this.ObtenerDatoMenor();
            float mayor = this.ObtenerDatoMayor();
            float rango = mayor - menor;
            int frecuencia;
            int cantDatos = varGenerados.Count;
            int contInt = 1;
            float z;
            float x;
            float pZactual = 0;
            float pZanterior = 0;
            float pX = 0;
            float Ei;
            float error;
            float suma = 0;
            float sumaError = 0;
            //int intervalos = (int)Math.Round(1 + 3.322 * Math.Log(varGenerados.Count));
            int intervalos = (int)Math.Sqrt(cantDatos);
            float amplitud = (float)Math.Round(rango / intervalos, 2);

            float limInf = 0;
            float limSup = 0;

            media = (float)Statistics.Mean(varGenerados);
            desv = (float)Statistics.StandardDeviation(varGenerados);
            varianza = (float)Statistics.Variance(varGenerados);
            Console.WriteLine("---------------- DATOS ----------------");
            Console.WriteLine("Media: " + media);
            Console.WriteLine("Desviacion estandar: " + desv);
            Console.WriteLine("Min: " + menor);
            Console.WriteLine("Max: " + mayor);
            Console.WriteLine("Rango: " + rango);
            Console.WriteLine("Amplitud: " + amplitud);
            Console.WriteLine("Inervalos: " + intervalos);
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("\n\nnum\tIntervalo\t Oi\t x\t z\t p(z)\t p(x)\t Ei\tError");
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            do
            {
                
                limSup = (float)Math.Round(limInf + amplitud, 2);
                frecuencia = isBetween(limInf, limSup);
                x = limSup;
                z = (float)Math.Round((x - media) / desv, 2);
                pZactual = (float)Math.Round(this.F(z),2); 
                pX = (float)Math.Round(pZactual-pZanterior,2);
                pZanterior = pZactual;
                Ei = cantDatos * pX;
                error = (float)Math.Pow((Ei - frecuencia), 2) / Ei;
                Console.WriteLine(contInt + "\t" + limInf + " - " + limSup + "\t" + frecuencia + "\t" + x + "\t" + z + "\t" + pZactual + "\t" + pX + "\t" + Ei + "\t" + error);
                limInf = limSup;
                suma += pX;
                contInt++;
                sumaError += error;
                
            } while (intervalos>contInt);
            pX = (float)Math.Round(1 - suma,2);
            Ei = cantDatos * pX;
            error = (float)Math.Pow((Ei - frecuencia), 2) / Ei;
            sumaError += error;
            Console.WriteLine(contInt + "\t" + limInf + " - " + limSup + "\t" + frecuencia + "\t" + x + "\t" + z + "\t" + pZactual + "\t" + pX + "\t" + Ei + "\t" + error);
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            double chiCalculada = sumaError;
            double chiTabla = ChiCuadrada(2, intervalos);
            Console.WriteLine("\nCHI CALCULADO: "+chiCalculada);
            Console.WriteLine("CHI TABLA: " + chiTabla+"\n");

            if (chiCalculada < chiTabla)
            {
                Console.WriteLine("no podemos rechazar la hipótesis de que la variable \r\naleatoria se comporta de acuerdo con una distribución de Normal :)");
            }
            else
            {
                Console.WriteLine("rechazamos la hipótesis de que la variable \r\naleatoria se comporta de acuerdo con una distribución de Normal :(");
            }


        }
        public void GeneradorVariablesNormal(int Corridas)
        {
            float num=0;
            for(int i = 0; i < Corridas; i++)
            {
                num = this.EcuacionRecursivaNormal();
                varGenerados.Add(num);
                Console.WriteLine(num);
            }
        }

        public int isBetween(float inf, float sup)
        {
            int i = 0;
            foreach(var num in varGenerados)
            {
                if (num >= inf && num < sup)
                {
                    i++;
                }
            }
            
            return i++;
        }
        public void MetodoChiCuadrada()
        {
            foreach(var num in varGenerados)
            {
                
            }
        }
    }
}
