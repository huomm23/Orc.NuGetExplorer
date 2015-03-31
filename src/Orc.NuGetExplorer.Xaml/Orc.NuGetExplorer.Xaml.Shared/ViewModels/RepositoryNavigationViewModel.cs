﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryNavigationViewModel.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer.ViewModels
{
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Fody;
    using Catel.MVVM;

    internal class RepositoryNavigationViewModel : ViewModelBase
    {
        #region Constructors
        public RepositoryNavigationViewModel(IRepositoryNavigatorService repositoryNavigatorService)
        {
            Argument.IsNotNull(() => repositoryNavigatorService);

            Navigator = repositoryNavigatorService.Navigator;
        }
        #endregion

        #region Properties
        [Model]
        [Expose("RepoCategories")]
        [Expose("SelectedRepository")]
        public RepositoryNavigator Navigator { get; private set; }
        #endregion

        protected override async Task Initialize()
        {
            await base.Initialize();

            Navigator.SelectedRepositoryCategory = Navigator.RepoCategories.FirstOrDefault();
            var selectedRepositoryCategory = Navigator.SelectedRepositoryCategory;
            if (selectedRepositoryCategory != null)
            {
                selectedRepositoryCategory.IsSelected = true;
                Navigator.SelectedRepository = selectedRepositoryCategory.Repositories.FirstOrDefault();
            }
        }
    }
}