using Cipher;
using EmailSender;
using ManagerEmail.Models.Domains;
using ManagerEmail.Models.Repositories;
using ManagerEmail.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ManagerEmail.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private EmailSender.Email _email;
        private EmailRepository _emailRepository = new EmailRepository();

        private string _emailReceiver;
       // private StringCipher _stringCipher = new StringCipher("C93EDD3E-4785-4BDE-BC3E-0A18EE87F330");
        private const string NotEncryptedPasswordPrefix = "encrypt:";

        public HomeController()
        {
            
                _emailReceiver = ConfigurationManager.AppSettings["ReceiverEmail"];





                _email = new EmailSender.Email(new EmailParams
                {
                    HostSmtp = ConfigurationManager.AppSettings["HostSmtp"],
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]),
                    EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]),
                    SenderName = ConfigurationManager.AppSettings["SenderName"],
                    SenderEmail = ConfigurationManager.AppSettings["SenderEmail"],
                    SenderEmailPassword = ConfigurationManager.AppSettings["SenderEmailPassword"]
                }); ;
           
        }
/*
        private string DecryptSenderEmailPassword()
        {
            var encryptedPassword = ConfigurationManager.AppSettings["SenderEmailPassword"];

            if (encryptedPassword.StartsWith(NotEncryptedPasswordPrefix))
            {
                encryptedPassword = _stringCipher
                    .Encrypt(encryptedPassword.Replace(NotEncryptedPasswordPrefix, ""));

                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configFile.AppSettings.Settings["SenderEmailPassword"].Value = encryptedPassword;
                configFile.Save();

            }
            return _stringCipher.Decrypt(encryptedPassword);
        }*/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Email()
        {
            var userId = User.Identity.GetUserId();
            var emails = _emailRepository.GetEmails(userId);
            return View(emails);
        }
      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Email(Models.Domains.Email email)
        {
            var userId = User.Identity.GetUserId();
            email.UserId = userId;
          

            if (!ModelState.IsValid)
            {
                var vm = PrepareEmailVm(email);
                return View("Index",vm);
            }


            _emailRepository.Add(email);
           await _email.Send(email.Title, email.Message, email.Recipient);

            return RedirectToAction("Email");

        }

        private EditEmailViewModel PrepareEmailVm(Models.Domains.Email email)
        {
            return new EditEmailViewModel
            {
                Email = email,
            };
        }

        [AllowAnonymous]

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [AllowAnonymous]

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}