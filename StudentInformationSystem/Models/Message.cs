using System;
using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string ReceiverIdentityNumber { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public string Sender { get; set; }
        public DateTime SentAt { get; set; }
    }

}