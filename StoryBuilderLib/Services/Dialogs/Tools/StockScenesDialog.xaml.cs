﻿using Microsoft.Toolkit.Mvvm.DependencyInjection;
using StoryBuilder.ViewModels.Tools;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StoryBuilder.Services.Dialogs.Tools
{
    public sealed partial class StockScenesDialog
    {
        public StockScenesViewModel StockScenesVm => Ioc.Default.GetService<StockScenesViewModel>();

        public StockScenesDialog()
        {
            InitializeComponent();
            DataContext = StockScenesVm;
        }
    }
}
