using Restaurant.DbManagers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Restaurant.Models.Entities;

namespace Restaurant.DbManagers.MainModules
{
    public class DiscountCodeManager : IDbManager<DiscountCode>
    {
        eslamise_RestaurantEntities db = new eslamise_RestaurantEntities();
        public void Create(DiscountCode item)
        {
            db.DiscountCodes.Add(item);
        }

        public void Delete(DiscountCode item)
        {
            db.DiscountCodes.Remove(item);
        }

        public void Edit(DiscountCode item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public List<DiscountCode> FildAll(Func<DiscountCode, bool> predicate)
        {
            return db.DiscountCodes.Where(a => predicate(a)).ToList();
        }

        public List<DiscountCode> FindAll()
        {
            return db.DiscountCodes.ToList();
        }

        public DiscountCode Find(string id)
        {
            return db.DiscountCodes.Find(id);
        }
        public DiscountCode Find(int id)
        {
            return db.DiscountCodes.Find(id);
        }

        public DiscountCode Get(Func<DiscountCode, bool> predicate)
        {
            return db.DiscountCodes.SingleOrDefault(a => predicate(a));
        }

        public DiscountCode Get(int id)
        {
            return db.DiscountCodes.Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}