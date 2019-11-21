using AppG2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppG2.Controller
{
    public class ContactService
    {
        public static List<Contacts> GetContact(string pathFile, string search=null)
        {
            if (File.Exists(pathFile))
            {
                var listLines = File.ReadAllLines(pathFile);
                List<Contacts> list = new List<Contacts>();
                if (search==null)
                {
                    foreach (var line in listLines)
                    {
                        var rs = line.Split(new char[] { '#' }); //tách chuỗi ngăn bởi dấu #
                        Contacts contact = new Contacts
                        {
                            IDContact = rs[0],
                            Name = rs[1],
                            Phone = rs[2],
                            Email = rs[3]
                        };
                        list.Add(contact);
                    }
                    return list;
                }
                else
                {
                    foreach (var line in listLines)
                    {
                        var rs = line.Split(new char[] { '#' });
                        if (rs[1].ToLower().Contains(search)|| rs[2].ToLower().Contains(search)|| rs[3].ToLower().Contains(search))
                        {
                            Contacts contact = new Contacts
                            {
                                IDContact = rs[0],
                                Name = rs[1],
                                Phone = rs[2],
                                Email = rs[3]
                            };
                            list.Add(contact);
                        }
                    }
                    return list;
                }
            }
            else
                return null;
        }

        public static void delete(string pathFile, string contactID)
        {
            if (File.Exists(pathFile))
            {
                var lineArr = File.ReadAllLines(pathFile);
                File.WriteAllText(pathFile, "");
                foreach (var line in lineArr)
                {
                    var historyLine = line.Split(new char[] { '#' });
                    if (historyLine[0] != contactID)
                    {
                        File.AppendAllText(pathFile, line+ "\n");
                    }
                }
            }
        }

        public static void add(string name, string phone, string email, string pathFile)
        {
            var max = 0;
            var lineArr = File.ReadAllLines(pathFile); 
            foreach (var line in lineArr)
            {
                var contactLine = line.Split(new char[] { '#' });
                if (max < int.Parse(contactLine[0])) max = int.Parse(contactLine[0]);
            }
            max++;
            if (File.Exists(pathFile))
            {
                string line = max + "#" + name + "#" + phone + "#" + email;
                File.AppendAllText(pathFile, line + "\n"); //ghi thêm vào file
            }
        }
        public static void edit(string name, string phone, string email, string pathFile, string contactID)
        {
            if (File.Exists(pathFile))
            {
                var lineArr = File.ReadAllLines(pathFile); //mảng lưu các dòng trong file
                File.WriteAllText(pathFile, ""); //ghi đè lên file
                foreach (var line in lineArr)
                {
                    var contactLine = line.Split(new char[] { '#' });
                    if (contactLine[0] != contactID)
                    {
                        File.AppendAllText(pathFile, line + "\n"); //ghi thêm vào file
                    }
                    else
                    {
                        string lineEdit = contactID + "#" + name + "#" + phone + "#" + email;
                        File.AppendAllText(pathFile, lineEdit + "\n");
                    }
                }
            }
        }

        /****************************ĐÂY LÀ DB******************************/
        public static List<Contacts> GetContactDB(string search = null)
        {
            if (search == null) search = "";
            return new AppG2Context().ContactsDbset.
                Where(e => (e.Name.Contains(search)) || (e.Email.Contains(search)) || (e.Phone.Contains(search))).
                OrderBy(e => e.Name).
                ToList();
        }

        public static void DeleteContactDB(string contactID)
        {
            var db = new AppG2Context();
            var contact = db.ContactsDbset.Where(e => e.IDContact == contactID).FirstOrDefault();
            if (contact != null)
                db.ContactsDbset.Remove(contact);
            db.SaveChanges();
        }

        public static void AddContactDB(Contacts contact)
        {
            var db = new AppG2Context();
            contact.IDContact = Guid.NewGuid().ToString();
            db.ContactsDbset.Add(contact);
            db.SaveChanges();
        }

        public static void EditContactDB(Contacts contact)
        {
            var db = new AppG2Context();
            var cnt = db.ContactsDbset.Find(contact.IDContact);
            cnt.Name = contact.Name;
            cnt.Email = contact.Email;
            cnt.Phone = contact.Phone;
            db.SaveChanges();
        }

        public static List<Contacts> GetContactInAlphabetic(string text)
        {
            List<Contacts> listContactAfterCharacter = new List<Contacts>();
            if (!text.Equals("All"))
            {
                List<Contacts> listContact = GetContactDB();
                foreach (var item in listContact)
                {
                    if (String.Compare(item.Character, text) >= 0)
                    {
                        listContactAfterCharacter.Add(item);
                    }
                }
                return listContactAfterCharacter;
            }
            else
            {
                return GetContactDB();
            }
        }
    }
}
