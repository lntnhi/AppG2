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
        
        /// <summary>
        /// Lấy sinh viên theo MSV từ DB
        /// </summary>
        /// <param name="idStudent">Mã SV</param>
        /// <returns>SV hoặc null nếu không tồn tại</returns>
        public static Student GetStudentDB (string idStudent)
        {
            return new AppG2Context().StudentDbset.Where(e => e.IDStudent == idStudent).FirstOrDefault(); //e là 1 sinh viên, là cái x đó
        }
        public static List<HistoryLearning> GetHistoryLearningDB (string idStudent)
        {
            return new AppG2Context().HistoryLearningDbset.Where(e => e.IDStudent == idStudent).OrderBy(e => e.YearFrom).ToList();
        }
        public static void deleteHistoryLearning (string pathHistoryFile, string historyID)
        {
            if (File.Exists(pathHistoryFile))
            {
                var lineArr = File.ReadAllLines(pathHistoryFile); //mảng lưu các dòng trong file
                File.WriteAllText(pathHistoryFile,""); //ghi đè lên file
                foreach (var line in lineArr)
                {
                    var historyLine = line.Split(new char[] { '#' });
                    if (historyLine[0]!=historyID)
                    {
                        File.AppendAllText(pathHistoryFile, line + "\n"); //ghi thêm vào file
                    }
                }
            }
        }

        public static void addHistory(int yearFrom, int yearEnd, string address, string studentID, string pathHistoryFile)
        {
            var max = 0;
            var lineArr = File.ReadAllLines(pathHistoryFile); //mảng lưu các dòng trong file
            foreach (var line in lineArr)
            {
                var historyLine = line.Split(new char[] { '#' });
                if (max < int.Parse(historyLine[0])) max = int.Parse(historyLine[0]);
            }
            max++;
            if (File.Exists(pathHistoryFile))
            {
                string line = max+ "#" + yearFrom + "#" + yearEnd + "#" + address + "#" + studentID;
                File.AppendAllText(pathHistoryFile, line+ "\n"); //ghi thêm vào file
            }
        }

        public static void editHistory(int yearFrom, int yearEnd, string address, string studentID, string pathHistoryFile, string historyID)
        {
            if (File.Exists(pathHistoryFile))
            {
                var lineArr = File.ReadAllLines(pathHistoryFile); //mảng lưu các dòng trong file
                File.WriteAllText(pathHistoryFile, ""); //ghi đè lên file
                foreach (var line in lineArr)
                {
                    var historyLine = line.Split(new char[] { '#' });
                    if (historyLine[0] != historyID)
                    {
                        File.AppendAllText(pathHistoryFile, line + "\n"); //ghi thêm vào file
                    }
                    else
                    {
                        string lineEdit = historyID + "#" + yearFrom + "#" + yearEnd + "#" + address + "#" + studentID;
                        File.AppendAllText(pathHistoryFile, lineEdit + "\n");
                    }
                }
            }
        }
    }
}
