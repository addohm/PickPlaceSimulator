using System;

namespace PickPackSim
{
    partial class TableForm
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Serial Number", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Line Number", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Optic Position", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Clamshell Position", System.Windows.Forms.HorizontalAlignment.Left);
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnRun = new System.Windows.Forms.Button();
            this.BtnReset = new System.Windows.Forms.Button();
            this.lvResults = new System.Windows.Forms.ListView();
            this.lvCol1Header = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvCol2Header = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvCol3Header = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvCol4Header = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label6 = new System.Windows.Forms.Label();
            this.lblRemaining = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblInserted = new System.Windows.Forms.Label();
            this.lblResults = new System.Windows.Forms.Label();
            this.SpCaller = new System.ComponentModel.BackgroundWorker();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(299, 400);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(75, 23);
            this.BtnClose.TabIndex = 1;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnRun
            // 
            this.BtnRun.Location = new System.Drawing.Point(218, 400);
            this.BtnRun.Name = "BtnRun";
            this.BtnRun.Size = new System.Drawing.Size(75, 23);
            this.BtnRun.TabIndex = 2;
            this.BtnRun.Text = "Run";
            this.BtnRun.UseVisualStyleBackColor = true;
            this.BtnRun.Click += new System.EventHandler(this.BtnRun_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.Location = new System.Drawing.Point(12, 400);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(75, 23);
            this.BtnReset.TabIndex = 3;
            this.BtnReset.Text = "Reset";
            this.BtnReset.UseVisualStyleBackColor = true;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // lvResults
            // 
            this.lvResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvCol1Header,
            this.lvCol2Header,
            this.lvCol3Header,
            this.lvCol4Header});
            this.lvResults.GridLines = true;
            listViewGroup1.Header = "Serial Number";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "Line Number";
            listViewGroup2.Name = "listViewGroup2";
            listViewGroup3.Header = "Optic Position";
            listViewGroup3.Name = "listViewGroup3";
            listViewGroup4.Header = "Clamshell Position";
            listViewGroup4.Name = "listViewGroup4";
            this.lvResults.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4});
            this.lvResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvResults.Location = new System.Drawing.Point(12, 121);
            this.lvResults.Name = "lvResults";
            this.lvResults.Size = new System.Drawing.Size(362, 273);
            this.lvResults.TabIndex = 8;
            this.lvResults.UseCompatibleStateImageBehavior = false;
            this.lvResults.View = System.Windows.Forms.View.Details;
            // 
            // lvCol1Header
            // 
            this.lvCol1Header.Text = "Serial Numbers";
            this.lvCol1Header.Width = 105;
            // 
            // lvCol2Header
            // 
            this.lvCol2Header.Text = "Line Number";
            this.lvCol2Header.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lvCol2Header.Width = 76;
            // 
            // lvCol3Header
            // 
            this.lvCol3Header.Text = "Optic Position";
            this.lvCol3Header.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lvCol3Header.Width = 79;
            // 
            // lvCol4Header
            // 
            this.lvCol4Header.Text = "Clamshell Position";
            this.lvCol4Header.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lvCol4Header.Width = 98;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(88, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Optics Remaining:";
            // 
            // lblRemaining
            // 
            this.lblRemaining.AutoSize = true;
            this.lblRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemaining.Location = new System.Drawing.Point(187, 8);
            this.lblRemaining.Name = "lblRemaining";
            this.lblRemaining.Size = new System.Drawing.Size(136, 55);
            this.lblRemaining.TabIndex = 11;
            this.lblRemaining.Text = "####";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Optics Inserted:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(194, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Results Inserted:";
            // 
            // lblInserted
            // 
            this.lblInserted.AutoSize = true;
            this.lblInserted.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInserted.Location = new System.Drawing.Point(113, 63);
            this.lblInserted.Name = "lblInserted";
            this.lblInserted.Size = new System.Drawing.Size(52, 55);
            this.lblInserted.TabIndex = 14;
            this.lblInserted.Text = "#";
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResults.Location = new System.Drawing.Point(286, 63);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(52, 55);
            this.lblResults.TabIndex = 15;
            this.lblResults.Text = "#";
            // 
            // SpCaller
            // 
            this.SpCaller.WorkerReportsProgress = true;
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(29, 257);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(327, 23);
            this.progress.Step = 25;
            this.progress.TabIndex = 16;
            this.progress.Visible = false;
            // 
            // TableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 435);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.lblInserted);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblRemaining);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lvResults);
            this.Controls.Add(this.BtnReset);
            this.Controls.Add(this.BtnRun);
            this.Controls.Add(this.BtnClose);
            this.Name = "TableForm";
            this.Text = "Pick and Pack Simulation";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Button BtnRun;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.ListView lvResults;
        private System.Windows.Forms.ColumnHeader lvCol1Header;
        private System.Windows.Forms.ColumnHeader lvCol2Header;
        private System.Windows.Forms.ColumnHeader lvCol3Header;
        private System.Windows.Forms.ColumnHeader lvCol4Header;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblRemaining;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblInserted;
        private System.Windows.Forms.Label lblResults;
        private System.ComponentModel.BackgroundWorker SpCaller;
        private System.Windows.Forms.ProgressBar progress;
    }
}

