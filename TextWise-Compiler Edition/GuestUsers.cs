using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Word = Microsoft.Office.Interop.Word;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Font;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using System.Drawing.Printing;
using PdfSharp.Pdf.Filters;
using To_Do_List_App;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace TextWise_Compiler_Edition
{
    public partial class GuestUsers : Form
    {
        private object colorDialog1;
        bool saved = true;

        public GuestUsers()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);

            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, 14, richTextBox1.Font.Style); // Adjust the size (e.g., 14)

            foreach (FontFamily fontFamily in FontFamily.Families)
            {
                comboBoxFont.Items.Add(fontFamily.Name);
            }

            // Set the default font in the ComboBox
            comboBoxFont.SelectedItem = richTextBox1.Font.FontFamily.Name;

            comboBoxFontSize.SelectedIndexChanged += comboBox2_SelectedIndexChanged;

            comboBoxFont.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            comboBoxFontSize.Items.AddRange(new object[] { 8, 10, 12, 14, 16, 18, 20 });
            comboBoxFontSize.DropDownStyle = ComboBoxStyle.DropDown;
            comboBoxFontSize.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxFontSize.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxFontSize.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            comboBoxFontSize.TextChanged += comboBox2_SelectedIndexChanged;


            ComboBoxTextColor.Items.AddRange(Enum.GetNames(typeof(KnownColor)));
            ComboBoxTextColor.DropDownStyle = ComboBoxStyle.DropDownList; // Set it to DropDownList to restrict to predefined colors
            ComboBoxTextColor.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;

        }



        // New File Open-------------------
        //checking if textbook is empty or not?

        private bool IsRichTextBoxEmpty()
        {
            return string.IsNullOrEmpty(richTextBox1.Text);
        }

        //asking user to save
        private bool AskUserToSaveChanges()
        {
            if (!IsRichTextBoxEmpty())
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        return SaveFile();
                    case DialogResult.No:
                        return true;
                    case DialogResult.Cancel:
                        return false;
                }
            }

            return true;
        }

        //save file

        private bool SaveFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    richTextBox1.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return false;
        }
        //menu new
        private void file_new_Click(object sender, EventArgs e)
        {
            if (AskUserToSaveChanges())
            {
                richTextBox1.Clear();
            }
        }

        //icon new
        private void hEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AskUserToSaveChanges())
            {
                richTextBox1.Clear();
            }
        }
        //Open file-------------------------------------------------------------------------------

        private void file_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
                currentFilePath = openFileDialog.FileName;
            }
        }

        //save option-----------------------------------------------------------------------------
        private string currentFilePath = string.Empty;
        private void file_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        //save as-------------------------------------------------------------------------------------

        private bool SaveAsFile(string filter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Files|*.txt|PDF Files|*.pdf|Word Files|*.doc|All Files|*.*",
                DefaultExt = "txt",
                AddExtension = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                string extension = Path.GetExtension(fileName).ToLower();

                try
                {
                    switch (extension)
                    {
                        case ".txt":
                            return SaveAsTxt(fileName);
                        case ".pdf":
                            return SaveAsPdf(fileName);
                        case ".doc":
                            return SaveAsDoc(fileName);
                        default:
                            // Handle other file formats or throw an exception if not supported
                            throw new NotSupportedException($"Saving as {extension} is not supported.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return false;
        }


        private bool SaveAsPdf(string fileName)
        {
            try
            {
                using (PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument())
                {
                    pdf.AddPage();

                    // Specify a font that supports Unicode characters
                    XFont font = new XFont("Arial Unicode MS", 12, XFontStyle.Regular);

                    using (XGraphics graphics = XGraphics.FromPdfPage(pdf.Pages[0]))
                    {
                        // Adjust the position and layout as needed
                        XTextFormatter formatter = new XTextFormatter(graphics);
                        XRect rect = new XRect(10, 10, pdf.Pages[0].Width, pdf.Pages[0].Height);
                        formatter.DrawString(richTextBox1.Text, font, XBrushes.Black, rect, XStringFormats.TopLeft);
                    }

                    // Save the PDF file with Unicode support
                    pdf.Save(fileName);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving PDF file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool SaveAsTxt(string fileName)
        {
            try
            {
                // Save the content in UTF-8 encoding
                File.WriteAllText(fileName, richTextBox1.Text, Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving TXT file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool SaveAsDoc(string fileName)
        {
            try
            {
                // Create a new Word application
                Word.Application wordApp = new Word.Application();

                // Add a new document to the Word application
                Word.Document doc = wordApp.Documents.Add();

                // Set the content of the Word document
                doc.Content.Text = richTextBox1.Text;

                // Save the document in the specified format (docx)
                doc.SaveAs2(fileName, Word.WdSaveFormat.wdFormatDocumentDefault);

                // Close the Word document and application
                doc.Close();
                wordApp.Quit();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving DOC file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void file_save_as_Click(object sender, EventArgs e)
        {
            SaveAsFile("All Files");

        }

        //Exit-------------------------------------------------------------------

        private void CloseApplication()
        {
            if (richTextBox1.Text == "")
            {
                Application.Exit();
            }
            else if (saved)
            {
                AskUserToSaveChanges();
                Application.Exit();
            }
            else
            {
                Application.Exit();
            }
        }

        private void file_exit_Click(object sender, EventArgs e)
        {
            CloseApplication();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AskUserToSaveChanges())
            {
                // If the user chooses to cancel the closing operation, set e.Cancel to true.
                e.Cancel = true;
            }
        }
        //Alignment------------------------------------------------------------------------------------
        private void SetTextAlignment(HorizontalAlignment alignment)
        {
            richTextBox1.SelectionAlignment = alignment;
        }
        private void fTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTextAlignment(HorizontalAlignment.Left);

        }
        private void ctToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTextAlignment(HorizontalAlignment.Center);
        }

        private void gToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTextAlignment(HorizontalAlignment.Right);

        }
        //Font Combox
        //font size
        private void ComboBoxFontSize_TextChanged(object sender, EventArgs e)
        {
            ApplySelectedFontSize();
        }
        private void ApplySelectedFontSize()
        {
            if (int.TryParse(comboBoxFontSize.Text, out int fontSize))
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, fontSize, richTextBox1.SelectionFont.Style);
            }
        }
        //font style
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFont = comboBoxFont.SelectedItem.ToString();
            richTextBox1.SelectionFont = new Font(selectedFont, richTextBox1.SelectionFont.Size, richTextBox1.SelectionFont.Style);
        }
        //font color
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ApplySelectedTextColor();
        }


        private void ApplySelectedTextColor()
        {
            if (Enum.TryParse(ComboBoxTextColor.SelectedItem.ToString(), out KnownColor knownColor))
            {
                Color color = Color.FromKnownColor(knownColor);
                richTextBox1.SelectionColor = color;
            }
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void heToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void rToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplaceForm f = new ReplaceForm(richTextBox1);
            f.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void coToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                richTextBox1.Copy();
            }
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                richTextBox1.Cut();
            }
        }

        private void paToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                richTextBox1.Paste();
            }
        }

        private void Menustrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            saved = false;

        }


        private void sToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void fToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog.Font;
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void iconNew_Click(object sender, EventArgs e)
        {
            if (AskUserToSaveChanges())
            {
                richTextBox1.Clear();
            }
        }

        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog.Color;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplySelectedFontSize();

        }

        private void paintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Draw newForm = new Draw();
            newForm.Show();
        }

        private void copToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                richTextBox1.Copy();
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                richTextBox1.Paste();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (richTextBox1.SelectionLength > 0)
            {
                richTextBox1.Cut();
            }
        }



        private void ShowFindDialog()
        {
            // Display the Find dialog
            richTextBox1.Find("");
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowFindDialog();


        }

        private void unToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void reToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }




        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void todoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToDoList to = new ToDoList();
            to.Show();
        }

        private bool isBold = false;
        private bool isUnderline = false;
        private bool isHighlight = false;



        private void UpdateTextFormatting()
        {
            // Set the font style based on user options
            FontStyle fontStyle = FontStyle.Regular;
            if (isBold)
                fontStyle |= FontStyle.Bold;
            if (isUnderline)
                fontStyle |= FontStyle.Underline;

            richTextBox1.SelectionFont = new Font(richTextBox1.Font, fontStyle);

            // Set the text color based on highlight option
            if (isHighlight)
                richTextBox1.SelectionBackColor = Color.Yellow;
            else
                richTextBox1.SelectionBackColor = richTextBox1.BackColor;
        }

        private void bold_Click_1(object sender, EventArgs e)
        {
            isBold = !isBold;
            UpdateTextFormatting();
        }

        private void underline_Click_1(object sender, EventArgs e)
        {
            isUnderline = !isUnderline;
            UpdateTextFormatting();
        }

        private void highlight_Click_1(object sender, EventArgs e)
        {
            isHighlight = !isHighlight;
            UpdateTextFormatting();
        }
        private void fToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FindForm f = new FindForm(richTextBox1);
            f.Show();
        }
        //compiler

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            saved = false;
        }

        private void buildRunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buildX();
            RunExecutable();
        }


        private void buildX()
        {
            SaveToCPPFile();
            try
            {
                string scFolder = "C:\\temp";
                string compilerLoc = "C:\\MinGW\\bin";
                string cppFileName = "temp.cpp";
                string outputFileName = "temp.exe";

                // Build the compilation command
                string command = $"{compilerLoc}\\g++.exe {scFolder}\\{cppFileName}";

                // Call the method to compile the C++ file
                CompileCppFile(command);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SaveToCPPFile()
        {
            try
            {
                string folderPath = "C:\\temp";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error creating folder: {0}", ex.Message);
            }
            saved = true;
            string scFolder = "C:\\temp";
            string filePath = Path.Combine(scFolder, "temp.cpp");
            try
            {
                // Convert the rich text to plain text
                string plainText = richTextBox1.Text;

                // Save the plain text to a CPP file
                File.WriteAllText(filePath, plainText);

                //MessageBox.Show("File saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CompileCppFile(string command)
        {

            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe"; // Use the actual path if g++ is not in the system PATH
                process.StartInfo.Arguments = $"/C {command}"; // /C tells cmd to run the command and then terminate
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();


                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    MessageBox.Show("Compilation successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Compilation failed:\n{output}\n{error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void RunExecutable()
        {
            string scFolder = "C:\\temp";
            string filePath = Path.Combine(scFolder, "temp.cpp");
            string outputFileName = "temp.exe";
            try
            {
                Process process = new Process();

                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = $"/K \"{scFolder}\\{outputFileName}\""; // Use /k to keep the console window open after command execution
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = false; // Do not redirect standard output
                process.StartInfo.RedirectStandardError = false; // Do not redirect standard error
                process.StartInfo.CreateNoWindow = false; // Keep the console window open

                process.Start();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while running the executable: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void find(object sender, EventArgs e)
        {
            FindForm f = new FindForm(richTextBox1);
            f.Show();
        }
        private PrintDocument printDocument = new PrintDocument();
        private PrintDialog printDialog = new PrintDialog();
        private PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
        private string documentToPrint = "";

        private void pToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show the print dialog
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // Set the document to print
                documentToPrint = richTextBox1.Text;
                printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument_PrintPage);


                // Print the document
                printDocument.Print();
            }
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Create a Font object for the font of the printed text
            Font printFont = new Font("Arial", 10);

            // Create a RectangleF to represent the margins of the page
            RectangleF margins = new RectangleF(e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Width, e.MarginBounds.Height);

            // Create a StringFormat object to format the text
            StringFormat format = new StringFormat();

            // Draw the text from the richTextBox on the printed page
            e.Graphics.DrawString(documentToPrint, printFont, Brushes.Black, margins, format);
        }

        private void file_print_Click(object sender, EventArgs e)
        {
            // Show the print dialog
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // Set the document to print
                documentToPrint = richTextBox1.Text;
                printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument_PrintPage);


                // Print the document
                printDocument.Print();
            }
        }
    }
}

