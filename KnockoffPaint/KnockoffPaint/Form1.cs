using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;

namespace KnockoffPaint
{
    public partial class Form1 : Form
    {
        public List<Figure> Memory;
        public List<Point> PointList;
        public Point[] points=new Point[0];        
        public int i = 0;
        public BinaryFormatter bnrformtr = new BinaryFormatter();
        Dictionary<string, FigureFactory> organizer = new Dictionary<string, FigureFactory>(5);
        public Form1()
        {
            InitializeComponent();
            var bitmap = new Bitmap(Canv.Width, Canv.Height);
            Canv.Image = (Image)bitmap.Clone();
            bitmap.Dispose();
            Memory = new List<Figure>();
            organizer.Add("Line", new LineFactory());             
            organizer.Add("Rectangle", new RectangleFactory());
            organizer.Add("Square", new SquareFactory());
            organizer.Add("Ellipsis", new EllipsisFactory());
            organizer.Add("Circle", new CircleFactory());
            organizer.Add("Polyline", new PolylineFactory());
            FgrOrg.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }    
             
        private void Lstbtn_Click(object sender, EventArgs e)
        {
            foreach (Figure figure in Memory)
            {
                Bitmap bmp = new Bitmap(Canv.Image, Canv.Width, Canv.Height);
                var graphics = Graphics.FromImage(bmp);
                figure.Draw(graphics);
                graphics.Dispose();
                Canv.Image.Dispose();
                Canv.Image = (Image)bmp.Clone();
                bmp.Dispose();
            }
            Memory.Clear();
        }        

        private void Canv_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);
            points = PointArr(points, point);            
            i++;
        }      
       
        private void Clrbtn_Click(object sender, EventArgs e)
        {
            var bitmap = new Bitmap(Canv.Width, Canv.Height);
            Canv.Image = (Image)bitmap.Clone();
            bitmap.Dispose();
        }  
        public Point[] PointArr(Point[] arr, Point point)
        {
            Point[] Arr = new Point[arr.Length + 1];
            int j;
            for (j=0; j<arr.Length; j++)
            {
                Arr[j] = arr[j];
            }
            Arr[j] = point;
            return Arr;
        }        

        private void FileOpnBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog OF = new OpenFileDialog();            
            if (OF.ShowDialog() != DialogResult.Cancel)
            {
                string str = OF.FileName;
                try
                {
                    using (FileStream filestream = new FileStream(str, FileMode.OpenOrCreate))
                    {
                        Memory = (List<Figure>)bnrformtr.Deserialize(filestream);
                    }
                    foreach (Figure figure in Memory)
                    {
                        Bitmap bmp = new Bitmap(Canv.Image, Canv.Width, Canv.Height);
                        var graphics = Graphics.FromImage(bmp);
                        figure.Draw(graphics);
                        graphics.Dispose();
                        Canv.Image.Dispose();
                        Canv.Image = (Image)bmp.Clone();
                        bmp.Dispose();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Incorrect file format");
                }
            }
        }

        private void FileSavBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog SF = new SaveFileDialog();             
            if (SF.ShowDialog() != DialogResult.Cancel)
            {
                string str = SF.FileName;
                try
                {                    
                    using (FileStream filestrm = new FileStream(str, FileMode.OpenOrCreate))
                    {
                        bnrformtr.Serialize(filestrm, Memory);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Incorrect file format");
                }
            }
        }

        private void Canv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (i >= 2)
            {
                string str = FgrOrg.Text;
                var tmp = organizer[str];
                var figure = tmp.Create(points);
                Bitmap bmp = new Bitmap(Canv.Image, Canv.Width, Canv.Height);
                var graphics = Graphics.FromImage(bmp);
                figure.Draw(graphics);
                graphics.Dispose();
                Canv.Image.Dispose();
                Canv.Image = (Image)bmp.Clone();
                bmp.Dispose();
                Memory.Add(figure);
                points = new Point[0];
                i = 0;
            }
        }

        private void LoadDllBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog LoadAssembly = new OpenFileDialog();
            if (LoadAssembly.ShowDialog() != DialogResult.Cancel)
            {
                string str = LoadAssembly.FileName;
                try
                {
                    String name = Path.GetFileNameWithoutExtension(str);
                    Assembly assembly = Assembly.LoadFile(str);
                    Type[] FigFactory = assembly.GetTypes();
                    var item = (FigureFactory)FigFactory[1].GetConstructors()[0].Invoke(new object[0]);
                    organizer.Add(name, item);
                    FgrOrg.Items.Add(name);
                }
                catch (Exception)
                {
                    MessageBox.Show("Incorrect file format");
                }
            }
        }
    }
}
