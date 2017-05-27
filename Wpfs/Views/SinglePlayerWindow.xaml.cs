using MazeLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
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
using MazeGeneratorLib;
using MazeLib;
using System.Timers;
using System.Windows.Threading;
using System.Threading;

namespace Wpfs.Views
{
    /// <summary>
    /// Interaction logic for SinglePlayerWindow.xaml
    /// </summary>
    public partial class SinglePlayerWindow : Window
    {
        private SinglePlayerViewModel vm;
        static char c;

        public SinglePlayerWindow(string name, int rows, int cols)
        {
            InitializeComponent();
            vm = new SinglePlayerViewModel(new SinglePlayerModel(name, rows, cols));
            this.DataContext = vm;
        }

     
        private void myCanvasSingle_Loaded(object sender, RoutedEventArgs e)
        {
            myCanvasSingle.Draw(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos, vm.VM_GoalPos);
        }



        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            int curRow = vm.VM_CurPos.Row;
            int curCol = vm.VM_CurPos.Col;

            if (e.Key == Key.Up && curRow > 0 && vm.VM_Maze[curRow - 1, curCol] == CellType.Free)
            {
                vm.VM_CurPos = new Position(vm.VM_CurPos.Row - 1, vm.VM_CurPos.Col);
            }
            else if (e.Key == Key.Down && curRow < vm.VM_MazeRows - 1 && vm.VM_Maze[curRow + 1, curCol] == CellType.Free)
            {
                vm.VM_CurPos = new Position(vm.VM_CurPos.Row + 1, vm.VM_CurPos.Col);
            }
            else if (e.Key == Key.Left && curCol > 0 && vm.VM_Maze[curRow , curCol - 1] == CellType.Free)
            {
                vm.VM_CurPos = new Position(vm.VM_CurPos.Row , vm.VM_CurPos.Col - 1);
            }
            else if (e.Key == Key.Right && curCol < vm.VM_MazeCols - 1 && vm.VM_Maze[curRow, curCol + 1] == CellType.Free)
            {
                vm.VM_CurPos = new Position(vm.VM_CurPos.Row, vm.VM_CurPos.Col + 1);
            }

            myCanvasSingle.Draw(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos, vm.VM_GoalPos);
            if (this.vm.Model.ReachedDestination())
            {
                MessageBoxResult result = MessageBox.Show("You Won!", "Reached destination");
            }

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

        private void restartBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to restart the game", "Restart Game", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    vm.VM_CurPos = vm.VM_InitialPos;
                    myCanvasSingle.Draw(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos, vm.VM_GoalPos);
                    break;

                case MessageBoxResult.No:
                    break;
            }
        }

        private void solveBtn_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            string solution = this.vm.Model.Solve(vm.VM_MazeName, vm.VM_SearchAlgorithm);

            while (!(vm.VM_CurPos.Equals(vm.VM_GoalPos)))
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => {

                    if (i < solution.Length)
                        MoveOneStep(solution[i++]);
                    Thread.Sleep(500);
                }

                ));

            }
        }

        public void MoveOneStep(char direction)
        {         
            int curRow = vm.VM_CurPos.Row;
            int curCol = vm.VM_CurPos.Col;

            if (direction == '2' && curRow > 0 && vm.VM_Maze[curRow - 1, curCol] == CellType.Free)
            {
                vm.VM_CurPos = new Position(vm.VM_CurPos.Row - 1, vm.VM_CurPos.Col);
            }
            else if (direction == '3' && curRow < vm.VM_MazeRows && vm.VM_Maze[curRow + 1, curCol] == CellType.Free)
            {
                vm.VM_CurPos = new Position(vm.VM_CurPos.Row + 1, vm.VM_CurPos.Col);
            }
            else if (direction == '0' && curCol > 0 && vm.VM_Maze[curRow, curCol - 1] == CellType.Free)
            {
                vm.VM_CurPos = new Position(vm.VM_CurPos.Row, vm.VM_CurPos.Col - 1);
            }
            else if (direction ==  '1' && curCol < vm.VM_MazeCols && vm.VM_Maze[curRow, curCol + 1] == CellType.Free)
            {
                vm.VM_CurPos = new Position(vm.VM_CurPos.Row, vm.VM_CurPos.Col + 1);
            }

            myCanvasSingle.Draw(vm.VM_Maze.ToString(), vm.VM_MazeRows, vm.VM_MazeCols, vm.VM_CurPos, vm.VM_GoalPos);
        }

        
    }
}
