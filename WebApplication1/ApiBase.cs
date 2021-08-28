using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutantApi
{
    public class ApiBase<T, TImplementacion> : ControllerBase
         where T : class, new()
         where TImplementacion : IRepositoryBase<T>
    {
        protected readonly TImplementacion RepoBusinessRules;

        public ApiBase(TImplementacion repoBusinessRules)
        {
            RepoBusinessRules = repoBusinessRules;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Respuesta"></param>
        /// <returns></returns>
        protected IActionResult RespuestaApi(int status)
        {
            return StatusCode(status);
        }
        protected IActionResult RespuestaApi(object obj)
        {
            return StatusCode(200, obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        protected async Task<IActionResult> ControladorExepciones(Func<Task<IActionResult>> func)
        {
            try
            {
                return await func().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return RespuestaApi(403);
            }
        }
    }
}
