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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpfs.Views;
using ServerMaze;
using System.Configuration;

namespace Wpfs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Background3.png")));
        }

        private void Click_Settings(object sender, RoutedEventArgs e)
        {
            SettingsWindow sWin = new SettingsWindow();
            sWin.Show();
            this.Close();
        }

       
        private void Click_SP(object sender, RoutedEventArgs e)
        {
            SPMenuWindow spm = new SPMenuWindow();
            spm.Show();
            this.Hide();
        }

        private void Click_MP(object sender, RoutedEventArgs e)
        {
            MPMenuWindow mpm = new MPMenuWindow();
            mpm.Show();
            this.Hide();
        }
    }
}
