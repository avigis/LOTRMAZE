using ClientMaze;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpfs.Models;
using Wpfs.Views;
using MazeGeneratorLib;
using MazeLib;

namespace Wpfs.ViewModels
{
    public class SinglePlayerViewModel : ViewModel
    {
        private SinglePlayerModel model;
        private string name;
        private int rows;
        private int cols;
        private Position curPos;
        
        public SinglePlayerViewModel(SinglePlayerModel model)
        {
            this.model = model;
            this.model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_"+e.PropertyName);
                };
        }

        public SinglePlayerModel Model {
            get { return model; }
            set { }
        }

        public string VM_MazeName
        {
            get { return this.model.MazeName; }
            set
            {
                this.model.MazeName = value;
                NotifyPropertyChanged("Maze");
            }
        }

        public int VM_MazeRows
        {
            get { return this.model.MazeRows; }
            set
            {
                this.model.MazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }

        public int VM_MazeCols
        {
            get { return this.model.MazeCols; }
            set
            {
                this.model.MazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }

        public int VM_SearchAlgorithm
        {
            get { return this.model.SearchAlgorithm; }
            set
            {
                this.model.SearchAlgorithm = value;
                NotifyPropertyChanged("SearchAlgorithm");
            }
        }

        public string Maze {
            get { return this.model.Maze.ToJSON(); }
            set { /*this.Maze = value;*/ }
        }

        public Position VM_InitialPos
        {
            get { return this.model.InitialPos; }
            set
            {
                this.model.InitialPos = value;
                NotifyPropertyChanged("InitialPos");
            }
        }

        public Position VM_GoalPos
        {
            get { return this.model.GoalPos; }
            set
            {
                this.model.GoalPos = value;
                NotifyPropertyChanged("GoalPos");
            }
        }

        public Position VM_CurPos
        {
            get { return this.model.CurPos; }
            set
            {
                this.model.CurPos = value;
                NotifyPropertyChanged("CurPos");
            }
        }

        public Maze VM_Maze
        {
            get { return this.model.Maze; }
            set
            {
                this.model.Maze = value;
                NotifyPropertyChanged("Maze");
            }
        }

        public void SaveSettings()
        {
            model.SaveSettings();
        }

    }
}
