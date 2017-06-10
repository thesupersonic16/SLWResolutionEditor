using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SLWResolutionEditor
{
    public partial class MainForm : Form
    {
        public IntPtr ProcessHandle = IntPtr.Zero;
        public int ProcessID = 0;
        public uint Address = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        public uint GetAddress()
        {
            // LOL, I have no idea how these work
            uint baseAddress = 0;
            if (Process.GetProcesses().Any(t => t.Id == ProcessID))
                baseAddress = ((uint)Process.GetProcessById(ProcessID).MainModule.BaseAddress.ToInt32()) + 0x1000;
            uint pointer1 = ProcessMemory.ReadUInt32(ProcessHandle, baseAddress + 0x402);
            uint pointer2 = ProcessMemory.ReadUInt32(ProcessHandle, pointer1);
            uint pointer3 = ProcessMemory.ReadUInt32(ProcessHandle, pointer2);
            return pointer3;
        }

        private void ButtonReadData_Click(object sender, EventArgs e)
        {
            if (ProcessHandle == IntPtr.Zero)
            { // No handle
                var processes = Process.GetProcessesByName("slw");
                if (processes.Length > 0)
                {
                    ProcessHandle = ProcessMemory.OpenProcess(
                        ProcessMemory.ProcessAccessFlags.All, false, processes[0].Id);
                    ProcessID = processes[0].Id;
                }
            }

            // Just in case
            if (ProcessHandle == IntPtr.Zero)
            {
                MessageBox.Show("Sonic Lost World is currently not running.",
                    "SLWResolutionEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Find the address
            Address = GetAddress();

            if (Address == 0)
            {
                MessageBox.Show("Something went wrong.", "SLWResolutionEditor",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Reads a block that contains all the resolution values
            var bytes = ProcessMemory.ReadBlock(ProcessHandle, Address, (uint) (numericUpDown1.Value + 1) * 0x10);

            // Clear the old list
            listView1.Items.Clear();

            for (int i = 0; i < numericUpDown1.Value; ++i)
            {
                int width = BitConverter.ToInt32(bytes, i * 0x10);
                int height = BitConverter.ToInt32(bytes, i * 0x10 + 0x4);

                var item = new ListViewItem(i.ToString("00"));                      // Index
                item.SubItems.Add(width.ToString());                                // Width
                item.SubItems.Add(height.ToString());                               // Height
                item.SubItems.Add(string.Format("0x{0:X8}", i * 0x10 + Address));   // Address
                listView1.Items.Add(item);
            }

            // Theme
            for (int i = 0; i < listView1.Items.Count; ++i)
            {
                if (++i % 2 == 0) listView1.Items[i].BackColor = Color.FromArgb(46, 46, 46);
                else listView1.Items[i].BackColor = Color.FromArgb(54, 54, 54);
            }
        }

        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                var item = listView1.SelectedItems[0];
                var form = new ResolutionEditForm(item);
                form.ShowDialog();
            }
        }

        /// <summary>
        /// Writes All the values to SLW's memory
        /// </summary>
        private void ButtonWriteData_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; ++i)
            {
                var item = listView1.Items[i];
                uint address = Address + (0x10 * (uint)i);
                int width = int.Parse(item.SubItems[1].Text);
                int height = int.Parse(item.SubItems[2].Text);
                ProcessMemory.WriteInt32(ProcessHandle, address, width);
                ProcessMemory.WriteInt32(ProcessHandle, address + 0x4, height);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Just in case, Its a good idea to close the handle
            ProcessMemory.CloseHandle(ProcessHandle);
        }

        #region Don't Look! From my horrible fork of SLWModLoader

        private void ListView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            // Colours
            var dark1 = Color.FromArgb(34, 34, 34);
            var dark2 = Color.FromArgb(70, 70, 70);

            // Draws the Header
            if (e.Bounds.Contains(listView1.PointToClient(MousePosition)))
                e.Graphics.FillRectangle(new SolidBrush(dark1), e.Bounds);
            else e.Graphics.FillRectangle(new SolidBrush(dark2), e.Bounds);
            var point = new Point(0, 6);
            point.X = e.Bounds.X;
            var column = listView1.Columns[e.ColumnIndex];
            e.Graphics.FillRectangle(new SolidBrush(dark1), point.X, 0, 2, e.Bounds.Height);
            point.X += column.Width / 2 - TextRenderer.MeasureText(column.Text, listView1.Font).Width / 2;
            TextRenderer.DrawText(e.Graphics, column.Text, listView1.Font, point, listView1.ForeColor);
        }

        private void ListView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        public static void AddAllChildControls(Control control, List<Control> controls)
        {
            controls.Add(control);
            foreach (Control control2 in control.Controls)
                AddAllChildControls(control2, controls);
        }

        /// <summary>
        /// It Applies a horrible dark theme to control and to all the controls
        /// </summary>
        /// <param name="control">The Main control that the dark them is being applied to</param>
        /// <param name="controls">Extra controls to apply the theme to</param>
        /// <returns></returns>
        public static bool ApplyDarkTheme(Control control, params Control[] controls)
        {
            var allControls = new List<Control>();

            AddAllChildControls(control, allControls);

            foreach (var control0 in controls)
            {
                if (!allControls.Contains(control0)) allControls.Add(control0);
                foreach (Control control1 in control0.Controls)
                    if (!allControls.Contains(control1))
                        allControls.Add(control1);
            }

            foreach (Control control0 in allControls)
            {
                control0.BackColor = Color.FromArgb(46, 46, 46);
                if (control0.ForeColor == Color.Black || control0.ForeColor == SystemColors.WindowText ||
                    control0.ForeColor == SystemColors.ControlText)
                    control0.ForeColor = Color.FromArgb(200, 200, 180);

                if (control0.GetType() == typeof(Button))
                {
                    ((Button)control0).FlatStyle = FlatStyle.Flat;
                    control0.BackColor = Color.FromArgb(54, 54, 54);
                }

                if (control0.GetType() == typeof(RadioButton))
                    ((RadioButton)control0).FlatStyle = FlatStyle.Flat;

                if (control0.GetType() == typeof(StatusStrip))
                    control0.BackColor = Color.FromArgb(54, 54, 54);

                if (control0.GetType() == typeof(TabPage) || control0.GetType() == typeof(LinkLabel) ||
                    control0.GetType() == typeof(CheckBox) || control0.GetType() == typeof(GroupBox) ||
                    control0.GetType() == typeof(Label))
                    control0.BackColor = Color.FromArgb(46, 46, 46);

                if (control0.GetType() == typeof(ListView))
                {
                    ((ListView)control0).OwnerDraw = true;
                    int i = 0;
                    foreach (ListViewItem lvi in ((ListView)control0).Items)
                        if (++i % 2 == 0) lvi.BackColor = Color.FromArgb(46, 46, 46);
                        else lvi.BackColor = Color.FromArgb(54, 54, 54);
                }

            }
            return true;
        }

        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            ApplyDarkTheme(this);
        }
    }
}
