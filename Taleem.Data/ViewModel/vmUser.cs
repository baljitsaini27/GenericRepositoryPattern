using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Taleem.Data.TaleemEntities;

namespace Taleem.Data.ViewModel
{ 
    public class vmUser
    {
        public vmUser()
        {
            User = new Users(); 
        }
          
        public Users User { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }
    }  
}
