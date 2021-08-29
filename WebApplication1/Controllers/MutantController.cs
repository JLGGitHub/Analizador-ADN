using BusinessRules.Interfaces;
using Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MutantApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mutant.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MutantController :  ApiBase<Entities.Adn, IAdn>
    {
        public MutantController(IAdn business): base(business)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Servico OK");
        }

        /// <summary>
        /// Servicio que identifica si una secuencia de ADN corresponde a un mutante o humano
        /// Cada verificacion es almacenada en base de datos
        /// </summary>
        /// <param name="adn"></param>
        /// <returns></returns>
        [HttpPost("Mutant")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MutantRequest))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(MutantRequest))]
        public Task<IActionResult> Mutant([FromBody] Entities.DTO.MutantRequest adn)
        {
            return ControllerExceptions(async () =>
            {
                return ApiResponse(await RepoBusinessRules.IsMutant(adn.Dna) ? 200 : 403);
            });
        }

        /// <summary>
        /// Servicio que retorna las estadisticas de mutantes versus humanos
        /// </summary>
        /// <param name="adn"></param>
        /// <returns></returns>
        [HttpGet("Stats")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MutantRequest))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(MutantRequest))]
        public Task<IActionResult> Stats()
        {

            return ControllerExceptions(async () =>
            {
                return ApiResponse(await RepoBusinessRules.Stats());
            });
        }
    }
}
