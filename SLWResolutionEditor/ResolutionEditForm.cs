using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLWResolutionEditor
{
    public partial class ResolutionEditForm : Form
    {
        public ListViewItem Item;

        public ResolutionEditForm()
        {
            InitializeComponent();
        }

        public ResolutionEditForm(ListViewItem item) : this()
        {
            numericUpDown1.Value = int.Parse(item.SubItems[1].Text);
            numericUpDown2.Value = int.Parse(item.SubItems[2].Text);
            Item = item;
        }

        private void ButtonSet_Click(object sender, EventArgs e)
        {
            Item.SubItems[1].Text = numericUpDown1.Value.ToString();
            Item.SubItems[2].Text = numericUpDown2.Value.ToString();
            Close();
        }

        private void NumericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 && !ModifierKeys.HasFlag(Keys.Shift))
            {
                ButtonSet_Click(null, null);
            }
        }

        private void ResolutionEditForm_Load(object sender, EventArgs e)
        {
            MainForm.ApplyDarkTheme(this);
        }
    }
}
