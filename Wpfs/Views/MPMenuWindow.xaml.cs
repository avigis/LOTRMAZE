using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MPMenuWindow.xaml
    /// </summary>
    public partial class MPMenuWindow : Window
    {
        private MultiPlayerViewModel vm;

        public MPMenuWindow()
        {
            InitializeComponent();
            this.mazeName = "Maze Name";
            this.mazeRows = Properties.Settings.Default.MazeRows;
            this.mazeCols = Properties.Settings.Default.MazeCols;
            waitLabel.Visibility = Visibility.Hidden;
            vm = new MultiPlayerViewModel(new MultiPlayerModel());
            this.DataContext = vm;
        }

        private string mazeName;
        public string MazeName
        {
            get { return vm.VM_MazeName; }
            set { mazeName = value; }
        }

        private int mazeRows;
        public int MazeRows
        {
            get { return vm.VM_MazeRows; }
            set { mazeRows = value; }
        }

        private int mazeCols;
        public int MazeCols
        {
            get { return vm.VM_MazeCols; }
            set { mazeCols = value; }
        }

        private ObservableCollection<string> gamesList;
        public ObservableCollection<string> GamesList {
            get { return vm.VM_GamesList; }
            set { gamesList = value; }
        }

        private void Join_Btn_Click(object sender, RoutedEventArgs e)
        {
            vm.Model.Join(cboGames.SelectedValue.ToString());
            MultiPlayerWindow mp = new MultiPlayerWindow(vm);
            mp.Show();
            this.Hide();
        }

        private void Start_Btn_Click(object sender, RoutedEventArgs e)
        {
            waitLabel.Visibility = Visibility.Visible;
            vm.Model.Start();
            //////////////////////////////////////////////////
            MultiPlayerWindow mp = new MultiPlayerWindow(vm);
            mp.Show();
            this.Hide();
        }

    }
}
