using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapWinGIS;


namespace MapTestvs2019
{
    public partial class Form1 : Form
    {
        private Shapefile _shape;
        public Form1()
        {
            InitializeComponent();
        }

        private void axMap1_MouseDownEvent(object sender, AxMapWinGIS._DMapEvents_MouseDownEvent e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void googleBtn_Click(object sender, EventArgs e)
        {
            axMap1.Projection = MapWinGIS.tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
        }

        private void centerHelwanBtn_Click(object sender, EventArgs e)
        {
            // set to helwan runway
            axMap1.Latitude = 29.8259655780143f;
            axMap1.Longitude = 31.3282147049904f;
        }

        private void wgsBtn_Click(object sender, EventArgs e)
        {
            axMap1.Projection = MapWinGIS.tkMapProjection.PROJECTION_WGS84;
        }

        private void centeroctBtn_Click(object sender, EventArgs e)
        {
            // set to 6oct runway
            axMap1.Latitude = 29.8146014f;
            axMap1.Longitude = 30.8256197f;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GeoProjection proj = new GeoProjection();
            proj.ImportFromEPSG(3857);

            _shape = new Shapefile();
            _shape.Open(@"C:\Users\zeyad\Desktop\Workspace\GIS\test.shp");
            _shape.Reproject(proj, 1);

            axMap1.AddLayer(_shape, true);
        }
    }
}
