﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageRepositoryExtensions.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Catel;
    using MethodTimer;
    using NuGet;

    internal static class PackageRepositoryExtensions
    {
        #region Methods
        public static async Task<IEnumerable<IPackage>> FindAllAsync(this IPackageRepository packageRepository, bool allowPrereleaseVersions,
            int skip = 0, int take = 10)
        {
            return await Task.Factory.StartNew(() => FindAll(packageRepository, allowPrereleaseVersions, skip, take));
        }

        [Time]
        public static IEnumerable<IPackage> FindAll(this IPackageRepository packageRepository, bool allowPrereleaseVersions,
            int skip = 0, int take = 10)
        {
            Argument.IsNotNull(() => packageRepository);

            return packageRepository.FindFiltered(string.Empty, allowPrereleaseVersions, skip, take);
        }

        public static async Task<IEnumerable<IPackage>> FindFilteredAsync(this IPackageRepository packageRepository, string filter,
            bool allowPrereleaseVersions, int skip = 0, int take = 10)
        {
            return await Task.Factory.StartNew(() => FindFiltered(packageRepository, filter, allowPrereleaseVersions, skip, take));
        }

        [Time]
        public static IEnumerable<IPackage> FindFiltered(this IPackageRepository packageRepository, string filter, bool allowPrereleaseVersions,
            int skip = 0, int take = 10)
        {
            Argument.IsNotNull(() => packageRepository);

            var queryable = BuildQueryForSingleVersion(packageRepository, filter, allowPrereleaseVersions);
            return queryable.OrderByDescending(x => x.DownloadCount).Skip(skip).Take(take).ToList();
        }

        public static async Task<int> CountPackagesAsync(this IPackageRepository packageRepository, string filter, bool allowPrereleaseVersions)
        {
            return await Task.Factory.StartNew(() => CountPackages(packageRepository, filter, allowPrereleaseVersions));
        }

        [Time]
        public static int CountPackages(this IPackageRepository packageRepository, string filter, bool allowPrereleaseVersions)
        {
            Argument.IsNotNull(() => packageRepository);

            try
            {
                var queryable = BuildQueryForSingleVersion(packageRepository, filter, allowPrereleaseVersions);
                var count = queryable.Count();
                return count;
            }
            catch
            {
                return 0;
            }

        }

        public static async Task<IEnumerable<IPackage>> FindPackageVersionsAsync(this IPackageRepository packageRepository, IPackage package, bool allowPrereleaseVersions, int skip, int minimalTake = 10)
        {
            return await Task.Factory.StartNew(() => FindPackageVersions(packageRepository, package, allowPrereleaseVersions, ref skip, minimalTake));
        }

        [Time]
        public static IEnumerable<IPackage> FindPackageVersions(this IPackageRepository packageRepository, IPackage package, bool allowPrereleaseVersions,
            ref int skip, int minimalTake = 10)
        {
            Argument.IsNotNull(() => packageRepository);

            if (skip < 0)
            {
                return Enumerable.Empty<IPackage>();
            }

            var queryable = packageRepository.GetPackages().Where(x => Equals(x.Id, package.Id)).Skip(skip).Take(minimalTake);

            var result = new List<IPackage>(queryable.ToList());

            if (result.Count < minimalTake)
            {
                skip = -1;
            }
            else
            {
                skip += minimalTake;
            }

            if (!allowPrereleaseVersions && result.Any())
            {
                result = result.Where(x => x.IsReleaseVersion()).ToList();

                var count = result.Count;

                if (skip >= 0 && count < minimalTake)
                {
                    var additional = packageRepository.FindPackageVersions(package, false, ref skip, minimalTake).ToList();
                    result.AddRange(additional);
                }
            }

            return result;
        }

        private static IQueryable<IPackage> BuildQueryForSingleVersion(IPackageRepository packageRepository, string filter, bool allowPrereleaseVersions)
        {
            Argument.IsNotNull(() => packageRepository);

            var queryable = packageRepository.GetPackages();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.Trim();
                queryable = queryable.Where(x => x.Title.Contains(filter));
            }

            if (allowPrereleaseVersions)
            {
                queryable = queryable.Where(x => x.IsAbsoluteLatestVersion);
            }
            else
            {
                queryable = queryable.Where(x => x.IsLatestVersion);
            }

            return queryable;
        }
        #endregion
    }
}