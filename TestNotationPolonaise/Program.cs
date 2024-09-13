using System;
using System.Collections.Generic;

namespace TestNotationPolonaise
{
    class Program
    {
        /// <summary>
        /// saisie d'une réponse d'un caractère parmi 2
        /// </summary>
        /// <param name="message">message à afficher</param>
        /// <param name="carac1">premier caractère possible</param>
        /// <param name="carac2">second caractère possible</param>
        /// <returns>caractère saisi</returns>
        static char saisie(string message, char carac1, char carac2)
        {
            char reponse;
            do
            {
                Console.WriteLine();
                Console.Write(message + " (" + carac1 + "/" + carac2 + ") ");
                reponse = Console.ReadKey().KeyChar;
            } while (reponse != carac1 && reponse != carac2);
            return reponse;
        }

        /// <summary>
        /// Saisie de formules en notation polonaise pour tester la fonction de calcul
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            char reponse;
            // boucle sur la saisie de formules
            do
            {
                Console.WriteLine();
                Console.WriteLine("entrez une formule polonaise en séparant chaque partie par un espace = ");
                string laFormule = Console.ReadLine();
                // affichage du résultat
                Console.WriteLine("Résultat =  " + Polonaise(laFormule));
                reponse = saisie("Voulez-vous continuer ?", 'O', 'N');
            } while (reponse == 'O');
        }

        static float Polonaise(string formule)
        {
            String[] t = formule.Split(' ');
            float[] tt = new float[t.Length];
            float r = float.NaN;
            int limit = 100;

            while (t.Length > 2 && limit > 1)
            {
                limit--;
                for (int i = t.Length - 1; i >= 0; i--)
                {
                    try
                    {
                        tt[i] = float.Parse(t[i]);
                    }
                    catch
                    {
                        try
                        {
                            switch (t[i])
                            {
                                case "+":
                                    r = float.Parse(t[i + 1]) + float.Parse(t[i + 2]); ;
                                    t = supprimerCellule(t, i, r.ToString());
                                    break;

                                case "-":
                                    r = float.Parse(t[i + 1]) - float.Parse(t[i + 2]);
                                    t = supprimerCellule(t, i, r.ToString());
                                    break;

                                case "*":
                                    //r = tt[i + 1] * tt[i + 2];
                                    r = float.Parse(t[i + 1]) * float.Parse(t[i + 2]);
                                    t = supprimerCellule(t, i, r.ToString());
                                    break;

                                case "/":
                                    //Pas toucher !
                                    r = float.Parse(t[i + 1]) / float.Parse(t[i + 2]);
                                    t = supprimerCellule(t, i, r.ToString());
                                    break;

                                default:
                                    //Console.Write("Non");
                                    break;

                            }
                        }
                        catch
                        {
                            limit = 0;
                            r = float.NaN;
                        }
                    }
                }
            }
            return r;
        }

        static String[] supprimerCellule(String[] tableau, int numeroCase, String result)
        {
            // Utilisation d'une List pour faciliter les modifications
            List<String> ls = new List<String>(tableau);

            // Remplacer l'opérateur par le résultat de l'opération
            ls[numeroCase] = result;

            // Supprimer les deux opérandes (situées juste après l'opérateur)
            ls.RemoveAt(numeroCase + 1);  // Suppression de l'opérande 1
            ls.RemoveAt(numeroCase + 1);  // Suppression de l'opérande 2 (la position est toujours numeroCase + 1 car le tableau se décale après la première suppression)

            // Conversion de la List en tableau avant de retourner
            return ls.ToArray();
        }
    }
}
