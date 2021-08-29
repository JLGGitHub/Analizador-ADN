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

        public Adn(IMainContext context): base(new AdnDao(context))
        {

        }

        /// <summary>
        /// Logica de negocio que permite determinar si una secuencia de ADN corresponde a la de un mutante
        /// Se agregan metodos de extension al string adn que vermiten realizar validaciones necesarias 
        /// como el nitrogenbase, dimension de la matriz, y los distintos metodos de validacion de secuencia (Horizontal, vertical, Oblique)
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

        /// <summary>
        /// Logica de negocio que retorna la estadistica mutantes versus humanos
        /// </summary>
        /// <returns></returns>
        public Task<MutantStatisticalResponse> Stats()
        {
            var Adns = DaoBusiness._context.Set<Entities.Adn>().ToList();
            var mutant = Adns.Count(item => item.Mutant);
            var human = Adns.Count(item => !item.Mutant);
            return Task.FromResult(new MutantStatisticalResponse { CountHumanDna = human, CountMutantDna = mutant, Ratio = ((float)mutant / (float)human) });
        }
    }
}
