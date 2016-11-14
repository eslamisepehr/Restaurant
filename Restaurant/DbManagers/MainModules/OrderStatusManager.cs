using Restaurant.DbManagers.Interfaces;
using Restaurant.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.DbManagers.MainModules
{
    public class OrderStatusManager : IDbManager<OrderStatu>
    {

        eslamise_RestaurantEntities db = new eslamise_RestaurantEntities();

        public void Create(OrderStatu item)
        {
            db.OrderStatus.Add(item);
        }

        public void Delete(OrderStatu item)
        {
            db.OrderStatus.Remove(item);
        }

        public void Edit(OrderStatu item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public List<OrderStatu> FildAll(Func<OrderStatu, bool> predicate)
        {
            return db.OrderStatus.Where(a => predicate(a)).ToList();
        }

        public List<OrderStatu> FindAll()
        {
            return db.OrderStatus.ToList();
        }

        public OrderStatu Find(string id)
        {
            return db.OrderStatus.Find(id);
        }
        public OrderStatu Find(int id)
        {
            return db.OrderStatus.Find(id);
        }

        public OrderStatu Get(Func<OrderStatu, bool> predicate)
        {
            return db.OrderStatus.SingleOrDefault(a => predicate(a));
        }

        public OrderStatu Get(int id)
        {
            return db.OrderStatus.Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}