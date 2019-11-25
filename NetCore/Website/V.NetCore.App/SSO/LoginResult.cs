using V.NetCore.Infrastructure;

namespace V.NetCore.App.SSO
{
    public class LoginResult : Response<string>
    {
        public string ReturnUrl;
        public string Token;
    }
}
