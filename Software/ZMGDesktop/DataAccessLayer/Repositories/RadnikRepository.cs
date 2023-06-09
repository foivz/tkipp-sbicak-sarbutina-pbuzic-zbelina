using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class RadnikRepository : Repository<Radnik>, IRadnikRepository
    {
        public RadnikRepository() : base(new ZMGBaza())
        {

        }

        public async Task<Radnik> DohvatiRadnikaAsync(string korime, string lozinka) {
            using (var sha256 = SHA256.Create()) {
                var enteredBytes = Encoding.UTF8.GetBytes(lozinka);
                var enteredHashedPassword = BitConverter.ToString(sha256.ComputeHash(enteredBytes)).Replace("-", "").ToLower();

                var query = await (from s in Entities
                                   where (s.Korime == korime && s.Lozinka == enteredHashedPassword)
                                   select s).FirstOrDefaultAsync();
                return query;
            }
        }


        public IQueryable<Radnik> DohvatiSveRadnike()
        {
            var query = from r in Entities
                        select r;
            return query;
        }

        public override int Update(Radnik entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

    }
}
