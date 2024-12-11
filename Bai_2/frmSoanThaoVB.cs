using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai_2
{
    public partial class frmSoanThaoVB : Form
    {
        private string savedFilePath;
        public frmSoanThaoVB()
        {
            InitializeComponent();
        }

        private void frmSoanThaoVB_Load(object sender, EventArgs e)
        {
            loadFont();
            loadSize();
            rtbText.Font = new Font("Tahoma", 14, FontStyle.Regular);
        }

        private void loadFont()
        {
            foreach (FontFamily fontFamily in new InstalledFontCollection().Families)
            {
                cbxFont.Items.Add(fontFamily.Name);
            }
            cbxFont.SelectedItem = "Tahoma";

            cbxFont.SelectedIndexChanged += (s, e) => UpdateFont();
            
        }

        private void loadSize()
        {
            int[] sizeValues = new int[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

            cbxSize.ComboBox.DataSource = sizeValues;
            cbxSize.SelectedItem = 14;

            cbxSize.SelectedIndexChanged += (s, e) => UpdateFont();
        }

        private void UpdateFont()
        {
            if (rtbText.SelectionFont != null)
            {
                string selectedFont = cbxFont.SelectedItem?.ToString() ?? "Tahoma";
                int selectedSize = int.Parse(cbxSize.SelectedItem.ToString());

                FontStyle currentStyle = rtbText.SelectionFont.Style;
                rtbText.SelectionFont = new Font(selectedFont, selectedSize, currentStyle);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbText.Clear();
            rtbText.Font = new Font("Tahoma", 14);
            rtbText.ForeColor = Color.Black; 
            rtbText.BackColor = Color.White;
            savedFilePath = string.Empty;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|Rich Text files (*.rtf)|*.rtf";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog.FileName;
                savedFilePath = openFileDialog.FileName;

                try
                {
                    // Kiểm tra đuôi tệp và mở tệp với loại xử lý thích hợp
                    if (Path.GetExtension(selectedFileName).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        rtbText.LoadFile(selectedFileName, RichTextBoxStreamType.PlainText); // Xử lý văn bản thuần
                    }
                    else if (Path.GetExtension(selectedFileName).Equals(".rtf", StringComparison.OrdinalIgnoreCase))
                    {
                        rtbText.LoadFile(selectedFileName, RichTextBoxStreamType.RichText); // Xử lý văn bản RTF
                    }

                    MessageBox.Show("Tập tin đã được mở thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi trong quá trình mở tập tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            /*rtbText.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|RichText files (*.rtf)|*.rtf";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog.FileName;
                savedFilePath = openFileDialog.FileName;
                try
                {
                    if (Path.GetExtension(selectedFileName).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        rtbText.LoadFile(selectedFileName, RichTextBoxStreamType.PlainText);
                    }
                    else
                    {
                        rtbText.LoadFile(selectedFileName, RichTextBoxStreamType.RichText);
                    }
                    MessageBox.Show("Tập tin đã được mở thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi trong quá trình mở tập tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }*/
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(savedFilePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf";
                saveFileDialog.DefaultExt = "rtf";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        rtbText.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                        savedFilePath = saveFileDialog.FileName;
                        MessageBox.Show("Tập tin đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi trong quá trình lưu tập tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                try
                {
                    rtbText.SaveFile(savedFilePath, RichTextBoxStreamType.RichText);
                    MessageBox.Show("Tập tin đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi trong quá trình lưu tập tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            if (rtbText.SelectionFont != null)
            {
                FontStyle style = rtbText.SelectionFont.Style;
                if (rtbText.SelectionFont.Bold)
                {
                    style &= ~FontStyle.Bold;
                }
                else
                {
                    style |= FontStyle.Bold;
                }
                rtbText.SelectionFont = new Font(rtbText.SelectionFont, style);
            }
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            if (rtbText.SelectionFont != null)
            {
                FontStyle style = rtbText.SelectionFont.Style;
                if (rtbText.SelectionFont.Italic)
                {
                    style &= ~FontStyle.Italic;
                }
                else
                {
                    style |= FontStyle.Italic;
                }
                rtbText.SelectionFont = new Font(rtbText.SelectionFont, style);
            }
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            if (rtbText.SelectionFont != null)
            {
                FontStyle style = rtbText.SelectionFont.Style;
                if (rtbText.SelectionFont.Underline)
                {
                    style &= ~FontStyle.Underline;
                }
                else
                {
                    style |= FontStyle.Underline;
                }
                rtbText.SelectionFont = new Font(rtbText.SelectionFont, style);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate { this.Close(); });
        }
    }
}
