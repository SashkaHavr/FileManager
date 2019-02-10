namespace WinFormsGUI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MoveBtn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getSelectedInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getCurrentInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastSearchResultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addStandartFileOpeningProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.listView1 = new System.Windows.Forms.ListView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.LeftOut = new System.Windows.Forms.Button();
            this.CopyBtn = new System.Windows.Forms.Button();
            this.RightOut = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.listView2 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MoveBtn
            // 
            this.MoveBtn.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
            this.MoveBtn.Location = new System.Drawing.Point(345, 140);
            this.MoveBtn.Name = "MoveBtn";
            this.MoveBtn.Size = new System.Drawing.Size(100, 38);
            this.MoveBtn.TabIndex = 0;
            this.MoveBtn.Text = "Move";
            this.MoveBtn.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionToolStripMenuItem,
            this.searchToolStripMenuItem1,
            this.logToolStripMenuItem,
            this.otherToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(782, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // actionToolStripMenuItem
            // 
            this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createDirectoryToolStripMenuItem,
            this.createFileToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.getSelectedInfoToolStripMenuItem,
            this.getCurrentInfoToolStripMenuItem});
            this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
            this.actionToolStripMenuItem.Size = new System.Drawing.Size(116, 24);
            this.actionToolStripMenuItem.Text = "System Entries";
            // 
            // createDirectoryToolStripMenuItem
            // 
            this.createDirectoryToolStripMenuItem.Name = "createDirectoryToolStripMenuItem";
            this.createDirectoryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.createDirectoryToolStripMenuItem.Size = new System.Drawing.Size(245, 26);
            this.createDirectoryToolStripMenuItem.Text = "Create Directory";
            // 
            // createFileToolStripMenuItem
            // 
            this.createFileToolStripMenuItem.Name = "createFileToolStripMenuItem";
            this.createFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.createFileToolStripMenuItem.Size = new System.Drawing.Size(245, 26);
            this.createFileToolStripMenuItem.Text = "Create File";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(245, 26);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(245, 26);
            this.renameToolStripMenuItem.Text = "Rename";
            // 
            // getSelectedInfoToolStripMenuItem
            // 
            this.getSelectedInfoToolStripMenuItem.Name = "getSelectedInfoToolStripMenuItem";
            this.getSelectedInfoToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.getSelectedInfoToolStripMenuItem.Size = new System.Drawing.Size(245, 26);
            this.getSelectedInfoToolStripMenuItem.Text = "Get Selected Info";
            // 
            // getCurrentInfoToolStripMenuItem
            // 
            this.getCurrentInfoToolStripMenuItem.Name = "getCurrentInfoToolStripMenuItem";
            this.getCurrentInfoToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.getCurrentInfoToolStripMenuItem.Size = new System.Drawing.Size(245, 26);
            this.getCurrentInfoToolStripMenuItem.Text = "Get Current Info";
            // 
            // searchToolStripMenuItem1
            // 
            this.searchToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem,
            this.lastSearchResultToolStripMenuItem});
            this.searchToolStripMenuItem1.Name = "searchToolStripMenuItem1";
            this.searchToolStripMenuItem1.Size = new System.Drawing.Size(65, 24);
            this.searchToolStripMenuItem1.Text = "Search";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.searchToolStripMenuItem.Text = "Search";
            // 
            // lastSearchResultToolStripMenuItem
            // 
            this.lastSearchResultToolStripMenuItem.Name = "lastSearchResultToolStripMenuItem";
            this.lastSearchResultToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.lastSearchResultToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.lastSearchResultToolStripMenuItem.Text = "Last Search Result";
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.logToolStripMenuItem.Text = "Log";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.showToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.showToolStripMenuItem.Text = "Show";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.clearToolStripMenuItem.Text = "Clear";
            // 
            // otherToolStripMenuItem
            // 
            this.otherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addStandartFileOpeningProgramToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
            this.otherToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.otherToolStripMenuItem.Text = "Other";
            // 
            // addStandartFileOpeningProgramToolStripMenuItem
            // 
            this.addStandartFileOpeningProgramToolStripMenuItem.Name = "addStandartFileOpeningProgramToolStripMenuItem";
            this.addStandartFileOpeningProgramToolStripMenuItem.Size = new System.Drawing.Size(316, 26);
            this.addStandartFileOpeningProgramToolStripMenuItem.Text = "Add standart file opening program";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(316, 26);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 429);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(782, 24);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 18);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 62);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(280, 353);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 31);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(280, 24);
            this.comboBox1.TabIndex = 9;
            // 
            // LeftOut
            // 
            this.LeftOut.Location = new System.Drawing.Point(298, 31);
            this.LeftOut.Name = "LeftOut";
            this.LeftOut.Size = new System.Drawing.Size(50, 25);
            this.LeftOut.TabIndex = 11;
            this.LeftOut.Text = "Out";
            this.LeftOut.UseVisualStyleBackColor = true;
            // 
            // CopyBtn
            // 
            this.CopyBtn.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
            this.CopyBtn.Location = new System.Drawing.Point(345, 280);
            this.CopyBtn.Name = "CopyBtn";
            this.CopyBtn.Size = new System.Drawing.Size(100, 38);
            this.CopyBtn.TabIndex = 13;
            this.CopyBtn.Text = "Copy";
            this.CopyBtn.UseVisualStyleBackColor = true;
            // 
            // RightOut
            // 
            this.RightOut.Location = new System.Drawing.Point(434, 32);
            this.RightOut.Name = "RightOut";
            this.RightOut.Size = new System.Drawing.Size(50, 25);
            this.RightOut.TabIndex = 16;
            this.RightOut.Text = "Out";
            this.RightOut.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(490, 32);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(280, 24);
            this.comboBox2.TabIndex = 15;
            // 
            // listView2
            // 
            this.listView2.Location = new System.Drawing.Point(490, 62);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(280, 353);
            this.listView2.TabIndex = 14;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Flat Folder icon.png");
            this.imageList1.Images.SetKeyName(1, "file.png");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 453);
            this.Controls.Add(this.RightOut);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.CopyBtn);
            this.Controls.Add(this.LeftOut);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MoveBtn);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button MoveBtn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button LeftOut;
        private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastSearchResultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Button CopyBtn;
        private System.Windows.Forms.ToolStripMenuItem getSelectedInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Button RightOut;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ToolStripMenuItem getCurrentInfoToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem addStandartFileOpeningProgramToolStripMenuItem;
    }
}