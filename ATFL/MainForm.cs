using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ATFL
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            taskListBox.DataSource = Tasks.Operation;
            modeToolStripStatusLabel.Text = "Студент";
        }

        private void ModeToolStripStatusLabel_Click(object sender, EventArgs e)
        {
            if (modeToolStripStatusLabel.Text == "Студент")
            {
                modeToolStripStatusLabel.Text = "Редактор";
            }
            else
            {
                modeToolStripStatusLabel.Text = "Студент";
            }
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            if (Program.R.Q.Count > 0) workFieldRichTextBox.AppendText(Program.R.Q.Dequeue());
            QueueToolStripStatusLabel.Text = Program.R.Q.Count + " действий";
        }

        private void ВыполнитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string testring = "s1: a -> s1, s1: a -> s2, s2: b -> s3, s3: a -> s1 | s1 s3";
            string Source = "testmachine3";
            //using (StreamReader fs = new StreamReader(Source + Program.R.Extension))
            {
                //while (true)
                {
                    //testring = fs.ReadLine();
                    //if (testring == null || testring == "q") break;
                    string TaskName = (taskListBox.SelectedItem as Task).Name;
                    MessageBox.Show(Program.R.MakeReport(TaskName, (taskListBox.SelectedItem as Task).Operation, testring));
                    stepButton.Enabled = true;
                    QueueToolStripStatusLabel.Text = Program.R.Q.Count + " действий";
                }
            }
        }

        private void TaskListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (taskListBox.SelectedItems.Count > 0) 
                выполнитьToolStripMenuItem.Enabled = true;
        }
    }
}
