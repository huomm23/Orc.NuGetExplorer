﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPackageManagerListeningService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using System;

    public interface IPackageManagerListeningService
    {
        #region Methods
        event EventHandler<NuGetPackageOperationEventArgs> PackageInstalling;
        event EventHandler<NuGetPackageOperationEventArgs> PackageInstalled;
        event EventHandler<NuGetPackageOperationEventArgs> PackageUninstalled;
        event EventHandler<NuGetPackageOperationEventArgs> PackageUninstalling;
        #endregion
    }
}