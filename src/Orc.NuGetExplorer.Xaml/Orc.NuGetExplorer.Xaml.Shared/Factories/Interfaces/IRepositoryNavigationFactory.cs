// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepositoryNavigationFactory.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    internal interface IRepositoryNavigationFactory
    {
        #region Methods
        RepositoryNavigator CreateRepoNavigator();
        #endregion
    }
}