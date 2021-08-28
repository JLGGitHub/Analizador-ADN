using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Utilities
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adn"></param>
        /// <returns></returns>
        public static bool NitrogenBase(this string[] adn)
        {
            List<bool> nitrogenBaseMutant = new List<bool>(); 
            foreach (var item in adn)
            {
                int countLetters = item.Length;
                var aux = Regex.Matches(item, "[ATCGatcg]", RegexOptions.IgnoreCase);
                nitrogenBaseMutant.Add((countLetters == aux.Count) ? true : false);
            }
            return nitrogenBaseMutant.All(x => x == true); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adn"></param>
        /// <returns></returns>
        public static bool MatrixNxN(this string[] adn)
        {
            int tam1 = adn[0].Length;
            int tam2 = adn.Length;
            return (tam1 == tam2) ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adn"></param>
        /// <returns></returns>
        public static int HorizontalSequenceSearch(this string[] adn)
        {
            int SequenceCounter = 0;
            foreach (var item in adn)
            {
                int auxCounter = 0;
                var letters = item.ToUpper().ToCharArray();
                for (int i = 0; i < item.Length -1; i++)
                {
                    if (letters[i] == letters[i + 1])
                    {
                        auxCounter++;
                        if (auxCounter >= Constants.MinimumSequenceRequirement)
                        {
                            SequenceCounter++;
                            auxCounter = 0;
                        }
                    }
                    else 
                    {
                        auxCounter = 0; 
                    }
                }
            }
            return SequenceCounter;
        }
    }
}
