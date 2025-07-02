using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TextWise_Compiler_Edition
{
    public partial class Draw : Form
    {
        private bool isDrawing;
        private Point lastPoint;
        private Pen drawPen;
        private Bitmap drawingBitmap;

        public Draw()
        {
            InitializeComponent();
            InitializeDrawing();
        }

        private void InitializeDrawing()
        {
            isDrawing = false;
            drawPen = new Pen(Color.Black, 2);
            drawingBitmap = new Bitmap(drawingPanel.Width, drawingPanel.Height);

            drawingPanel.MouseDown += DrawingPanel_MouseDown;
            drawingPanel.MouseMove += DrawingPanel_MouseMove;
            drawingPanel.MouseUp += DrawingPanel_MouseUp;
            drawingPanel.Paint += DrawingPanel_Paint;

            drawingPanel.DoubleBuffered(true); // Enable double buffering for the panel
        }

        private void DrawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            lastPoint = e.Location;
        }

        private void DrawingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                using (Graphics g = Graphics.FromImage(drawingBitmap))
                {
                    g.DrawLine(drawPen, lastPoint, e.Location);
                    lastPoint = e.Location;
                }

                drawingPanel.Invalidate(); // Force the panel to repaint
            }
        }

        private void DrawingPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(drawingBitmap, Point.Empty);
        }

        private void ChooseColor(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    drawPen.Color = colorDialog.Color;
                }
            }
        }

        private void ClearDrawing(object sender, EventArgs e)
        {
            using (Graphics g = Graphics.FromImage(drawingBitmap))
            {
                g.Clear(drawingPanel.BackColor);
            }

            drawingPanel.Invalidate();
        }

        private void SaveImage(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|GIF Image|*.gif|TIFF Image|*.tif|Bitmap Image|*.bmp|Icon|*.ico|Windows Metafile|*.emf|Enhanced Metafile|*.emf";
                saveFileDialog.Title = "Save Image";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ImageFormat selectedFormat = ImageFormat.Jpeg;

                        string extension = System.IO.Path.GetExtension(saveFileDialog.FileName);
                        switch (extension.ToLower())
                        {
                            case ".png":
                                selectedFormat = ImageFormat.Png;
                                break;
                            case ".gif":
                                selectedFormat = ImageFormat.Gif;
                                break;
                            case ".tif":
                            case ".tiff":
                                selectedFormat = ImageFormat.Tiff;
                                break;
                            case ".bmp":
                                selectedFormat = ImageFormat.Bmp;
                                break;
                            case ".ico":
                                selectedFormat = ImageFormat.Icon;
                                break;
                            case ".emf":
                                selectedFormat = ImageFormat.Emf;
                                break;
                                // For .jpg or other extensions, default to JPEG
                        }

                        using (Bitmap saveBitmap = new Bitmap(drawingPanel.Width, drawingPanel.Height))
                        {
                            drawingPanel.DrawToBitmap(saveBitmap, new Rectangle(0, 0, drawingPanel.Width, drawingPanel.Height));
                            saveBitmap.Save(saveFileDialog.FileName, selectedFormat);
                        }

                        MessageBox.Show("Image saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Draw_Load(object sender, EventArgs e)
        {
            // Additional initialization code if needed
        }
    }

    public static class ControlExtensions
    {
        public static void DoubleBuffered(this Control control, bool enable)
        {
            var doubleBufferProperty = typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            doubleBufferProperty?.SetValue(control, enable, null);
        }
    }
}
