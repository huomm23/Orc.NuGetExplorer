﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INuGetConfigurationService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using System;
    using System.Collections.Generic;

    public interface INuGetConfigurationService
    {
        #region Methods
        string GetDestinationFolder();
        void SetDestinationFolder(string value);
        IEnumerable<IPackageSource> LoadPackageSources(bool onlyEnabled = false);

        [ObsoleteEx(ReplacementTypeOrMember = "SavePackageSource(string, string, bool, bool, bool)", TreatAsErrorFromVersion = "1.0", RemoveInVersion = "2.0")]
        bool SavePackageSource(string name, string source, bool isEnabled = true, bool isOfficial = true);
        bool SavePackageSource(string name, string source, bool isEnabled = true, bool isOfficial = true, bool verifyFeed = true);

        [Obsolete("Use DisablePackageSource")]
        void DeletePackageSource(string name, string source);
        void DisablePackageSource(string name, string source);
        void SavePackageSources(IEnumerable<IPackageSource> packageSources);
        void SetIsPrereleaseAllowed(IRepository repository, bool value);
        bool GetIsPrereleaseAllowed(IRepository repository);
        #endregion
    }
}