﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditablePackageSource.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using System.ComponentModel;
    using Catel.Data;

    internal class EditablePackageSource : ModelBase
    {
        #region Properties
        public bool IsEnabled { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public string PreviousSourceValue { get; set; }
        [DefaultValue(true)]
        public bool? IsValid { get; set; }
        #endregion
    }
}