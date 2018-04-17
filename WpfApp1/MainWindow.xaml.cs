﻿using Microsoft.Win32;
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
        string connectionString;
        SqlConnection connection;
       
        public MainWindow()
        {
           
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.DatabaseConnectionString"].ConnectionString;
            programList = new ObservableCollection<ListItem>();
            ListView.ItemsSource = programList;

            //programList.Add(new ListItem() { Name = "test", Path = "test/test" });
            PopulateList();
          
         

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
            try
            {
                ListItem newItem = new ListItem { Name = textBox1.Tag.ToString(), Path = textBox1.Text.ToString() };

            if (utils.checkForDuplicates(programList, newItem)==true){ programList.Add(newItem); }
            else { Console.WriteLine("Duplicate Name or Path"); }
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Nothing to import");
            }


        }
        void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext as ListItem;
            if (item != null)
            {
                try
                {
                    System.Diagnostics.Process.Start(item.Path);

                    Console.WriteLine("Program at "+item.Path+" executed");
                }
                catch(System.ComponentModel.Win32Exception)
                {
                    MessageBox.Show("Invalid path!");
                }
            }
        }

        private void PopulateList()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM ProgramList",connection))
            {
                connection.Open();
                DataTable programTable = new DataTable();
                adapter.Fill(programTable);
                ListView.ItemsSource =programTable.DefaultView;


            }
        }
    }
}
