using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ComponentModel;
using System.Threading.Tasks;
using Core.Contract;
using Core.Object;

namespace Video.Areas.API
{
    [ServiceContract]
    public partial interface IUserAPI
    {
        [OperationContractAttribute(AsyncPattern = true)]
        [WebInvoke(UriTemplate = "{format}/user/login",
               Method = "POST",
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare),
        Description("User Login")]
        IAsyncResult BeginLogin(string format, LoginContract user, AsyncCallback callback, object asyncState);
        ObjectResult<LoginResult> EndLogin(IAsyncResult result);

        [OperationContractAttribute(AsyncPattern = true)]
        [WebInvoke(UriTemplate = "{format}/user/logout",
               Method = "POST",
               ResponseFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Bare),
        Description("User Login")]
        IAsyncResult BeginLogout(string format, LogoutContract user, AsyncCallback callback, object asyncState);
        ServiceResult EndLogout(IAsyncResult result);


    }
}
