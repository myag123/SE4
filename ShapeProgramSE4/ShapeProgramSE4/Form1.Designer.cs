namespace ShapeProgramSE4
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.canvasBox = new System.Windows.Forms.PictureBox();
            this.txtCmdBox = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.lbl1 = new System.Windows.Forms.Label();
            this.helpBtn = new System.Windows.Forms.Button();
            this.txtCmdLine = new System.Windows.Forms.TextBox();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl3ErrorMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.canvasBox)).BeginInit();
            this.SuspendLayout();
            // 
            // canvasBox
            // 
            this.canvasBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.canvasBox.Location = new System.Drawing.Point(476, 92);
            this.canvasBox.Name = "canvasBox";
            this.canvasBox.Size = new System.Drawing.Size(500, 448);
            this.canvasBox.TabIndex = 0;
            this.canvasBox.TabStop = false;
            this.canvasBox.Paint += new System.Windows.Forms.PaintEventHandler(this.canvasBox_Paint);
            // 
            // txtCmdBox
            // 
            this.txtCmdBox.Location = new System.Drawing.Point(12, 92);
            this.txtCmdBox.Multiline = true;
            this.txtCmdBox.Name = "txtCmdBox";
            this.txtCmdBox.Size = new System.Drawing.Size(404, 400);
            this.txtCmdBox.TabIndex = 1;
            this.txtCmdBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txtCmdBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCmdBox_KeyDown);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(322, 586);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(94, 29);
            this.btnRun.TabIndex = 2;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(12, 69);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(195, 20);
            this.lbl1.TabIndex = 3;
            this.lbl1.Text = "Enter your command below:";
            // 
            // helpBtn
            // 
            this.helpBtn.Location = new System.Drawing.Point(882, 620);
            this.helpBtn.Name = "helpBtn";
            this.helpBtn.Size = new System.Drawing.Size(94, 29);
            this.helpBtn.TabIndex = 4;
            this.helpBtn.Text = "Help";
            this.helpBtn.UseVisualStyleBackColor = true;
            // 
            // txtCmdLine
            // 
            this.txtCmdLine.Location = new System.Drawing.Point(12, 539);
            this.txtCmdLine.Name = "txtCmdLine";
            this.txtCmdLine.Size = new System.Drawing.Size(404, 27);
            this.txtCmdLine.TabIndex = 5;
            this.txtCmdLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCmdLine_KeyDown);
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(12, 508);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(232, 20);
            this.lbl2.TabIndex = 6;
            this.lbl2.Text = "Enter your short command below:";
            // 
            // lbl3ErrorMsg
            // 
            this.lbl3ErrorMsg.AutoSize = true;
            this.lbl3ErrorMsg.Location = new System.Drawing.Point(476, 560);
            this.lbl3ErrorMsg.Name = "lbl3ErrorMsg";
            this.lbl3ErrorMsg.Size = new System.Drawing.Size(79, 20);
            this.lbl3ErrorMsg.TabIndex = 7;
            this.lbl3ErrorMsg.Text = "ResultMSg";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 661);
            this.Controls.Add(this.lbl3ErrorMsg);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.txtCmdLine);
            this.Controls.Add(this.helpBtn);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.txtCmdBox);
            this.Controls.Add(this.canvasBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.canvasBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox canvasBox;
        private System.Windows.Forms.TextBox txtCmdBox;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Button helpBtn;
        private System.Windows.Forms.TextBox txtCmdLine;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl3ErrorMsg;
    }
}

