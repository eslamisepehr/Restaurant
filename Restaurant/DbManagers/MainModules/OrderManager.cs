using Restaurant.DbManagers.Interfaces;
using Restaurant.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.DbManagers.MainModules
{
    public class OrderManager : IDbManager<Order>
    {

        eslamise_RestaurantEntities db = new eslamise_RestaurantEntities();

        public void Create(Order item)
        {
            db.Orders.Add(item);
        }

        public void Delete(Order item)
        {
            db.Orders.Remove(item);
        }

        public void Edit(Order item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public List<Order> FildAll(Func<Order, bool> predicate)
        {
            return db.Orders.Where(a => predicate(a)).ToList();
        }

        public List<Order> FindAll()
        {
            return db.Orders.ToList();
        }

        public Order Find(string id)
        {
            return db.Orders.Find(id);
        }
        public Order Find(int id)
        {
            return db.Orders.Find(id);
        }

        public Order Get(Func<Order, bool> predicate)
        {
            return db.Orders.SingleOrDefault(a => predicate(a));
        }

        public Order Get(int id)
        {
            return db.Orders.Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}