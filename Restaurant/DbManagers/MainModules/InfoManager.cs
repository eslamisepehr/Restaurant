using Restaurant.DbManagers.Interfaces;
using Restaurant.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.DbManagers.MainModules
{
    public class InfoManager : IDbManager<Info>
    {

        eslamise_RestaurantEntities db = new eslamise_RestaurantEntities();

        public void Create(Info item)
        {
            db.Infoes.Add(item);
        }

        public void Delete(Info item)
        {
            db.Infoes.Remove(item);
        }

        public void Edit(Info item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public List<Info> FildAll(Func<Info, bool> predicate)
        {
            return db.Infoes.Where(a => predicate(a)).ToList();
        }

        public List<Info> FindAll()
        {
            return db.Infoes.ToList();
        }

        public Info Find(string id)
        {
            return db.Infoes.Find(id);
        }

        public Info Find(int id)
        {
            return db.Infoes.Find(id);
        }

        public Info Get(Func<Info, bool> predicate)
        {
            return db.Infoes.SingleOrDefault(a => predicate(a));
        }

        public Info Get(int id)
        {
            return db.Infoes.Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}