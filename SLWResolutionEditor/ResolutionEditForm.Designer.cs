namespace SLWResolutionEditor
{
    partial class ResolutionEditForm
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
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.LabelDesc = new System.Windows.Forms.Label();
            this.LabelWidth = new System.Windows.Forms.Label();
            this.LabelHeight = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.ButtonSet = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(66, 37);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericUpDown1_KeyPress);
            // 
            // LabelDesc
            // 
            this.LabelDesc.AutoSize = true;
            this.LabelDesc.BackColor = System.Drawing.Color.White;
            this.LabelDesc.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.LabelDesc.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.LabelDesc.Location = new System.Drawing.Point(12, 9);
            this.LabelDesc.Name = "LabelDesc";
            this.LabelDesc.Size = new System.Drawing.Size(146, 25);
            this.LabelDesc.TabIndex = 1;
            this.LabelDesc.Text = "Edit Resolution: ";
            // 
            // LabelWidth
            // 
            this.LabelWidth.AutoSize = true;
            this.LabelWidth.Location = new System.Drawing.Point(14, 39);
            this.LabelWidth.Name = "LabelWidth";
            this.LabelWidth.Size = new System.Drawing.Size(41, 13);
            this.LabelWidth.TabIndex = 2;
            this.LabelWidth.Text = "Width: ";
            // 
            // LabelHeight
            // 
            this.LabelHeight.AutoSize = true;
            this.LabelHeight.Location = new System.Drawing.Point(14, 64);
            this.LabelHeight.Name = "LabelHeight";
            this.LabelHeight.Size = new System.Drawing.Size(44, 13);
            this.LabelHeight.TabIndex = 3;
            this.LabelHeight.Text = "Height: ";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(66, 62);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown2.TabIndex = 4;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericUpDown1_KeyPress);
            // 
            // ButtonSet
            // 
            this.ButtonSet.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonSet.Location = new System.Drawing.Point(12, 91);
            this.ButtonSet.Name = "ButtonSet";
            this.ButtonSet.Size = new System.Drawing.Size(211, 22);
            this.ButtonSet.TabIndex = 5;
            this.ButtonSet.Text = "Set";
            this.ButtonSet.UseVisualStyleBackColor = true;
            this.ButtonSet.Click += new System.EventHandler(this.ButtonSet_Click);
            // 
            // ResolutionEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(235, 125);
            this.Controls.Add(this.ButtonSet);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.LabelHeight);
            this.Controls.Add(this.LabelWidth);
            this.Controls.Add(this.LabelDesc);
            this.Controls.Add(this.numericUpDown1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResolutionEditForm";
            this.Text = "Edit Resolution";
            this.Load += new System.EventHandler(this.ResolutionEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label LabelDesc;
        private System.Windows.Forms.Label LabelWidth;
        private System.Windows.Forms.Label LabelHeight;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button ButtonSet;
    }
}