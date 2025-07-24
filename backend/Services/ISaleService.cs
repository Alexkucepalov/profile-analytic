using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horizons.Connection;
using Horizons.Models;

namespace Horizons.Services
{
    public interface ISaleService
    {
        List<Assortment> GetAssortmentApriori(long id, ApplicationContext context);
        List<Assortment> GetFrequentlyAssortment(ApplicationContext context);
        List<Contrpartner> GetContrpartnersByDivision(ApplicationContext context, string division);
        List<Assortment> GetFrequentlyAssortmentByContrpartner(ApplicationContext context, long id);
    }
}
