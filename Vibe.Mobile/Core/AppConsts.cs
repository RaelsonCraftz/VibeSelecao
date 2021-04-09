using System;
using System.Collections.Generic;
using System.Text;

namespace Vibe.Mobile.Core
{
    public class AppConsts
    {
#if DEBUG
        public const string RemoteApiUrl = "https://vibeselecao.azurewebsites.net/api";
#else
        public const string RemoteApiUrl = "https://vibeselecao.azurewebsites.net/api";
#endif
    }
}
