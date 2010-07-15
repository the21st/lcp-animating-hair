namespace AnimatingHair.GUI
{
    partial class VisualTrackBar
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.label = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar.AutoSize = false;
            this.trackBar.LargeChange = 15;
            this.trackBar.Location = new System.Drawing.Point( 0, 16 );
            this.trackBar.Maximum = 149;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size( 151, 20 );
            this.trackBar.SmallChange = 4;
            this.trackBar.TabIndex = 0;
            this.trackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar.ValueChanged += new System.EventHandler( this.trackBar_ValueChanged );
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point( 3, 0 );
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size( 35, 13 );
            this.label.TabIndex = 1;
            this.label.Text = "Name";
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point( 157, 16 );
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size( 50, 20 );
            this.textBox.TabIndex = 2;
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.textBox_KeyPress );
            // 
            // VisualTrackBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.textBox );
            this.Controls.Add( this.label );
            this.Controls.Add( this.trackBar );
            this.Name = "VisualTrackBar";
            this.Size = new System.Drawing.Size( 207, 36 );
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox textBox;
    }
}
