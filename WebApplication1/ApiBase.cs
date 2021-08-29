using DataAccess.Interfaces;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Utilities;
using Utilities.Enumerations;

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
        protected IActionResult ApiResponse(int status)
        {
            if(status == 200)
                return StatusCode(status, new ModelResponseApi {Status = status, Message = EnumMessages.isMutant.GetEnumDescription()});
            else
                return StatusCode(status, new ModelResponseApi { Status = status, Message = EnumMessages.isHuman.GetEnumDescription() });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected IActionResult ApiResponse(object obj)
        {
            return StatusCode(200, obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        protected async Task<IActionResult> ControllerExceptions(Func<Task<IActionResult>> func)
        {
            try
            {
                return await func().ConfigureAwait(false);
            }
            catch (Exception)
            {
                return ApiResponse(403);
            }
        }
    }
}
