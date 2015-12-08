﻿namespace AvalonStudio.TextEditor.Rendering
{
    using Document;
    using Perspex;
    using Perspex.Media;

    class PragmaMarkTextLineTransformer : IDocumentLineTransformer
    {
        private SolidColorBrush pragmaBrush = new SolidColorBrush(Color.FromArgb(0x88, 0xB8, 0x48, 0xFF));
        private SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(0xD0, 0xB8, 0x48, 0xFF));

        public void TransformLine(TextView textView, DrawingContext context, Rect lineBounds, DocumentLine line, FormattedText formattedText)
        {
            if (formattedText.Text.Contains("#pragma mark"))
            {
                int startIndex = formattedText.Text.IndexOf("#pragma mark");

                formattedText.SetForegroundBrush(pragmaBrush, startIndex, 12);
                formattedText.SetForegroundBrush(brush, startIndex + 12, formattedText.Text.Length - 12);
            }
        }
    }
}
