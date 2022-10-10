using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;



namespace AlgoritmoLineal_Simulacion
{
    public class AlgoritmoLineal_Simulacion
    {
        static List<int> x;
        static List<float> r;
        static List<string> intervalos;
        static string sResultPruebas = "";
        static void Main(string[] args)
        {
            //g = 13, k= 15
            
            AlgoritmoLineal(6, 8192, 15, 13);
            PruebaUniformidad(8192, 113.145);
            PruebaDeMedias();
            PruebaVarianza(95, 7942.039569, 8443.748976);
            PruebaDeIndependencia(8192, 1.96);
            Dado();
            EscribirArchivo();
            //OpenMicrosoftExcel(@"C:\Mis archivos\Quinto semestre\Simulación\Simulacion.txt");


        }

        public static void Dado()
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
                sResultPruebas = sResultPruebas + "\nLa varianza es: " + Math.Pow(desv,2);
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

        //Prueba de corridas arriba y abajo de la media 
        static public void PruebaDeIndependencia(int cantidadDatos, double zTabla)
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
                if(x >= media)
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
            double num = (2 * n0 * n1) * (2 * n0* n1 - n);
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


            if ((-1*zTabla)<zCalculada && zCalculada < zTabla)
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


        
        static public void PruebaUniformidad(int numDatos,double chiTabla) 
        {
            intervalos = new List<string>();
            double m = Math.Ceiling(Math.Sqrt(numDatos)); ;
            double anchoClase = 1 /m;
            double a=0, b= anchoClase;
            int cantNumIntervalo = 0;
            double E = Math.Ceiling(r.Count() / m);
            double chiCalculado = 0;

            for (int i = 0; i < m; i++) 
            {
                
                foreach (double x in r)
                {
                    if (EntreDosIntervalos(a, b, x)) cantNumIntervalo++;
                    
                }
                intervalos.Add("["+Decimal.Round((decimal)a, 6) + ", " + Decimal.Round((decimal)b, 6) + ") = " + cantNumIntervalo);
                Console.WriteLine((i + 1) + " --> \t[" + Decimal.Round((decimal)a, 6) + "," + Decimal.Round((decimal)b, 6) + ") = "+ cantNumIntervalo);
                
                chiCalculado = chiCalculado + Math.Pow((E - cantNumIntervalo), 2);
                cantNumIntervalo = 0;
                a = b;
                b =b + anchoClase;
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
        public static void PruebaVarianza(int nvlConfianza, double chiNormal, double chiComplemento)
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
        static public void PruebaDeMedias()
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
        static public bool EntreDosIntervalos(double a, double b, double valor)
        {
            if (valor >= a && valor <= b)
                return true;

            return false;
        }
        static public bool ValidarNumeros()
        {
            
            var duplicados = r.GroupBy(x => x).Where(g => g.Count()> 1).Select(x => new { Numero = x.Key, Veces = x.Count() }).ToList();


            if (duplicados.Count() > 0)
            {
                Console.WriteLine("El patron se repite");
                Console.WriteLine(String.Join(", ", duplicados));
                r.RemoveAt(r.Count() - 1);
                return true;
            }
            return false;
        }
        public static void AlgoritmoLineal(int x0, int c, int k, int g)
        {
            int a = 1+(4*k);
            c = c - 1;
            int o =0;
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

                
                if (o==0)
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

        static public void EscribirArchivo()
        {
            TextWriter Escribir = new StreamWriter("C:\\Mis archivos\\Quinto semestre\\Simulación\\Simulacion.txt");
            
            Escribir.WriteLine("Números pseudoaleatorios\n");
            
            foreach (double num in r)
            {
                Escribir.WriteLine(num+"\n");
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
        static void OpenMicrosoftExcel(string f)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "Simulacion.txt";
            startInfo.Arguments = f;
            Process.Start(startInfo);
        }
    }
}