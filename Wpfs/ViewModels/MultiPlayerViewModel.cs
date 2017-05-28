using MazeLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpfs.Models;

namespace Wpfs.ViewModels
{
    public class MultiPlayerViewModel : ViewModel
    {
        private MultiPlayerModel model;

        public MultiPlayerViewModel(MultiPlayerModel model)
        {
            this.model = model;
            this.model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }



        public MultiPlayerModel Model
        {
            get { return model; }
            set { }
        }

        public MyTelnetClient VM_Client1
        {
            get { return this.model.Client1; }
            set
            {
                this.model.Client1 = value;
                NotifyPropertyChanged("Client1");
            }
        }

        //public MyTelnetClient VM_Client2
        //{
        //    get { return this.model.Client2; }
        //    set
        //    {
        //        this.model.Client2 = value;
        //        NotifyPropertyChanged("Client2");
        //    }
        //}

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

        public Position VM_CurPos1
        {
            get { return this.model.CurPos1; }
            set
            {
                this.model.CurPos1 = value;
                NotifyPropertyChanged("CurPos1");
            }
        }

        public Position VM_CurPos2
        {
            get { return this.model.CurPos2; }
            set
            {
                this.model.CurPos2 = value;
                NotifyPropertyChanged("CurPos2");
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

        public ObservableCollection<string> VM_GamesList {
            get { return this.model.GamesList; }
            set
            {
                this.model.GamesList = value;
                NotifyPropertyChanged("GamesList");
            }
        }

        public bool VM_SecondClientClosed {
            get { return this.model.SecondClientClosed; }
            set {
                this.model.SecondClientClosed = value;
                NotifyPropertyChanged("SecondClientClosed");
            }
        }

    }
}
