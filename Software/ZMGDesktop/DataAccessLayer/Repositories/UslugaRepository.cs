﻿using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UslugaRepository : Repository<Usluga>, IUslugaRepository
    {
        public UslugaRepository() : base(new ZMGBaza())
        {

        }
        public override int Update(Usluga entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<Usluga> GetAll()
        {
            var query = from p in Entities.Include("Materijal")
                        select p; 
            return query;
        }
        

        public IQueryable<Usluga> DohvatiUsluguPoNazivu(string naziv)
        {
            var query = from p in Entities.Include("Materijal")
                        where p.Naziv == naziv
                        select p;
            return query;
        }

        public IQueryable<string> DohvatiUslugeDistinct()
        {
            var query = from p in Entities.Include("Materijal")
                        select p.Naziv;
            return query;
        }
    }
}
