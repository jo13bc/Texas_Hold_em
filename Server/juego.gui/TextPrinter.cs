using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.juego.gui
{
    public class TextPrinter
    {
        public enum VerticalAlign {
            TOP, MIDDLE, BOTTOM
        }
        public enum HorizontalAlign {
            LEFT,CENTET,RIGHT
        }
        private Font font;
        private Color color;
        private int width, height;
        private VerticalAlign vAlign = VerticalAlign.TOP;
        private HorizontalAlign hAlign = HorizontalAlign.LEFT

        public Font GetFont() {
            return font;
        }
        public void setFont(Font font) {
            this.font = font;
        }
        public int getWidth() {
            return width;
        }
        public void setWidth(int width) {
            this.width = width;
        }
        public int getHeight() {
            return height;
        }
        public void setHeigt(int height) {
            this.height = height;
        }
        public VerticalAlign GetVerticalAlign() {
            return vAlign;
        } 
        public void setVerticalAlign(VerticalAlign vAlign) {
            this.vAlign = vAlign;
        }
        public HorizontalAlign getHorizontalAlign() {
            return hAlign;
        }
        public void setHorizontalAlign(HorizontalAlign hAlign)
        {
            this.hAlign = hAlign;
        }
        public ConsoleColor getColor() {
            return color;
        }
        public void setColor(ConsoleColor color)
        {
            this.color = color;
        }
        private int getOffSetX(int widthText) {
            int result = 0;
            if (hAlign == HorizontalAlign.CENTER)
            {
                result = (width - widthText) / 2;
            }
            else if (hAlign == HorizontalAlign.RIGHT) {
                result = width - widthText;
            }
            return result;
        }
        private int getOffSetY(int ascent, int descent) {
            int result = ascent;
            if (vAlign == VerticalAlign.MIDDLE)
            {
                result = (height + ascent - descent) / 2;

            }
            else if (vAlign == VerticalAlign.BOTTOM) {
                result = height - descent;

            }
            return result;
        }
        public void print(Graphics g, string text, int x, int y) {
            g.setColor(color);
            g.setFont(font);
            FontMetrics fm = g.getFontMetrics(front);
            int widthText = fm.stringWidth(text);
            g.drawstring(text,
                x + getOffSetX(widthText),
                y + getOffSetY(fm.getAscent(), fm.getDescent()));


        }

    }
}
