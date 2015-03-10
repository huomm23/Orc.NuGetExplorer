﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDefaultPackageSourcesProvider.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using System.Collections.Generic;

    public interface IDefaultPackageSourcesProvider
    {
        #region Methods
        IEnumerable<IPackageSource> GetDefaultPackages();
        #endregion
    }
}