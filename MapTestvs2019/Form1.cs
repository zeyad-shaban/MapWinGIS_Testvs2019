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
        private Shapefile _shapeLayer;
        private Shapefile _markersLayer;
        private Shape _selectedMarker = null;

        public Form1()
        {
            InitializeComponent();

            // Set map initials
            axMap1.CursorMode = tkCursorMode.cmNone;
            axMap1.SendMouseDown = true;
            axMap1.SendMouseUp = true;
            axMap1.SendMouseMove = true;

            axMap1.Longitude = 31.3282147049904f;
            axMap1.Latitude = 29.8259655780143f;
        }

        private void axMap1_MouseDownEvent(object sender, AxMapWinGIS._DMapEvents_MouseDownEvent e)
        {
            if (e.button == 1)          // left button
            {
                double x = 0.0;
                double y = 0.0;
                axMap1.PixelToProj(e.x, e.y, ref x, ref y);

                int markerIndex = MapHelper.GetMarkerAt(_markersLayer, x, y, axMap1.CurrentScale * 0.001);

                if (markerIndex != -1)
                {
                    _selectedMarker = _markersLayer.Shape[markerIndex];
                }
                else
                {
                    _selectedMarker = null;
                    MapHelper.InsertMarker(_markersLayer, x, y);
                    axMap1.Redraw();
                }
            }
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void googleBtn_Click(object sender, EventArgs e)
        {
            axMap1.Projection = MapWinGIS.tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
        }

        // set location to helwan runway
        private void centerHelwanBtn_Click(object sender, EventArgs e)
        {
            axMap1.Latitude = 29.8259655780143f;
            axMap1.Longitude = 31.3282147049904f;
        }

        private void wgsBtn_Click(object sender, EventArgs e)
        {
            axMap1.Projection = MapWinGIS.tkMapProjection.PROJECTION_WGS84;
        }

        // set location to 6oct runway
        private void centeroctBtn_Click(object sender, EventArgs e)
        {
            axMap1.Latitude = 29.8146014f;
            axMap1.Longitude = 30.8256197f;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Adding shape file [needs fixing]
            GeoProjection proj = new GeoProjection();
            proj.ImportFromEPSG(3857);

            //_shapeLayer = new Shapefile();
            //_shapeLayer.Open(@"C:\Users\zeyad\Desktop\Workspace\GIS\test.shp");
            //_shapeLayer.Reproject(proj, 1);

            //axMap1.AddLayer(_shapeLayer, true);

            // Markers layer
            _markersLayer = new Shapefile();
            bool success = _markersLayer.CreateNew("", ShpfileType.SHP_POINT);
            if (!success)
            {
                MessageBox.Show("Failed to create marker layer: " + _markersLayer.ErrorMsg[_markersLayer.LastErrorCode]);
                return;
            }
            axMap1.AddLayer(_markersLayer, true);
        }

        private void axMap1_MouseUpEvent(object sender, AxMapWinGIS._DMapEvents_MouseUpEvent e)
        {
            _selectedMarker = null;
        }

        private void axMap1_MouseMoveEvent(object sender, AxMapWinGIS._DMapEvents_MouseMoveEvent e)
        {
            // Detect mouse movement to drag points around
            if (_selectedMarker == null) return;

            double x = 0.0;
            double y = 0.0;

            axMap1.PixelToProj(e.x, e.y, ref x, ref y);

            _selectedMarker.Point[0].x = x;
            _selectedMarker.Point[0].y = y;

            axMap1.Redraw();
        }
    }
}
