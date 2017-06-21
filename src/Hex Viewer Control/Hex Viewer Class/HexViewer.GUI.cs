using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace MAAK.Controls
{
    partial class HexViewer
    {
        #region GUI Methods
        #region Constants
        private class GUIConstants
        {
            public const int c_rowAddressOffsetWidth = 80;
            public const int c_colAddressOffsetWidth = 24;
            public const int c_colCharacterWidth = 12;
        }
        #endregion
        #region Stock Objects		
        protected enum GuiStockObjects
        {
            Brush_BaseOffset,
            Brush_ByteOffset,
            Brush_EvenByteVal,
            Brush_OddByteVal,
            Brush_SelectionHighlight,
            Brush_ByteHexValue,
            Brush_ByteASCIIValue,
            Brush_EditedByteHexValue,
            Brush_EditedByteASCIIValue,
            Unknown = -1,
        };
        protected class GUIStaticStock
        {
            static private Brush defaultBrush_BaseOffset = new LinearGradientBrush(new Rectangle(0, 0, 16, 16),
                Color.FromArgb(128, Color.LightGray), Color.White, LinearGradientMode.Horizontal);

            static private Brush defaultBrush_ByteOffset = new LinearGradientBrush(new Rectangle(0, 0, 16, 16),
                                Color.FromArgb(128, Color.LightGray), Color.White, LinearGradientMode.Vertical);

            static private Brush defaultBrush_EvenByteVal = new LinearGradientBrush(new Rectangle(0, 0, 16, 16),
                                Color.FromArgb(10, Color.RoyalBlue), Color.FromArgb(100, Color.RoyalBlue), LinearGradientMode.Horizontal);

            static private Brush defaultBrush_OddByteVal = new LinearGradientBrush(new Rectangle(0, 0, 16, 16),
                                Color.White, Color.FromArgb(200, Color.LightGray), LinearGradientMode.Horizontal);

            static private Brush defaultBrush_SelectionHighlight = new SolidBrush(Color.FromArgb(128, Color.RoyalBlue));

            static private Brush defaultBrush_ByteHexValue = new SolidBrush(Color.Navy);
            
            static private Brush defaultBrush_ByteASCIIValue = new SolidBrush(Color.Black);

            static private Brush defaultBrush_EditedByteHexValue = new SolidBrush(Color.Red);

            static private Brush defaultBrush_EditedByteASCIIValue = new SolidBrush(Color.Red);
            static private Dictionary<GuiStockObjects, object> stockObjects = new Dictionary<GuiStockObjects, object>();

            static public Dictionary<GuiStockObjects, object> StockObject
            {
                get
                {
                    if (stockObjects.Count == 0)
                    {
                        Type tGUIStaticStock = typeof(GUIStaticStock);
                        FieldInfo [] staticFields = tGUIStaticStock.GetFields(BindingFlags.Static | BindingFlags.NonPublic);
                        GuiStockObjects enField = GuiStockObjects.Unknown;
                        foreach (FieldInfo field in staticFields)
                        {
                            try
                            {
                                enField = (GuiStockObjects)Enum.Parse(typeof(GuiStockObjects), field.Name.Replace("default", ""));
                                stockObjects[enField] = field.GetValue(null);
                            }
                            catch{}
                        }
                    }
                    return stockObjects;
                }
            }
        }
        protected void setStockBrushColor(GuiStockObjects enStockBrush, Color color)
        {
            if (color != HexViewer.DeafultBrushColor)
                setBrush(enStockBrush, new SolidBrush(color));
            else
                setBrush(enStockBrush, GUIStaticStock.StockObject[enStockBrush] as Brush);
            Invalidate();
        }
        protected Color getStockBrushColor(GuiStockObjects enStockBrush)
        {
            Color retColor;
            Brush objBrush = getBrush(enStockBrush);

            if (objBrush == GUIStaticStock.StockObject[enStockBrush] as Brush)
                retColor = HexViewer.DeafultBrushColor;
            else
                retColor = (objBrush as SolidBrush).Color;

            return retColor;
        }
        protected void setBrush(GuiStockObjects enStockBrush, Brush brush)
        {
            switch (enStockBrush)
            {
                case GuiStockObjects.Brush_BaseOffset:
                    brshBaseOffset = brush;
                    break;
                case GuiStockObjects.Brush_ByteOffset:
                    brshByteOffset = brush;
                    break;
                case GuiStockObjects.Brush_EvenByteVal:
                    brshByteValEven = brush;
                    break;
                case GuiStockObjects.Brush_OddByteVal:
                    brshByteValOdd = brush;
                    break;
                case GuiStockObjects.Brush_SelectionHighlight:
                    brshSelectionHighlight = brush;
                    break;
                case GuiStockObjects.Brush_ByteHexValue:
                    brshByteHexValue = brush;
                    break;
                case GuiStockObjects.Brush_ByteASCIIValue:
                    brshByteASCIIValue = brush;
                    break;
                case GuiStockObjects.Brush_EditedByteHexValue:
                    brshEditedByteHexValue = brush;
                    break;
                case GuiStockObjects.Brush_EditedByteASCIIValue:
                    brshEditedByteASCIIValue = brush;
                    break;
                case GuiStockObjects.Unknown:
                default:
                    break;
            }
        }

        protected Brush getBrush(GuiStockObjects enStockBrush)
        {
            switch (enStockBrush)
            {
                case GuiStockObjects.Brush_BaseOffset:
                    return brshBaseOffset;					
                case GuiStockObjects.Brush_ByteOffset:
                    return  brshByteOffset;					
                case GuiStockObjects.Brush_EvenByteVal:
                    return  brshByteValEven;					
                case GuiStockObjects.Brush_OddByteVal:
                    return  brshByteValOdd;					
                case GuiStockObjects.Brush_SelectionHighlight:
                    return  brshSelectionHighlight;					
                case GuiStockObjects.Brush_ByteHexValue:
                    return  brshByteHexValue;					
                case GuiStockObjects.Brush_ByteASCIIValue:
                    return  brshByteASCIIValue;					
                case GuiStockObjects.Brush_EditedByteHexValue:
                    return  brshEditedByteHexValue;					
                case GuiStockObjects.Brush_EditedByteASCIIValue:
                    return  brshEditedByteASCIIValue;					
                case GuiStockObjects.Unknown:
                default:
                    return null;					
            }
        }
        #endregion
        #region Drawing Objects
        private Brush brshBaseOffset	= GUIStaticStock.StockObject[GuiStockObjects.Brush_BaseOffset] as Brush;
        private Brush brshByteOffset	= GUIStaticStock.StockObject[GuiStockObjects.Brush_ByteOffset] as Brush;

        private Brush brshByteValEven = GUIStaticStock.StockObject[GuiStockObjects.Brush_EvenByteVal] as Brush;
        private Brush brshByteValOdd	= GUIStaticStock.StockObject[GuiStockObjects.Brush_OddByteVal] as Brush;

        private Brush brshSelectionHighlight = GUIStaticStock.StockObject[GuiStockObjects.Brush_SelectionHighlight] as Brush;

        private Pen penBlack2 = new Pen(Color.Black, 1.0f);
        private Pen penGray1 = new Pen(Color.Gray, 1.0f);
        private Pen penWhite1 = new Pen(Color.White, 1.0f);

        private StringFormat stringFormatCenter = new StringFormat();
        private Font fontMain = new Font("Courier New", 10);
        private Font fontMainBold = new Font("Courier New", 10, FontStyle.Bold);

        private Brush brshByteHexValue					= GUIStaticStock.StockObject[GuiStockObjects.Brush_ByteHexValue] as Brush;
        private Brush brshByteASCIIValue				= GUIStaticStock.StockObject[GuiStockObjects.Brush_ByteASCIIValue] as Brush;
        private Brush brshEditedByteHexValue		= GUIStaticStock.StockObject[GuiStockObjects.Brush_EditedByteHexValue] as Brush;
        private Brush brshEditedByteASCIIValue	= GUIStaticStock.StockObject[GuiStockObjects.Brush_EditedByteASCIIValue] as Brush;
        #endregion
        #region GUI Protected Properties
        protected int BaseAddressColumnWidth
        {
            get
            {
                return GUIConstants.c_rowAddressOffsetWidth;
            }
        }
        protected int ByteHexValueSquareWidth
        {
            get
            {
                return GUIConstants.c_colAddressOffsetWidth;
            }
        }
        protected int ByteCharacterValueSquareWidth
        {
            get
            {
                return GUIConstants.c_colCharacterWidth;
            }
        }
        #endregion
        #region GUI Helper Methods
        protected int getRowOffsetsPageSize(Rectangle pageRect)
        {
            return (pageRect.Height - ByteHexValueSquareWidth) / ByteHexValueSquareWidth;
        }

        protected int getRowOffsetsPageDataSize(Rectangle pageRect)
        {
            return getRowOffsetsPageSize(pageRect) * m_colAddressOffsetSize;
        }
        protected int getEffectiveRowOffset(int rowAddressOffest, int maxRowAddressOffset, Rectangle pageRect)
        {
            int retRowAddressOffset = rowAddressOffest;
            if (rowAddressOffest + getRowOffsetsPageSize(pageRect) > maxRowAddressOffset && maxRowAddressOffset > getRowOffsetsPageSize(pageRect))
                retRowAddressOffset = maxRowAddressOffset - getRowOffsetsPageSize(pageRect);
            return retRowAddressOffset;
        }
        protected void getByteRect(ref Rectangle rect, int rowAddressOffset, int bytePos)
        {
            getByteRect(ref rect, rowAddressOffset, bytePos / m_colAddressOffsetSize, bytePos % m_colAddressOffsetSize);
        }
        protected void getByteRect(ref Rectangle rect, int rowAddressOffset, int rowIndex, int colIndex)
        {
            rect.X = BaseAddressColumnWidth;
            rect.Y = ByteHexValueSquareWidth;
            rect.Width = rect.Height = ByteHexValueSquareWidth;
            rect.Offset(colIndex * ByteHexValueSquareWidth, (rowIndex - rowAddressOffset) * ByteHexValueSquareWidth);
        }
        protected void getByteCharRect(ref Rectangle rect, int rowAddressOffset, int bytePos)
        {
            getByteCharRect(ref rect, rowAddressOffset, bytePos / m_colAddressOffsetSize, bytePos % m_colAddressOffsetSize);
        }
        protected void getByteCharRect(ref Rectangle rect, int rowAddressOffset, int rowIndex, int colIndex)
        {
            rect.X = BaseAddressColumnWidth + getDataWidth();
            rect.Y = ByteHexValueSquareWidth;
            rect.Width = ByteCharacterValueSquareWidth;
            rect.Height = ByteHexValueSquareWidth;
            rect.Offset(colIndex * ByteCharacterValueSquareWidth, (rowIndex - rowAddressOffset) * ByteHexValueSquareWidth);
        }
        protected bool isBytePosVisible(int rowAddressOffset, Rectangle boundary, Point bytePos)
        {
            int effectiveRowAddressOffset = getEffectiveRowOffset(rowAddressOffset, sbMainScrollBar.Maximum, boundary);
            return bytePos.Y >= effectiveRowAddressOffset && bytePos.Y <= effectiveRowAddressOffset + getRowOffsetsPageSize(boundary) - 1 &&
                bytePos.X >= 0 && bytePos.X < m_colAddressOffsetSize;
        }
        protected int getDataWidth()
        {
            return m_colAddressOffsetSize * ByteHexValueSquareWidth;
        }
        protected void normalizeLinearGradientBrushes(Brush brush, Point origin, int width, int height)
        {
            if (brush is LinearGradientBrush)
            {
                LinearGradientBrush lineareGradientBursh = brush as LinearGradientBrush;
                lineareGradientBursh.ResetTransform();
                lineareGradientBursh.TranslateTransform(origin.X, origin.Y);
                lineareGradientBursh.ScaleTransform(width / lineareGradientBursh.Rectangle.Width,
                    height / lineareGradientBursh.Rectangle.Height);
            }
        }
        #endregion
        #region GUI Draw Methods
        protected void drawCanvas(Graphics g, Rectangle boundary, int rowAddressOffest)
        {
            stringFormatCenter.Alignment = StringAlignment.Center;
            stringFormatCenter.LineAlignment = StringAlignment.Center;

            Point pt1 = new Point();
            Point pt2 = new Point();
            Rectangle rect = new Rectangle();

            /////////////////////////////////////////////////
            //draw base address and byte offset shading
            rect.X = boundary.Left;
            rect.Y = boundary.Top + ByteHexValueSquareWidth;
            rect.Width = BaseAddressColumnWidth;
            rect.Height = boundary.Height - ByteHexValueSquareWidth;
            normalizeLinearGradientBrushes(brshBaseOffset, rect.Location, rect.Width, rect.Height);
            g.FillRectangle(brshBaseOffset, rect);

            rect.X = boundary.Left + BaseAddressColumnWidth;
            rect.Y = boundary.Top;
            rect.Width = boundary.Width;
            rect.Height = ByteHexValueSquareWidth;
            normalizeLinearGradientBrushes(brshByteOffset, rect.Location, rect.Width, rect.Height);
            g.FillRectangle(brshByteOffset, rect);

            pt1.X = boundary.Left + BaseAddressColumnWidth;
            pt1.Y = boundary.Top;
            pt2.X = boundary.Left + BaseAddressColumnWidth;
            pt2.Y = boundary.Bottom;
            
            pt1.X--;
            pt2.X--;
            g.DrawLine(penWhite1, pt1, pt2);
            pt1.X++;
            pt2.X++;
            g.DrawLine(penGray1, pt1, pt2);

            pt1.X = boundary.Left;
            pt1.Y = boundary.Top + ByteHexValueSquareWidth;
            pt2.X = boundary.Right;
            pt2.Y = boundary.Top + ByteHexValueSquareWidth;

            pt1.Y--;
            pt2.Y--;
            g.DrawLine(penWhite1, pt1, pt2);
            pt1.Y++;
            pt2.Y++;
            g.DrawLine(penGray1, pt1, pt2);

            /////////////////////////////////////////////////
            //Draw verical grid
            pt1.Y = boundary.Top + ByteHexValueSquareWidth + 1;
            pt2.Y = boundary.Bottom;
            rect.Y = pt1.Y;
            rect.Width = ByteHexValueSquareWidth - 2;
            rect.Height = pt2.Y - pt1.Y;
            for (int i = 0; i < m_colAddressOffsetSize; ++i)
            {
                pt1.X = pt2.X = boundary.Left + BaseAddressColumnWidth + (i + 1) * ByteHexValueSquareWidth;

                //draw odd/even byte shades
                rect.X = pt1.X - ByteHexValueSquareWidth + 1;

                if (i % 2 == 0)
                {
                    normalizeLinearGradientBrushes(brshByteValEven, rect.Location, ByteHexValueSquareWidth, ByteHexValueSquareWidth);					
                    g.FillRectangle(brshByteValEven, rect);
                }
                else
                {
                    normalizeLinearGradientBrushes(brshByteValOdd, rect.Location, ByteHexValueSquareWidth, ByteHexValueSquareWidth);
                    g.FillRectangle(brshByteValOdd, rect);
                }

                rect.Offset(0, -ByteHexValueSquareWidth);
                rect.Height = ByteHexValueSquareWidth;

                g.DrawString(i.ToString("X2"), fontMain, Brushes.White, rect, stringFormatCenter);
                rect.Offset(-2, -2);
                g.DrawString(i.ToString("X2"), fontMainBold, Brushes.Black, rect, stringFormatCenter);
                rect.Offset(2, 2);
                rect.Offset(0, ByteHexValueSquareWidth);
                rect.Height = pt2.Y - pt1.Y;

                pt1.X--;
                pt2.X--;
                g.DrawLine(penGray1, pt1, pt2);
                pt1.X++;
                pt2.X++;
                g.DrawLine(penWhite1, pt1, pt2);
            }

            /////////////////////////////////////////////////
            //Draw horizontal grid and offset strings			
            rect.X = boundary.Left;
            rect.Width = BaseAddressColumnWidth;
            rect.Height = ByteHexValueSquareWidth - 2;

            pt1.X = boundary.Left;
            pt2.X = boundary.Right;
            int rowOffsetsPageSize = getRowOffsetsPageSize(boundary);
            int effectiveRowAddressOffset = getEffectiveRowOffset(rowAddressOffest, sbMainScrollBar.Maximum, boundary);
            int offset = effectiveRowAddressOffset * m_colAddressOffsetSize;
            for (int j = effectiveRowAddressOffset; j < effectiveRowAddressOffset + rowOffsetsPageSize; ++j)
            {
                pt1.Y = pt2.Y = boundary.Top + 2 * ByteHexValueSquareWidth + (j - effectiveRowAddressOffset) * ByteHexValueSquareWidth;
                //g.DrawLine(penBlack2, pt1, pt2);

                rect.Y = pt2.Y - ByteHexValueSquareWidth;
                g.DrawString(offset.ToString("X8"), fontMain, Brushes.White, rect, stringFormatCenter);
                rect.Offset(-2, -2);
                g.DrawString(offset.ToString("X8"), fontMainBold, Brushes.Black, rect, stringFormatCenter);
                rect.Offset(2, 2);
                offset += m_colAddressOffsetSize;

                pt1.Y--;
                pt2.Y--;
                g.DrawLine(penGray1, pt1, pt2);
                pt1.Y++;
                pt2.Y++;
                g.DrawLine(penWhite1, pt1, pt2);
            }

        }
                
        protected void drawData(Graphics g, Rectangle boundary, int rowAddressOffest)
        {
            if (DataLength > 0)
            {
                Rectangle rect = new Rectangle();

                int rowOffsetsPageSize = getRowOffsetsPageSize(boundary);
                int effectiveRowAddressOffset = getEffectiveRowOffset(rowAddressOffest, sbMainScrollBar.Maximum, boundary);

                int pageDataStartIndex = effectiveRowAddressOffset * m_colAddressOffsetSize;
                int pageDataLength = getDataRange(m_pageDataBuffer, pageDataStartIndex);
                char byteCharacter;
                Point bytePos;
                Brush byteCharBrush = null;
                bool bKeyExist = false;
                for (int i = 0; i < pageDataLength; ++i)
                {
                    bytePos = bytePosFromByteIndex(pageDataStartIndex + i);
                    
                    //draw bytes
                    getByteRect(ref rect, effectiveRowAddressOffset, pageDataStartIndex + i);
                    bKeyExist = m_dictEditedBytes.ContainsKey(bytePos);
                    if (bKeyExist)
                        g.DrawString(m_dictEditedBytes[bytePos].ToString("X2"), fontMain, brshEditedByteHexValue, rect, stringFormatCenter);
                    else
                        g.DrawString(m_pageDataBuffer[i].ToString("X2"), fontMain, brshByteHexValue, rect, stringFormatCenter);

                    //draw characters
                    getByteCharRect(ref rect, effectiveRowAddressOffset, pageDataStartIndex + i);
                    if (bKeyExist)
                    {
                        byteCharacter = (char)m_dictEditedBytes[bytePos];
                        byteCharBrush = brshEditedByteASCIIValue;
                    }
                    else
                    {
                        byteCharacter = (char)m_pageDataBuffer[i];
                        byteCharBrush = brshByteASCIIValue;
                    }
                    if (!char.IsControl((byteCharacter)))
                        g.DrawString(byteCharacter.ToString(), fontMain, byteCharBrush, rect, stringFormatCenter);
                    else
                        g.DrawString(".", fontMain, Brushes.Gray, rect, stringFormatCenter);
                }
            }
        }
        protected void drawSelection(Graphics g, Rectangle boundary, int rowAddressOffest)
        {
            if (isBytePosVisible(rowAddressOffest, boundary, m_selectedByte))
            {
                Rectangle selectedRect = new Rectangle();
                int effectiveRowAddressOffset = getEffectiveRowOffset(rowAddressOffest, sbMainScrollBar.Maximum, boundary);
                getByteRect(ref selectedRect, effectiveRowAddressOffset, m_selectedByte.Y, m_selectedByte.X);
                selectedRect.Inflate(-1, -1);
                g.FillRectangle(brshSelectionHighlight, selectedRect);
                getByteCharRect(ref selectedRect, effectiveRowAddressOffset, m_selectedByte.Y, m_selectedByte.X);
                g.FillRectangle(brshSelectionHighlight, selectedRect);
            }
        }
        
        protected void invalidateByte(int rowAddressOffset, Point bytePos)
        {
            Rectangle rectByte = new Rectangle();
            Rectangle rectChar = new Rectangle();
            getByteRect(ref rectByte, rowAddressOffset, bytePos.Y, bytePos.X);
            getByteCharRect(ref rectChar, rowAddressOffset, bytePos.Y, bytePos.X);
            Invalidate(rectByte);
            Invalidate(rectChar);
        }
        protected void invalidateByte(Point bytePos)
        {
            int effectiveRowAddressOffset = getEffectiveRowOffset(sbMainScrollBar.Value, sbMainScrollBar.Maximum, ClientRectangle);
            invalidateByte(effectiveRowAddressOffset, bytePos);
        }
        #endregion
        #endregion
    }
}
