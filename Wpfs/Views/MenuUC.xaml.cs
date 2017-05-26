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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpfs.Views
{
    /// <summary>
    /// Interaction logic for MenuUC.xaml
    /// </summary>
    public partial class MenuUC : UserControl
    {
        public MenuUC()
        {
            InitializeComponent();
        }



        public string MenuMazeName
        {
            get { return (string)GetValue(MenuMazeNameProperty); }
            set { SetValue(MenuMazeNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuMazeNameProperty =
            DependencyProperty.Register("MenuMazeName", typeof(string), typeof(MenuUC)/*, new PropertyMetadata(0)*/);


        public int MenuMazeRows
        {
            get { return (int)GetValue(MenuMazeRowsProperty); }
            set { SetValue(MenuMazeRowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeRows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuMazeRowsProperty =
            DependencyProperty.Register("MenuMazeRows", typeof(int), typeof(MenuUC)/*, new PropertyMetadata(0)*/);



        public int MenuMazeCols
        {
            get { return (int)GetValue(MenuMazeColsProperty); }
            set { SetValue(MenuMazeColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeCols.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuMazeColsProperty =
            DependencyProperty.Register("MenuMazeCols", typeof(int), typeof(MenuUC)/*, new PropertyMetadata(0)*/);


    }
}
