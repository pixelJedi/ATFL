namespace ATFL
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настроитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.задачаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вывестиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сбросToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.modeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.sourceToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.queueToolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.CountToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.infoModeTab = new System.Windows.Forms.TabControl();
            this.theoryTabPage = new System.Windows.Forms.TabPage();
            this.theoryListBox = new System.Windows.Forms.ListBox();
            this.taskTabPage = new System.Windows.Forms.TabPage();
            this.taskListBox = new System.Windows.Forms.ListBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.buttonsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.stepButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.TestString = new System.Windows.Forms.TextBox();
            this.workFieldRichTextBox = new System.Windows.Forms.RichTextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.показатьФорматToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.infoModeTab.SuspendLayout();
            this.theoryTabPage.SuspendLayout();
            this.taskTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.buttonsFlowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.задачаToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(978, 33);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "Строка меню";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.настроитьToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(181, 30);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.ОткрытьToolStripMenuItem_Click);
            // 
            // настроитьToolStripMenuItem
            // 
            this.настроитьToolStripMenuItem.Name = "настроитьToolStripMenuItem";
            this.настроитьToolStripMenuItem.Size = new System.Drawing.Size(181, 30);
            this.настроитьToolStripMenuItem.Text = "Настроить";
            this.настроитьToolStripMenuItem.Click += new System.EventHandler(this.НастроитьToolStripMenuItem_Click);
            // 
            // задачаToolStripMenuItem
            // 
            this.задачаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вывестиToolStripMenuItem,
            this.сбросToolStripMenuItem,
            this.показатьФорматToolStripMenuItem});
            this.задачаToolStripMenuItem.Name = "задачаToolStripMenuItem";
            this.задачаToolStripMenuItem.Size = new System.Drawing.Size(81, 29);
            this.задачаToolStripMenuItem.Text = "Задача";
            // 
            // вывестиToolStripMenuItem
            // 
            this.вывестиToolStripMenuItem.Enabled = false;
            this.вывестиToolStripMenuItem.Name = "вывестиToolStripMenuItem";
            this.вывестиToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.вывестиToolStripMenuItem.Size = new System.Drawing.Size(225, 30);
            this.вывестиToolStripMenuItem.Text = "Вывести";
            this.вывестиToolStripMenuItem.Click += new System.EventHandler(this.ВывестиToolStripMenuItem_Click);
            // 
            // сбросToolStripMenuItem
            // 
            this.сбросToolStripMenuItem.Enabled = false;
            this.сбросToolStripMenuItem.Name = "сбросToolStripMenuItem";
            this.сбросToolStripMenuItem.Size = new System.Drawing.Size(225, 30);
            this.сбросToolStripMenuItem.Text = "Сброс";
            this.сбросToolStripMenuItem.Click += new System.EventHandler(this.СбросToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(93, 29);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modeToolStripStatusLabel,
            this.sourceToolStripStatusLabel,
            this.queueToolStripProgressBar,
            this.CountToolStripStatusLabel});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 420);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(978, 30);
            this.mainStatusStrip.TabIndex = 1;
            this.mainStatusStrip.Text = "Строка состояния";
            // 
            // modeToolStripStatusLabel
            // 
            this.modeToolStripStatusLabel.Name = "modeToolStripStatusLabel";
            this.modeToolStripStatusLabel.Size = new System.Drawing.Size(67, 25);
            this.modeToolStripStatusLabel.Text = "Режим";
            this.modeToolStripStatusLabel.Click += new System.EventHandler(this.ModeToolStripStatusLabel_Click);
            // 
            // sourceToolStripStatusLabel
            // 
            this.sourceToolStripStatusLabel.Name = "sourceToolStripStatusLabel";
            this.sourceToolStripStatusLabel.Size = new System.Drawing.Size(155, 25);
            this.sourceToolStripStatusLabel.Text = "Источник данных";
            // 
            // queueToolStripProgressBar
            // 
            this.queueToolStripProgressBar.AutoToolTip = true;
            this.queueToolStripProgressBar.Name = "queueToolStripProgressBar";
            this.queueToolStripProgressBar.Size = new System.Drawing.Size(100, 24);
            this.queueToolStripProgressBar.ToolTipText = "Ожидание";
            // 
            // CountToolStripStatusLabel
            // 
            this.CountToolStripStatusLabel.Name = "CountToolStripStatusLabel";
            this.CountToolStripStatusLabel.Size = new System.Drawing.Size(19, 25);
            this.CountToolStripStatusLabel.Text = "-";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 33);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.infoModeTab);
            this.splitContainer1.Panel1MinSize = 150;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(978, 387);
            this.splitContainer1.SplitterDistance = 237;
            this.splitContainer1.TabIndex = 2;
            // 
            // infoModeTab
            // 
            this.infoModeTab.Controls.Add(this.theoryTabPage);
            this.infoModeTab.Controls.Add(this.taskTabPage);
            this.infoModeTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoModeTab.Location = new System.Drawing.Point(0, 0);
            this.infoModeTab.Name = "infoModeTab";
            this.infoModeTab.SelectedIndex = 0;
            this.infoModeTab.Size = new System.Drawing.Size(237, 387);
            this.infoModeTab.TabIndex = 0;
            // 
            // theoryTabPage
            // 
            this.theoryTabPage.Controls.Add(this.theoryListBox);
            this.theoryTabPage.Location = new System.Drawing.Point(4, 29);
            this.theoryTabPage.Name = "theoryTabPage";
            this.theoryTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.theoryTabPage.Size = new System.Drawing.Size(229, 354);
            this.theoryTabPage.TabIndex = 0;
            this.theoryTabPage.Text = "Теория";
            this.theoryTabPage.UseVisualStyleBackColor = true;
            // 
            // theoryListBox
            // 
            this.theoryListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theoryListBox.FormattingEnabled = true;
            this.theoryListBox.ItemHeight = 20;
            this.theoryListBox.Location = new System.Drawing.Point(3, 3);
            this.theoryListBox.Name = "theoryListBox";
            this.theoryListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.theoryListBox.Size = new System.Drawing.Size(223, 348);
            this.theoryListBox.TabIndex = 0;
            // 
            // taskTabPage
            // 
            this.taskTabPage.Controls.Add(this.taskListBox);
            this.taskTabPage.Location = new System.Drawing.Point(4, 29);
            this.taskTabPage.Name = "taskTabPage";
            this.taskTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.taskTabPage.Size = new System.Drawing.Size(229, 354);
            this.taskTabPage.TabIndex = 1;
            this.taskTabPage.Text = "Практика";
            this.taskTabPage.UseVisualStyleBackColor = true;
            // 
            // taskListBox
            // 
            this.taskListBox.DisplayMember = "Name";
            this.taskListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskListBox.FormattingEnabled = true;
            this.taskListBox.ItemHeight = 20;
            this.taskListBox.Location = new System.Drawing.Point(3, 3);
            this.taskListBox.Name = "taskListBox";
            this.taskListBox.Size = new System.Drawing.Size(223, 348);
            this.taskListBox.TabIndex = 0;
            this.taskListBox.ValueMember = "Operation";
            this.taskListBox.SelectedIndexChanged += new System.EventHandler(this.TaskListBox_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.buttonsFlowLayoutPanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.workFieldRichTextBox);
            this.splitContainer2.Size = new System.Drawing.Size(737, 387);
            this.splitContainer2.SplitterDistance = 38;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            // 
            // buttonsFlowLayoutPanel
            // 
            this.buttonsFlowLayoutPanel.Controls.Add(this.stepButton);
            this.buttonsFlowLayoutPanel.Controls.Add(this.TestString);
            this.buttonsFlowLayoutPanel.Controls.Add(this.NextButton);
            this.buttonsFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.buttonsFlowLayoutPanel.MinimumSize = new System.Drawing.Size(750, 0);
            this.buttonsFlowLayoutPanel.Name = "buttonsFlowLayoutPanel";
            this.buttonsFlowLayoutPanel.Size = new System.Drawing.Size(750, 38);
            this.buttonsFlowLayoutPanel.TabIndex = 0;
            // 
            // stepButton
            // 
            this.stepButton.AutoSize = true;
            this.stepButton.Enabled = false;
            this.stepButton.Location = new System.Drawing.Point(3, 3);
            this.stepButton.Name = "stepButton";
            this.stepButton.Size = new System.Drawing.Size(75, 30);
            this.stepButton.TabIndex = 0;
            this.stepButton.Text = "Шаг";
            this.stepButton.UseVisualStyleBackColor = true;
            this.stepButton.Click += new System.EventHandler(this.StepButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.AutoSize = true;
            this.NextButton.Enabled = false;
            this.NextButton.Location = new System.Drawing.Point(670, 3);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(52, 30);
            this.NextButton.TabIndex = 1;
            this.NextButton.Text = ">";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // TestString
            // 
            this.TestString.Dock = System.Windows.Forms.DockStyle.Top;
            this.TestString.Location = new System.Drawing.Point(84, 3);
            this.TestString.Name = "TestString";
            this.TestString.ReadOnly = true;
            this.TestString.Size = new System.Drawing.Size(580, 26);
            this.TestString.TabIndex = 2;
            this.TestString.WordWrap = false;
            // 
            // workFieldRichTextBox
            // 
            this.workFieldRichTextBox.AccessibleName = "WorkField";
            this.workFieldRichTextBox.AccessibleRole = System.Windows.Forms.AccessibleRole.Document;
            this.workFieldRichTextBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.workFieldRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workFieldRichTextBox.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.workFieldRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.workFieldRichTextBox.Name = "workFieldRichTextBox";
            this.workFieldRichTextBox.ReadOnly = true;
            this.workFieldRichTextBox.Size = new System.Drawing.Size(737, 348);
            this.workFieldRichTextBox.TabIndex = 0;
            this.workFieldRichTextBox.Text = "";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // показатьФорматToolStripMenuItem
            // 
            this.показатьФорматToolStripMenuItem.Name = "показатьФорматToolStripMenuItem";
            this.показатьФорматToolStripMenuItem.Size = new System.Drawing.Size(252, 30);
            this.показатьФорматToolStripMenuItem.Text = "Показать формат";
            this.показатьФорматToolStripMenuItem.Click += new System.EventHandler(this.показатьФорматToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(950, 300);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.infoModeTab.ResumeLayout(false);
            this.theoryTabPage.ResumeLayout(false);
            this.taskTabPage.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.buttonsFlowLayoutPanel.ResumeLayout(false);
            this.buttonsFlowLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl infoModeTab;
        private System.Windows.Forms.TabPage theoryTabPage;
        private System.Windows.Forms.TabPage taskTabPage;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.FlowLayoutPanel buttonsFlowLayoutPanel;
        private System.Windows.Forms.RichTextBox workFieldRichTextBox;
        private System.Windows.Forms.Button stepButton;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ListBox theoryListBox;
        private System.Windows.Forms.ListBox taskListBox;
        private System.Windows.Forms.ToolStripMenuItem настроитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel modeToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem задачаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вывестиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сбросToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar queueToolStripProgressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.TextBox TestString;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel sourceToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel CountToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem показатьФорматToolStripMenuItem;
    }
}