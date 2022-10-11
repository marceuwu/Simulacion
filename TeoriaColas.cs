﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoLineal_Simulacion
{
    internal class TeoriaColas
    {
        int numPersonas;
        float turno;

        float salario;
        float tiempoExtra;
        float costoEsperaCamion;

        DateTime horaLaboralInicio = new DateTime(2022,10,07,23,0,0);
        DateTime horaAlmacen = new DateTime(2022, 10, 08, 07, 30, 0);
        DateTime horaComida = new DateTime(2022, 10, 08, 03, 00, 0);

        List<float> PSE;
        public TeoriaColas(int numPersonas, float turno, float salario, float tiempoExtra, float costoEsperaCamion)
        {
            this.numPersonas = numPersonas;
            this.turno = turno;
            this.salario = salario;
            this.tiempoExtra = tiempoExtra;
            this.costoEsperaCamion = costoEsperaCamion;

            NumPseudoaleatorios oNumeros = new NumPseudoaleatorios(6, 8192, 15, 13);
            PSE = oNumeros.getNumerosPseudoaleatrios();
        }
        public void Implementar(int numCorridas)
        {
            int contCorridas=0;
            int i = 0;
            int numCamiones;
            int tiempoEntreLlegada;
            DateTime horaLlegada;
            DateTime horaDescarga;
            DateTime horaSalidaCamion;
            int tiempoDescarga;
            int tiempoEspera;

            while (contCorridas < numCorridas)
            {
                Console.WriteLine("--------------------------------- CORRIDA <" + contCorridas + ">------------------------------------------------------");
                horaLlegada = horaLaboralInicio;
                horaSalidaCamion = new DateTime(2022, 10, 08, 0, 0, 0); ;
                tiempoDescarga = 0;
                
                numCamiones = TransfInversaCamionesEspera(PSE[i]);
                Console.Write("\n" + PSE[i]);
                Console.Write("\t" + numCamiones);
                for (int j=0; ; j++)
                {
                    
                    i++;
                    tiempoEntreLlegada = TransfInversaTiempoEntreLlegadas(PSE[i]);
                    horaLlegada = horaLlegada.AddMinutes(tiempoEntreLlegada);
                    if (horaLlegada > horaAlmacen)
                    {
                        break;
                    }
                    if (j == 0 || horaLlegada > horaSalidaCamion)
                    {
                        horaDescarga = horaLlegada;
                    }
                    else
                    {

                        horaDescarga = horaSalidaCamion;
                    }
                
                    Console.Write("\n" + PSE[i]);
                    Console.Write("\t" + tiempoEntreLlegada);
                    Console.Write("\t" + horaLlegada.TimeOfDay);
                    Console.Write("\t" + horaDescarga.TimeOfDay);
                    i++;
                    switch (numPersonas)
                    {
                        case 3:
                            tiempoDescarga = TransfInversaTiempoServico3Personas(PSE[i]);
                            break;
                        case 4:
                            tiempoDescarga = TransfInversaTiempoServico4Personas(PSE[i]);
                            break;
                        case 5:
                            break;
                        case 6:
                            break;

                    }

                    Console.Write("\t" + PSE[i]);
                    Console.Write("\t" + tiempoDescarga);


                    horaSalidaCamion = horaDescarga.AddMinutes(tiempoDescarga);
                    if(horaSalidaCamion == horaComida)
                    {
                        horaSalidaCamion = horaSalidaCamion.AddMinutes(30);
                    }
                    tiempoEspera = (int)(horaDescarga- horaLlegada).TotalMinutes;
                
                    Console.Write("\t" + horaSalidaCamion.TimeOfDay);
                    Console.Write("\t" + tiempoEspera);
                    
                }
                Console.WriteLine("\n----------------------------------------------------------------------------------------------------");
                contCorridas++;
            }
            Console.ReadLine();
        }
        public int TransfInversaCamionesEspera(float pse)
        {
            if (pse >= 0 && pse < 0.5)
            {
                return 0;
            }
            else if (pse >= 0.5 && pse < 0.75)
            {
                return 1;
            }
            else if (pse >= 0.75 && pse < 0.9)
            {
                return 2;
            }
            else if (pse >= 0.9 && pse < 1)
            {
                return 3;
            }
           
            return 0;
        }

        public int TransfInversaTiempoEntreLlegadas(float pse)
        {
            if (pse >= 0 && pse < 0.02)
            {
                return 20;
            }
            else if (pse >= 0.02 && pse < 0.1)
            {
                return 25;
            }
            else if (pse >= 0.1 && pse < 0.22)
            {
                return 30;
            }
            else if (pse >= 0.22 && pse < 0.47)
            {
                return 35;
            }
            else if (pse >= 0.47 && pse < 0.67)
            {
                return 40;

            }
            else if (pse >= 0.67 && pse < 0.82)
            {
                return 45;
            }
            else if (pse >= 0.82 && pse < 0.92)
            {
                return 50;
            }
            else if (pse >= 0.92 && pse < 0.97)
            {
                return 55;
            }
            else if (pse >= 0.97 && pse < 1)
            {
                return 60;
            }
            return 0;
        }

        public int TransfInversaTiempoServico3Personas(float pse)
        {
            if (pse >= 0 && pse < 0.05)
            {
                return 20;
            }
            else if (pse >= 0.05 && pse < 0.15)
            {
                return 25;
            }
            else if (pse >= 0.15 && pse < 0.35)
            {
                return 30;
            }
            else if (pse >= 0.35 && pse < 0.60)
            {
                return 35;
            }
            else if (pse >= 0.60 && pse < 0.72)
            {
                return 40;

            }
            else if (pse >= 0.72 && pse < 0.82)
            {
                return 45;
            }
            else if (pse >= 0.82 && pse < 0.9)
            {
                return 50;
            }
            else if (pse >= 0.9 && pse < 0.96)
            {
                return 55;
            }
            else if (pse >= 0.96 && pse < 1)
            {
                return 60;
            }
            return 0;
        }

        public int TransfInversaTiempoServico4Personas(float pse)
        {
            if (pse >= 0 && pse < 0.05)
            {
                return 15;
            }
            else if (pse >= 0.05 && pse < 0.15)
            {
                return 20;
            }
            else if (pse >= 0.15 && pse < 0.20)
            {
                return 25;
            }
            else if (pse >= 0.20 && pse < 0.60)
            {
                return 35;
            }
            else if (pse >= 0.60 && pse < 0.72)
            {
                return 40;

            }
            else if (pse >= 0.72 && pse < 0.82)
            {
                return 45;
            }
            else if (pse >= 0.82 && pse < 0.9)
            {
                return 50;
            }
            else if (pse >= 0.9 && pse < 0.96)
            {
                return 55;
            }
            else if (pse >= 0.96 && pse < 1)
            {
                return 60;
            }
            return 0;
        }
    }
}
