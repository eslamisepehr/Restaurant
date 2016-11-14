using Restaurant.DbManagers.Interfaces;
using Restaurant.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.DbManagers.MainModules
{
    public class UserAddressManager : IDbManager<UserAddress>
    {

        eslamise_RestaurantEntities db = new eslamise_RestaurantEntities();

        public void Create(UserAddress item)
        {
            db.UserAddresses.Add(item);
        }

        public void Delete(UserAddress item)
        {
            db.UserAddresses.Remove(item);
        }

        public void Edit(UserAddress item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public List<UserAddress> FildAll(Func<UserAddress, bool> predicate)
        {
            return db.UserAddresses.Where(a => predicate(a)).ToList();
        }

        public List<UserAddress> FindAll()
        {
            return db.UserAddresses.ToList();
        }

        public UserAddress Find(string id)
        {
            return db.UserAddresses.Find(id);
        }
        public UserAddress Find(int id)
        {
            return db.UserAddresses.Find(id);
        }

        public UserAddress Get(Func<UserAddress, bool> predicate)
        {
            return db.UserAddresses.SingleOrDefault(a => predicate(a));
        }

        public UserAddress Get(int id)
        {
            return db.UserAddresses.Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}