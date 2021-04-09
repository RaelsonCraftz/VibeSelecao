using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Vibe.Mobile.Services.Shared
{
    public interface IApplicationService
    {
        string AccessToken { get; }

        string UserEmail { get; }

        void SetToken(string token);

        void SetEmail(string email);
    }

    public class ApplicationService : IApplicationService
    {
        public string AccessToken { get; private set; }

        public string UserEmail { get; private set; }

        public ApplicationService()
        {
            
        }

        public void SetToken(string token)
        {
            AccessToken = token;
            Preferences.Set(nameof(AccessToken), token);
        }

        public void SetEmail(string email)
        {
            UserEmail = email;
        }
    }
}
