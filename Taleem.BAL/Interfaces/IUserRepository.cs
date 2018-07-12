using System;
using System.Collections.Generic;
using System.Text;
using Taleem.Data.TaleemEntities;

namespace Taleem.BAL.Interfaces
{
    public interface IUserRepository : IRepository<Users>
    { 
        Users GetByEmail(string Email);
    }
}
