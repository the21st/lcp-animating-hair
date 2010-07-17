using System;
using System.Windows.Forms;

namespace AnimatingHair.GUI
{
    public partial class VisualTrackBar : UserControl
    {
        private float minValue;
        private float maxValue;
        private float range;
        private bool intData = false;
        private bool loaded = false;

        public VisualTrackBar()
        {
            InitializeComponent();

            trackBar.Maximum = trackBar.Width - 20;
        }

        public string Label
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        public void BindFloatData( object dataSource, string dataMember, float min, float max )
        {
            intData = false;
            bindData( dataSource, dataMember, min, max );
        }

        public void BindIntData( object dataSource, string dataMember, int min, int max )
        {
            intData = true;
            bindData( dataSource, dataMember, min, max );
        }

        private void bindData( object dataSource, string dataMember, float min, float max )
        {
            Binding binding = new Binding( "Text", dataSource, dataMember, false, DataSourceUpdateMode.OnPropertyChanged );
            textBox.DataBindings.Add( binding );
            minValue = min;
            maxValue = max;
            range = max - min;

            // manual refresh of the binding by reflection, binding.ReadValue() does not work
            float value;
            if (intData)
                value = (int)dataSource.GetType().GetProperty( dataMember ).GetValue( dataSource, null );
            else
                value = (float)dataSource.GetType().GetProperty( dataMember ).GetValue( dataSource, null );

            value -= min;
            value /= range;
            value *= trackBar.Maximum;
            trackBar.Value = (int)Math.Round( value );

            loaded = true;
        }

        private void trackBar_ValueChanged( object sender, EventArgs e )
        {
            if ( !loaded ) return;

            float value = (float)trackBar.Value / trackBar.Maximum;
            value *= range;
            value += minValue;
            if ( intData )
                textBox.Text = Convert.ToInt32( value ).ToString();
            else
                textBox.Text = value.ToString();
        }

        private void textBox_TextChanged( object sender, EventArgs e )
        {
            if ( !loaded ) return;

            loaded = false;

            float value;
            if ( float.TryParse( textBox.Text, out value ) )
            {
                value -= minValue;
                value /= range;
                if ( value > 1 )
                    trackBar.Value = trackBar.Maximum;
                else if ( value < 0 )
                    trackBar.Value = trackBar.Minimum;
                else
                {
                    value *= trackBar.Maximum;
                    trackBar.Value = Convert.ToInt32( value );
                }
            }
            loaded = true;
        }
    }
}
