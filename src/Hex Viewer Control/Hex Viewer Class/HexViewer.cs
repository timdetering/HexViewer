using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MAAK.Controls
{
    public partial class HexViewer : UserControl
    {
        #region Events
        /// <summary>
        /// Data callback event handler for virual mode
        /// </summary>
        public event HexViewerDataEventHandler DataCallBack;
        #endregion

        #region Private Members
        private byte[] m_mainDataSource;
        private int m_colAddressOffsetSize = 16;
        private int m_dataLength;
        private Point m_selectedByte = new Point(0, 0);
        private bool m_isVirual = false;
        private byte[] m_pageDataBuffer;
        private Dictionary<Point, byte> m_dictEditedBytes = new Dictionary<Point, byte>();
        private SortedList<int, int> m_changedBytes = new SortedList<int, int>();
        #endregion
        #region Enumerations
        /// <summary>
        /// Enumeration for the number of bytes to be displayed per row
        /// </summary>
        /// <enum-value-summary name="BytesPerRow1">One byte per row</enum-value-summary>
        /// <enum-value-summary name="BytesPerRow2">Two bytes per row</enum-value-summary>
        /// <enum-value-summary name="BytesPerRow4">Four bytes per row</enum-value-summary>
        /// <enum-value-summary name="BytesPerRow8">Eight bytes per row</enum-value-summary>
        /// <enum-value-summary name="BytesPerRow16">Sixteen bytes per row</enum-value-summary>
        /// <enum-value-summary name="BytesPerRow32">Thirty two bytes per row</enum-value-summary>
        public enum BytesPerRow
        {	
            BytesPerRow1 = 1,
            BytesPerRow2 = 2,
            BytesPerRow4 = 4,
            BytesPerRow8 = 8,
            BytesPerRow16 = 16,
            BytesPerRow32 = 32,
        }
        #endregion
        #region Defaults and Constants
        /// <summary>
        /// Pre-defined color for setting default brushes
        /// </summary>
        static public Color DeafultBrushColor = Color.Empty;
        static public Color CustomBrushColor = Color.FromArgb(0, 1, 0, 0);
        #endregion
        #region Properties
        /// <summary>
        /// Get the length of data displayed by the control, or set the length if virual data mode is enabled
        /// </summary>
        [CategoryAttribute("Behavior"), DescriptionAttribute("Set teh length of data if VirualData is set to true")]
        public int DataLength
        {
            get
            {
                return m_dataLength;
            }
            set
            {
                if (VirualData)
                {
                    setDataLength(value);
                }
                else
                {
                    HexViewerVirualNotSetException exception =
                        new HexViewerVirualNotSetException("Data Length cannot be set unless VirualData property is set to true");
                }
            }
        }
        /// <summary>
        /// Set if data is stored by the control or the control should retreive the data from the client using callback event
        /// </summary>
        [CategoryAttribute("Behavior"), DescriptionAttribute("Set if the control should not hold the data and the client is responsible to provide that data")]
        public bool VirualData
        {
            get { return m_isVirual; }
            set {m_isVirual = value;}
        }
        /// <summary>
        /// Set or get the number of bytes to be displayed per each row
        /// </summary>
        [CategoryAttribute("Behavior"), DescriptionAttribute("Number of bytes to be spanned per every row")]
        public BytesPerRow NumberOfBytePerRow
        {
            get
            {
                return (BytesPerRow)m_colAddressOffsetSize;
            }
            set
            {
                m_colAddressOffsetSize = (int)value;
                if (m_mainDataSource != null)
                {
                    setScrollBarRange(DataLength);
                    sbMainScrollBar.Value = 0;
                }
                Invalidate();
            }
        }
        /// <summary>
        /// Set or get the color of base address offset labels' background
        /// </summary>
        [CategoryAttribute("Appearance"), DescriptionAttribute("Set the color of the base address offset label background")]
        public Color BaseOffsetBackground
        {
            get { return getStockBrushColor(GuiStockObjects.Brush_BaseOffset); }
            set { setStockBrushColor(GuiStockObjects.Brush_BaseOffset, value); }
        }
        /// <summary>
        /// Set or get the color of byte address offset labels' background
        /// </summary>
        [CategoryAttribute("Appearance"), DescriptionAttribute("Set the color of the byte offset label background")]
        public Color ByteOffsetBackground
        {
            get { return getStockBrushColor(GuiStockObjects.Brush_ByteOffset); }
            set { setStockBrushColor(GuiStockObjects.Brush_ByteOffset, value); }
        }
        /// <summary>
        /// Set or get the color of odd byte values background
        /// </summary>
        [CategoryAttribute("Appearance"), DescriptionAttribute("Set the color of odd bytes background")]
        public Color OddByteBackground
        {
            get { return getStockBrushColor(GuiStockObjects.Brush_OddByteVal); }
            set { setStockBrushColor(GuiStockObjects.Brush_OddByteVal, value); }
        }
        /// <summary>
        /// Set or get the color of even byte values background
        /// </summary>
        [CategoryAttribute("Appearance"), DescriptionAttribute("Set the color of even bytes background")]
        public Color EvenByteBackground
        {
            get { return getStockBrushColor(GuiStockObjects.Brush_EvenByteVal); }
            set { setStockBrushColor(GuiStockObjects.Brush_EvenByteVal, value); }
        }
        /// <summary>
        /// Set or get the color of selected byte highlight
        /// </summary>
        [CategoryAttribute("Appearance"), DescriptionAttribute("Set the color of the selection highlight")]
        public Color SelectedByteHighlight
        {
            get { return getStockBrushColor(GuiStockObjects.Brush_SelectionHighlight); }
            set { setStockBrushColor(GuiStockObjects.Brush_SelectionHighlight, value); }
        }
        /// <summary>
        /// Set or get the text color of byte's hex value
        /// </summary>
        [CategoryAttribute("Appearance"), DescriptionAttribute("Set the text color of unchanged  byte's hex value")]
        public Color ByteHexValueColor
        {
            get { return getStockBrushColor(GuiStockObjects.Brush_ByteHexValue); }
            set { setStockBrushColor(GuiStockObjects.Brush_ByteHexValue, value); }
        }
        /// <summary>
        /// Set or get the text color of byte's character value
        /// </summary>
        [CategoryAttribute("Appearance"), DescriptionAttribute("Set the text color of unchanged  byte's character value")]
        public Color ByteCharacterValueColor
        {
            get { return getStockBrushColor(GuiStockObjects.Brush_ByteASCIIValue); }
            set { setStockBrushColor(GuiStockObjects.Brush_ByteASCIIValue, value); }
        }
        /// <summary>
        /// Set or get the text color of changed byte's hex value
        /// </summary>
        [CategoryAttribute("Appearance"), DescriptionAttribute("Set the text color of changed  byte's hex value")]
        public Color ChangedByteHexValueColor
        {
            get { return getStockBrushColor(GuiStockObjects.Brush_EditedByteHexValue); }
            set { setStockBrushColor(GuiStockObjects.Brush_EditedByteHexValue, value); }
        }
        /// <summary>
        /// Set or get the text color of changed byte's character value
        /// </summary>
        [CategoryAttribute("Appearance"), DescriptionAttribute("Set the text color of changed  byte's character value")]
        public Color ChangedCharacterValueColor
        {
            get { return getStockBrushColor(GuiStockObjects.Brush_EditedByteASCIIValue); }
            set { setStockBrushColor(GuiStockObjects.Brush_EditedByteASCIIValue, value); }
        }


        /// <summary>
        /// Set or get the brush of base address offset labels' background
        /// </summary>
        public Brush BaseOffsetBackgroundBrush
        {
            get { return getBrush(GuiStockObjects.Brush_BaseOffset); }
            set { setBrush(GuiStockObjects.Brush_BaseOffset, value); }
        }
        /// <summary>
        /// Set or get the brush of byte address offset labels' background
        /// </summary>
        public Brush ByteOffsetBackgroundBrush
        {
            get { return getBrush(GuiStockObjects.Brush_ByteOffset); }
            set { setBrush(GuiStockObjects.Brush_ByteOffset, value); }
        }
        /// <summary>
        /// Set or get the brush of odd byte values background
        /// </summary>
        public Brush OddByteBackgroundBrush
        {
            get { return getBrush(GuiStockObjects.Brush_OddByteVal); }
            set { setBrush(GuiStockObjects.Brush_OddByteVal, value); }
        }
        /// <summary>
        /// Set or get the brush of even byte values background
        /// </summary>
        public Brush EvenByteBackgroundBrush
        {
            get { return getBrush(GuiStockObjects.Brush_EvenByteVal); }
            set { setBrush(GuiStockObjects.Brush_EvenByteVal, value); }
        }
        /// <summary>
        /// Byte array to be displayed by the control if virual data mode not enabled
        /// </summary>
        public byte[] ByteDataSource
        {
            get
            {
                return m_mainDataSource;
            }

            set
            {
                if (value != null)
                {
                    setData(value);
                }
            }
        }
        /// <summary>
        /// sorted list with changed bytes indexes
        /// </summary>
        public SortedList<int, int> ChangedBytes
        {
            get
            {
                return m_changedBytes;
            }
        }
        public new Rectangle ClientRectangle
        {
            get
            {
                Rectangle retRect = new Rectangle();
                retRect = base.ClientRectangle;
                return retRect;
            }
        }
        #endregion
        public HexViewer()
        {
            InitializeComponent();
        }
        #region Private Helpers
        /// <summary>
        /// Set the scroll bar's ranges
        /// </summary>
        /// <param name="dataSize"></param>
        protected void setScrollBarRange(int dataSize)
        {
            sbMainScrollBar.Minimum = 0;
            sbMainScrollBar.Maximum = dataSize / m_colAddressOffsetSize +
                (dataSize % m_colAddressOffsetSize == 0 ? 0 : 1);
            sbMainScrollBar.LargeChange = getRowOffsetsPageSize(ClientRectangle);
            sbMainScrollBar.Update();
            m_pageDataBuffer = new byte[getRowOffsetsPageDataSize(ClientRectangle)];
            GC.Collect();
        }

        protected int getDataRange(byte[] byteArray, int startIndex)
        {
            int retDataLength = Math.Min(byteArray.Length, DataLength - startIndex);
            if (!m_isVirual)
            {
                Array.Copy(m_mainDataSource, startIndex, byteArray, 0, retDataLength);
            }
            else
            {
                if (DataCallBack != null)
                {
                    HexViewerDataCallbackEvent e = new HexViewerDataCallbackEvent(startIndex, retDataLength);
                    e.DataReference = byteArray;
                    DataCallBack(this, e);
                }
                else
                {
                    retDataLength = 0;
                }
            }
            return retDataLength;
        }
        protected void setDataLength(int length)
        {
            m_dataLength = length;
            setScrollBarRange(DataLength);
            m_dictEditedBytes.Clear();
            m_changedBytes.Clear();
            Invalidate();
        }
        protected void setData(byte[] btDataArr)
        {
            m_mainDataSource = btDataArr;
            setDataLength(m_mainDataSource.Length);
        }


        private int byteIndexFromBytePos(Point point)
        {
            return point.X + point.Y * m_colAddressOffsetSize;
        }

        protected Point bytePosFromByteIndex(int index)
        {
            return new Point(index % m_colAddressOffsetSize, index / m_colAddressOffsetSize);
        }
        #endregion

        

        #region Event Handlers

        private void HexViewer_Paint(object sender, PaintEventArgs e)
        {
            drawCanvas(e.Graphics, ClientRectangle, sbMainScrollBar.Value);
            drawSelection(e.Graphics, ClientRectangle, sbMainScrollBar.Value);
            drawData(e.Graphics, ClientRectangle, sbMainScrollBar.Value);
        }

        private void HexViewer_Load(object sender, EventArgs e)
        {
        }

        private void sbMainScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }

        private void HexViewer_Resize(object sender, EventArgs e)
        {
            //if(btMainDataSource != null)
                setScrollBarRange(DataLength);
        }
        #endregion
        #region Selection and Mouse Methods
        #region Selection Helper Methods
        private Point bytePosFromPoint(Point point)
        {
            point.X -= BaseAddressColumnWidth;
            point.Y -= ByteHexValueSquareWidth;
            if (point.X < 0)
                point.X = 0;
            if (point.Y < 0)
                point.Y = 0;
            
            Point retPoint = new Point();
            retPoint.X = point.X / ByteHexValueSquareWidth;
            retPoint.Y = point.Y / ByteHexValueSquareWidth;
            if (retPoint.X >= m_colAddressOffsetSize)
                retPoint.X = m_colAddressOffsetSize - 1;
            if (retPoint.Y >= getRowOffsetsPageSize(ClientRectangle))
                retPoint.Y = getRowOffsetsPageSize(ClientRectangle) - 1;

            retPoint.Y += getEffectiveRowOffset(sbMainScrollBar.Value, sbMainScrollBar.Maximum, ClientRectangle);			
            return retPoint;
        }
        private bool offsetSelectedByte(int x, int y)
        {
            Point newSelectedByte = m_selectedByte;
            newSelectedByte.Offset(x, y);
            long byteIndex = byteIndexFromBytePos(newSelectedByte);
            if (byteIndex >= 0 && byteIndex < DataLength &&
                newSelectedByte.X >= 0 && newSelectedByte.X < m_colAddressOffsetSize)
            {
                m_selectedByte = newSelectedByte;
            }
            return m_selectedByte == newSelectedByte;
        }
        
        private void selectBytePosFromPoint(Point point)
        {
            Point oldselection = m_selectedByte;
            m_selectedByte = bytePosFromPoint(point);
            long byteIndex = byteIndexFromBytePos(m_selectedByte);
            if (byteIndex < 0 || byteIndex >= DataLength)
            {
                m_selectedByte = oldselection;
            }
            else
            {
                invalidateByte(m_selectedByte);
                invalidateByte(oldselection);
            }
        }
        #endregion
        #region Mouse Event Handlers
        private void HexViewer_MouseDown(object sender, MouseEventArgs e)
        {
            selectBytePosFromPoint(e.Location);
        }

        private void HexViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                selectBytePosFromPoint(e.Location);	
            }
        }

        private void HexViewer_MouseUp(object sender, MouseEventArgs e)
        {
        }
        #endregion		

        #endregion

        #region Keyboard Editing Methods
        #region Editing Helper Methods
        protected void updateByteValue(Point bytePos, char digit)
        {
            byte btDigitVal = (digit >= '0' && digit <= '9') ? (byte)(digit - '0') : (byte)(digit - 'a' + 10);
            byte btByteVal = getByteValue(bytePos);
            bool bKeyExists = m_dictEditedBytes.ContainsKey(bytePos);
            if (btByteVal != (byte)(btByteVal << 4 | btDigitVal))
            {
                m_dictEditedBytes[bytePos] = (byte)(btByteVal << 4 | btDigitVal);
                if (!bKeyExists)
                {
                    m_changedBytes.Add(byteIndexFromBytePos(bytePos), byteIndexFromBytePos(bytePos));
                }
            }
        }
        protected byte getByteValue(Point bytePos)
        {
            byte btRetByte = 0;
            if (m_dictEditedBytes.ContainsKey(bytePos))
                btRetByte = m_dictEditedBytes[bytePos];
            else
            {
                byte[] btData = new byte[1];
                getDataRange(btData, byteIndexFromBytePos(bytePos));
                btRetByte = btData[0];
            }
            return btRetByte;
        }
        protected void keyScorll(char KeyChar)
        {
        }
        #endregion
        #region Keyboard Event Handlers
        private void HexViewer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 'A' && e.KeyChar <= 'F' ||
                e.KeyChar >= 'a' && e.KeyChar <= 'f' ||
                e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                updateByteValue(m_selectedByte, char.ToLower(e.KeyChar));
                invalidateByte(m_selectedByte);
            }
            else
            {
                keyScorll(e.KeyChar);
            }
        }
        #endregion

        private void sbMainScrollBar_KeyDown(object sender, KeyEventArgs e)
        {			
            switch (e.KeyCode)
            {
                case Keys.Down:
                    offsetSelectedByte(0, 1);
                    e.Handled = isBytePosVisible(sbMainScrollBar.Value, ClientRectangle, m_selectedByte);
                    Invalidate();
                    break;
                case Keys.Up:
                    offsetSelectedByte(0, -1);
                    e.Handled = isBytePosVisible(sbMainScrollBar.Value, ClientRectangle, m_selectedByte);
                    Invalidate();
                    break;
                case Keys.Left:
                    offsetSelectedByte(-1, 0);
                    Invalidate();
                    e.Handled = true;
                    break;
                case Keys.Right:
                    e.Handled = true;
                    offsetSelectedByte(1, 0);
                    Invalidate();
                    break;
                default:
                    if (e.KeyValue >= 'A' && e.KeyValue <= 'F' ||
                        e.KeyValue >= 'a' && e.KeyValue <= 'f' ||
                        e.KeyValue >= '0' && e.KeyValue <= '9')
                    {
                        updateByteValue(m_selectedByte, char.ToLower((char)e.KeyValue));
                        invalidateByte(m_selectedByte);
                        e.Handled = true;
                    }
                    break;
            }
        }
        #endregion

    }

    public class HexViewerDataCallbackEvent
    {
        public HexViewerDataCallbackEvent(int startIndex, int dataLength)
        {
            this.startIndex = startIndex;
            this.dataLength = dataLength;
        }
        #region Data Members
        private byte[] btDataRef ;
        private int startIndex;
        private int dataLength;
        #endregion

        #region Properties
        public byte[] DataReference
        {
            get { return btDataRef; }
            set { btDataRef = value; }
        }
        public int StartIndex
        {
            get { return startIndex; }
        }
        public int Length
        {
            get { return dataLength; }
        }
        #endregion
    }

    public delegate void HexViewerDataEventHandler(object sender, HexViewerDataCallbackEvent e);


    [global::System.Serializable]
    public class HexViewerVirualNotSetException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public HexViewerVirualNotSetException() { }
        public HexViewerVirualNotSetException(string message) : base(message) { }
        public HexViewerVirualNotSetException(string message, Exception inner) : base(message, inner) { }
        protected HexViewerVirualNotSetException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}