using System;
using System.Collections.Generic;
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
using System.Diagnostics;
using System.Threading;

namespace Hanoi
{
    
    public partial class MainWindow : Window
    {
        static int numMoves;
        public MainWindow()
        {
            InitializeComponent();

            if (IsLoaded == true)
                Environment.Exit(0);

        }
        
        private void Manage_Components(bool State)
        {
            btnStart.IsEnabled = State;
            txtNum_Disks.IsEnabled = State;
        }

        
        void CreateTowers(int Total_Disks)
        {
           
            Manage_Components(false);
            playground_A.Children.Clear();
            playground_B.Children.Clear();
            playground_C.Children.Clear();
           
            Tower A = new Tower('A',ref playground_A,Total_Disks);
            Tower B = new Tower('B', ref playground_B,Total_Disks);
            Tower C = new Tower('C', ref playground_C, Total_Disks);
            
            for (int i = Total_Disks; i >0; i--)  A.Add(i);
            
            Stopwatch sw = Stopwatch.StartNew();
            MoveDisks(Total_Disks, ref A, ref C, ref B);
            sw.Stop();
            MessageBox.Show(string.Format("Time taken: {0}ms.\n Moves needed: {1}", sw.ElapsedMilliseconds,numMoves));
            Manage_Components(true);
        }

        
        void MoveDisks(int numDisk,ref Tower source, ref Tower destination, ref Tower temp)
        {
            
            if (numDisk == 1)
            {
                numMoves++;
                destination.Add(source.Remove());
            }
            else
            {
                MoveDisks(numDisk - 1, ref source, ref temp,ref destination );
                numMoves++;
                destination.Add(source.Remove());
                MoveDisks(numDisk - 1, ref temp, ref destination, ref source);
            }
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            CreateTowers(Convert.ToInt16(txtNum_Disks.Text));
        }
        
        private void TxtNum_Disks_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(txtNum_Disks.Text, out int n) && n > 0)
            {
                btnStart.IsEnabled = true;
                txtMoves.Text = Convert.ToString(Math.Pow(2, n) - 1);
            }
            else{
                txtMoves.Text = "";
                btnStart.IsEnabled = false;
            }
        }
    }
}
