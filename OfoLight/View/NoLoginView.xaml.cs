﻿using OfoLight.ViewModel;
using Windows.UI.Xaml.Controls;

namespace OfoLight.View
{
    /// <summary>
    /// 未登录页面
    /// </summary>
    public sealed partial class NoLoginView : Page
    {
        /// <summary>
        /// 未登录页面
        /// </summary>
        public NoLoginView()
        {
            this.InitializeComponent();
            DataContext = new NoLoginViewModel();
        }
    }
}
