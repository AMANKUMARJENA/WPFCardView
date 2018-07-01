using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

namespace CardView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> TileControls = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            fillData();
        }

        private void fillData()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                String CmdString = "SELECT OrderId, TokenId, OrderDate, OrderStatus FROM Orders";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                
                Container.ItemsSource = dt.DefaultView;
            }
        }
        private void fillData(int status)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                String CmdString = "SELECT OrderId, TokenId, OrderDate, OrderStatus FROM Orders where OrderStatus=" + status;
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);

                Container.ItemsSource = dt.DefaultView;
            }
        }

        private void btnTile_Click(object sender, RoutedEventArgs e)
        {
            TileControls.Clear();
            TextBlock _TextBlock = new TextBlock();
            object msg = (sender as Button).Content;
            StackPanel stk1 = (msg as StackPanel);

            foreach (var child in stk1.Children)
            {
                if (child.GetType().ToString() == "System.Windows.Controls.TextBlock")
                {
                    _TextBlock = (TextBlock)child;
                    //MessageBox.Show(_TextBlock.Text);
                    TileControls.Add(_TextBlock.Text);
                }
            }

            MessageBox.Show(TileControls[1].ToString());
        }

        private void cmbStatusType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cmbStatusType.SelectedIndex)
            {
                case 0:
                    fillData(0);
                    break;
                case 1:
                    fillData(1);
                    break;
                case 2:
                    fillData(2);
                    break;
                case 3:
                    fillData(3);
                    break;
                case 4:
                    fillData(4);
                    break;
                case 5:
                    fillData();
                    break;
            }
        }


    }
}
