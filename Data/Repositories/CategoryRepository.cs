using DrinkAndGo.Data.Interfaces;
using DrinkAndGo.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkAndGo.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDBContext _appDBContext;
        public CategoryRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public IEnumerable<Category> Categories =>_appDBContext.Categories;// this is a lambda expression

        // alternative implementation if you dont understand lambda

       /* public IEnumerable<Category> Categories
        {
            get
            
            {
               return _appDBContext.Categories;
            }
        }*/



    }
}
