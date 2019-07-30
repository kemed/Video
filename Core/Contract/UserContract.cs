using System.Runtime.Serialization;

namespace Core.Contract
{
    [DataContract]
    public class UserEditorContract
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string CreateDate { get; set; }
        [DataMember]
        public string UpdateDate { get; set; }

    }
    [DataContract]
    public class AddUserContract : UserEditorContract
    {
        [DataMember]
        public string UserID { get; set; }
    }
    public class UpdateUserContract : UserEditorContract
    {
        [DataMember]
        public string UserID { get; set; }
    }
    [DataContract]
    public class LoginContract
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string DeviceID { get; set; }
    }
    [DataContract]
    public class LogoutContract
    {
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public string DeviceID { get; set; }
    }


}
