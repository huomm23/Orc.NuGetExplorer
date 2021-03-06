﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageManagerLogListenerBase.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using Catel;

    public abstract class PackageManagerLogListenerBase
    {
        #region Fields
        private readonly INuGetLogListeningSevice _nuGetLogListeningSevice;
        #endregion

        #region Constructors
        public PackageManagerLogListenerBase(INuGetLogListeningSevice nuGetLogListeningSevice)
        {
            Argument.IsNotNull(() => nuGetLogListeningSevice);

            _nuGetLogListeningSevice = nuGetLogListeningSevice;

            nuGetLogListeningSevice.Error += OnError;
            nuGetLogListeningSevice.Info += OnInfo;
            nuGetLogListeningSevice.Debug += OnDebug;
            nuGetLogListeningSevice.Warning += OnWarning;
        }
        #endregion

        #region Methods
        protected virtual void OnWarning(object sender, NuGetLogRecordEventArgs e)
        {
        }

        protected virtual void OnDebug(object sender, NuGetLogRecordEventArgs e)
        {
        }

        protected virtual void OnInfo(object sender, NuGetLogRecordEventArgs e)
        {
        }

        protected virtual void OnError(object sender, NuGetLogRecordEventArgs e)
        {
        }
        #endregion
    }
}