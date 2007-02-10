using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using MbUnit.GUI.Controls;
using MbUnit.GUI.Controls.Enums;

using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Actions;
using ICSharpCode.TextEditor;

using ZedGraph;

namespace MbUnit.GUI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            // Set the application version in the window title.
            Version appVersion = Assembly.GetCallingAssembly().GetName().Version;
            this.Text = String.Format(this.Text, appVersion.Major, appVersion.Minor);

            GraphPane graphPane = zedGraphControl1.GraphPane;
            graphPane.Title.Text = "Total Test Results";
            graphPane.XAxis.Title.Text = "Number of Tests";
            graphPane.YAxis.Title.Text = "Test Suites";


            // Make up some data points
            string[] labels = { "Test Assembly 1", "Test Assembly 2"};
            double[] x = {1, 2};
            double[] x2 = {1, 5};
            double[] x3 = {4, 10};

            // Generate a red bar with "Curve 1" in the legend
            BarItem myCurve = graphPane.AddBar("Fail", x, null, Color.Red);
            // Fill the bar with a red-white-red color gradient for a 3d look
            myCurve.Bar.Fill = new Fill(Color.Red, Color.White, Color.Red, 90f);

            // Generate a blue bar with "Curve 2" in the legend
            myCurve = graphPane.AddBar("Ignore", x2, null, Color.Yellow);
            // Fill the bar with a Blue-white-Blue color gradient for a 3d look
            myCurve.Bar.Fill = new Fill(Color.Blue, Color.White, Color.Blue, 90f);

            // Generate a green bar with "Curve 3" in the legend
            myCurve = graphPane.AddBar("Pass", x3, null, Color.Green);
            // Fill the bar with a Green-white-Green color gradient for a 3d look
            myCurve.Bar.Fill = new Fill(Color.Green, Color.White, Color.Green, 90f);

            // Draw the Y tics between the labels instead of at the labels
            graphPane.YAxis.MajorTic.IsBetweenLabels = true;

            // Set the YAxis labels
            graphPane.YAxis.Scale.TextLabels = labels;
            // Set the YAxis to Text type
            graphPane.YAxis.Type = AxisType.Text;

            // Set the bar type to stack, which stacks the bars by automatically accumulating the values
            graphPane.BarSettings.Type = BarType.Stack;

            // Make the bars horizontal by setting the BarBase to "Y"
            graphPane.BarSettings.Base = BarBase.Y;

            // Fill the chart background with a color gradient
            graphPane.Chart.Fill = new Fill(Color.White,
               Color.FromArgb(255, 255, 166), 45.0F);
                      
            zedGraphControl1.AxisChange();



        }

        private void Form_Load(object sender, EventArgs e)
        {
            treeFilterCombo.SelectedIndex = 1;
            filterTestResultsCombo.SelectedIndex = 0;
            graphsFilterBox1.SelectedIndex = 0;

            TestTreeNode project = new TestTreeNode("Test Project 1.0", 0, 0);
            projectTree.Nodes.Add(project);

            TestTreeNode namespaces = new TestTreeNode("Namespaces", 0, 0);
            project.Nodes.Add(namespaces);

            TestTreeNode ns = new TestTreeNode("TestNamespace", 1, 1);
            namespaces.Nodes.Add(ns);

            TestTreeNode cl = new TestTreeNode("Class1", 2, 2);
            ns.Nodes.Add(cl);

            TestTreeNode m1 = new TestTreeNode("TestMethod", 3, 3);
            m1.TestState = TestState.Success;
            cl.Nodes.Add(m1);

            TestTreeNode m2 = new TestTreeNode("AnotherMethod", 3, 3);
            m2.TestState = TestState.Failure;
            cl.Nodes.Add(m2);

            TestTreeNode cl2 = new TestTreeNode("Class2", 2, 2);
            ns.Nodes.Add(cl2);

            TestTreeNode m3 = new TestTreeNode("MethodThatWorks", 3, 3);
            m3.TestState = TestState.Success;
            cl2.Nodes.Add(m3);

            TestTreeNode m4 = new TestTreeNode("DoesntWork", 3, 3);
            m4.TestState = TestState.Failure;
            cl2.Nodes.Add(m4);

            TestTreeNode m5 = new TestTreeNode("DoGetProgress", 3, 3);
            m5.TestState = TestState.Success;
            cl2.Nodes.Add(m5);

            TestTreeNode m6 = new TestTreeNode("BuildTree", 3, 3);
            m6.TestState = TestState.Success;
            cl2.Nodes.Add(m6);

            project.ExpandAll();

        }

        private void fileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //private void linkSummary_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    if (panelResults.Height >= 149)
        //    {
        //        linkSummary.Text = "Show test summary...";

        //        while (panelResults.Height >= 68)
        //        {
        //            panelResults.Height -= 4;
        //            Application.DoEvents();
        //        }
        //    }
        //    else
        //    {
        //        linkSummary.Text = "Hide test summary...";

        //        while (panelResults.Height <= 149)
        //        {
        //            panelResults.Height += 4;
        //            Application.DoEvents();
        //        }
        //    }
        //}

        private void tlbStart_Click(object sender, EventArgs e)
        {
            testProgressStatusBar.Clear();
            testProgressStatusBar.Total = 50;
            for (int i = 0; i < testProgressStatusBar.Total; i++)
            {
                if (i == 12 || i == 20 || i == 28 || i == 29 || i == 38 || i == 40 || i == 45 || i == 46 || i == 47 || i == 18 || i == 25)
                    testProgressStatusBar.Failed += 1;
                else if (i == 30 || i == 42)
                    testProgressStatusBar.Ignored += 1;
                else
                    testProgressStatusBar.Passed += 1;

                Application.DoEvents();
                System.Threading.Thread.Sleep(50);
            }
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            About aboutForm = new About();
            aboutForm.ShowDialog();

            if (aboutForm != null)
                aboutForm.Dispose();
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();        
            }
        }

        private void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;           
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
        }

        private void reloadToolbarButton_Click(object sender, EventArgs e)
        {
            trayIcon.ShowBalloonTip(5, "MbUnit Test Notice", "Recent changes have caused 5 of your unit tests to fail.", ToolTipIcon.Error);
        }

        private void scriptingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scripting scriptingWindow = new Scripting();
            scriptingWindow.ShowDialog();

            if (scriptingWindow != null)
                scriptingWindow.Dispose();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "MbUnit Projects | *.mbunit";
            openFileDialog1.FileOk += new CancelEventHandler(openFileDialog1_FileOk);
            openFileDialog1.ShowDialog();
        }

        void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            MessageBox.Show(openFileDialog1.FileName);
        }

        private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = "MbUnit Projects |*.mbunit";
            saveFileDialog1.Filter = "MbUnit Projects |*.mbunit";
            saveFileDialog1.FileOk +=new CancelEventHandler(saveFileDialog1_FileOk);
            saveFileDialog1.ShowDialog();
        }

        void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            MessageBox.Show(saveFileDialog1.FileName);
        }
    }
}