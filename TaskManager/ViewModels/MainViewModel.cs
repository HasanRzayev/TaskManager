using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public RelayCommand Create
        {
            get => new RelayCommand(() =>
            {
                Popupisopen=true;
           
            });
        }

        public RelayCommand Add
        {
            get => new RelayCommand(() =>
            {
                Popupisopen=true;
                Process.Start(Text);
            });
        }
        public RelayCommand Exit
        {
            get => new RelayCommand(() =>
            {
                Popupisopen=false;
            });
        }
        private bool popupisopen;

        public bool Popupisopen
        {
            get { return popupisopen; }
            set
            {
                popupisopen = value;
                OnPropertyChanged();
            }
        }
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Process> processes { get; set; }
        public MainViewModel(){

            processes = new ObservableCollection<Process>();    
            foreach (var process in Process.GetProcesses())
            {
                processes.Add(process);


            }
     
         }
    }
}
