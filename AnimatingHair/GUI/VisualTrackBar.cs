using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnimatingHair.GUI
{
    public partial class VisualTrackBar : UserControl
    {
        private float minValue;
        private float maxValue;
        private float range;

        public VisualTrackBar()
        {
            InitializeComponent();
        }

        public string Label
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        public void BindFloatData(object dataSource, string dataMember, float min, float max)
        {
            textBox.DataBindings.Add( new Binding( "Text", dataSource, dataMember ) );
            minValue = min;
            maxValue = max;
            range = max - min;
        }

        private void trackBar_ValueChanged( object sender, EventArgs e )
        {
            // asd
            float value = (float)trackBar.Value / trackBar.Maximum;
            value *= range;
            value += minValue;
            textBox.Text = value.ToString();
        }

        private void textBox_KeyPress( object sender, KeyPressEventArgs e )
        {
            //if (e.KeyChar == '\r')
            //    Console.WriteLine(  );
        }
    }
}
