using DataAccess.ContextDb;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Implementation
{
    public class AdnDao: RepositoryBase<Entities.Adn>, IAdnDao
    {
        public AdnDao(MainContext context): base(context)
        {

        }
    }
}
