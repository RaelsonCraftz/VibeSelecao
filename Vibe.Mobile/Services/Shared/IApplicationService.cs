using System;
using System.Collections.Generic;
using System.Text;
using Vibe.Domain.Model;
using Vibe.Mobile.Core;
using Xamarin.Essentials;

namespace Vibe.Mobile.Services.Shared
{
    public interface IApplicationService
    {
        CredenciaisUsuario Usuario { get; }

        string AccessToken { get; }

        void SetCacheUsuario(string accessToken, CredenciaisUsuario usuario);
    }

    public class ApplicationService : IApplicationService
    {
        public CredenciaisUsuario Usuario { get; private set; }

        public string AccessToken { get; private set; }

        public void SetCacheUsuario(string accessToken, CredenciaisUsuario usuario)
        {
            Usuario = usuario;
            AccessToken = accessToken;

            Preferences.Set(PreferenceKeys.AccessToken, accessToken);
            Preferences.Set(PreferenceKeys.UsuarioCpf, usuario.Cpf);
            Preferences.Set(PreferenceKeys.UsuarioSenhaHash, usuario.SenhaHash);
        }
    }
}
