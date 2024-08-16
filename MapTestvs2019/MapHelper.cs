using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapWinGIS;
using System.Windows.Forms;


namespace MapTestvs2019
{
    public static class MapHelper
    {
        public static void InsertMarker(Shapefile markersLayer, double x, double y)
        {
            if (!markersLayer.StartEditingShapes())
            {
                MessageBox.Show("Failed to start editing markers layer: " + markersLayer.ErrorMsg[markersLayer.LastErrorCode]);
            }
            Shape shp = new Shape();
            shp.Create(ShpfileType.SHP_POINT);

            var pnt = new MapWinGIS.Point();
            pnt.x = x;
            pnt.y = y;
            int index = shp.numPoints;
            shp.InsertPoint(pnt, ref index);

            index = markersLayer.NumShapes;
            if (!markersLayer.EditInsertShape(shp, ref index))
            {
                MessageBox.Show("Failed to insert shape: " + markersLayer.ErrorMsg[markersLayer.LastErrorCode]);
                return;
            }
            markersLayer.StopEditingShapes();
        }

        public static int GetMarkerAt(Shapefile markersLayer, double x, double y, double tolerance=1)
        {
            for (int i = 0; i < markersLayer.NumShapes; ++i)
            {
                var point = markersLayer.Shape[i].Point[0];

                double dist = MathHelper.GetDist2Points(x, y, point.x, point.y);
                if (dist < tolerance)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
