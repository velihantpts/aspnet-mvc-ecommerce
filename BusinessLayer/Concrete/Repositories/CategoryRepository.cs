using BusinessLayer.Abstract;
using DataAccessLayer.DataContext;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.Repositories
{
    public class CategoryRepository:GenericRepository<Category>
    {
        Context1 db = new Context1();
       public List<Product> CategoryDetails(int id)
        {
            return db.Products.Where(x => x.CategoryId == id).ToList();
        }

        
    }
}
