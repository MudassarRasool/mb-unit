// Copyright 2007 MbUnit Project - http://www.mbunit.com/
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

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MbUnit.Icarus.Controls.Enums;

namespace MbUnit.Icarus.Controls
{
    public class TestTreeView : TreeView
    {
        private ImageList testStateImages;
        private bool useTriStateCheckBoxes = true;

        public TestTreeView()
        {
            testStateImages = new ImageList();
            DrawMode = TreeViewDrawMode.OwnerDrawText;

            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        #region Overrides

        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            BeginUpdate();

            base.OnAfterCheck(e);

            if (UseTriStateCheckBoxes)
            {
                switch (e.Action)
                {
                    case TreeViewAction.ByKeyboard:
                    case TreeViewAction.ByMouse:
                        {
                            if (e.Node is TestTreeNode)
                            {
                                // Toggle to the next state.
                                TestTreeNode tn = e.Node as TestTreeNode;
                                if (tn != null) tn.Toggle();
                            }

                            break;
                        }
                    case TreeViewAction.Collapse:
                    case TreeViewAction.Expand:
                    case TreeViewAction.Unknown:
                    default:
                        {
                            // Do nothing.
                            break;
                        }
                }
            }

            EndUpdate();
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            if (e.Node.IsVisible && e.Node is TestTreeNode)
            {
                TestTreeNode node = e.Node as TestTreeNode;

                // Clear the nodes text display area so we have a blank canvas to work from.
                e.Graphics.FillRectangle(new SolidBrush(BackColor), e.Bounds.X, e.Bounds.Y, (Width - e.Bounds.X),
                                         e.Bounds.Height);

                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null) nodeFont = Font;

                // Setup the text colour, default to WindowText on any non leaf node.
                Brush nodeColor = SystemBrushes.WindowText;
                if ((e.State & TreeNodeStates.Focused) != 0)
                    nodeColor = SystemBrushes.HighlightText;

                else if (node != null)
                    if (node.TestState == TestStates.Failed)
                        nodeColor = new SolidBrush(Color.Red);

                    else if (node.TestState == TestStates.Success)
                        nodeColor = new SolidBrush(Color.Green);

                    else if (node.TestState == TestStates.Ignored)
                        nodeColor = new SolidBrush(Color.SlateGray);

                SizeF textSize = e.Graphics.MeasureString(e.Node.Text, nodeFont);

                // If we do not want to hide the selection, paint it in again.
                if (SelectedNode == e.Node && !HideSelection)
                {
                    nodeColor = SystemBrushes.HighlightText;

                    if (node != null)
                        if (node.TestState != TestStates.Undefined)
                            e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), e.Bounds.X + 17, e.Bounds.Y,
                                                     textSize.Width, e.Bounds.Height);
                        else
                            e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), e.Bounds.X, e.Bounds.Y,
                                                     textSize.Width, e.Bounds.Height);
                }

                // If the test icons have been set and we are at a leaf node, draw the extra image.
                if (node != null)
                    if (testStateImages.Images.Count > 0 && node.TestState != TestStates.Undefined)
                    {
                        // Draw the highlighted background if the node has been selected.
                        if ((e.State & TreeNodeStates.Focused) != 0)
                            e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), e.Bounds.X + 17, e.Bounds.Y,
                                                     textSize.Width, e.Bounds.Height);

                        e.Graphics.DrawImageUnscaled(testStateImages.Images[(int) node.TestState - 1], e.Bounds.X,
                                                     e.Bounds.Y);
                        e.Graphics.DrawString(e.Node.Text, nodeFont, nodeColor, e.Bounds.X + 16, e.Bounds.Y + 1);
                    }
                    else
                    {
                        // Draw a regular node, we do not need to extend the highlight area or add any image.
                        if ((e.State & TreeNodeStates.Focused) != 0)
                            e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), e.Bounds.X, e.Bounds.Y,
                                                     textSize.Width, e.Bounds.Height);

                        e.Graphics.DrawString(e.Node.Text, nodeFont, nodeColor, e.Bounds.X, e.Bounds.Y + 1);
                    }
            }
        }


        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);

            TestTreeNode node = e.Node as TestTreeNode;

            // Extend the selectable region of the control by 16px to include the image.
            if ((node != null) &&
                (node.TestState != TestStates.Undefined) &&
                (e.Node.Bounds.Right + 16 >= e.Location.X) &&
                (e.Node.Bounds.Left + 16 <= e.Location.X) &&
                (e.Button == MouseButtons.Left))


                SelectedNode = e.Node;
        }

        #endregion

        #region Public Properties

        [Browsable(true),
         Category("Test States")]
        public ImageList TestStateImageList
        {
            get { return testStateImages; }
            set { testStateImages = value; }
        }

        [Category("Appearance"),
         Description("If enabled the parent checkboxes will indicate the state of children."),
         DefaultValue(true),
         TypeConverter(typeof (bool)),
         Editor("System.Boolean", typeof (bool))]
        public bool UseTriStateCheckBoxes
        {
            get { return useTriStateCheckBoxes; }
            set { useTriStateCheckBoxes = value; }
        }

        #endregion
    }
}