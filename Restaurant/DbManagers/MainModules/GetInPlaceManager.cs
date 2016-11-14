using Restaurant.DbManagers.Interfaces;
using Restaurant.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.DbManagers.MainModules
{
    public class GetInPlaceManager : IDbManager<GetInPlace>
    {

        eslamise_RestaurantEntities db = new eslamise_RestaurantEntities();

        public void Create(GetInPlace item)
        {
            db.GetInPlaces.Add(item);
        }

        public void Delete(GetInPlace item)
        {
            db.GetInPlaces.Remove(item);
        }

        public void Edit(GetInPlace item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public List<GetInPlace> FildAll(Func<GetInPlace, bool> predicate)
        {
            return db.GetInPlaces.Where(a => predicate(a)).ToList();
        }

        public List<GetInPlace> FindAll()
        {
            return db.GetInPlaces.ToList();
        }

        public GetInPlace Find(string id)
        {
            return db.GetInPlaces.Find(id);
        }
        public GetInPlace Find(int id)
        {
            return db.GetInPlaces.Find(id);
        }

        public GetInPlace Get(Func<GetInPlace, bool> predicate)
        {
            return db.GetInPlaces.SingleOrDefault(a => predicate(a));
        }

        public GetInPlace Get(int id)
        {
            return db.GetInPlaces.Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}