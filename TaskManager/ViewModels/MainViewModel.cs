using BespokeFusion;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;


namespace TaskManager.ViewModels
{
    public class pro
    {
        public pro(Process process)
        {
            this.process=process;
            if (process.ProcessName.Length == 0)
                isrunning="no";
            else isrunning="running";
            var startTime = DateTime.UtcNow;
            var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
           

            var endTime = DateTime.UtcNow;
            var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;
            var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
            cpu=cpuUsageTotal * 100;


            
        }

        public Process process { get; set; }
        public string image { get; set; }
        public string isrunning { get; set; }
        public string ico { get; set; }


        public double cpu { get; set; }
    }
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
                    Selected_Process.process.Kill();
             
                    processes.Remove(selected_Process);


                }
            });
        }
        public RelayCommand Refresh
        {
            get => new RelayCommand(() =>
            {
                if (Selected_Process != null)
                {

                    processes.Clear();
                    foreach (var process in Process.GetProcesses())
                    {
                        processes.Add(new pro(process));

                    }

                }
            });
        }
        private pro selected_Process;

        public pro Selected_Process
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
                return ((item as pro).process.ProcessName.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);

        }
        public RelayCommand Add
        {
            get => new RelayCommand(() =>
            {
                try
                {
                    Popupisopen=true;
                    Process.Start(Text);
                    processes.Clear();

                    foreach (var process in Process.GetProcesses())
                    {
                        processes.Add(new pro(process));


                    }
                }
                catch (Exception)
                {

                    MaterialMessageBox.ShowError(@"Nooooooooooooooooooooo");
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

        public ObservableCollection<pro> processes { get; set; }
        public MainViewModel(){

            processes = new ObservableCollection<pro>();    
            foreach (var process in Process.GetProcesses())
            {
                processes.Add(new pro(process));
             
            }
            
     
         }
    }
}
