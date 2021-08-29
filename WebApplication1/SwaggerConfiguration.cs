using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutantApi
{
    public class SwaggerConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public SwaggerConfiguration(IConfiguration configuration)
        {
            if (configuration != null)
            {
                EndpointDescription = configuration.GetSection(Utilities.Constants.SwaggerConfiguration + nameof(EndpointDescription)).Value;
                EndpointSwaggerJson = configuration.GetSection(Utilities.Constants.SwaggerConfiguration + nameof(EndpointSwaggerJson)).Value;
                ContactName = configuration.GetSection(Utilities.Constants.SwaggerConfiguration + nameof(ContactName)).Value;
                ContactUrl = new System.Uri(configuration.GetSection(Utilities.Constants.SwaggerConfiguration + nameof(ContactUrl)).Value);
                ContactEmail = configuration.GetSection(Utilities.Constants.SwaggerConfiguration + nameof(ContactEmail)).Value;
                DocNameV1 = configuration.GetSection(Utilities.Constants.SwaggerConfiguration + nameof(DocNameV1)).Value;
                DocInfoTitle = configuration.GetSection(Utilities.Constants.SwaggerConfiguration + nameof(DocInfoTitle)).Value;
                DocInfoVersion = configuration.GetSection(Utilities.Constants.SwaggerConfiguration + nameof(DocInfoVersion)).Value;
                DocInfoDescription = configuration.GetSection(Utilities.Constants.SwaggerConfiguration + nameof(DocInfoDescription)).Value;
            }
        }

        /// <summary>
        /// <para>Foo API v1</para>
        /// </summary>
        public string EndpointDescription { get; }

        /// <summary>
        /// <para>/swagger/v1/swagger.json</para>
        /// </summary>
        public string EndpointSwaggerJson { get; }

        /// <summary>
        /// <para>[Nombre]</para>
        /// </summary>
        public string ContactName { get; }

        /// <summary>
        /// <para>[Contacto]</para>
        /// </summary>
        public System.Uri ContactUrl { get; }

        /// <summary>
        /// <para>[Contacto]</para>
        /// </summary>
        public string ContactEmail { get; }

        /// <summary>
        /// <para>v1</para>
        /// </summary>
        public string DocNameV1 { get; }

        /// <summary>
        /// <para>Foo API</para>
        /// </summary>
        public string DocInfoTitle { get; }

        /// <summary>
        /// <para>v1</para>
        /// </summary>
        public string DocInfoVersion { get; }

        /// <summary>
        /// <para>Prueba Mercado Libre.  </para>
        /// </summary>
        public string DocInfoDescription { get; }
    }
}
