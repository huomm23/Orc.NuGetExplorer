// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IImageResolveService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using System;
    using System.Windows.Media;

    public interface IImageResolveService
    {
        #region Methods
        ImageSource ResolveImageFromUri(Uri uri, string defaultUrl = null);
        #endregion
    }
}