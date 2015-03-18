﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageActionService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using System;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Services;
    using NuGet;

    internal class PackageActionService : IPackageActionService
    {
        #region Fields
        private readonly IPackageRepository _localRepository;
        private readonly ILogger _logger;
        private readonly INuGetPackageManager _packageManager;
        private readonly IPackageRepositoryService _packageRepositoryService;
        private readonly IPackageQueryService _packageQueryService;
        private readonly IPleaseWaitService _pleaseWaitService;
        #endregion

        #region Constructors
        public PackageActionService(IPleaseWaitService pleaseWaitService, INuGetPackageManager packageManager,
            IPackageRepositoryService packageRepositoryService, ILogger logger, IPackageQueryService packageQueryService)
        {
            Argument.IsNotNull(() => pleaseWaitService);
            Argument.IsNotNull(() => packageManager);
            Argument.IsNotNull(() => packageRepositoryService);
            Argument.IsNotNull(() => logger);
            Argument.IsNotNull(() => packageQueryService);

            _pleaseWaitService = pleaseWaitService;
            _packageManager = packageManager;
            _packageRepositoryService = packageRepositoryService;
            _logger = logger;
            _packageQueryService = packageQueryService;

            _localRepository = packageRepositoryService.LocalRepository;

            DependencyVersion = DependencyVersion.Lowest;
        }
        #endregion

        #region Properties
        public DependencyVersion DependencyVersion { get; set; }
        #endregion

        #region Methods
        public string GetActionName(PackageOperationType operationType)
        {
            return Enum.GetName(typeof(PackageOperationType), operationType);           
        }

        public async Task Execute(PackageOperationType operationType, PackageDetails packageDetails, IPackageRepository sourceRepository = null, bool allowedPrerelease = false)
        {
            Argument.IsNotNull(() => packageDetails);

            await Task.Factory.StartNew(() =>
            {
                using (_pleaseWaitService.WaitingScope())
                {
                    switch (operationType)
                    {
                        case PackageOperationType.Uninstall:
                            UninstallPackage(packageDetails);
                            break;
                        case PackageOperationType.Install:
                            InstallPackage(packageDetails, allowedPrerelease, sourceRepository);
                            break;
                        case PackageOperationType.Update:
                            UpdatePackages(packageDetails, allowedPrerelease);
                            break;
                    }
                }
            });
        }

        public bool CanExecute(PackageOperationType operationType, PackageDetails packageDetails)
        {
            if (packageDetails == null)
            {
                return false;
            }

            if (operationType == PackageOperationType.Install)
            {
                if (packageDetails.IsActionExecuted == null)
                {
                    var count = _packageQueryService.CountPackages(_localRepository, packageDetails.Id);
                    packageDetails.IsActionExecuted = count != 0;
                    return count == 0;
                }

                return !packageDetails.IsActionExecuted.Value;
            }

            packageDetails.IsActionExecuted = null;
            return true;
        }

        public bool IsRefreshReqired(PackageOperationType operationType)
        {
            switch (operationType)
            {
                case PackageOperationType.Uninstall:
                    return true;
                case PackageOperationType.Install:
                    return false;
                case PackageOperationType.Update:
                    return true;
            }

            return false;
        }

        private void UninstallPackage(PackageDetails packageDetails)
        {
            Argument.IsNotNull(() => packageDetails);

            using (_packageManager.OperationsBatchContext(packageDetails, PackageOperationType.Uninstall))
            {
                var dependentsResolver = new DependentsWalker(_localRepository, null);

                var walker = new UninstallWalker(_localRepository, dependentsResolver, null,
                    _logger, true, false);

                try
                {
                    var operations = walker.ResolveOperations(packageDetails.Package); // checking uninstall ability

                    _packageManager.UninstallPackage(packageDetails.Package, false, true);
                }
                catch (Exception exception)
                {
                    _logger.Log(MessageLevel.Error, exception.Message);
                }
            }
        }

        private void InstallPackage(PackageDetails packageDetails, bool allowedPrerelease, IPackageRepository sourceRepository = null)
        {
            Argument.IsNotNull(() => packageDetails);

            if (sourceRepository == null)
            {
                sourceRepository = _packageRepositoryService.GetSourceAggregateRepository();
            }

            using (_packageManager.OperationsBatchContext(packageDetails, PackageOperationType.Install))
            {
                var walker = new InstallWalker(_localRepository, sourceRepository, null, _logger, false, allowedPrerelease,
                    DependencyVersion);

                try
                {
                    var operations = walker.ResolveOperations(packageDetails.Package); // checking install ability

                    _packageManager.InstallPackage(packageDetails.Package, false, allowedPrerelease, false);
                }
                catch (Exception exception)
                {
                    _logger.Log(MessageLevel.Error, exception.Message);
                }
            }
        }

        private void UpdatePackages(PackageDetails packageDetails, bool allowedPrerelease)
        {
            Argument.IsNotNull(() => packageDetails);

            using (_packageManager.OperationsBatchContext(packageDetails, PackageOperationType.Update))
            {
                try
                {
                    _packageManager.UpdatePackage(packageDetails.Package, true, allowedPrerelease);
                }
                catch (Exception exception)
                {
                    _logger.Log(MessageLevel.Error, exception.Message);
                }
            }
        }
        #endregion
    }
}