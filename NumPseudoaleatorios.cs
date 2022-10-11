using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AlgoritmoLineal_Simulacion
{
    internal class NumPseudoaleatorios
    {
        List<int> x;
        List<float> r;
        List<string> intervalos;
        string sResultPruebas = "";

        public NumPseudoaleatorios(int x0, int c, int k, int g)
        {
            AlgoritmoLineal(x0, c, k, g);
            EscribirArchivo();
        }

        public List<float> getNumerosPseudoaleatrios()
        {
            return r;
        }
        public void AlgoritmoLineal(int x0, int c, int k, int g)
        {
            int a = 1 + (4 * k);
            c = c - 1;
            int o = 0;
            int contador = 1;
            int m = (int)Math.Pow(2, g);
            int operacion;
            bool duplicado;
            int i = 0;
            x = new List<int>();
            r = new List<float>();

            x.Add(x0);

            do
            {
                operacion = (a * x[i] + c) % m;
                x.Add(operacion);
                r.Add((float)x[i] / (m - 1));


                if (o == 0)
                {
                    x.RemoveAt(0);
                    i = -1;
                    o = 1;
                }

                contador++;
                i++;
                //Console.WriteLine(contador + "\t" + x[i] + "\t" + r[i]);
                Console.WriteLine(r[i]);
                duplicado = ValidarNumeros();

            } while (duplicado != true);
        }


        //Prueba de corridas arriba y abajo de la media 
        public void PruebaDeIndependencia(int cantidadDatos, double zTabla)
        {
            double media = 0.5;
            double mediaCalculada;
            double varianzaCalculada;
            double zCalculada;
            int C0 = 0;
            int C1 = 0;
            int n0 = 0;
            int n1 = 0;
            int x0 = 0; //hay ceros
            int x1 = 0;

            List<float> s = new List<float>();

            foreach (double x in r)
            {
                if (x >= media)
                {
                    s.Add(1);
                }
                else
                {
                    s.Add(0);
                }
            }

            for (int i = 0; i < s.Count() - 1; i++)
            {
                if (s[i] == 0 && s[i + 1] == 1) C0++;
                if (s[i] == 1 && s[i + 1] == 0) C0++;
            }

            foreach (int i in s)
            {
                if (i == 0) n0++;
                if (i == 1) n1++;
            }

            double n = r.Count();
            mediaCalculada = ((2 * n0 * n1) / cantidadDatos) + 0.5;
            double num = (2 * n0 * n1) * (2 * n0 * n1 - n);
            double denom = (Math.Pow(n, 2)) * (n - 1);

            varianzaCalculada = num / denom;


            //zCalculada = (C0 - mediaCalculada) / Math.Sqrt(varianzaCalculada);
            zCalculada = (C0 - mediaCalculada) / Math.Sqrt(varianzaCalculada);

            Console.WriteLine("\n**********************************************************************************");
            Console.WriteLine("\nPrueba de independecia");
            Console.WriteLine("Datos:");
            Console.WriteLine("Corridas observadas: " + C0);
            Console.WriteLine("Numero de ceros: " + n0);
            Console.WriteLine("Numero de unos: " + n1);
            Console.WriteLine("Valor de la media: " + media);
            Console.WriteLine("Valor de la varianza: " + varianzaCalculada);
            Console.WriteLine("Valor de la z: " + zCalculada);

            sResultPruebas = sResultPruebas + "\n**********************************************************************************";
            sResultPruebas = sResultPruebas + "\nPrueba de independecia";
            sResultPruebas = sResultPruebas + "\nDatos:";
            sResultPruebas = sResultPruebas + "\nCorridas observadas: " + C0;
            sResultPruebas = sResultPruebas + "\nNumero de ceros: " + n0;
            sResultPruebas = sResultPruebas + "\nNumero de unos: " + n1;
            sResultPruebas = sResultPruebas + "\nValor de la media: " + mediaCalculada;
            sResultPruebas = sResultPruebas + "\nValor de la varianza: " + varianzaCalculada;
            sResultPruebas = sResultPruebas + "\nEl valor de Z calculado es de: " + zCalculada;


            if ((-1 * zTabla) < zCalculada && zCalculada < zTabla)
            {
                sResultPruebas = sResultPruebas + "\nNo podemos rechazar que el conjunto ri es independientes";
                sResultPruebas = sResultPruebas + "\ncon un nivel de confianza del 95% ";
                Console.WriteLine("No podemos rechazar que el conjunto ri son independientes, con un nivel de confianza del 95% ");
            }
            else
            {
                sResultPruebas = sResultPruebas + "El conjunto no es independiente";
                Console.WriteLine("El conjunto no es independiente :(");
            }
            Console.WriteLine("\n**********************************************************************************");
            sResultPruebas = sResultPruebas + "\n**********************************************************************************";
        }



        public void PruebaUniformidad(int numDatos, double chiTabla)
        {
            intervalos = new List<string>();
            double m = Math.Ceiling(Math.Sqrt(numDatos)); ;
            double anchoClase = 1 / m;
            double a = 0, b = anchoClase;
            int cantNumIntervalo = 0;
            double E = Math.Ceiling(r.Count() / m);
            double chiCalculado = 0;

            for (int i = 0; i < m; i++)
            {

                foreach (double x in r)
                {
                    if (EntreDosIntervalos(a, b, x)) cantNumIntervalo++;

                }
                intervalos.Add("[" + Decimal.Round((decimal)a, 6) + ", " + Decimal.Round((decimal)b, 6) + ") = " + cantNumIntervalo);
                Console.WriteLine((i + 1) + " --> \t[" + Decimal.Round((decimal)a, 6) + "," + Decimal.Round((decimal)b, 6) + ") = " + cantNumIntervalo);

                chiCalculado = chiCalculado + Math.Pow((E - cantNumIntervalo), 2);
                cantNumIntervalo = 0;
                a = b;
                b = b + anchoClase;
            }

            chiCalculado = chiCalculado / E;
            sResultPruebas = sResultPruebas + "\n**********************************************************************************";
            sResultPruebas = sResultPruebas + "\nPrueba de uniformidad";
            sResultPruebas = sResultPruebas + "\nEl valor de Chi calculado es de: " + chiCalculado;

            Console.WriteLine("\n**********************************************************************************");
            Console.WriteLine("\nPrueba de uniformidad");
            Console.WriteLine("\nEl valor de Chi calculado es de: " + chiCalculado);
            if (chiCalculado < chiTabla)
            {
                sResultPruebas = sResultPruebas + "\nNo podemos rechazar que el conjunto sigue una distribucion uniforme \n El conjunto ha pasado la prueba de uniformidad c:";

                Console.WriteLine("No podemos rechazar que el conjunto sigue una distribucion uniforme");
                Console.WriteLine("El conjunto ha pasado la prueba de uniformidad c:");

            }
            else
            {
                sResultPruebas = sResultPruebas + "El conjunto no sigue una distribucion uniforme";
                Console.WriteLine("El conjunto no sigue una distribucion uniforme");
            }
            sResultPruebas = sResultPruebas + "\n**********************************************************************************";
            Console.WriteLine("\n**********************************************************************************");
        }
        public void PruebaVarianza(int nvlConfianza, double chiNormal, double chiComplemento)
        {
            double media = 0;
            sResultPruebas = sResultPruebas + "\n**********************************************************************************";
            sResultPruebas = sResultPruebas + "\nPrueba de varianza";
            Console.WriteLine("\n**********************************************************************************");
            Console.WriteLine("\nPrueba de varianza");
            foreach (double a in r)
            {
                media = media + a;
            }
            media = media / r.Count();
            double var = 0;

            foreach (double a in r)
            {
                var = var + (Math.Pow((a - media), 2));
            }
            var = var / (r.Count() - 1);
            sResultPruebas = sResultPruebas + "\nLa varianza es : " + var;
            Console.WriteLine("La varianza es : " + var);

            double limInf = 0, limSup = 0;

            limInf = chiNormal / (12 * (r.Count() - 1));
            limSup = chiComplemento / (12 * (r.Count() - 1));

            sResultPruebas = sResultPruebas + "\nEl limite inferior es: " + limInf + ", y el superior es: " + limSup;
            Console.WriteLine("El limite inferior es: " + limInf + ", y el superior es: " + limSup);
            if (var > limInf && var < limSup)
            {
                sResultPruebas = sResultPruebas + "\nEl conjunto ha pasado la prueba de varianza!";
                sResultPruebas = sResultPruebas + "\nLa varianza del conjunto se encuentra entre los limites de aceptacion,\npor lo que no podemos rechazar que el conjunto tiene una varianza de 1/12,\ncon un nivel de aceptacion del " + nvlConfianza + "%";
                Console.WriteLine("El conjunto ha pasado la prueba de varianza!!");
                Console.WriteLine("La varianza del conjunto se encuentra entre los limites de aceptacion,\npor lo que no podemos rechazar que el conjunto tiene una varianza de 1/12,\ncon un nivel de aceptacion del " + nvlConfianza + "%");
            }
            else
            {
                sResultPruebas = sResultPruebas + "\nEl conjunto no ha pasado la prueba de varianza!";
                Console.WriteLine("El conjunto no ha pasado la prueba de varianza!!");
            }
            sResultPruebas = sResultPruebas + "\n**********************************************************************************";
            Console.WriteLine("\n**********************************************************************************");
        }
        public void PruebaDeMedias()
        {
            double media = 0;
            double cotaInf = 0, cotaSup = 0;
            double zAlfa = 1.96;
            double mult = 1 / Math.Sqrt(r.Count() * 12);

            foreach (double a in r)
            {
                media = media + a;
            }
            media = media / r.Count();
            sResultPruebas = sResultPruebas + "\n**********************************************************************************";
            Console.WriteLine("\n**********************************************************************************");
            sResultPruebas = sResultPruebas + "\nPrueba de medias";
            sResultPruebas = sResultPruebas + "\nLa media del conjunto es: " + media;
            Console.WriteLine("\nPrueba de medias");
            Console.WriteLine("La media del conjunto es: " + media);



            cotaInf = (0.5) - (zAlfa * mult);
            cotaSup = (0.5) + (zAlfa * mult);

            Console.WriteLine("La cota inferior es: " + cotaInf + ", y la cota superior es: " + cotaSup);
            sResultPruebas = sResultPruebas + "\nLa cota inferior es: " + cotaInf + ", y la cota superior es: " + cotaSup;
            sResultPruebas = sResultPruebas + "\n**********************************************************************************";
            Console.WriteLine("\n**********************************************************************************");
        }
        public bool EntreDosIntervalos(double a, double b, double valor)
        {
            if (valor >= a && valor <= b)
                return true;

            return false;
        }
        public bool ValidarNumeros()
        {

            var duplicados = r.GroupBy(x => x).Where(g => g.Count() > 1).Select(x => new { Numero = x.Key, Veces = x.Count() }).ToList();


            if (duplicados.Count() > 0)
            {
                Console.WriteLine("El patron se repite");
                Console.WriteLine(String.Join(", ", duplicados));
                r.RemoveAt(r.Count() - 1);
                return true;
            }
            return false;
        }
        public void EscribirArchivo()
        {
            TextWriter Escribir = new StreamWriter("C:\\Mis archivos\\Quinto semestre\\Simulación\\Numeros pseudoaleatorios.txt");

            Escribir.WriteLine("Números pseudoaleatorios\n");

            foreach (double num in r)
            {
                Escribir.WriteLine(num + "\n");
            }
            Escribir.WriteLine("Intervalos\n");
            foreach (string inter in intervalos)
            {
                Escribir.WriteLine(inter + "\n");
            }
            sResultPruebas = sResultPruebas + "\n=DISTR.NORM.N(3.32,3.05,9.43,VERDADERO)";
            Escribir.WriteLine(sResultPruebas);
            Escribir.Close();
        }
    }
}
