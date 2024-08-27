using ManagerEmail.Models.Domains;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ManagerEmail.Models.Repositories
{
    public class EmailRepository
    {

        public void Add(Email email)
        {
            using (var context = new ApplicationDbContext())
            {
                email.CreatedDate = DateTime.Now;
                email.SentDate = DateTime.Now;
                email.File = "";
                email.Sender = ConfigurationManager.AppSettings["SenderEmail"];
                context.Email.Add(email);


                context.SaveChanges();
            }
        }

        public List<Email> GetEmails(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Email
                    .Where(x => x.UserId == userId).ToList();
            }

        }
    }
}