using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATFL
{
    public partial class OptionsForm : Form
    {
        public string TempDir { get; set; } = "";
        public string TempExt { get; set; } = "";
        public OptionsForm()
        {
            InitializeComponent();
        }

        private void ChangeDirButton_Click(object sender, EventArgs e)
        {
            DialogResult DR = folderBrowserDialog1.ShowDialog();
            if (DR == DialogResult.OK)
            {
                TempDir = folderBrowserDialog1.SelectedPath + "\\";
                DirNameLabel.Text = TempDir;
            }
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            DirNameLabel.Text = Program.R.DestDir;
            ExtensionNameLabel.Text = Program.R.Extension;
        }
    }
}
