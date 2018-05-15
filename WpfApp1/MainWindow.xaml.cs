using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Configuration;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //  public List<Program> programList { get; set; }
      
        public ObservableCollection<ListItem> programList = new ObservableCollection<ListItem>();
        Utils utils = new Utils();
        


        public MainWindow()
        {
           
            InitializeComponent();
            
            programList = new ObservableCollection<ListItem>();
            ListView.Items.Clear();
            ListView.ItemsSource = null;
            ListView.ItemsSource = programList;

            //programList.Add(new ListItem() { Name = "test", Path = "test/test" });
           
            utils.PopulateList(ListView);
            
          
         

            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.ShowDialog();
            textBox1.Text = fd.FileName;
            
            
            textBox1.Tag = fd.SafeFileName;
        }

        private void importButton_Click(object sender, RoutedEventArgs e)
        {
           
           
            {
                utils.ImportToDB(textBox1.Tag.ToString(), textBox1.Text.ToString());
                
            }

            utils.PopulateList(ListView);
        

        }
        void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            var item = ((FrameworkElement)e.OriginalSource).DataContext as DataRowView;

           
            if (item != null)
            {
                try
                {
                    System.Diagnostics.Process.Start(item["Path"].ToString());

                    Console.WriteLine("Program at "+ item["Path"].ToString() + " executed");
                }
                catch(System.ComponentModel.Win32Exception)
                {
                    MessageBox.Show("Invalid path!");
                }
            }
        }


        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                foreach (DataRowView eachItem in ListView.SelectedItems)
                {     
                utils.deleteFromDB((string) eachItem[2]);
                }
                utils.PopulateList(ListView);
            }

            if ((Keyboard.Modifiers & ModifierKeys.Control)==ModifierKeys.Control)
            {
                e.Handled=true;
            }
            
        }



      
    }
}

