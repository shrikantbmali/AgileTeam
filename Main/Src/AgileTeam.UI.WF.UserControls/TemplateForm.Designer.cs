namespace AgileTeam.UI.WF.UserControls
{
	partial class TemplateForm
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
			this.lblHeading = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// lblHeading
			// 
			this.lblHeading.AutoSize = true;
			this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHeading.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lblHeading.Location = new System.Drawing.Point(12, 9);
			this.lblHeading.Name = "lblHeading";
			this.lblHeading.Size = new System.Drawing.Size(51, 20);
			this.lblHeading.TabIndex = 0;
			this.lblHeading.Text = "label1";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 384);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(675, 45);
			this.panel1.TabIndex = 1;
			// 
			// TemplateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(675, 429);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.lblHeading);
			this.Name = "TemplateForm";
			this.Text = "TemplateForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		protected System.Windows.Forms.Label lblHeading;
		protected System.Windows.Forms.Panel panel1;
	}
}