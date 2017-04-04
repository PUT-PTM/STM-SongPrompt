namespace SongPrompt
{
    partial class SongPrompt
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
            this.connectionGB = new System.Windows.Forms.GroupBox();
            this.checkConnBtn = new System.Windows.Forms.Button();
            this.connectionStatusLbl = new System.Windows.Forms.Label();
            this.connLbl = new System.Windows.Forms.Label();
            this.trackInfoGB = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.titleLbl = new System.Windows.Forms.Label();
            this.authorLbl = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.titleSetLbl = new System.Windows.Forms.Label();
            this.authorSetLbl = new System.Windows.Forms.Label();
            this.connectionGB.SuspendLayout();
            this.trackInfoGB.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectionGB
            // 
            this.connectionGB.Controls.Add(this.checkConnBtn);
            this.connectionGB.Controls.Add(this.connectionStatusLbl);
            this.connectionGB.Controls.Add(this.connLbl);
            this.connectionGB.Location = new System.Drawing.Point(15, 15);
            this.connectionGB.Margin = new System.Windows.Forms.Padding(5);
            this.connectionGB.Name = "connectionGB";
            this.connectionGB.Size = new System.Drawing.Size(250, 75);
            this.connectionGB.TabIndex = 0;
            this.connectionGB.TabStop = false;
            this.connectionGB.Text = "Połączenie";
            // 
            // checkConnBtn
            // 
            this.checkConnBtn.Location = new System.Drawing.Point(10, 40);
            this.checkConnBtn.Margin = new System.Windows.Forms.Padding(5);
            this.checkConnBtn.Name = "checkConnBtn";
            this.checkConnBtn.Size = new System.Drawing.Size(230, 23);
            this.checkConnBtn.TabIndex = 2;
            this.checkConnBtn.Text = "Połącz ze Spotify";
            this.checkConnBtn.UseVisualStyleBackColor = true;
            this.checkConnBtn.Click += new System.EventHandler(this.checkConnBtn_Click);
            // 
            // connectionStatusLbl
            // 
            this.connectionStatusLbl.AutoSize = true;
            this.connectionStatusLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.connectionStatusLbl.Location = new System.Drawing.Point(200, 20);
            this.connectionStatusLbl.Margin = new System.Windows.Forms.Padding(5);
            this.connectionStatusLbl.Name = "connectionStatusLbl";
            this.connectionStatusLbl.Size = new System.Drawing.Size(40, 13);
            this.connectionStatusLbl.TabIndex = 1;
            this.connectionStatusLbl.Text = "BRAK";
            // 
            // connLbl
            // 
            this.connLbl.AutoSize = true;
            this.connLbl.Location = new System.Drawing.Point(10, 20);
            this.connLbl.Margin = new System.Windows.Forms.Padding(5);
            this.connLbl.Name = "connLbl";
            this.connLbl.Size = new System.Drawing.Size(117, 13);
            this.connLbl.TabIndex = 0;
            this.connLbl.Text = "Połączenie do Spotify: ";
            // 
            // trackInfoGB
            // 
            this.trackInfoGB.Controls.Add(this.tableLayoutPanel1);
            this.trackInfoGB.Location = new System.Drawing.Point(15, 100);
            this.trackInfoGB.Margin = new System.Windows.Forms.Padding(5);
            this.trackInfoGB.Name = "trackInfoGB";
            this.trackInfoGB.Size = new System.Drawing.Size(250, 122);
            this.trackInfoGB.TabIndex = 1;
            this.trackInfoGB.TabStop = false;
            this.trackInfoGB.Text = "Informacje o utworze";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this.tableLayoutPanel1.Controls.Add(this.titleLbl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.authorLbl, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.timeLabel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.titleSetLbl, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.authorSetLbl, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(225, 90);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // titleLbl
            // 
            this.titleLbl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.titleLbl.AutoSize = true;
            this.titleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.titleLbl.Location = new System.Drawing.Point(16, 8);
            this.titleLbl.Name = "titleLbl";
            this.titleLbl.Size = new System.Drawing.Size(41, 13);
            this.titleLbl.TabIndex = 0;
            this.titleLbl.Text = "Tytuł:";
            // 
            // authorLbl
            // 
            this.authorLbl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.authorLbl.AutoSize = true;
            this.authorLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.authorLbl.Location = new System.Drawing.Point(16, 38);
            this.authorLbl.Name = "authorLbl";
            this.authorLbl.Size = new System.Drawing.Size(41, 13);
            this.authorLbl.TabIndex = 1;
            this.authorLbl.Text = "Autor:";
            // 
            // timeLabel
            // 
            this.timeLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(112, 68);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(60, 13);
            this.timeLabel.TabIndex = 2;
            this.timeLabel.Text = "0:00 / 3:30";
            // 
            // titleSetLbl
            // 
            this.titleSetLbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.titleSetLbl.AutoSize = true;
            this.titleSetLbl.Location = new System.Drawing.Point(63, 8);
            this.titleSetLbl.Name = "titleSetLbl";
            this.titleSetLbl.Size = new System.Drawing.Size(38, 13);
            this.titleSetLbl.TabIndex = 3;
            this.titleSetLbl.Text = "NONE";
            // 
            // authorSetLbl
            // 
            this.authorSetLbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.authorSetLbl.AutoSize = true;
            this.authorSetLbl.Location = new System.Drawing.Point(63, 38);
            this.authorSetLbl.Name = "authorSetLbl";
            this.authorSetLbl.Size = new System.Drawing.Size(38, 13);
            this.authorSetLbl.TabIndex = 4;
            this.authorSetLbl.Text = "NONE";
            // 
            // SongPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 235);
            this.Controls.Add(this.trackInfoGB);
            this.Controls.Add(this.connectionGB);
            this.Name = "SongPrompt";
            this.Text = "SongPrompt";
            this.connectionGB.ResumeLayout(false);
            this.connectionGB.PerformLayout();
            this.trackInfoGB.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox connectionGB;
        private System.Windows.Forms.Label connectionStatusLbl;
        private System.Windows.Forms.Label connLbl;
        private System.Windows.Forms.Button checkConnBtn;
        private System.Windows.Forms.GroupBox trackInfoGB;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label titleLbl;
        private System.Windows.Forms.Label authorLbl;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label titleSetLbl;
        private System.Windows.Forms.Label authorSetLbl;
    }
}

