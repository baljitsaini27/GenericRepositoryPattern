using System;
using System.Collections.Generic;
using System.Text;
using Taleem.BAL.Interfaces;
using Taleem.Data.TaleemEntities;
using System.Linq;  
  
namespace Taleem.BAL.Repositories
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(TaleemDbContext context) : base(context)
        {

        }

        public Users GetByEmail(string Email)
        {
             return _context.Users.FirstOrDefault(x => x.Email == Email);
        }  

        public static List<DropDownlist> GetDepartmentList()
        {
            List<DropDownlist> Department = new List<DropDownlist>();
            DropDownlist obj;
            obj  = new DropDownlist();
            obj.Id = 1;
            obj.Name = "Accounts";  
            Department.Add(obj);
            obj = new DropDownlist();
            obj.Id = 2;
            obj.Name = "Computers";
            Department.Add(obj);
            obj = new DropDownlist();
            obj.Id = 3;
            obj.Name = "Support";
            Department.Add(obj);
            obj = new DropDownlist();
            obj.Id = 4;
            obj.Name = "Teaching";
            Department.Add(obj);
            obj = new DropDownlist();
            obj.Id = 5;
            obj.Name = "Admin";
            Department.Add(obj);
            return Department;
        }
    }

    public class DropDownlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
