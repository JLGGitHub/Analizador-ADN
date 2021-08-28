using BusinessRules.Interfaces;
using DataAccess.ContextDb;
using DataAccess.Implementation;
using DataAccess.Interfaces;
using System.Threading.Tasks;
using Utilities;
using Newtonsoft.Json;
using Entities.DTO;
using System.Linq;

namespace BusinessRules.Implementation
{


    public class Adn : BusinessRulesBase<Entities.Adn, IAdnDao>, IAdn
    {

        public Adn(MainContext context): base(new AdnDao(context))
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adn"></param>
        /// <returns></returns>
        public Task<bool> IsMutant(string[] adn)
        {
            int sequence = 0; 
            if(adn.NitrogenBase() && adn.MatrixNxN())
            {
                sequence = adn.HorizontalSequenceSearch();
            }

            DaoBusiness.Create(new Entities.Adn { AdnChain = JsonConvert.SerializeObject(adn).ToUpper(), Mutant = (sequence > Constants.MinimalMutantSequence) });
            return Task.FromResult(sequence > Constants.MinimalMutantSequence);
        }

        public Task<MutantStatisticalResponse> Stats()
        {
            var x = DaoBusiness._context.Adns.ToList();
            return Task.FromResult(new MutantStatisticalResponse { CountHumanDna = x.Count(item => item.Mutant), CountMutantDna = x.Count(item => !item.Mutant), Ratio = 0 });
        }
    }
}
