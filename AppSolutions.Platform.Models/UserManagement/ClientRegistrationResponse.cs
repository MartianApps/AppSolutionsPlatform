using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.UserManagement
{
    [DataContract]
    public class ClientRegistrationResponse
    {
        [DataMember]
        public ClientRegistrationResponseStatus State { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public string RegisteredClientId { get; set; }

        [DataMember]
        public string Email { get; set; }

        public static ClientRegistrationResponse Success(string registeredClientId, string email)
        {
            return new ClientRegistrationResponse
            {
                State = ClientRegistrationResponseStatus.AccountCreatedAndMailSent,
                RegisteredClientId = registeredClientId,
                Email = email
            };
        }

        public static ClientRegistrationResponse Error(Exception e)
        {
            return new ClientRegistrationResponse
            {
                State = ClientRegistrationResponseStatus.Error,
                ErrorMessage = e.Message
            };
        }
    }

    public enum ClientRegistrationResponseStatus
    {
        AccountCreatedAndMailSent,
        Error
    }
}
