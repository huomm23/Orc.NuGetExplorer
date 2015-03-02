﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepoCategory.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public class RepoCategory
    {
        #region Fields
        private int _selectedIndex;
        #endregion

        #region Constructors
        public RepoCategory(RepoCategoryType category)
        {
            Name = Enum.GetName(typeof (RepoCategoryType), category);
            Repos = new ObservableCollection<NamedRepo>();
        }
        #endregion

        #region Properties
        [DefaultValue(false)]
        public bool IsSelected { get; set; }

        public string Name { get; set; }
        public IList<NamedRepo> Repos { get; private set; }
        #endregion
    }
}