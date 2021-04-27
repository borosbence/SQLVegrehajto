using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace SQLVegrehajto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string script;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = Application.Current.Resources["OpenFilter"].ToString();
            ofd.Title = Application.Current.Resources["OpenTitle"].ToString();

            if (ofd.ShowDialog() == true)
            {
                FileLabel.Text = ofd.SafeFileName;
                script = File.ReadAllText(ofd.FileName);
            }
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            MessageTextBlock.Text = Application.Current.Resources["TestProcess"].ToString();
            string connectionString = "server=" + ServerTextBox.Text + ";"
                                     + "userid=" + UsernameTextBox.Text + ";"
                                     + "password=" + PasswordPasswordBox.Password + ";"
                                     + "persistsecurityinfo=True;"
                                     + "database=" + DatabaseTextBox.Text;
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {

                    conn.Open();
                }
                MessageTextBlock.Text = Application.Current.Resources["TestSuccess"].ToString();
            }
            catch (Exception ex)
            {
                MessageTextBlock.Text = ex.Message;
            }
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {

            MessageTextBlock.Text = Application.Current.Resources["RunProcess"].ToString();

            string connectionString = "server=" + ServerTextBox.Text + ";"
                                     + "userid=" + UsernameTextBox.Text + ";"
                                     + "password=" + PasswordPasswordBox.Password + ";"
                                     + "persistsecurityinfo=True;"
                                     + "database=" + DatabaseTextBox.Text;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand createCmd = new MySqlCommand(script, conn);
                try
                {
                    createCmd.ExecuteNonQuery();
                    MessageTextBlock.Text = Application.Current.Resources["RunSuccess"].ToString();
                }
                catch (Exception ex)
                {
                    MessageTextBlock.Text = ex.Message;
                }
            }
        }
    }
}
