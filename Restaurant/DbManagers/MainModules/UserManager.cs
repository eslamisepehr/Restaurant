using Restaurant.DbManagers.Interfaces;
using Restaurant.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.DbManagers.MainModules
{
    public class UserManager : IDbManager<AspNetUser>
    {

        eslamise_RestaurantEntities db = new eslamise_RestaurantEntities();

        public void Create(AspNetUser item)
        {
            db.AspNetUsers.Add(item);
        }

        public void Delete(AspNetUser item)
        {
            db.AspNetUsers.Remove(item);
        }

        public void Edit(AspNetUser item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }


        public List<AspNetUser> FildAll(Func<AspNetUser, bool> predicate)
        {
            return db.AspNetUsers.Where(a => predicate(a)).ToList();
        }

        public List<AspNetUser> FindAll()
        {
            return db.AspNetUsers.ToList();
        }

        public AspNetUser Find(string id)
        {
            return db.AspNetUsers.Find(id);
        }
        public AspNetUser Find(int id)
        {
            return db.AspNetUsers.Find(id);
        }

        public AspNetUser Get(Func<AspNetUser, bool> predicate)
        {
            return db.AspNetUsers.SingleOrDefault(a => predicate(a));
        }

        public AspNetUser Get(string id)
        {
            return db.AspNetUsers.Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}