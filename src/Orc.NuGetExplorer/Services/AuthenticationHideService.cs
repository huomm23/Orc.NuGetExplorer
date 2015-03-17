﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationHideService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using System;
    using Catel;
    using NuGet;

    public class AuthenticationSilencerService : IAuthenticationSilencerService
    {
        #region Fields
        private readonly ICredentialProvider _credentialProvider;
        #endregion

        #region Constructors
        public AuthenticationSilencerService(ICredentialProvider credentialProvider)
        {
            Argument.IsNotNull(() => credentialProvider);

            _credentialProvider = credentialProvider;
        }
        #endregion

        #region Methods
        public IDisposable UseAuthentication(bool authenticateIfRequired = true)
        {
            var originalCredentialProvider = HttpClient.DefaultCredentialProvider;

            return new DisposableToken<ICredentialProvider>(originalCredentialProvider, token => SetupDefaultCredentialProvider(authenticateIfRequired),
                token => RestoreDefaultCredentialProvider(token.Instance));
        }

        private void RestoreDefaultCredentialProvider(ICredentialProvider originalCredentialProvider)
        {
            HttpClient.DefaultCredentialProvider = originalCredentialProvider;
        }

        private void SetupDefaultCredentialProvider(bool authenticateIfRequired)
        {
            if (!authenticateIfRequired)
            {
                HttpClient.DefaultCredentialProvider = NullCredentialProvider.Instance;
            }
            else
            {
                HttpClient.DefaultCredentialProvider = _credentialProvider;
            }
        }
        #endregion
    }
}