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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adn"></param>
        /// <returns></returns>
        public static int VerticalSequenceSearch(this string[] adn)
        {
            int SequenceCounter = 0;
            var matrix = GenerateMatrix(adn);
            for (int i = 0; i < adn.Length; i++)
            {
                int auxCounter = 0;
                for (int j = 0; j < adn.Length - 1; j++)
                {
                    if (matrix[j,i] == matrix[j + 1, i])
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="adn"></param>
        /// <returns></returns>
        public static int ObliqueSequenceSearch(this string[] adn)
        {
            int SequenceCounter = 0;
            var matrix = GenerateMatrix(adn);
            for (int i = 0; i < adn.Length; i++)
            {
                int auxCounter = 0;
                for (int j = 0; j < adn.Length - 1; j++)
                {
                    if (ControlledException(() => matrix[j, (i+j)]) == ControlledExceptionPlus(() => matrix[j + 1, (i + j + 1)]))
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="adn"></param>
        /// <returns></returns>
        private static char[,] GenerateMatrix(string[] adn)
        {
            char[,] auxMarix = new char[adn.Length, adn.Length];

            for (int i = 0; i < adn[0].Length; i++)
            {
                var letters = adn[i].ToUpper().ToCharArray();

                for (int j = 0; j < letters.Length; j++)
                {
                    auxMarix[i, j] = letters[j];
                }
            }
            return auxMarix; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        private static char ControlledExceptionPlus(Func<char> fun)
        {
            try
            {
                return fun();
            }
            catch (Exception)
            {

                return 'x';
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        private static char ControlledException(Func<char> fun)
        {
            try
            {
                return fun();
            }
            catch (Exception)
            {

                return 'q';
            }
        }


    }
}
