using BusinessRules.Implementation;
using BusinessRules.Interfaces;
using DataAccess.ContextDb;
using Entities.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Text;

namespace UnitTestProyect
{
    [TestClass]
    public class AdnTest : BaseTest<IAdn>
    {
        private readonly Mock<IMainContext> adaptadorContext;

        public AdnTest()
        {
            adaptadorContext = new Mock<IMainContext>();

            var adn = new List<Entities.Adn>()
            {
                new Entities.Adn{ AdnChain =  JsonConvert.SerializeObject(new string[] { "ATGCQA", "CQGTGC", "TTQTGT", "AGAAGG", "CCCCQA", "TCACTG" }), Mutant = false, Id = 1 },
                new Entities.Adn{ AdnChain =  JsonConvert.SerializeObject(new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" }), Mutant = true, Id = 1 },
                new Entities.Adn{ AdnChain =  JsonConvert.SerializeObject(new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" }), Mutant = true, Id = 1 },
                new Entities.Adn{ AdnChain = JsonConvert.SerializeObject(new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" }), Mutant = true, Id = 1 }
            };
            DbSet<Entities.Adn> myDbSet = GetQueryableMockDbSet(adn);
            adaptadorContext.Setup(item => item.Set<Entities.Adn>()).Returns(myDbSet);
            AddAdaptadorMock();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Mutant_is_true()
        {
            var BusinessRulesAplicaciones = new BusinessRules.Implementation.Adn(adaptadorContext.Object);
            var Dna = new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };

            var lstReturn = BusinessRulesAplicaciones.IsMutant(Dna).Result;
            ////Assert
            Assert.IsTrue(lstReturn);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Mutant_is_false()
        {
            var BusinessRulesAplicaciones = new BusinessRules.Implementation.Adn(adaptadorContext.Object);
            var Dna = new string[] { "QTGCGA", "CQGTGC", "TQATGT", "AQAAGG", "CCCCTA", "TCQCTG" };

            var lstReturn = BusinessRulesAplicaciones.IsMutant(Dna).Result;
            ////Assert
            Assert.IsTrue(!lstReturn);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Stats()
        {
            var BusinessRulesAplicaciones = new BusinessRules.Implementation.Adn(adaptadorContext.Object);
           
            var lstReturn = BusinessRulesAplicaciones.Stats().Result;
            ////Assert
            Assert.IsTrue(lstReturn != null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void NitrogenBase_is_true()
        {
            var Dna = new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            Assert.IsTrue(Utilities.ExtensionMethods.NitrogenBase(Dna));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void NitrogenBase_is_false()
        {
            var Dna = new string[] { "QOGCGA", "CQGTGC", "TQATGT", "AQAAGG", "CCCCTA", "TCQCTG" };
            Assert.IsFalse(Utilities.ExtensionMethods.NitrogenBase(Dna));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void HorizontalSequenceSearch_is_true()
        {
            var Dna = new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            Assert.IsTrue(Utilities.ExtensionMethods.HorizontalSequenceSearch(Dna) == 1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void HorizontalSequenceSearch_is_false()
        {
            var Dna = new string[] { "QOGCGA", "CQGTGC", "TQATGT", "AQAAGG", "CPCCTA", "TCQCTG" };
            Assert.IsTrue(Utilities.ExtensionMethods.HorizontalSequenceSearch(Dna) == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void VerticalSequenceSearch_is_true()
        {
            var Dna = new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            Assert.IsTrue(Utilities.ExtensionMethods.VerticalSequenceSearch(Dna) == 1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void VerticalSequenceSearch_is_false()
        {
            var Dna = new string[] { "ATGCAA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            Assert.IsTrue(Utilities.ExtensionMethods.VerticalSequenceSearch(Dna) == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ObliqueSequenceSearch_is_true()
        {
            var Dna = new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            Assert.IsTrue(Utilities.ExtensionMethods.ObliqueSequenceSearch(Dna) == 1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ObliqueSequenceSearch_is_false()
        {
            var Dna = new string[] { "TTGCAA", "CTGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            Assert.IsTrue(Utilities.ExtensionMethods.ObliqueSequenceSearch(Dna) == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void MatrixNxN_is_true()
        {
            var Dna = new string[] { "TTGCAA", "CTGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            Assert.IsTrue(Utilities.ExtensionMethods.MatrixNxN(Dna));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void MatrixNxN_is_false()
        {
            var Dna = new string[] { "QTTGCAA", "CTGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            Assert.IsFalse(Utilities.ExtensionMethods.MatrixNxN(Dna));
        }

        // <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GenerateMatrix()
        {
            var Dna = new string[] { "TTGCAA", "CTGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            Assert.IsTrue(Utilities.ExtensionMethods.GenerateMatrix(Dna).Length > 1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetEnumDescription()
        {
            Assert.IsTrue(Utilities.ExtensionMethods.GetEnumDescription(Utilities.Enumerations.EnumMessages.isMutant) == "Felicidades, es un Mutante");
        }
    }
}
