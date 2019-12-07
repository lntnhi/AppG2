namespace AppG2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AppG2.Model.AppG2Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppG2.Model.AppG2Context context)
        {
            context.StudentDbset.AddOrUpdate(
                new Model.Student
                {
                IDStudent = "102T1",
                FirstName = "Nhi",
                LastName = "Lê",
                DOB = new DateTime(1900,2,2),
                Gender = Model.GENDER.Female,
                POB = "Huế"
                },
                new Model.Student
                {
                    IDStudent = "102T2",
                    FirstName = "Mon",
                    LastName = "Lê",
                    DOB = new DateTime(1901, 2, 2),
                    Gender = Model.GENDER.Female,
                    POB = "Huế"
                });
            context.SaveChanges();
            for (int i =1; i<=12; i++)
            {
                context.HistoryLearningDbset.AddOrUpdate(
                    new Model.HistoryLearning
                    {
                        IDHistoryLearning = i.ToString(),
                        IDStudent = "102T1",
                        Address = "Hai Bà Trưng",
                        YearFrom = 1906 + i,
                        YearEnd = 1097 + i
                    });
                context.SaveChanges();
            }
            for (int i = 1; i <= 12; i++)
            {
                context.HistoryLearningDbset.AddOrUpdate(
                    new Model.HistoryLearning
                    {
                        IDHistoryLearning = (i + 12).ToString(),
                        IDStudent = "102T2",
                        Address = "Quốc học",
                        YearFrom = 1906 + i,
                        YearEnd = 1097 + i
                    });
                context.SaveChanges();
            }

            context.UserDbset.AddOrUpdate(
                new Model.User
                {
                    Username = "admin",
                    Password = "123",
                    FullName = "Nhi"
                },
                new Model.User
                {
                    Username = "sau",
                    Password = "456",
                    FullName = "Nhi"
                });
            context.SaveChanges();

            context.ContactsDbset.AddOrUpdate(
                new Model.Contacts
                {
                    IDContact = "1",
                    Name = "Mùa Xuân",
                    Phone = "0987654321",
                    Email = "haha@gmail.com",
                    Username = "admin"
                },
                new Model.Contacts
                {
                    IDContact = "2",
                    Name = "Sang",
                    Phone = "0987654322",
                    Email = "haha2@gmail.com",
                    Username = "admin"
                },
                new Model.Contacts
                {
                    IDContact = "3",
                    Name = "Có",
                    Phone = "0987654323",
                    Email = "haha2@gmail.com",
                    Username = "admin"
                },
                new Model.Contacts
                {
                    IDContact = "4",
                    Name = "Anh Đào",
                    Phone = "0987654322",
                    Email = "haha2@gmail.com",
                    Username = "admin"
                },
                new Model.Contacts
                {
                    IDContact = "5",
                    Name = "Haha",
                    Phone = "0987654325",
                    Email = "haha2@gmail.com",
                    Username = "admin"
                },
                new Model.Contacts
                {
                    IDContact = "6",
                    Name = "Hihi",
                    Phone = "0987654326",
                    Email = "haha2@gmail.com",
                    Username = "admin"
                },
                new Model.Contacts
                {
                    IDContact = "7",
                    Name = "Nhi",
                    Phone = "0987654322",
                    Email = "haha2@gmail.com",
                    Username = "admin"
                },
                new Model.Contacts
                {
                    IDContact = "8",
                    Name = "Heo",
                    Phone = "0987654322",
                    Email = "haha2@gmail.com",
                    Username = "admin"
                },
                new Model.Contacts
                {
                    IDContact = "9",
                    Name = "Bò",
                    Phone = "0987654322",
                    Email = "haha2@gmail.com",
                    Username = "admin"
                },
                new Model.Contacts
                {
                    IDContact = "10",
                    Name = "Gà",
                    Phone = "0987654322",
                    Email = "haha2@gmail.com",
                    Username = "admin"
                });
            context.SaveChanges();
        }
    }
}
