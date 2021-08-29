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
        /// 
        /// </summary>
        /// <param name="adn"></param>
        /// <returns></returns>
        [HttpPost("Mutant")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MutantRequest))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(MutantRequest))]
        public Task<IActionResult> Mutant([FromBody] Entities.DTO.MutantRequest adn)
        {

            return ControladorExepciones(async () =>
            {
                return RespuestaApi(await RepoBusinessRules.IsMutant(adn.Dna) ? 200 : 403);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adn"></param>
        /// <returns></returns>
        [HttpGet("Stats")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MutantRequest))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(MutantRequest))]
        public Task<IActionResult> Stats()
        {

            return ControladorExepciones(async () =>
            {
                return RespuestaApi(await RepoBusinessRules.Stats());
            });
        }
    }
}
