using DrinkAndGo.Data.Interfaces;
using DrinkAndGo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkAndGo.Data.Repositories
{
    public class DrinkRepository : IDrinkRepository
    {
        private readonly AppDBContext _appDbContext;
        public DrinkRepository(AppDBContext appDBContext)
        {
            _appDbContext = appDBContext;
        }
        public IEnumerable<Drink> Drinks => _appDbContext.Drinks.Include(c=>c.Category);

        public IEnumerable<Drink> PreferredDrinks =>_appDbContext.Drinks.Where(p=>p.IsPreferredDrink).Include(c=>c.Category);

        public Drink getDrinkByID(int drinkID) => _appDbContext.Drinks.FirstOrDefault(p => p.DrinkID == drinkID);
        
    }
}
