using System;
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
    /// Interaction logic for SPMenuWindow.xaml
    /// </summary>
    public partial class SPMenuWindow : Window
    {
        //SinglePlayerViewModel vm;
        public SPMenuWindow()
        {
            InitializeComponent();
            this.mazeName = "Maze Name";
            this.mazeRows = Properties.Settings.Default.MazeRows;
            this.mazeCols = Properties.Settings.Default.MazeCols;
            this.DataContext = this;
        }

        private string mazeName;
        public string MazeName
        {
            get { return mazeName; }
            set { mazeName = value; }
        }

        private int mazeRows;
        public int MazeRows
        {
            get { return mazeRows; }
            set { mazeRows = value; }
        }

        private int mazeCols;
        public int MazeCols
        {
            get { return mazeCols; }
            set { mazeCols = value; }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            SinglePlayerWindow sp = new SinglePlayerWindow(mazeName, mazeRows, mazeCols);
            sp.Show();
            this.Hide();
        }
    }
}
