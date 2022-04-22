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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.canvasBox = new System.Windows.Forms.PictureBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl3ErrorMsg = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSaveBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripLoadBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripHelpBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripListBtn = new System.Windows.Forms.ToolStripButton();
            this.btnVldte = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.richTxtCmdLine = new System.Windows.Forms.RichTextBox();
            this.richTxtCmdBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.canvasBox)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // canvasBox
            // 
            this.canvasBox.BackColor = System.Drawing.Color.DeepPink;
            this.canvasBox.Location = new System.Drawing.Point(476, 109);
            this.canvasBox.Name = "canvasBox";
            this.canvasBox.Size = new System.Drawing.Size(500, 448);
            this.canvasBox.TabIndex = 0;
            this.canvasBox.TabStop = false;
            this.canvasBox.Paint += new System.Windows.Forms.PaintEventHandler(this.canvasBox_Paint);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(322, 611);
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
            this.lbl1.Location = new System.Drawing.Point(12, 92);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(195, 20);
            this.lbl1.TabIndex = 3;
            this.lbl1.Text = "Enter your command below:";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(12, 537);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(232, 20);
            this.lbl2.TabIndex = 6;
            this.lbl2.Text = "Enter your short command below:";
            // 
            // lbl3ErrorMsg
            // 
            this.lbl3ErrorMsg.AutoSize = true;
            this.lbl3ErrorMsg.Location = new System.Drawing.Point(476, 578);
            this.lbl3ErrorMsg.Name = "lbl3ErrorMsg";
            this.lbl3ErrorMsg.Size = new System.Drawing.Size(79, 20);
            this.lbl3ErrorMsg.TabIndex = 7;
            this.lbl3ErrorMsg.Text = "ResultMSg";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightCyan;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSaveBtn,
            this.toolStripLoadBtn,
            this.toolStripHelpBtn,
            this.toolStripListBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(997, 27);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSaveBtn
            // 
            this.toolStripSaveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSaveBtn.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSaveBtn.Image")));
            this.toolStripSaveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSaveBtn.Name = "toolStripSaveBtn";
            this.toolStripSaveBtn.Size = new System.Drawing.Size(29, 24);
            this.toolStripSaveBtn.Text = "Save";
            // 
            // toolStripLoadBtn
            // 
            this.toolStripLoadBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLoadBtn.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLoadBtn.Image")));
            this.toolStripLoadBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLoadBtn.Name = "toolStripLoadBtn";
            this.toolStripLoadBtn.Size = new System.Drawing.Size(29, 24);
            this.toolStripLoadBtn.Text = "Load Project";
            // 
            // toolStripHelpBtn
            // 
            this.toolStripHelpBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripHelpBtn.Image = ((System.Drawing.Image)(resources.GetObject("toolStripHelpBtn.Image")));
            this.toolStripHelpBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripHelpBtn.Name = "toolStripHelpBtn";
            this.toolStripHelpBtn.Size = new System.Drawing.Size(29, 24);
            this.toolStripHelpBtn.Text = "Help";
            this.toolStripHelpBtn.Click += new System.EventHandler(this.toolStripHelpBtn_Click);
            // 
            // toolStripListBtn
            // 
            this.toolStripListBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripListBtn.Image = ((System.Drawing.Image)(resources.GetObject("toolStripListBtn.Image")));
            this.toolStripListBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripListBtn.Name = "toolStripListBtn";
            this.toolStripListBtn.Size = new System.Drawing.Size(29, 24);
            this.toolStripListBtn.Text = "Syntax List";
            this.toolStripListBtn.Click += new System.EventHandler(this.toolStripListBtn_Click);
            // 
            // btnVldte
            // 
            this.btnVldte.Location = new System.Drawing.Point(12, 611);
            this.btnVldte.Name = "btnVldte";
            this.btnVldte.Size = new System.Drawing.Size(94, 29);
            this.btnVldte.TabIndex = 9;
            this.btnVldte.Text = "Validate\r\n";
            this.btnVldte.UseVisualStyleBackColor = true;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.lblHeader.Location = new System.Drawing.Point(219, 38);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(512, 31);
            this.lblHeader.TabIndex = 10;
            this.lblHeader.Text = "Graphical Programming Language Application";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(166, 611);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(94, 29);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // richTxtCmdLine
            // 
            this.richTxtCmdLine.Location = new System.Drawing.Point(12, 560);
            this.richTxtCmdLine.Multiline = false;
            this.richTxtCmdLine.Name = "richTxtCmdLine";
            this.richTxtCmdLine.Size = new System.Drawing.Size(404, 27);
            this.richTxtCmdLine.TabIndex = 12;
            this.richTxtCmdLine.Text = "";
            this.richTxtCmdLine.TextChanged += new System.EventHandler(this.richTxtCmdLine_TextChanged);
            this.richTxtCmdLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTxtCmdLine_KeyDown);
            // 
            // richTxtCmdBox
            // 
            this.richTxtCmdBox.Location = new System.Drawing.Point(12, 115);
            this.richTxtCmdBox.Name = "richTxtCmdBox";
            this.richTxtCmdBox.Size = new System.Drawing.Size(437, 419);
            this.richTxtCmdBox.TabIndex = 14;
            this.richTxtCmdBox.Text = "";
            this.richTxtCmdBox.TextChanged += new System.EventHandler(this.richTxtCmdBox_TextChanged);
            this.richTxtCmdBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTxtCmdBox_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 661);
            this.Controls.Add(this.richTxtCmdBox);
            this.Controls.Add(this.richTxtCmdLine);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.btnVldte);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lbl3ErrorMsg);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.canvasBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.canvasBox)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox canvasBox;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl3ErrorMsg;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripSaveBtn;
        private System.Windows.Forms.ToolStripButton toolStripLoadBtn;
        private System.Windows.Forms.ToolStripButton toolStripHelpBtn;
        private System.Windows.Forms.Button btnVldte;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.ToolStripButton toolStripListBtn;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RichTextBox richTxtCmdLine;
        private System.Windows.Forms.RichTextBox richTxtCmdBox;
    }
}

