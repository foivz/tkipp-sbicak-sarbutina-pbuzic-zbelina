using EntitiesLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class PrimkaServices
    {
        private readonly IPrimkaRepository primkaRepository;

        public PrimkaServices(IPrimkaRepository _primkaRepository)
        {
            this.primkaRepository = _primkaRepository;
        }

        public bool DodajPrimku(Primka primka)
        {
            bool uspjeh = false;
            
            
                int affectedRows = primkaRepository.Add(primka);
                uspjeh = affectedRows > 0;
            
            return uspjeh;
        }
    }
}
