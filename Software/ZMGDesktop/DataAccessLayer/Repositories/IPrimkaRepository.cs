using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IPrimkaRepository
    {
        int Add(Primka entity, bool saveChanges = true);
        int Update(Primka entity, bool saveChanges = true);
    }
}
