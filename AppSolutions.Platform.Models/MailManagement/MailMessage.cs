using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.MailManagement
{
    [DataContract]
    public class MailMessage
    {
        [DataMember]
        public Guid EmailMessageId { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public DateTime? SentDate { get; set; }

        //
        // Summary:
        //     Gets or sets a value indicating whether the mail message body is in Html.
        //
        // Returns:
        //     true if the message body is in Html; else false. The default is false.
        [DataMember]
        public bool IsBodyHtml { get; set; }

        //
        // Summary:
        //     Gets or sets the message body.
        //
        // Returns:
        //     A System.String value that contains the body text.
        [DataMember]
        public string Body { get; set; }

        //
        // Summary:
        //     Gets or sets the subject line for this e-mail message.
        //
        // Returns:
        //     A System.String that contains the subject content.
        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public ICollection<MailAddress> CC { get; set; }

        [DataMember]
        public ICollection<MailAddress> Bcc { get; set; }


        [DataMember]
        public ICollection<MailAddress> To { get; set; }

        [DataMember]
        public MailAddress From { get; set; }

        [DataMember]
        public string CreationUser { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public string UpdateUser { get; set; }

        [DataMember]
        public DateTime? UpdateDate { get; set; }
    }
}
