using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRBank.Application.ViewModel
{
    public class NewClientViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public int Age { get; set; }
        [EmailAddress(ErrorMessage = "The field {0} is invalid")]
        public string Email { get; set; }
        public string CelNumber { get; set; }
        public int ManagerId { get; set; }
    }
}
