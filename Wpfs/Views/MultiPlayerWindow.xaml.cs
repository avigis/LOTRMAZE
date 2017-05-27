using MazeLib;
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
using Wpfs.ViewModels;

namespace Wpfs.Views
{
    /// <summary>
    /// Interaction logic for MultiPlayerWindow.xaml
    /// </summary>
    public partial class MultiPlayerWindow : Window
    {
        private MultiPlayerViewModel vm;
        public MultiPlayerWindow(MultiPlayerViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            this.DataContext = vm;
        }

        private void MainBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to leave?", "Main menu", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MainWindow mainWin = new MainWindow();
                    mainWin.Show();
                    this.Hide();
                    break;

                case MessageBoxResult.No:
                    break;
            }
        }

        private void MyCanvasMulti_Loaded(object sender, RoutedEventArgs e)
        {
            myCanvasPlayer1.DrawMulti(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos1, vm.VM_GoalPos, 1);
            myCanvasPlayer2.DrawMulti(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos2, vm.VM_GoalPos, 2);
        }

        //private void OnKeyDownHandler(object sender, KeyEventArgs e)
        //{
        //    int curRow = vm.VM_CurPos1.Row;
        //    int curCol = vm.VM_CurPos1.Col;

        //    if (e.Key == Key.Up && curRow > 0 && vm.VM_Maze[curRow - 1, curCol] == CellType.Free)
        //    {
        //        vm.VM_CurPos1 = new Position(vm.VM_CurPos1.Row - 1, vm.VM_CurPos1.Col);
        //    }
        //    else if (e.Key == Key.Down && curRow < vm.VM_MazeRows - 1 && vm.VM_Maze[curRow + 1, curCol] == CellType.Free)
        //    {
        //        vm.VM_CurPos1 = new Position(vm.VM_CurPos1.Row + 1, vm.VM_CurPos1.Col);
        //    }
        //    else if (e.Key == Key.Left && curCol > 0 && vm.VM_Maze[curRow, curCol - 1] == CellType.Free)
        //    {
        //        vm.VM_CurPos1 = new Position(vm.VM_CurPos1.Row, vm.VM_CurPos1.Col - 1);
        //    }
        //    else if (e.Key == Key.Right && curCol < vm.VM_MazeCols - 1 && vm.VM_Maze[curRow, curCol + 1] == CellType.Free)
        //    {
        //        vm.VM_CurPos1 = new Position(vm.VM_CurPos1.Row, vm.VM_CurPos1.Col + 1);
        //    }

        //    myCanvasPlayer1.DrawMulti(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos1, vm.VM_GoalPos, 1);
        //    myCanvasPlayer2.DrawMulti(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos2, vm.VM_GoalPos, 2);
        //    //if (this.vm.Model.ReachedDestination())
        //    //{
        //    //    MessageBoxResult result = MessageBox.Show("You Won!", "Reached destination");
        //    //}

        //}

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            int curRow = vm.VM_CurPos1.Row;
            int curCol = vm.VM_CurPos1.Col;

            if (e.Key == Key.Up && curRow > 0 && vm.VM_Maze[curRow - 1, curCol] == CellType.Free)
            {
                vm.Model.MoveUp();
            }
            else if (e.Key == Key.Down && curRow < vm.VM_MazeRows - 1 && vm.VM_Maze[curRow + 1, curCol] == CellType.Free)
            {
                vm.Model.MoveDown();
            }
            else if (e.Key == Key.Left && curCol > 0 && vm.VM_Maze[curRow, curCol - 1] == CellType.Free)
            {
                vm.Model.MoveLeft();
            }
            else if (e.Key == Key.Right && curCol < vm.VM_MazeCols - 1 && vm.VM_Maze[curRow, curCol + 1] == CellType.Free)
            {
                vm.Model.MoveRight();
            }

            myCanvasPlayer1.DrawMulti(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos1, vm.VM_GoalPos, 1);
            myCanvasPlayer2.DrawMulti(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos2, vm.VM_GoalPos, 2);
            //if (this.vm.Model.ReachedDestination())
            //{
            //    MessageBoxResult result = MessageBox.Show("You Won!", "Reached destination");
            //}

        }
    }
}
