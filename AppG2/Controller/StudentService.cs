using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppG2.Model;

namespace AppG2.Controller
{
    public class StudentService
    {
        /// <summary>
        /// Lấy SV Theo mã SV từ MockData
        /// </summary>
        /// <param name="idStudent">Mã SV</param>
        /// <returns>SV có mã tương ứng hoặc null</returns>
        public static Student GetStudent(string idStudent)
        {
            Student student = new Student
            {
                IDStudent = idStudent,
                FirstName = "Nhi",
                LastName = "Lê",
                DOB = new DateTime(2000,5,5),
                POB = "Huế",
                Gender = GENDER.Female
            };
            student.ListHistoryLearning = new List<HistoryLearning>();
            for (int i = 1; i<=12; i++)
            {
                HistoryLearning historyLearning = new HistoryLearning
                {
                    IDHistoryLearning = i.ToString(),
                    YearFrom = 2006 + i,
                    YearEnd = 2007 + i,
                    Address = "THPT Hihihi",
                    IDStudent = idStudent
                };
                student.ListHistoryLearning.Add(historyLearning);
            }
            return student;
        }

        /// <summary>
        /// Lấy sinh viên theo MSV từ file
        /// </summary>
        /// <param name="pathDataFile">Đường dẫn tới file chứa dữ liệu</param>
        /// <param name="idStudent">Mã SV</param>
        /// <returns>SV hoặc null nếu không tồn tại</returns>
        public static Student GetStudent (string pathHistoryFile, string pathStudentFile, string idStudent)
        {
            if (File.Exists(pathStudentFile) && File.Exists(pathHistoryFile))
            {
                CultureInfo culture = CultureInfo.InvariantCulture; //theo định dạng datetime mình tự quy định
                var listLines = File.ReadAllLines(pathStudentFile);
                var listHistory = File.ReadAllLines(pathHistoryFile);
                foreach (var line in listLines)
                {
                    var rs = line.Split(new char[] { '#' }); //tách chuỗi ngăn bởi dấu #
                    Student student = new Student
                    {
                        IDStudent = rs[0],
                        LastName = rs[1],
                        FirstName = rs[2],
                        Gender = rs[3] == "Male" ? GENDER.Male : (rs[3] == "Female" ? GENDER.Female : GENDER.Other),
                        DOB = DateTime.ParseExact(rs[4], "yyyy-MM-dd", culture),
                        POB = rs[5]
                    };
                    if (student.IDStudent == idStudent)
                    {
                        student.ListHistoryLearning = new List<HistoryLearning>();
                        foreach (var historyLine in listHistory)
                        {
                            var res = historyLine.Split(new char[] { '#' });
                            HistoryLearning historyLearning = new HistoryLearning
                            {
                                IDHistoryLearning = res[0],
                                YearFrom = int.Parse(res[1]),
                                YearEnd = int.Parse(res[2]),
                                Address = res[3],
                                IDStudent = res[4]
                            };
                            if (student.IDStudent == historyLearning.IDStudent)
                                student.ListHistoryLearning.Add(historyLearning);
                        }
                        return student;
                    }
                }
                return null;
            }
            else
                return null;
        }
    }
}
