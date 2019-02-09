using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sefazProcess.Models;
using sefazProcess.Repository;
using Tapioca.HATEOAS.Utils;

namespace sefazProcess.Business.Implementations
{
    public class ProductBusImpl : IProductBusiness
    {
        protected IProductRepository _repository;

        public ProductBusImpl(IProductRepository repository)
        {
            _repository = repository;
        }
        

        public Product FindById(long id)
        {
            Product produto = new Product();
            try
            {
                produto = _repository.FindById(id);
                produto.urlCoordinator = @"http://maps.google.com/maps?q=" + produto.numeroLongitude + "," + produto.numeroLatitude;
                return produto;
            
            }catch(Exception ex)
            {
                throw ex;
            }
        }

 
        public PagedSearchDTO<Product> FindByKeyWordPaged(long codigoGTIN, string sortDirection, int pageSize, int page)
        {
            List<Product> products = new List<Product>();

            page = page > 0 ? page - 1 : 0;
            int offset = pageSize * page;
            string query = @"select * from Product p where 1 = 1 ";
            query = query + $" and p.codigoGTIN = {codigoGTIN} ";
            query = query + $"and p.numeroLatitude != '' and p.numeroLongitude != '' ";
            query = query + $" order by p.valorUnitario {sortDirection} limit {pageSize} offset {offset}";

            string countQuery = @"select count(*) from Product u where 1 = 1 ";
            countQuery = countQuery + $" and u.codigoGTIN = {codigoGTIN} ";

            products = _repository.FindByKeyWordPaged(query);
            foreach(var p in products)
            {
                p.urlCoordinator = @"http://maps.google.com/maps?q=" + p.numeroLongitude + "," + p.numeroLatitude;
            }
            int totalResults = _repository.GetCount(countQuery);


            return new PagedSearchDTO<Product>
            {
                CurrentPage = page + 1,
                List = products,
                PageSize = pageSize,
                SortDirections = sortDirection,
                TotalResults = totalResults
                
            };
        }
    }
}
