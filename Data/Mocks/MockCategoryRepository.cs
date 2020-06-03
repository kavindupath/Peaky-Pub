using DrinkAndGo.Data.Interfaces;
using DrinkAndGo.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrinkAndGo.Data.Models;

namespace DrinkAndGo.Data.Mocks
{
    public class MockCategoryRepository :ICategoryRepository
    {
        public IEnumerable<Category> Categories
        {
           /* get
            {
                return new List<Category>
                     {
                         new Category { CategoryName = "Alcoholic", Description = "All alcoholic drinks" },
                         new Category { CategoryName = "Non-alcoholic", Description = "All non-alcoholic drinks" }
                     };
            }*/

         
            get
            {
                List<Category> CategoryList = new List<Category>();
                Category c1 = new Category();
                Category c2 = new Category();

                c1.CategoryName = "Alcoholic";
                c1.Description = "All alcoholic drinks";


                c2.CategoryName = "Non-Alcoholic";
                c2.Description = "All non-alcoholic drinks";

                CategoryList.Add(c1);
                CategoryList.Add(c2);

                return CategoryList;


            }
            

        }


    }
}
