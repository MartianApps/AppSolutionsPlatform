using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.MailManagement
{
    [DataContract]
    public class MailServiceOperationResponse
    {
        [DataMember]
        public MailServiceOperationResult Result { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string StackTrace { get; set; }

        public static MailServiceOperationResponse CreateSuccess()
        {
            return new MailServiceOperationResponse
            {
                Result = MailServiceOperationResult.Success
            };
        }

        public static MailServiceOperationResponse CreateInternalServerError(Exception e)
        {
            return new MailServiceOperationResponse
            {
                Result = MailServiceOperationResult.Error,
                Message = e.Message,
                StackTrace = e.StackTrace
            };
        }
    }

    public enum MailServiceOperationResult
    {
        Success,
        Error
    }
}
