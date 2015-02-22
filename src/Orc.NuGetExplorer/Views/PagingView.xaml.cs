﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PagingView.xaml.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer.Views
{
    using System.Windows;
    using Catel.MVVM.Views;

    /// <summary>
    /// Interaction logic for PagingView.xaml
    /// </summary>
    public partial class PagingView
    {
        #region Fields
        public static readonly DependencyProperty VisiblePagesProperty = DependencyProperty.Register("VisiblePages",
            typeof (int), typeof (PagingView),
            new FrameworkPropertyMetadata(5, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty ItemsCountProperty = DependencyProperty.Register("ItemsCount",
           typeof(int), typeof(PagingView),
           new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty ItemsPerPageProperty = DependencyProperty.Register("ItemsPerPage",
           typeof(int), typeof(PagingView),
           new FrameworkPropertyMetadata(10, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty ItemIndexProperty = DependencyProperty.Register("ItemIndex",
           typeof(int), typeof(PagingView),
           new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion

        #region Constructors
        public PagingView()
        {
            InitializeComponent();
        }

        static PagingView()
        {
            typeof (PagingView).AutoDetectViewPropertiesToSubscribe();
        }
        #endregion

        #region Properties
        [ViewToViewModel(MappingType = ViewToViewModelMappingType.ViewToViewModel)]
        public int VisiblePages
        {
            get { return (int)GetValue(VisiblePagesProperty); }
            set { SetValue(VisiblePagesProperty, value); }
        }

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.ViewToViewModel)]
        public int ItemsCount
        {
            get { return (int)GetValue(ItemsCountProperty); }
            set { SetValue(ItemsCountProperty, value); }
        }

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.ViewToViewModel)]
        public int ItemsPerPage
        {
            get { return (int)GetValue(ItemsPerPageProperty); }
            set { SetValue(ItemsPerPageProperty, value); }
        }

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.ViewToViewModel)]
        public int ItemIndex
        {
            get { return (int)GetValue(ItemIndexProperty); }
            set { SetValue(ItemIndexProperty, value); }
        }
        #endregion
    }
}