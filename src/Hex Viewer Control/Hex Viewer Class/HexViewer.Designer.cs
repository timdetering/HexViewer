namespace MAAK.Controls
{
    partial class HexViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
					this.sbMainScrollBar = new System.Windows.Forms.VScrollBar();
					this.SuspendLayout();
					// 
					// sbMainScrollBar
					// 
					this.sbMainScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
					this.sbMainScrollBar.Location = new System.Drawing.Point(497, 0);
					this.sbMainScrollBar.Name = "sbMainScrollBar";
					this.sbMainScrollBar.Size = new System.Drawing.Size(17, 220);
					this.sbMainScrollBar.TabIndex = 0;
					this.sbMainScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sbMainScrollBar_Scroll);
					this.sbMainScrollBar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sbMainScrollBar_KeyDown);
					// 
					// HexViewer
					// 
					this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.Controls.Add(this.sbMainScrollBar);
					this.DoubleBuffered = true;
					this.Name = "HexViewer";
					this.Size = new System.Drawing.Size(514, 220);
					this.Load += new System.EventHandler(this.HexViewer_Load);
					this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HexViewer_MouseDown);
					this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HexViewer_MouseMove);
					this.Resize += new System.EventHandler(this.HexViewer_Resize);
					this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HexViewer_KeyPress);
					this.Paint += new System.Windows.Forms.PaintEventHandler(this.HexViewer_Paint);
					this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HexViewer_MouseUp);
					this.ResumeLayout(false);

        }

        #endregion

			private System.Windows.Forms.VScrollBar sbMainScrollBar;
    }
}
