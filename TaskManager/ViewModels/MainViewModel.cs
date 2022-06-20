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
using System.Windows.Data;

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
        public RelayCommand Delete
        {
            get => new RelayCommand(() =>
            {
               if(Selected_Process != null)
                {
                    Selected_Process.Kill();
             
                    processes.Remove(selected_Process);


                }
            });
        }
        private Process selected_Process;

        public Process Selected_Process
        {
            get { return selected_Process; }
            set { selected_Process = value; OnPropertyChanged(); }
        }
        private string serachtext;

        public string SearchText
        {
            get { return serachtext; }
            set
            {
                serachtext = value; OnPropertyChanged();
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(processes);
                view.Filter = userFilter;
            }
        }
        private bool userFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchText))
                return true;
            else
                return ((item as Process).ProcessName.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);

        }
        public RelayCommand Add
        {
            get => new RelayCommand(() =>
            {
                Popupisopen=true;
                Process.Start(Text);
                processes.Clear();

                foreach (var process in Process.GetProcesses())
                {
                    processes.Add(process);


                }
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
