using sefazProcess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tapioca.HATEOAS.Utils;

namespace sefazProcess.Business
{
    public interface IProductBusiness
    {
        Product FindById(long id);
        PagedSearchDTO<Product> FindByKeyWordPaged(long codigoGTIN, string sortDirection, int pageSize, int page);
    }
}
