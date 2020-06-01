using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContadorPalabras
{
    class Program
    {
        
        static void Main(string[] args)
        {
          
            Console.WriteLine("ingrese parrafo: ");
            string parrafo = Console.ReadLine();


            Dictionary<string, int> lista = new Dictionary<string, int>();
            string[] solopalabras = parrafo.Split();

            Parallel.ForEach(solopalabras, //objeto iterable almacen de datos
                () => { return new Dictionary<String, int>(); }, //inicializamos el almacen local TLS
                (palabra, loopControl, localD) =>//cueprpo del algoritmo concurrente.
                {
                    string pal = Convert.ToString(palabra);
                    if (!localD.ContainsKey(palabra))
                    {
                        localD.Add(palabra, 1);
                    }
                    else
                    {
                        localD[palabra]++;
                    }
                    return localD;
                },
               (localReduce) =>// finalizador (Reduce)
               {
                   lock(lista)
                   {
                       foreach (String palabra in localReduce.Keys)
                       {


                           if (!lista.ContainsKey(palabra))
                           {
                               lista.Add(palabra, 1);
                           }
                           else
                           {
                               lista[palabra]++;
                           }
                       
                       }


                   }
               }
            );
            foreach(KeyValuePair<string, int> par in lista)
            {
                Console.WriteLine("Letra: {0} valor: {1}", par.Key, par.Value);
            }
                
           
            
            Console.ReadKey();
        }
    }
}

