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
            if (adn.NitrogenBase() && adn.MatrixNxN())
            {
                sequence = adn.HorizontalSequenceSearch() + adn.ObliqueSequenceSearch() + adn.VerticalSequenceSearch();
            }

            DaoBusiness.Create(new Entities.Adn { AdnChain = JsonConvert.SerializeObject(adn).ToUpper(), Mutant = (sequence > Constants.MinimalMutantSequence) });
            return Task.FromResult(sequence > Constants.MinimalMutantSequence);
        }

        public Task<MutantStatisticalResponse> Stats()
        {
            var Adns = DaoBusiness._context.Adns.ToList();
            var mutant = Adns.Count(item => item.Mutant);
            var human = Adns.Count(item => !item.Mutant);
            return Task.FromResult(new MutantStatisticalResponse { CountHumanDna = human, CountMutantDna = mutant, Ratio = ((float)mutant / (float)human)  });
        }
    }
}
