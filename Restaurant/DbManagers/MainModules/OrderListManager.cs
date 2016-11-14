using Restaurant.DbManagers.Interfaces;
using Restaurant.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.DbManagers.MainModules
{
    public class OrderListManager : IDbManager<OrderList>
    {

        eslamise_RestaurantEntities db = new eslamise_RestaurantEntities();

        public void Create(OrderList item)
        {
            db.OrderLists.Add(item);
        }

        public void Delete(OrderList item)
        {
            db.OrderLists.Remove(item);
        }

        public void Edit(OrderList item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public List<OrderList> FildAll(Func<OrderList, bool> predicate)
        {
            return db.OrderLists.Where(a => predicate(a)).ToList();
        }

        public List<OrderList> FindAll()
        {
            return db.OrderLists.ToList();
        }

        public OrderList Find(string id)
        {
            return db.OrderLists.Find(id);
        }
        public OrderList Find(int id)
        {
            return db.OrderLists.Find(id);
        }

        public OrderList Get(Func<OrderList, bool> predicate)
        {
            return db.OrderLists.SingleOrDefault(a => predicate(a));
        }

        public OrderList Get(int id)
        {
            return db.OrderLists.Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}