using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Windows.Threading;


namespace Hanoi
{
    
    class Tower
    {
        private const int maxDisks = 64;
        
        private int[] disks = new int[maxDisks];
        
        private int numDisks;
        public char ID { get; private set; }
       
        private Canvas canvas { get; set; }
        
        private List<Image> Images_Of_Disks = new List<Image>();
        
        private  double Total_disks { get;set;}
       
        private List<double> prevDiskImagePos = new List<double>();
        
        private double diskImagePos;
       
        public Tower(char ID,ref Canvas canvas,int Total_disks){
            this.ID = ID;
            this.Total_disks = Total_disks;
            this.canvas = canvas;
            numDisks = 0;
            Canvas.SetTop(canvas, canvas.ActualHeight);
            Canvas.SetBottom(canvas, 5);
            diskImagePos = Canvas.GetBottom(canvas);
        }
        
        public void Add(int disk)
        {
            
            if (disk > maxDisks)
            {
                throw new ArgumentException("You cannot add more disks than the max capacity of a tower\n");
            }
            else if (numDisks >= 0)
            {
                if (numDisks != 0 && disks[numDisks - 1] < disk)
                {
                    throw new ArgumentException(string.Format("You cannot add a bigger disk {1} ontop of a smaller disk {0}\nDisk size {2} object {3}", disks[numDisks - 1], disk, numDisks, ID));
                }
                disks[numDisks] = disk;
                
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate {
                    System.Threading.Thread.Sleep(400);
                    Image diskImage = new Image();
                    diskImage.Source = new BitmapImage(new Uri("pack://application:,,,/Hanoi;component/Images/disk.png"));
                    diskImage.Width = (canvas.Width)*disks[numDisks]/ Total_disks;
                    Images_Of_Disks.Add(diskImage);
                    prevDiskImagePos.Add(diskImagePos);
                    canvas.Children.Add(diskImage);
                    Canvas.SetRight(diskImage, canvas.Width / 2 - (diskImage.Width / 2));           
                    Canvas.SetBottom(diskImage, diskImagePos);
                    diskImagePos += diskImage.Width/disk;  
                }));
                numDisks++;
            }
        }
        
        public int Remove()
        {
            
            if (numDisks > 0)
            {
                int TopDisk = disks[--numDisks];
                disks[numDisks] = 0;
                diskImagePos = prevDiskImagePos[numDisks];
                prevDiskImagePos.RemoveAt(numDisks);
                
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate { 
                    canvas.Children.Remove(Images_Of_Disks[numDisks]);
                    Images_Of_Disks.Remove(Images_Of_Disks[numDisks]);
                    System.Threading.Thread.Sleep(400);
                }));

                return TopDisk;
            }
            else throw new ArgumentException("Cannot remove a disk from an empty tower");
        }
    }
}
