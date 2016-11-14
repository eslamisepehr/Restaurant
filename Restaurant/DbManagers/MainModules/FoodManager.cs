using Restaurant.DbManagers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Restaurant.Models.Entities;

namespace Restaurant.DbManagers.MainModules
{
    public class FoodManager : IDbManager<Food>
    {
        eslamise_RestaurantEntities db = new eslamise_RestaurantEntities();

        public void Create(Food item)
        {
            db.Foods.Add(item);
        }

        public void Delete(Food item)
        {
            db.Foods.Remove(item);
        }

        public void Edit(Food item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public List<Food> FildAll(Func<Food, bool> predicate)
        {
            return db.Foods.Where(a => predicate(a)).ToList();
        }

        public List<Food> FindAll()
        {
            return db.Foods.ToList();
        }

        public Food Find(string id)
        {
            return db.Foods.Find(id);
        }
        public Food Find(int id)
        {
            return db.Foods.Find(id);
        }

        public Food Get(Func<Food, bool> predicate)
        {
            return db.Foods.SingleOrDefault(a => predicate(a));
        }

        public Food Get(int id)
        {
            return db.Foods.Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}