using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sefazProcess.Models;

namespace sefazProcess.Repository.Implementations
{
    public class ProductRepositoryImpl : IProductRepository
    {
        private DatabaseContext _context;
        private DbSet<Product> dataset;

        public ProductRepositoryImpl(DatabaseContext context)
        {
            _context = context;
            dataset = _context.Set<Product>();
        }
        

        public Product FindById(long id)
        {
            return _context.Product.SingleOrDefault(u => u.id.Equals(id));
        }

        public List<Product> FindByKeyWordPaged(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                var products = dataset.FromSql<Product>(query).ToList();
                return products;
            }
            else
            {
                return null;
            }
        }

        public int GetCount(string query)
        {
            var result = "";
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
            }

            return Int32.Parse(result);
        }
    }
}
