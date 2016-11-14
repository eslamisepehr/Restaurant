using Restaurant.DbManagers.Interfaces;
using Restaurant.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.DbManagers.MainModules
{
    public class UserRoleManager : IDbManager<AspNetUserRole>
    {

        eslamise_RestaurantEntities db = new eslamise_RestaurantEntities();

        public void Create(AspNetUserRole item)
        {
            db.AspNetUserRoles.Add(item);
        }

        public void Delete(AspNetUserRole item)
        {
            db.AspNetUserRoles.Remove(item);
        }

        public void Edit(AspNetUserRole item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public List<AspNetUserRole> FildAll(Func<AspNetUserRole, bool> predicate)
        {
            return db.AspNetUserRoles.Where(a => predicate(a)).ToList();
        }

        public List<AspNetUserRole> FindAll()
        {
            return db.AspNetUserRoles.ToList();
        }

        public AspNetUserRole Find(string id)
        {
            return db.AspNetUserRoles.Find(id);
        }
        public AspNetUserRole Find(int id)
        {
            return db.AspNetUserRoles.Find(id);
        }

        //public AspNetUserRole Get(Func<AspNetUser, bool> predicate)
        //{
        //    return db.AspNetUserRoles.SingleOrDefault(a => predicate(a));
        //}

        public AspNetUserRole Get(string id)
        {
            return db.AspNetUserRoles.Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}