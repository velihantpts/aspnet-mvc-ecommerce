using BusinessLayer.Abstract;
using DataAccessLayer.DataContext;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.Repositories
{
   public class ProductRepository:GenericRepository<Product>
    {
        Context1 db = new Context1();
        public List<Product> GetProductList()
        {
            return db.Products.Include("Category").ToList();
        }
        public List<Product> GetPopularProduct()
        {
            return db.Products.Include("Category").Where(x => x.Popular == true).Take(3).ToList();
        }
    }
}
