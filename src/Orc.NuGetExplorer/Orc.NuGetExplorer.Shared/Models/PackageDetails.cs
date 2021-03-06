﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageDetails.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using System;
    using System.Collections.Generic;
    using Catel;
    using Catel.Data;
    using NuGet;

    internal class PackageDetails : ModelBase, IPackageDetails
    {
        #region Constructors
        internal PackageDetails(IPackage package)
        {
            Argument.IsNotNull(() => package);

            Package = package;
            Version = package.Version.Version;
            Id = package.Id;
            Title = string.IsNullOrWhiteSpace(package.Title) ? package.Id : package.Title;
            FullName = package.GetFullName();
            Description = package.Description;
            IconUrl = package.IconUrl;

            Published = package.Published == null ? (DateTime?) null : package.Published.Value.LocalDateTime;
            SpecialVersion = package.Version.SpecialVersion;
            IsAbsoluteLatestVersion = package.IsAbsoluteLatestVersion;

            IsPrerelease = !string.IsNullOrWhiteSpace(SpecialVersion);
        }
        #endregion

        #region Properties
        public string Id { get; private set; }
        public string Title { get; private set; }

        public IEnumerable<string> Authors
        {
            get { return Package.Authors; }
        }

        DateTimeOffset? IPackageDetails.Published
        {
            get
            {
                var dataServicePackage = Package as DataServicePackage;
                return dataServicePackage == null ? ((PackageDetails)this).Published : dataServicePackage.Published;
            }
        }

        public int? DownloadCount
        {
            get
            {
                var dataServicePackage = Package as DataServicePackage;
                return dataServicePackage == null ? null : (int?) dataServicePackage.DownloadCount;
            }
        }

        public string Dependencies
        {
            get
            {
                var dataServicePackage = Package as DataServicePackage;
                return dataServicePackage == null ? null : dataServicePackage.Dependencies;
            }
        }

        public bool? IsInstalled { get; set; }
        public string FullName { get; private set; }
        public string Description { get; private set; }
        public Uri IconUrl { get; private set; }
        internal IPackage Package { get; private set; }
        public DateTime? Published { get; private set; }
        public Version Version { get; private set; }
        public string SpecialVersion { get; private set; }
        public bool IsAbsoluteLatestVersion { get; private set; }
        public bool IsLatestVersion { get; private set; }
        public bool IsPrerelease { get; private set; }
        #endregion
    }
}