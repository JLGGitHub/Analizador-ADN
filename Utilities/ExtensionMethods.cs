using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Utilities.Enumerations;

namespace Utilities
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Metodo de extension que determina si un ADN cumple con el criterio de base nigrogenada
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
        /// Metodo de extension que valida dimensiones de una matriz
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
        /// Metodo de extension que valida secuencia del adn horizontalmente
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
        /// Metodo de extension que valida secuencia del adn verticalmente
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
        /// Metodo de extension que valida secuencia del adn oblicuamente
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
        /// Metodo de extension que genera una matriz en base a una estructura de adn de entrada
        /// </summary>
        /// <param name="adn"></param>
        /// <returns></returns>
        public static char[,] GenerateMatrix(string[] adn)
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
        /// Metodo de extension para controlar exepciones
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
        ///  Metodo de extension para controlar exepciones
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this EnumMessages value)
        {
            if (value != null)
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());
                DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

                if (attributes != null && attributes.Any())
                {
                    return attributes.First().Description;
                }

                return value.ToString();
            }
            return null;
        }
    }
}
