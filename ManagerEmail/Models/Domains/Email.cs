using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerEmail.Models.Domains
{
    [Bind(Exclude = "Id")]
    public class Email
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Recipient { get; set; }
        public string Sender { get; set; }
        [Required]
        public string Message { get; set; }
        public string File { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime SentDate { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }


        public ApplicationUser User { get; set; }


    }
}