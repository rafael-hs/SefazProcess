using sefazProcess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sefazProcess.Repository
{
    public interface IProductRepository
    {
        Product FindById(long id);
        List<Product> FindByKeyWordPaged(string query);
        int GetCount(string query);
    }
}
