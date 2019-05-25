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
            sourceToolStripStatusLabel.Text = Program.R.Source;
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
            if (Program.R.GetNextStep(out string nextStep))
            {
                int rest = Program.R.StepsLeft;
                queueToolStripProgressBar.ToolTipText = rest - 1 + " осталось";
                CountToolStripStatusLabel.Text = ++queueToolStripProgressBar.Value + "/" + queueToolStripProgressBar.Maximum;
                workFieldRichTextBox.AppendText(nextStep);
                workFieldRichTextBox.ScrollToCaret();
            }
            else
            {
                stepButton.Enabled = false;
                CountToolStripStatusLabel.Text = "Всё";
            }
        }
        private void ВывестиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            СбросToolStripMenuItem_Click(this, e);
            backgroundWorker1.RunWorkerAsyn​c();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (Program.R.PassToNextInput())
            {
                SetNewTaskData();
                TestString.Text = Program.R.Q.Peek().message;
            }
            else NextButton.Enabled = false;
        }

        private void TaskListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            вывестиToolStripMenuItem.Enabled = (taskListBox.SelectedItems.Count > 0);
        }

        private void СбросToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearScreen();
            Program.R.Clear();
            сбросToolStripMenuItem.Enabled = false;
        }

        private void НастроитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm OF = new OptionsForm();
            if (OF.ShowDialog() == DialogResult.OK)
            {
                Program.R.DestDir   = OF.TempDir;
                Program.R.Extension = OF.TempExt;
            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            using (StreamReader fs = new StreamReader(Program.R.SourceFileName))
            {
                while (true)
                {
                    string testring = fs.ReadLine();
                    if (testring == null || testring == "q") break;
                    string TaskName = (taskListBox.SelectedItem as Task).Name;
                    Program.R.MakeReport(TaskName, (taskListBox.SelectedItem as Task).Operation, testring);
                }
            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetNewTaskData();
            stepButton.Enabled = true;
        }

        private void SetNewTaskData()
        {
            ClearScreen();
            сбросToolStripMenuItem.Enabled = true;
            if (Program.R.Q.Where(s => s.flag == 'e').Count()>0) NextButton.Enabled = true;
            TestString.Text = Program.R.Q.Peek().message;
            int count = Program.R.StepsLeft;
            queueToolStripProgressBar.Maximum = count;
            queueToolStripProgressBar.ToolTipText = count - 1 + " осталось";
            CountToolStripStatusLabel.Text = count.ToString();
            stepButton.Enabled = true;
        }
        private void ClearScreen()
        {
            workFieldRichTextBox.Clear();
            queueToolStripProgressBar.Value = 0;
            CountToolStripStatusLabel.Text = "-";
        }

        private void ОткрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult DR = openFileDialog1.ShowDialog();
            if (DR == DialogResult.OK)
            {
                Program.R.Source = openFileDialog1.FileName;
                sourceToolStripStatusLabel.Text = openFileDialog1.FileName;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                StepButton_Click(sender, e);
        }

        private void показатьФорматToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show((taskListBox.SelectedItem as Task).Format);
        }
    }
}
