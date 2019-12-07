using AppG2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public static List<Contacts> GetContactDB(User user, string search = null)
        {
            var db = new AppG2Context();
            if (search == null) search = "";
            return db.ContactsDbset.
                Where(e =>(e.Username == user.Username) && ((e.Name.Contains(search)) || (e.Email.Contains(search)) || (e.Phone.Contains(search)))).
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
            db.ContactsDbset.Add(contact);
            db.SaveChanges();
        }

        public static void EditContactDB(Contacts contact)
        {
            var db = new AppG2Context();
            //List<Contacts> listContacts = GetContactDB(user);
            //Contacts contactOld = isExist(contact, listContacts);
            //if (contactOld != null)
            //{
                var cnt = db.ContactsDbset.Find(contact.IDContact);
                cnt.Name = contact.Name;
                cnt.Email = contact.Email;
                cnt.Phone = contact.Phone;
                db.SaveChanges();
            //}            
        }
        public static void EditContactImport(Contacts contact)
        {
            var db = new AppG2Context();
            var cnt = db.ContactsDbset.Where(e => e.Phone == contact.Phone && e.Username == contact.Username).FirstOrDefault();
            cnt.Name = contact.Name;
            cnt.Email = contact.Email;
            db.SaveChanges();
        }
        public static List<Contacts> GetContactInAlphabetic(User user, string text)
        {
            List<Contacts> listContactAfterCharacter = new List<Contacts>();
            if (!text.Equals("All"))
            {
                List<Contacts> listContact = GetContactDB(user);
                foreach (var item in listContact)
                {
                    if (string.Compare(item.Character, text) >= 0)
                    {
                        listContactAfterCharacter.Add(item);
                    }
                }
                return listContactAfterCharacter;
            }
            else
            {
                return GetContactDB(user);
            }
        }
    
        public static User login(string username, string password)
        {
            var db = new AppG2Context();
            var user = db.UserDbset.Where(e => e.Username == username && e.Password == password).FirstOrDefault();
            if (user != null) return user;
            return null;
        }
        public static Contacts isExist(Contacts contact, List<Contacts> listContacts)
        {
            foreach (var contactOld in listContacts)
            {
                if (contactOld.Phone.Equals(contact.Phone)) return contactOld;
            }
            return null;
        }

        public static void Import(User user, List<Contacts> listContacts)
        {
            var db = new AppG2Context();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn file .csv";
            ofd.Filter = "File csv(*.csv)|*.csv";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var listLines = File.ReadAllLines(ofd.FileName);
                foreach (var line in listLines)
                {
                    var rs = line.Split(new char[] { ',' });
                    Contacts contact = new Contacts
                    {
                        IDContact = Guid.NewGuid().ToString(),
                        Name = rs[0],
                        Phone = rs[1],
                        Email = rs[2],
                        Username = user.Username
                    };
                    if (isExist(contact, listContacts) == null)
                        db.ContactsDbset.Add(contact);
                    else EditContactImport(contact);
                }
                db.SaveChanges();
            }
        }
    }
}
