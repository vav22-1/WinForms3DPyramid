using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms3DPyramid
{
    public class FigureColor
    {
        private Color linesColor;
        private Color verticesColor;
        private Color connectionsColor;
        private Color facesColor;
        public FigureColor(Color linesColor, Color verticesColor, Color connectionsColor, Color facesColor)
        {
            this.linesColor = linesColor;
            this.verticesColor = verticesColor;
            this.connectionsColor = connectionsColor;
            this.facesColor = facesColor;
        }
        public Color GetLinesColor() => linesColor;

        public Color GetVerticesColor() => verticesColor;

        public Color GetConnectionsColor() => connectionsColor;

        public Color GetFacesColor() => facesColor;

        public void SetLinesColor(Color color) => linesColor = color;

        public void SetVerticesColor(Color color) => verticesColor = color;

        public void SetConnectionsColor(Color color) => connectionsColor = color;

        public void SetFacesColor(Color color) => facesColor = color;

    }
}
