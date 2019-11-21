using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppG2.Model
{
    public class Student
    {
        [Key]
        public string IDStudent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GENDER Gender { get; set; } //định nghĩa bằng kiểu GENDER ở dưới, là 1 trong 3 cái đó
        public DateTime DOB { get; set; }
        public string POB { get; set; }
        public ICollection<HistoryLearning> ListHistoryLearning { get; set; } //1 SV có 1 hoặc nhiều QTHT
    }
    public enum GENDER
    {
        Male, Female, Other
    }
}
