using Restaurant.DbManagers.Interfaces;
using Restaurant.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.DbManagers.MainModules
{
    public class RestaurantTableManager : IDbManager<RestaurantTable>
    {

        eslamise_RestaurantEntities db = new eslamise_RestaurantEntities();

        public void Create(RestaurantTable item)
        {
            db.RestaurantTables.Add(item);
        }

        public void Delete(RestaurantTable item)
        {
            db.RestaurantTables.Remove(item);
        }

        public void Edit(RestaurantTable item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public List<RestaurantTable> FildAll(Func<RestaurantTable, bool> predicate)
        {
            return db.RestaurantTables.Where(a => predicate(a)).ToList();
        }

        public List<RestaurantTable> FindAll()
        {
            return db.RestaurantTables.ToList();
        }

        public RestaurantTable Find(string id)
        {
            return db.RestaurantTables.Find(id);
        }
        public RestaurantTable Find(int id)
        {
            return db.RestaurantTables.Find(id);
        }

        public RestaurantTable Get(Func<RestaurantTable, bool> predicate)
        {
            return db.RestaurantTables.SingleOrDefault(a => predicate(a));
        }

        public RestaurantTable Get(int id)
        {
            return db.RestaurantTables.Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}