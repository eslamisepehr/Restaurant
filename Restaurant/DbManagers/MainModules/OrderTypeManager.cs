using Restaurant.DbManagers.Interfaces;
using Restaurant.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.DbManagers.MainModules
{
    public class OrderTypeManager : IDbManager<OrderType>
    {

        eslamise_RestaurantEntities db = new eslamise_RestaurantEntities();

        public void Create(OrderType item)
        {
            db.OrderTypes.Add(item);
        }

        public void Delete(OrderType item)
        {
            db.OrderTypes.Remove(item);
        }

        public void Edit(OrderType item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public List<OrderType> FildAll(Func<OrderType, bool> predicate)
        {
            return db.OrderTypes.Where(a => predicate(a)).ToList();
        }

        public List<OrderType> FindAll()
        {
            return db.OrderTypes.ToList();
        }

        public OrderType Find(string id)
        {
            return db.OrderTypes.Find(id);
        }
        public OrderType Find(int id)
        {
            return db.OrderTypes.Find(id);
        }

        public OrderType Get(Func<OrderType, bool> predicate)
        {
            return db.OrderTypes.SingleOrDefault(a => predicate(a));
        }

        public OrderType Get(int id)
        {
            return db.OrderTypes.Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}