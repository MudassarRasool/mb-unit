// Copyright 2008 MbUnit Project - http://www.mbunit.com/
// Portions Copyright 2000-2004 Jonathan De Halleux, Jamie Cansdale
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Gallio.Icarus.Controls;
using TestStatusBar = Gallio.Icarus.Controls.TestStatusBar;
using TestTreeView = Gallio.Icarus.Controls.TestTreeView;
using TestResultsList = Gallio.Icarus.Controls.TestResultsList;
using TestResultsGraph = Gallio.Icarus.Controls.TestResultsGraph;

namespace Gallio.Icarus
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.fileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.assembliesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAssemblyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAssemblyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.newProjectToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openProjectToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.reloadToolbarButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.startButton = new System.Windows.Forms.ToolStripButton();
            this.stopButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolbarButton = new System.Windows.Forms.ToolStripButton();
            this.treeImages = new System.Windows.Forms.ImageList(this.components);
            this.stateImages = new System.Windows.Forms.ImageList(this.components);
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.projectTabs = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.testTree = new Gallio.Icarus.Controls.TestTreeView();
            this.testTreeMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.expandAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.expandFailedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.resetTestsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.removeAssemblyToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.assemblyList = new System.Windows.Forms.ListView();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.assemblyListMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeAssemblyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeFilterCombo = new System.Windows.Forms.ComboBox();
            this.lblSortTreeBy = new System.Windows.Forms.Label();
            this.testResultsTabs = new System.Windows.Forms.TabControl();
            this.testResultsTabPage = new System.Windows.Forms.TabPage();
            this.testResultsList = new Gallio.Icarus.Controls.TestResultsList();
            this.TestCol = new System.Windows.Forms.ColumnHeader();
            this.ResultCol = new System.Windows.Forms.ColumnHeader();
            this.DurationCol = new System.Windows.Forms.ColumnHeader();
            this.TypeCol = new System.Windows.Forms.ColumnHeader();
            this.NamespaceCol = new System.Windows.Forms.ColumnHeader();
            this.AssemblyCol = new System.Windows.Forms.ColumnHeader();
            this.resultsFilterPanel = new System.Windows.Forms.Panel();
            this.filterTestResultsCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.logStreamsTabPage = new System.Windows.Forms.TabPage();
            this.logBody = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.logStream = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.performanceMonitorTabPage = new System.Windows.Forms.TabPage();
            this.testResultsGraph = new Gallio.Icarus.Controls.TestResultsGraph();
            this.panel2 = new System.Windows.Forms.Panel();
            this.graphsFilterBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.reportTabPage = new System.Windows.Forms.TabPage();
            this.reportViewer = new System.Windows.Forms.WebBrowser();
            this.panel4 = new System.Windows.Forms.Panel();
            this.reportTypes = new System.Windows.Forms.ComboBox();
            this.btnSaveReportAs = new System.Windows.Forms.Button();
            this.lblReportType = new System.Windows.Forms.Label();
            this.panelResults = new System.Windows.Forms.Panel();
            this.testProgressStatusBar = new Gallio.Icarus.Controls.TestStatusBar();
            this.label2 = new System.Windows.Forms.Label();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.projectTabs.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.testTreeMenuStrip.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.assemblyListMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.testResultsTabs.SuspendLayout();
            this.testResultsTabPage.SuspendLayout();
            this.resultsFilterPanel.SuspendLayout();
            this.logStreamsTabPage.SuspendLayout();
            this.panel3.SuspendLayout();
            this.performanceMonitorTabPage.SuspendLayout();
            this.panel2.SuspendLayout();
            this.reportTabPage.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelResults.SuspendLayout();
            this.trayMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.assembliesToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1003, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "Main Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectMenuItem,
            this.toolStripSeparator3,
            this.saveProjectToolStripMenuItem,
            this.saveProjectAsToolStripMenuItem,
            this.toolStripSeparator4,
            this.fileExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newProjectToolStripMenuItem.Image")));
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.newProjectToolStripMenuItem.Text = "&New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // openProjectMenuItem
            // 
            this.openProjectMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openProjectMenuItem.Image")));
            this.openProjectMenuItem.Name = "openProjectMenuItem";
            this.openProjectMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openProjectMenuItem.Size = new System.Drawing.Size(188, 22);
            this.openProjectMenuItem.Text = "&Open Project";
            this.openProjectMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(185, 6);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveProjectToolStripMenuItem.Image")));
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.saveProjectToolStripMenuItem.Text = "&Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // saveProjectAsToolStripMenuItem
            // 
            this.saveProjectAsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveProjectAsToolStripMenuItem.Image")));
            this.saveProjectAsToolStripMenuItem.Name = "saveProjectAsToolStripMenuItem";
            this.saveProjectAsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.saveProjectAsToolStripMenuItem.Text = "Save Project &As...";
            this.saveProjectAsToolStripMenuItem.Click += new System.EventHandler(this.saveProjectAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(185, 6);
            // 
            // fileExit
            // 
            this.fileExit.Name = "fileExit";
            this.fileExit.Size = new System.Drawing.Size(188, 22);
            this.fileExit.Text = "&Exit";
            this.fileExit.Click += new System.EventHandler(this.fileExit_Click);
            // 
            // assembliesToolStripMenuItem
            // 
            this.assembliesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAssemblyToolStripMenuItem,
            this.removeAssemblyToolStripMenuItem,
            this.reloadToolStripMenuItem});
            this.assembliesToolStripMenuItem.Name = "assembliesToolStripMenuItem";
            this.assembliesToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.assembliesToolStripMenuItem.Text = "&Assemblies";
            // 
            // addAssemblyToolStripMenuItem
            // 
            this.addAssemblyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addAssemblyToolStripMenuItem.Image")));
            this.addAssemblyToolStripMenuItem.Name = "addAssemblyToolStripMenuItem";
            this.addAssemblyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.addAssemblyToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.addAssemblyToolStripMenuItem.Text = "&Add Assemblies...";
            this.addAssemblyToolStripMenuItem.Click += new System.EventHandler(this.addAssemblyToolStripMenuItem_Click);
            // 
            // removeAssemblyToolStripMenuItem
            // 
            this.removeAssemblyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("removeAssemblyToolStripMenuItem.Image")));
            this.removeAssemblyToolStripMenuItem.Name = "removeAssemblyToolStripMenuItem";
            this.removeAssemblyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.removeAssemblyToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.removeAssemblyToolStripMenuItem.Text = "&Remove Assemblies";
            this.removeAssemblyToolStripMenuItem.Click += new System.EventHandler(this.removeAssemblyToolStripMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("reloadToolStripMenuItem.Image")));
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.reloadToolStripMenuItem.Text = "R&eload";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsMenuItem
            // 
            this.optionsMenuItem.Name = "optionsMenuItem";
            this.optionsMenuItem.Size = new System.Drawing.Size(134, 22);
            this.optionsMenuItem.Text = "&Options...";
            this.optionsMenuItem.Click += new System.EventHandler(this.optionsMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.toolStripSeparator5,
            this.aboutMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(126, 22);
            this.helpToolStripMenuItem1.Text = "&Help";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(123, 6);
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(126, 22);
            this.aboutMenuItem.Text = "&About...";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 685);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1003, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.AutoSize = false;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(886, 17);
            this.toolStripStatusLabel.Spring = true;
            this.toolStripStatusLabel.Text = "Ready...";
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripButton,
            this.openProjectToolStripButton,
            this.toolStripSeparator1,
            this.reloadToolbarButton,
            this.toolStripSeparator6,
            this.startButton,
            this.stopButton,
            this.toolStripSeparator2,
            this.helpToolbarButton});
            this.mainToolStrip.Location = new System.Drawing.Point(3, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(256, 25);
            this.mainToolStrip.TabIndex = 3;
            this.mainToolStrip.Text = "Main Menu";
            // 
            // newProjectToolStripButton
            // 
            this.newProjectToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newProjectToolStripButton.Image")));
            this.newProjectToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newProjectToolStripButton.Name = "newProjectToolStripButton";
            this.newProjectToolStripButton.Size = new System.Drawing.Size(48, 22);
            this.newProjectToolStripButton.Text = "New";
            this.newProjectToolStripButton.ToolTipText = "New Project";
            this.newProjectToolStripButton.Click += new System.EventHandler(this.newProjectToolStripButton_Click);
            // 
            // openProjectToolStripButton
            // 
            this.openProjectToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openProjectToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openProjectToolStripButton.Image")));
            this.openProjectToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openProjectToolStripButton.Name = "openProjectToolStripButton";
            this.openProjectToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openProjectToolStripButton.Text = "Open";
            this.openProjectToolStripButton.ToolTipText = "Open Project";
            this.openProjectToolStripButton.Click += new System.EventHandler(this.openProjectToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // reloadToolbarButton
            // 
            this.reloadToolbarButton.Image = ((System.Drawing.Image)(resources.GetObject("reloadToolbarButton.Image")));
            this.reloadToolbarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reloadToolbarButton.Name = "reloadToolbarButton";
            this.reloadToolbarButton.Size = new System.Drawing.Size(60, 22);
            this.reloadToolbarButton.Text = "Reload";
            this.reloadToolbarButton.Click += new System.EventHandler(this.reloadToolbarButton_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // startButton
            // 
            this.startButton.Image = ((System.Drawing.Image)(resources.GetObject("startButton.Image")));
            this.startButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(51, 22);
            this.startButton.Text = "Start";
            this.startButton.ToolTipText = "Start Tests";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopButton.Enabled = false;
            this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
            this.stopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(23, 22);
            this.stopButton.Text = "Stop";
            this.stopButton.ToolTipText = "Stop Tests";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolbarButton
            // 
            this.helpToolbarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolbarButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolbarButton.Image")));
            this.helpToolbarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolbarButton.Name = "helpToolbarButton";
            this.helpToolbarButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolbarButton.Text = "Help";
            this.helpToolbarButton.Click += new System.EventHandler(this.helpToolbarButton_Click);
            // 
            // treeImages
            // 
            this.treeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeImages.ImageStream")));
            this.treeImages.TransparentColor = System.Drawing.Color.Magenta;
            this.treeImages.Images.SetKeyName(0, "App.ico");
            this.treeImages.Images.SetKeyName(1, "VSObject_Assembly.bmp");
            this.treeImages.Images.SetKeyName(2, "VSObject_Namespace.bmp");
            this.treeImages.Images.SetKeyName(3, "VSObject_Class.bmp");
            this.treeImages.Images.SetKeyName(4, "VSObject_Method.bmp");
            // 
            // stateImages
            // 
            this.stateImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("stateImages.ImageStream")));
            this.stateImages.TransparentColor = System.Drawing.Color.Transparent;
            this.stateImages.Images.SetKeyName(0, "tick.png");
            this.stateImages.Images.SetKeyName(1, "help-browser.png");
            this.stateImages.Images.SetKeyName(2, "error.png");
            this.stateImages.Images.SetKeyName(3, "cross.png");
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.splitContainer);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(1003, 636);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 24);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(1003, 661);
            this.toolStripContainer.TabIndex = 5;
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.mainToolStrip);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.projectTabs);
            this.splitContainer.Panel1.Controls.Add(this.panel1);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(5, 5, 0, 3);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.testResultsTabs);
            this.splitContainer.Panel2.Controls.Add(this.panelResults);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(0, 5, 5, 3);
            this.splitContainer.Size = new System.Drawing.Size(1003, 636);
            this.splitContainer.SplitterDistance = 321;
            this.splitContainer.TabIndex = 4;
            // 
            // projectTabs
            // 
            this.projectTabs.Controls.Add(this.tabPage3);
            this.projectTabs.Controls.Add(this.tabPage5);
            this.projectTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectTabs.Location = new System.Drawing.Point(5, 36);
            this.projectTabs.Name = "projectTabs";
            this.projectTabs.SelectedIndex = 0;
            this.projectTabs.Size = new System.Drawing.Size(316, 597);
            this.projectTabs.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.testTree);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(308, 571);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Class View";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // testTree
            // 
            this.testTree.CheckBoxes = true;
            this.testTree.ContextMenuStrip = this.testTreeMenuStrip;
            this.testTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testTree.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.testTree.HideSelection = false;
            this.testTree.ImageIndex = 0;
            this.testTree.ImageList = this.treeImages;
            this.testTree.Location = new System.Drawing.Point(3, 3);
            this.testTree.Name = "testTree";
            this.testTree.SelectedImageIndex = 0;
            this.testTree.Size = new System.Drawing.Size(302, 565);
            this.testTree.TabIndex = 2;
            this.testTree.TestStateImageList = this.stateImages;
            this.testTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.testTree_AfterCheck);
            this.testTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.testTree_AfterSelect);
            // 
            // testTreeMenuStrip
            // 
            this.testTreeMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expandAllMenuItem,
            this.collapseAllMenuItem,
            this.toolStripSeparator7,
            this.expandFailedMenuItem,
            this.toolStripSeparator9,
            this.resetTestsMenuItem,
            this.toolStripSeparator10,
            this.removeAssemblyToolStripMenuItem2});
            this.testTreeMenuStrip.Name = "classTreeMenuStrip";
            this.testTreeMenuStrip.Size = new System.Drawing.Size(173, 132);
            // 
            // expandAllMenuItem
            // 
            this.expandAllMenuItem.Name = "expandAllMenuItem";
            this.expandAllMenuItem.Size = new System.Drawing.Size(172, 22);
            this.expandAllMenuItem.Text = "&Expand All";
            this.expandAllMenuItem.Click += new System.EventHandler(this.expandAllMenuItem_Click);
            // 
            // collapseAllMenuItem
            // 
            this.collapseAllMenuItem.Name = "collapseAllMenuItem";
            this.collapseAllMenuItem.Size = new System.Drawing.Size(172, 22);
            this.collapseAllMenuItem.Text = "&Collapse All";
            this.collapseAllMenuItem.Click += new System.EventHandler(this.collapseAllMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(169, 6);
            // 
            // expandFailedMenuItem
            // 
            this.expandFailedMenuItem.Name = "expandFailedMenuItem";
            this.expandFailedMenuItem.Size = new System.Drawing.Size(172, 22);
            this.expandFailedMenuItem.Text = "Expand &Failed";
            this.expandFailedMenuItem.Click += new System.EventHandler(this.expandFailedMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(169, 6);
            // 
            // resetTestsMenuItem
            // 
            this.resetTestsMenuItem.Name = "resetTestsMenuItem";
            this.resetTestsMenuItem.Size = new System.Drawing.Size(172, 22);
            this.resetTestsMenuItem.Text = "&Reset Tests";
            this.resetTestsMenuItem.Click += new System.EventHandler(this.resetTestsMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(169, 6);
            // 
            // removeAssemblyToolStripMenuItem2
            // 
            this.removeAssemblyToolStripMenuItem2.Enabled = false;
            this.removeAssemblyToolStripMenuItem2.Name = "removeAssemblyToolStripMenuItem2";
            this.removeAssemblyToolStripMenuItem2.Size = new System.Drawing.Size(172, 22);
            this.removeAssemblyToolStripMenuItem2.Text = "Remove Assembly";
            this.removeAssemblyToolStripMenuItem2.Click += new System.EventHandler(this.removeAssemblyToolStripMenuItem2_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.assemblyList);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(308, 571);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Assemblies";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // assemblyList
            // 
            this.assemblyList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.assemblyList.ContextMenuStrip = this.assemblyListMenuStrip;
            this.assemblyList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assemblyList.FullRowSelect = true;
            this.assemblyList.Location = new System.Drawing.Point(3, 3);
            this.assemblyList.MultiSelect = false;
            this.assemblyList.Name = "assemblyList";
            this.assemblyList.Size = new System.Drawing.Size(302, 565);
            this.assemblyList.TabIndex = 0;
            this.assemblyList.UseCompatibleStateImageBehavior = false;
            this.assemblyList.View = System.Windows.Forms.View.Details;
            this.assemblyList.SelectedIndexChanged += new System.EventHandler(this.assemblyList_SelectedIndexChanged);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Name";
            this.columnHeader9.Width = 99;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Version";
            this.columnHeader10.Width = 70;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Location";
            this.columnHeader11.Width = 128;
            // 
            // assemblyListMenuStrip
            // 
            this.assemblyListMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeAssemblyToolStripMenuItem1});
            this.assemblyListMenuStrip.Name = "assemblyListMenuStrip";
            this.assemblyListMenuStrip.Size = new System.Drawing.Size(173, 26);
            // 
            // removeAssemblyToolStripMenuItem1
            // 
            this.removeAssemblyToolStripMenuItem1.Enabled = false;
            this.removeAssemblyToolStripMenuItem1.Name = "removeAssemblyToolStripMenuItem1";
            this.removeAssemblyToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
            this.removeAssemblyToolStripMenuItem1.Text = "Remove Assembly";
            this.removeAssemblyToolStripMenuItem1.Click += new System.EventHandler(this.removeAssemblyToolStripMenuItem1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeFilterCombo);
            this.panel1.Controls.Add(this.lblSortTreeBy);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(316, 31);
            this.panel1.TabIndex = 1;
            // 
            // treeFilterCombo
            // 
            this.treeFilterCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeFilterCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.treeFilterCombo.FormattingEnabled = true;
            this.treeFilterCombo.Items.AddRange(new object[] {
            "Namespaces",
            "Authors",
            "Categories",
            "Importance",
            "TestsOn"});
            this.treeFilterCombo.Location = new System.Drawing.Point(70, 3);
            this.treeFilterCombo.Name = "treeFilterCombo";
            this.treeFilterCombo.Size = new System.Drawing.Size(243, 21);
            this.treeFilterCombo.TabIndex = 1;
            this.treeFilterCombo.SelectedIndexChanged += new System.EventHandler(this.treeFilterCombo_SelectedIndexChanged);
            // 
            // lblSortTreeBy
            // 
            this.lblSortTreeBy.AutoSize = true;
            this.lblSortTreeBy.Location = new System.Drawing.Point(3, 6);
            this.lblSortTreeBy.Name = "lblSortTreeBy";
            this.lblSortTreeBy.Size = new System.Drawing.Size(61, 13);
            this.lblSortTreeBy.TabIndex = 0;
            this.lblSortTreeBy.Text = "Sort tree by";
            // 
            // testResultsTabs
            // 
            this.testResultsTabs.Controls.Add(this.testResultsTabPage);
            this.testResultsTabs.Controls.Add(this.logStreamsTabPage);
            this.testResultsTabs.Controls.Add(this.performanceMonitorTabPage);
            this.testResultsTabs.Controls.Add(this.reportTabPage);
            this.testResultsTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testResultsTabs.Location = new System.Drawing.Point(0, 61);
            this.testResultsTabs.Name = "testResultsTabs";
            this.testResultsTabs.SelectedIndex = 0;
            this.testResultsTabs.Size = new System.Drawing.Size(673, 572);
            this.testResultsTabs.TabIndex = 1;
            // 
            // testResultsTabPage
            // 
            this.testResultsTabPage.Controls.Add(this.testResultsList);
            this.testResultsTabPage.Controls.Add(this.resultsFilterPanel);
            this.testResultsTabPage.Location = new System.Drawing.Point(4, 22);
            this.testResultsTabPage.Name = "testResultsTabPage";
            this.testResultsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.testResultsTabPage.Size = new System.Drawing.Size(665, 546);
            this.testResultsTabPage.TabIndex = 0;
            this.testResultsTabPage.Text = "Test Results";
            this.testResultsTabPage.UseVisualStyleBackColor = true;
            // 
            // testResultsList
            // 
            this.testResultsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TestCol,
            this.ResultCol,
            this.DurationCol,
            this.TypeCol,
            this.NamespaceCol,
            this.AssemblyCol});
            this.testResultsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testResultsList.Location = new System.Drawing.Point(3, 42);
            this.testResultsList.Name = "testResultsList";
            this.testResultsList.Size = new System.Drawing.Size(659, 501);
            this.testResultsList.TabIndex = 0;
            this.testResultsList.UseCompatibleStateImageBehavior = false;
            this.testResultsList.View = System.Windows.Forms.View.Details;
            // 
            // TestCol
            // 
            this.TestCol.Text = "Test";
            this.TestCol.Width = 200;
            // 
            // ResultCol
            // 
            this.ResultCol.Text = "Result";
            this.ResultCol.Width = 100;
            // 
            // DurationCol
            // 
            this.DurationCol.Text = "Duration (ms)";
            this.DurationCol.Width = 100;
            // 
            // TypeCol
            // 
            this.TypeCol.Text = "Type";
            // 
            // NamespaceCol
            // 
            this.NamespaceCol.Text = "Namespace";
            // 
            // AssemblyCol
            // 
            this.AssemblyCol.Text = "Assembly";
            // 
            // resultsFilterPanel
            // 
            this.resultsFilterPanel.Controls.Add(this.filterTestResultsCombo);
            this.resultsFilterPanel.Controls.Add(this.label3);
            this.resultsFilterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.resultsFilterPanel.Location = new System.Drawing.Point(3, 3);
            this.resultsFilterPanel.Name = "resultsFilterPanel";
            this.resultsFilterPanel.Size = new System.Drawing.Size(659, 39);
            this.resultsFilterPanel.TabIndex = 1;
            // 
            // filterTestResultsCombo
            // 
            this.filterTestResultsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterTestResultsCombo.FormattingEnabled = true;
            this.filterTestResultsCombo.Items.AddRange(new object[] {
            "All tests",
            "Passed tests",
            "Failed tests",
            "Inconclusive tests"});
            this.filterTestResultsCombo.Location = new System.Drawing.Point(65, 9);
            this.filterTestResultsCombo.Name = "filterTestResultsCombo";
            this.filterTestResultsCombo.Size = new System.Drawing.Size(189, 21);
            this.filterTestResultsCombo.TabIndex = 1;
            this.filterTestResultsCombo.SelectedIndexChanged += new System.EventHandler(this.filterTestResultsCombo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Show only";
            // 
            // logStreamsTabPage
            // 
            this.logStreamsTabPage.Controls.Add(this.logBody);
            this.logStreamsTabPage.Controls.Add(this.panel3);
            this.logStreamsTabPage.Location = new System.Drawing.Point(4, 22);
            this.logStreamsTabPage.Name = "logStreamsTabPage";
            this.logStreamsTabPage.Size = new System.Drawing.Size(665, 546);
            this.logStreamsTabPage.TabIndex = 3;
            this.logStreamsTabPage.Text = "Log Streams";
            this.logStreamsTabPage.UseVisualStyleBackColor = true;
            this.logStreamsTabPage.Click += new System.EventHandler(this.logStreamsTabPage_Click);
            // 
            // logBody
            // 
            this.logBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logBody.Location = new System.Drawing.Point(0, 39);
            this.logBody.Name = "logBody";
            this.logBody.Size = new System.Drawing.Size(665, 507);
            this.logBody.TabIndex = 0;
            this.logBody.Text = "";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.logStream);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(665, 39);
            this.panel3.TabIndex = 2;
            // 
            // logStream
            // 
            this.logStream.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.logStream.FormattingEnabled = true;
            this.logStream.Location = new System.Drawing.Point(75, 9);
            this.logStream.Name = "logStream";
            this.logStream.Size = new System.Drawing.Size(189, 21);
            this.logStream.TabIndex = 1;
            this.logStream.SelectedIndexChanged += new System.EventHandler(this.logStream_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Select log:";
            // 
            // performanceMonitorTabPage
            // 
            this.performanceMonitorTabPage.Controls.Add(this.testResultsGraph);
            this.performanceMonitorTabPage.Controls.Add(this.panel2);
            this.performanceMonitorTabPage.Location = new System.Drawing.Point(4, 22);
            this.performanceMonitorTabPage.Name = "performanceMonitorTabPage";
            this.performanceMonitorTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.performanceMonitorTabPage.Size = new System.Drawing.Size(665, 546);
            this.performanceMonitorTabPage.TabIndex = 1;
            this.performanceMonitorTabPage.Text = "Performance Monitor";
            this.performanceMonitorTabPage.UseVisualStyleBackColor = true;
            // 
            // testResultsGraph
            // 
            this.testResultsGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testResultsGraph.Location = new System.Drawing.Point(3, 45);
            this.testResultsGraph.Name = "testResultsGraph";
            this.testResultsGraph.ScrollMaxX = 0;
            this.testResultsGraph.ScrollMaxY = 0;
            this.testResultsGraph.ScrollMaxY2 = 0;
            this.testResultsGraph.ScrollMinX = 0;
            this.testResultsGraph.ScrollMinY = 0;
            this.testResultsGraph.ScrollMinY2 = 0;
            this.testResultsGraph.Size = new System.Drawing.Size(659, 498);
            this.testResultsGraph.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.graphsFilterBox1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(659, 42);
            this.panel2.TabIndex = 1;
            // 
            // graphsFilterBox1
            // 
            this.graphsFilterBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.graphsFilterBox1.FormattingEnabled = true;
            this.graphsFilterBox1.Items.AddRange(new object[] {
            "Test results by Type",
            "Test results by Namespace",
            "Test results by Assembly"});
            this.graphsFilterBox1.Location = new System.Drawing.Point(58, 10);
            this.graphsFilterBox1.Name = "graphsFilterBox1";
            this.graphsFilterBox1.Size = new System.Drawing.Size(189, 21);
            this.graphsFilterBox1.TabIndex = 2;
            this.graphsFilterBox1.SelectedIndexChanged += new System.EventHandler(this.graphsFilterBox1_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Graphs:";
            // 
            // reportTabPage
            // 
            this.reportTabPage.Controls.Add(this.reportViewer);
            this.reportTabPage.Controls.Add(this.panel4);
            this.reportTabPage.Location = new System.Drawing.Point(4, 22);
            this.reportTabPage.Name = "reportTabPage";
            this.reportTabPage.Size = new System.Drawing.Size(665, 546);
            this.reportTabPage.TabIndex = 4;
            this.reportTabPage.Text = "Report";
            this.reportTabPage.UseVisualStyleBackColor = true;
            // 
            // reportViewer
            // 
            this.reportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer.Location = new System.Drawing.Point(0, 42);
            this.reportViewer.MinimumSize = new System.Drawing.Size(20, 20);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(665, 504);
            this.reportViewer.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.reportTypes);
            this.panel4.Controls.Add(this.btnSaveReportAs);
            this.panel4.Controls.Add(this.lblReportType);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(665, 42);
            this.panel4.TabIndex = 2;
            // 
            // reportTypes
            // 
            this.reportTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.reportTypes.FormattingEnabled = true;
            this.reportTypes.Location = new System.Drawing.Point(66, 11);
            this.reportTypes.Name = "reportTypes";
            this.reportTypes.Size = new System.Drawing.Size(121, 21);
            this.reportTypes.TabIndex = 5;
            this.reportTypes.SelectedIndexChanged += new System.EventHandler(this.reportTypes_SelectedIndexChanged);
            // 
            // btnSaveReportAs
            // 
            this.btnSaveReportAs.AutoSize = true;
            this.btnSaveReportAs.Enabled = false;
            this.btnSaveReportAs.Location = new System.Drawing.Point(193, 11);
            this.btnSaveReportAs.Name = "btnSaveReportAs";
            this.btnSaveReportAs.Size = new System.Drawing.Size(51, 23);
            this.btnSaveReportAs.TabIndex = 4;
            this.btnSaveReportAs.Text = "Save...";
            this.btnSaveReportAs.UseVisualStyleBackColor = true;
            this.btnSaveReportAs.Click += new System.EventHandler(this.btnSaveReportAs_Click);
            // 
            // lblReportType
            // 
            this.lblReportType.AutoSize = true;
            this.lblReportType.Location = new System.Drawing.Point(11, 15);
            this.lblReportType.Name = "lblReportType";
            this.lblReportType.Size = new System.Drawing.Size(49, 13);
            this.lblReportType.TabIndex = 3;
            this.lblReportType.Text = "Save as:";
            // 
            // panelResults
            // 
            this.panelResults.Controls.Add(this.testProgressStatusBar);
            this.panelResults.Controls.Add(this.label2);
            this.panelResults.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelResults.Location = new System.Drawing.Point(0, 5);
            this.panelResults.Name = "panelResults";
            this.panelResults.Size = new System.Drawing.Size(673, 56);
            this.panelResults.TabIndex = 0;
            // 
            // testProgressStatusBar
            // 
            this.testProgressStatusBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.testProgressStatusBar.BackColor = System.Drawing.Color.White;
            this.testProgressStatusBar.ElapsedTime = 0;
            this.testProgressStatusBar.Failed = 0;
            this.testProgressStatusBar.FailedColor = System.Drawing.Color.Red;
            this.testProgressStatusBar.Font = new System.Drawing.Font("Verdana", 8F);
            this.testProgressStatusBar.Ignored = 0;
            this.testProgressStatusBar.IngoredColor = System.Drawing.Color.Gold;
            this.testProgressStatusBar.Location = new System.Drawing.Point(7, 23);
            this.testProgressStatusBar.Name = "testProgressStatusBar";
            this.testProgressStatusBar.Passed = 0;
            this.testProgressStatusBar.PassedColor = System.Drawing.Color.Green;
            this.testProgressStatusBar.Size = new System.Drawing.Size(659, 23);
            this.testProgressStatusBar.Skipped = 0;
            this.testProgressStatusBar.SkippedColor = System.Drawing.Color.SteelBlue;
            this.testProgressStatusBar.TabIndex = 4;
            this.testProgressStatusBar.Text = "{0} tests - {1} successes - {2} ignored - {3} skipped - {4} failures - {5:0.0}s";
            this.testProgressStatusBar.Total = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Test Status";
            // 
            // trayIcon
            // 
            this.trayIcon.BalloonTipText = "All tests are good";
            this.trayIcon.ContextMenuStrip = this.trayMenuStrip;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "Gallio Icarus";
            this.trayIcon.Visible = true;
            this.trayIcon.DoubleClick += new System.EventHandler(this.trayIcon_DoubleClick);
            // 
            // trayMenuStrip
            // 
            this.trayMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitMenuItem});
            this.trayMenuStrip.Name = "trayMenuStrip";
            this.trayMenuStrip.Size = new System.Drawing.Size(104, 26);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(103, 22);
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 707);
            this.Controls.Add(this.toolStripContainer);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Main";
            this.Text = "Gallio Icarus {0}.{1}";
            this.Load += new System.EventHandler(this.Form_Load);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.projectTabs.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.testTreeMenuStrip.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.assemblyListMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.testResultsTabs.ResumeLayout(false);
            this.testResultsTabPage.ResumeLayout(false);
            this.resultsFilterPanel.ResumeLayout(false);
            this.resultsFilterPanel.PerformLayout();
            this.logStreamsTabPage.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.performanceMonitorTabPage.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.reportTabPage.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panelResults.ResumeLayout(false);
            this.panelResults.PerformLayout();
            this.trayMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileExit;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton newProjectToolStripButton;
        private System.Windows.Forms.ToolStripButton openProjectToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton startButton;
        private System.Windows.Forms.ToolStripButton stopButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton helpToolbarButton;
        private System.Windows.Forms.ToolStripMenuItem assembliesToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem addAssemblyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAssemblyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox treeFilterCombo;
        private System.Windows.Forms.Label lblSortTreeBy;
        private System.Windows.Forms.TabControl testResultsTabs;
        private System.Windows.Forms.TabPage testResultsTabPage;
        private System.Windows.Forms.TabPage performanceMonitorTabPage;
        private System.Windows.Forms.Panel panelResults;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage logStreamsTabPage;
        private System.Windows.Forms.ToolStripButton reloadToolbarButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ImageList treeImages;
        private TestResultsList testResultsList;
        private System.Windows.Forms.ColumnHeader TestCol;
        private System.Windows.Forms.ColumnHeader ResultCol;
        private System.Windows.Forms.ColumnHeader DurationCol;
        private TestTreeView testTree;
        private System.Windows.Forms.ImageList stateImages;
        private TestStatusBar testProgressStatusBar;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.Panel resultsFilterPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl projectTabs;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ListView assemblyList;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsMenuItem;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip testTreeMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem expandAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseAllMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem expandFailedMenuItem;
        private TestResultsGraph testResultsGraph;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox graphsFilterBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem resetTestsMenuItem;
        private System.Windows.Forms.ContextMenuStrip trayMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem removeAssemblyToolStripMenuItem2;
        private System.Windows.Forms.ContextMenuStrip assemblyListMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem removeAssemblyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.RichTextBox logBody;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox logStream;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox filterTestResultsCombo;
        private System.Windows.Forms.TabPage reportTabPage;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnSaveReportAs;
        private System.Windows.Forms.Label lblReportType;
        private System.Windows.Forms.ComboBox reportTypes;
        private System.Windows.Forms.WebBrowser reportViewer;
        private System.Windows.Forms.ColumnHeader TypeCol;
        private System.Windows.Forms.ColumnHeader NamespaceCol;
        private System.Windows.Forms.ColumnHeader AssemblyCol;
    }
}


