using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public static class Constants
    {
        public static readonly int MinimalMutantSequence = 1; 
        public static readonly int MinimumSequenceRequirement = 3;


        /// <summary>
        /// Nombre de la cache de "Connection" de la MemoryCache
        /// </summary>
        public static readonly string CacheConnectionMemoryCache = "DefaultAppConnection";

        /// <summary>
        /// Tag De Configuración del Swagger en el AppSettings
        /// </summary>
        public static readonly string SwaggerConfiguration = "SwaggerConfiguration:";

    }
}
