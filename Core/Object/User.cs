
namespace Core.Object
{
    public class LoginResult
    {
        public MiniProfile User { get; set; }
        public string DeviceID { get; set; }
        public string Token { get; set; }
    }
    public class MiniProfile
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

    }


}
