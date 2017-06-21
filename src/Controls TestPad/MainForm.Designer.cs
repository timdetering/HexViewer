namespace Controls_TestPad
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
                    this.tabControl1 = new System.Windows.Forms.TabControl();
                    this.tabHexViewerCtrl = new System.Windows.Forms.TabPage();
                    this.btnHxVuLoadFile = new System.Windows.Forms.Button();
                    this.btnHxVuGenRand = new System.Windows.Forms.Button();
                    this.label1 = new System.Windows.Forms.Label();
                    this.txtHxVuRandDataSize = new System.Windows.Forms.TextBox();
                    this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
                    this.groupBox1 = new System.Windows.Forms.GroupBox();
                    this.groupBox2 = new System.Windows.Forms.GroupBox();
                    this.HxVuChkVirualMode = new System.Windows.Forms.CheckBox();
                    this.hexViewer1 = new MAAK.Controls.HexViewer();
                    this.tabControl1.SuspendLayout();
                    this.tabHexViewerCtrl.SuspendLayout();
                    this.groupBox1.SuspendLayout();
                    this.groupBox2.SuspendLayout();
                    this.SuspendLayout();
                    // 
                    // tabControl1
                    // 
                    this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                            | System.Windows.Forms.AnchorStyles.Left)
                                            | System.Windows.Forms.AnchorStyles.Right)));
                    this.tabControl1.Controls.Add(this.tabHexViewerCtrl);
                    this.tabControl1.Location = new System.Drawing.Point(209, 12);
                    this.tabControl1.Name = "tabControl1";
                    this.tabControl1.SelectedIndex = 0;
                    this.tabControl1.Size = new System.Drawing.Size(753, 436);
                    this.tabControl1.TabIndex = 1;
                    this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
                    // 
                    // tabHexViewerCtrl
                    // 
                    this.tabHexViewerCtrl.Controls.Add(this.groupBox2);
                    this.tabHexViewerCtrl.Controls.Add(this.groupBox1);
                    this.tabHexViewerCtrl.Controls.Add(this.hexViewer1);
                    this.tabHexViewerCtrl.Location = new System.Drawing.Point(4, 22);
                    this.tabHexViewerCtrl.Name = "tabHexViewerCtrl";
                    this.tabHexViewerCtrl.Padding = new System.Windows.Forms.Padding(3);
                    this.tabHexViewerCtrl.Size = new System.Drawing.Size(745, 410);
                    this.tabHexViewerCtrl.TabIndex = 1;
                    this.tabHexViewerCtrl.Text = "Hex Viewer Control";
                    this.tabHexViewerCtrl.UseVisualStyleBackColor = true;
                    // 
                    // btnHxVuLoadFile
                    // 
                    this.btnHxVuLoadFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                    this.btnHxVuLoadFile.Location = new System.Drawing.Point(14, 40);
                    this.btnHxVuLoadFile.Name = "btnHxVuLoadFile";
                    this.btnHxVuLoadFile.Size = new System.Drawing.Size(99, 23);
                    this.btnHxVuLoadFile.TabIndex = 4;
                    this.btnHxVuLoadFile.Text = "Load File...";
                    this.btnHxVuLoadFile.UseVisualStyleBackColor = true;
                    this.btnHxVuLoadFile.Click += new System.EventHandler(this.btnHxVuLoadFile_Click);
                    // 
                    // btnHxVuGenRand
                    // 
                    this.btnHxVuGenRand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                    this.btnHxVuGenRand.Location = new System.Drawing.Point(112, 35);
                    this.btnHxVuGenRand.Name = "btnHxVuGenRand";
                    this.btnHxVuGenRand.Size = new System.Drawing.Size(74, 23);
                    this.btnHxVuGenRand.TabIndex = 3;
                    this.btnHxVuGenRand.Text = "Generate";
                    this.btnHxVuGenRand.UseVisualStyleBackColor = true;
                    this.btnHxVuGenRand.Click += new System.EventHandler(this.btnHxVuGenRand_Click);
                    // 
                    // label1
                    // 
                    this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                    this.label1.AutoSize = true;
                    this.label1.Location = new System.Drawing.Point(6, 21);
                    this.label1.Name = "label1";
                    this.label1.Size = new System.Drawing.Size(56, 13);
                    this.label1.TabIndex = 2;
                    this.label1.Text = "Data Size:";
                    // 
                    // txtHxVuRandDataSize
                    // 
                    this.txtHxVuRandDataSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                    this.txtHxVuRandDataSize.Location = new System.Drawing.Point(8, 37);
                    this.txtHxVuRandDataSize.Name = "txtHxVuRandDataSize";
                    this.txtHxVuRandDataSize.Size = new System.Drawing.Size(98, 20);
                    this.txtHxVuRandDataSize.TabIndex = 1;
                    this.txtHxVuRandDataSize.Text = "1024";
                    // 
                    // propertyGrid1
                    // 
                    this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                            | System.Windows.Forms.AnchorStyles.Left)));
                    this.propertyGrid1.Location = new System.Drawing.Point(12, 12);
                    this.propertyGrid1.Name = "propertyGrid1";
                    this.propertyGrid1.Size = new System.Drawing.Size(191, 436);
                    this.propertyGrid1.TabIndex = 2;
                    // 
                    // groupBox1
                    // 
                    this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                    this.groupBox1.Controls.Add(this.label1);
                    this.groupBox1.Controls.Add(this.txtHxVuRandDataSize);
                    this.groupBox1.Controls.Add(this.btnHxVuGenRand);
                    this.groupBox1.Location = new System.Drawing.Point(6, 327);
                    this.groupBox1.Name = "groupBox1";
                    this.groupBox1.Size = new System.Drawing.Size(204, 77);
                    this.groupBox1.TabIndex = 5;
                    this.groupBox1.TabStop = false;
                    this.groupBox1.Text = "Generate Random Data";
                    // 
                    // groupBox2
                    // 
                    this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                    this.groupBox2.Controls.Add(this.HxVuChkVirualMode);
                    this.groupBox2.Controls.Add(this.btnHxVuLoadFile);
                    this.groupBox2.Location = new System.Drawing.Point(226, 327);
                    this.groupBox2.Name = "groupBox2";
                    this.groupBox2.Size = new System.Drawing.Size(161, 76);
                    this.groupBox2.TabIndex = 6;
                    this.groupBox2.TabStop = false;
                    this.groupBox2.Text = "Load File";
                    // 
                    // HxVuChkVirualMode
                    // 
                    this.HxVuChkVirualMode.AutoSize = true;
                    this.HxVuChkVirualMode.Location = new System.Drawing.Point(14, 17);
                    this.HxVuChkVirualMode.Name = "HxVuChkVirualMode";
                    this.HxVuChkVirualMode.Size = new System.Drawing.Size(111, 17);
                    this.HxVuChkVirualMode.TabIndex = 5;
                    this.HxVuChkVirualMode.Text = "Virtual Data Mode";
                    this.HxVuChkVirualMode.UseVisualStyleBackColor = true;
                    // 
                    // hexViewer1
                    // 
                    this.hexViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                            | System.Windows.Forms.AnchorStyles.Left)
                                            | System.Windows.Forms.AnchorStyles.Right)));
                    this.hexViewer1.BaseOffsetBackground = System.Drawing.Color.Empty;
                    this.hexViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    this.hexViewer1.ByteCharacterValueColor = System.Drawing.Color.Empty;
                    this.hexViewer1.ByteDataSource = null;
                    this.hexViewer1.ByteHexValueColor = System.Drawing.Color.Empty;
                    this.hexViewer1.ByteOffsetBackground = System.Drawing.Color.Empty;
                    this.hexViewer1.ChangedByteHexValueColor = System.Drawing.Color.Empty;
                    this.hexViewer1.ChangedCharacterValueColor = System.Drawing.Color.Empty;
                    this.hexViewer1.DataLength = 0;
                    this.hexViewer1.EvenByteBackground = System.Drawing.Color.Empty;
                    this.hexViewer1.Location = new System.Drawing.Point(3, 3);
                    this.hexViewer1.Name = "hexViewer1";
                    this.hexViewer1.NumberOfBytePerRow = MAAK.Controls.HexViewer.BytesPerRow.BytesPerRow16;
                    this.hexViewer1.OddByteBackground = System.Drawing.Color.Empty;
                    this.hexViewer1.SelectedByteHighlight = System.Drawing.Color.Empty;
                    this.hexViewer1.Size = new System.Drawing.Size(736, 318);
                    this.hexViewer1.TabIndex = 0;
                    this.hexViewer1.VirualData = false;
                    // 
                    // MainForm
                    // 
                    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                    this.ClientSize = new System.Drawing.Size(974, 460);
                    this.Controls.Add(this.propertyGrid1);
                    this.Controls.Add(this.tabControl1);
                    this.Name = "MainForm";
                    this.Text = "Form1";
                    this.tabControl1.ResumeLayout(false);
                    this.tabHexViewerCtrl.ResumeLayout(false);
                    this.groupBox1.ResumeLayout(false);
                    this.groupBox1.PerformLayout();
                    this.groupBox2.ResumeLayout(false);
                    this.groupBox2.PerformLayout();
                    this.ResumeLayout(false);

        }

        #endregion

        private MAAK.Controls.HexViewer hexViewer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabHexViewerCtrl;
            private System.Windows.Forms.Button btnHxVuGenRand;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.TextBox txtHxVuRandDataSize;
            private System.Windows.Forms.Button btnHxVuLoadFile;
            private System.Windows.Forms.PropertyGrid propertyGrid1;
            private System.Windows.Forms.GroupBox groupBox1;
            private System.Windows.Forms.GroupBox groupBox2;
            private System.Windows.Forms.CheckBox HxVuChkVirualMode;
    }
}

