namespace AgileTeam.UI.WF.Views
{
	partial class DialogTemplateForm
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblHeading = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 394);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(624, 48);
			this.panel1.TabIndex = 0;
			// 
			// lblHeading
			// 
			this.lblHeading.AutoSize = true;
			this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHeading.ForeColor = System.Drawing.Color.RoyalBlue;
			this.lblHeading.Location = new System.Drawing.Point(12, 9);
			this.lblHeading.Name = "lblHeading";
			this.lblHeading.Size = new System.Drawing.Size(151, 24);
			this.lblHeading.TabIndex = 0;
			this.lblHeading.Text = "Custom Heading";
			// 
			// DialogTemplateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 442);
			this.Controls.Add(this.lblHeading);
			this.Controls.Add(this.panel1);
			this.Name = "DialogTemplateForm";
			this.Text = "DialogTemplateForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		protected System.Windows.Forms.Label lblHeading;
		protected System.Windows.Forms.Panel panel1;
	}
}