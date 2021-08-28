using DataAccess.Interfaces;
using Entities.DTO;
using System.Threading.Tasks;

namespace BusinessRules.Interfaces
{
    public interface IAdn: IAdnDao, IRepositoryBase<Entities.Adn>
    {
        Task<bool> IsMutant(string[] adn);
        Task<MutantStatisticalResponse> Stats();

    }
}
