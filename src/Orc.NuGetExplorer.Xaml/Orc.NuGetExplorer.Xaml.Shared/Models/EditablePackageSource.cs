﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditablePackageSource.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
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

        [DefaultValue(FeedVerificationResult.Unknown)]
        public FeedVerificationResult FeedVerificationResult { get; set; }

        public bool? IsValid
        {
            get
            {
                if (IsValidName == null || IsValidSource == null)
                {
                    return null;
                }

                return IsValidName.Value && IsValidSource.Value;
            }
            set
            {
                IsValidName = value;
                IsValidSource = value;
            }
        }

        [DefaultValue(true)]
        public bool? IsValidName { get; set; }

        [DefaultValue(true)]
        public bool? IsValidSource { get; set; }
        #endregion
    }
}