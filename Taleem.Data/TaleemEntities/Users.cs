using System;
using System.Collections.Generic;

namespace Taleem.Data.TaleemEntities
{
    public partial class Users
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int UserType { get; set; }
        public long SchoolId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int Status { get; set; }
    }
}
