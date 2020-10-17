using challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface ICompensationRepository
    {
        List<Compensation> GetById(String id);
        Compensation Create(Compensation compensation);
        Task SaveAsync();
    }
}
