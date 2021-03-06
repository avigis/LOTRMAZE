﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpfs.Models;
using Wpfs.ViewModels;

namespace Wpfs.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SettingsViewModel vm;

        public SettingsWindow()
        {
            ISettingsModel model = new ApplicationSettingsModel();
            InitializeComponent();
            vm = new SettingsViewModel(model);
            this.DataContext = vm;
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveSettings();
            MainWindow win = new MainWindow(); //(MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow(); //(MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
    }
}
