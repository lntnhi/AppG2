using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppG2.Model
{
    public class Contacts
    {
        [Key]
        public string IDContact { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Character
        {
            get
            {
                return Name[0].ToString().ToUpper();
            }
        }
        public string Username { get; set; }
        [ForeignKey("Username")]
        public User User { get; set; }
    }
}
