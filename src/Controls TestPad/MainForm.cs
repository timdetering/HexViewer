using System;
using System.IO;
using System.Windows.Forms;

namespace MAAK.Controls.TestPad
{

    interface IGuiControlTestPad<_ControlType>
    {
        void InitControl(_ControlType control);
    }
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            propertyGrid1.SelectedObject = hexViewer1;
        }

        public void InitiTestPadObjects()
        {
        }

        #region Hex Viewer TestPad
        private void btnHxVuGenRand_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtHxVuRandDataSize.Text))
            {
                hexViewer1.VirualData = false;
                int dataSize = int.Parse(txtHxVuRandDataSize.Text);
                byte[] btData = new byte[dataSize];
                Random rand = new Random();
                rand.NextBytes(btData);
                hexViewer1.ByteDataSource = btData;
                //hexViewer1.EvenByteBackgroundBrush = new System.Drawing.Drawing2D.HatchBrush(HatchStyle.Cross, Color.Red);
            }
        }

        private FileStream HxVuStream = null;
        private BinaryReader HxVuBinaryReader = null;
        private void btnHxVuLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            if (ofDialog.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                if (HxVuStream != null)
                {
                    HxVuBinaryReader.Close();
                    HxVuStream.Dispose();
                }
                HxVuStream = File.OpenRead(ofDialog.FileName);
                HxVuBinaryReader = new BinaryReader(HxVuStream);
                if (!HxVuChkVirualMode.Checked)
                {
                    hexViewer1.VirualData = false;
                    byte[] dataArray = new byte[HxVuStream.Length];
                    HxVuStream.Read(dataArray, 0, dataArray.Length);
                    hexViewer1.ByteDataSource = dataArray;
                }
                else
                {
                    hexViewer1.VirualData = true;
                    hexViewer1.DataCallBack += new HexViewerDataEventHandler(hexViewer1_DataCallBack);
                    hexViewer1.DataLength = (int)HxVuStream.Length;
                }

                Cursor = Cursors.Arrow;

            }
        }

        void hexViewer1_DataCallBack(object sender, HexViewerDataCallbackEvent e)
        {
            if(HxVuBinaryReader != null)
            {
                HxVuStream.Seek(e.StartIndex, SeekOrigin.Begin);
                HxVuStream.Read(e.DataReference, 0, e.Length);
                
            }
        }		
        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    propertyGrid1.SelectedObject = hexViewer1;
                    break;
                default:
                    break;
            }
        }		
    }
}