using Restaurant.DbManagers.Interfaces;
using Restaurant.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.DbManagers.MainModules
{
    public class CourierManager : IDbManager<Courier>
    {
        eslamise_RestaurantEntities db = new eslamise_RestaurantEntities();

        public void Create(Courier item)
        {
            db.Couriers.Add(item);
        }

        public void Delete(Courier item)
        {
            db.Couriers.Remove(item);
        }

        public void Edit(Courier item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public List<Courier> FildAll(Func<Courier, bool> predicate)
        {
            return db.Couriers.Where(a => predicate(a)).ToList();
        }

        public List<Courier> FindAll()
        {
            return db.Couriers.ToList();
        }

        public Courier Find(string id)
        {
            return db.Couriers.Find(id);
        }
        public Courier Find(int id)
        {
            return db.Couriers.Find(id);
        }

        public Courier Get(Func<Courier, bool> predicate)
        {
            return db.Couriers.SingleOrDefault(a => predicate(a));
        }

        public Courier Get(int id)
        {
            return db.Couriers.Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}