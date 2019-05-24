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
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.задачаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выполнитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.modeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.infoModeTab = new System.Windows.Forms.TabControl();
            this.theoryTabPage = new System.Windows.Forms.TabPage();
            this.theoryListBox = new System.Windows.Forms.ListBox();
            this.taskTabPage = new System.Windows.Forms.TabPage();
            this.taskListBox = new System.Windows.Forms.ListBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.buttonsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.stepButton = new System.Windows.Forms.Button();
            this.workFieldRichTextBox = new System.Windows.Forms.RichTextBox();
            this.QueueToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.справкаToolStripMenuItem,
            this.задачаToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(942, 33);
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
            // 
            // настроитьToolStripMenuItem
            // 
            this.настроитьToolStripMenuItem.Name = "настроитьToolStripMenuItem";
            this.настроитьToolStripMenuItem.Size = new System.Drawing.Size(181, 30);
            this.настроитьToolStripMenuItem.Text = "Настроить";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(93, 29);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // задачаToolStripMenuItem
            // 
            this.задачаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выполнитьToolStripMenuItem});
            this.задачаToolStripMenuItem.Name = "задачаToolStripMenuItem";
            this.задачаToolStripMenuItem.Size = new System.Drawing.Size(81, 29);
            this.задачаToolStripMenuItem.Text = "Задача";
            // 
            // выполнитьToolStripMenuItem
            // 
            this.выполнитьToolStripMenuItem.Enabled = false;
            this.выполнитьToolStripMenuItem.Name = "выполнитьToolStripMenuItem";
            this.выполнитьToolStripMenuItem.Size = new System.Drawing.Size(252, 30);
            this.выполнитьToolStripMenuItem.Text = "Выполнить";
            this.выполнитьToolStripMenuItem.Click += new System.EventHandler(this.ВыполнитьToolStripMenuItem_Click);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modeToolStripStatusLabel,
            this.QueueToolStripStatusLabel});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 420);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(942, 30);
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
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 33);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.infoModeTab);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(942, 387);
            this.splitContainer1.SplitterDistance = 262;
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
            this.infoModeTab.Size = new System.Drawing.Size(262, 387);
            this.infoModeTab.TabIndex = 0;
            // 
            // theoryTabPage
            // 
            this.theoryTabPage.Controls.Add(this.theoryListBox);
            this.theoryTabPage.Location = new System.Drawing.Point(4, 29);
            this.theoryTabPage.Name = "theoryTabPage";
            this.theoryTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.theoryTabPage.Size = new System.Drawing.Size(254, 354);
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
            this.theoryListBox.Size = new System.Drawing.Size(248, 348);
            this.theoryListBox.TabIndex = 0;
            // 
            // taskTabPage
            // 
            this.taskTabPage.Controls.Add(this.taskListBox);
            this.taskTabPage.Location = new System.Drawing.Point(4, 29);
            this.taskTabPage.Name = "taskTabPage";
            this.taskTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.taskTabPage.Size = new System.Drawing.Size(254, 354);
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
            this.taskListBox.Size = new System.Drawing.Size(248, 348);
            this.taskListBox.TabIndex = 0;
            this.taskListBox.ValueMember = "Operation";
            this.taskListBox.SelectedIndexChanged += new System.EventHandler(this.TaskListBox_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainer2.Size = new System.Drawing.Size(676, 387);
            this.splitContainer2.SplitterDistance = 44;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            // 
            // buttonsFlowLayoutPanel
            // 
            this.buttonsFlowLayoutPanel.Controls.Add(this.stepButton);
            this.buttonsFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.buttonsFlowLayoutPanel.Name = "buttonsFlowLayoutPanel";
            this.buttonsFlowLayoutPanel.Size = new System.Drawing.Size(676, 44);
            this.buttonsFlowLayoutPanel.TabIndex = 0;
            // 
            // stepButton
            // 
            this.stepButton.AutoSize = true;
            this.stepButton.Location = new System.Drawing.Point(3, 3);
            this.stepButton.Name = "stepButton";
            this.stepButton.Size = new System.Drawing.Size(75, 30);
            this.stepButton.TabIndex = 0;
            this.stepButton.Text = "Шаг";
            this.stepButton.UseVisualStyleBackColor = true;
            this.stepButton.Click += new System.EventHandler(this.StepButton_Click);
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
            this.workFieldRichTextBox.Size = new System.Drawing.Size(676, 342);
            this.workFieldRichTextBox.TabIndex = 0;
            this.workFieldRichTextBox.Text = "";
            // 
            // QueueToolStripStatusLabel
            // 
            this.QueueToolStripStatusLabel.Name = "QueueToolStripStatusLabel";
            this.QueueToolStripStatusLabel.Size = new System.Drawing.Size(84, 25);
            this.QueueToolStripStatusLabel.Text = "Очередь";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
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
        private System.Windows.Forms.ToolStripMenuItem выполнитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel QueueToolStripStatusLabel;
    }
}