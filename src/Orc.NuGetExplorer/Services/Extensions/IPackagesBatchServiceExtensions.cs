﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPackagesBatchServiceExtensions.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catel;

    public static class IPackagesBatchServiceExtensions
    {
        #region Methods
        public static async Task ShowPackagesBatchAsync(this IPackageBatchService packageBatchService, IEnumerable<IPackageDetails> packageDetails, PackageOperationType operationType)
        {
            Argument.IsNotNull(() => packageBatchService);

            await Task.Factory.StartNew(() => packageBatchService.ShowPackagesBatch(packageDetails, operationType));
        }
        #endregion
    }
}