﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageManagerWatcherBase.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using Catel;

    public abstract class PackageManagerWatcherBase
    {
        #region Constructors
        public PackageManagerWatcherBase(IPackageOperationNotificationService packageOperationNotificationService)
        {
            Argument.IsNotNull(() => packageOperationNotificationService);

            packageOperationNotificationService.OperationStarting += OnOperationStarting;
            packageOperationNotificationService.OperationFinished += OnOperationFinished;
            packageOperationNotificationService.OperationsBatchStarting += OnOperationsBatchStarting;
            packageOperationNotificationService.OperationsBatchFinished += OnOperationsBatchFinished;
        }
        #endregion

        #region Methods
        protected virtual void OnOperationsBatchFinished(object sender, PackageOperationBatchEventArgs e)
        {
        }

        protected virtual void OnOperationsBatchStarting(object sender, PackageOperationBatchEventArgs e)
        {
        }

        protected virtual void OnOperationFinished(object sender, PackageOperationEventArgs e)
        {
        }

        protected virtual void OnOperationStarting(object sender, PackageOperationEventArgs e)
        {
        }
        #endregion
    }
}