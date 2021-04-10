namespace GMM
{
    partial class Form1
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
            this.btnTestMM = new System.Windows.Forms.Button();
            this.btnTestClass = new System.Windows.Forms.Button();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.lblNum = new System.Windows.Forms.Label();
            this.pic1 = new System.Windows.Forms.PictureBox();
            this.btnGMMND = new System.Windows.Forms.Button();
            this.txtNumClusters = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTennisGMM = new System.Windows.Forms.Button();
            this.btnTennisSwarm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTestMM
            // 
            this.btnTestMM.Location = new System.Drawing.Point(30, 21);
            this.btnTestMM.Margin = new System.Windows.Forms.Padding(2);
            this.btnTestMM.Name = "btnTestMM";
            this.btnTestMM.Size = new System.Drawing.Size(174, 30);
            this.btnTestMM.TabIndex = 0;
            this.btnTestMM.Text = "GMM 1D";
            this.btnTestMM.UseVisualStyleBackColor = true;
            this.btnTestMM.Click += new System.EventHandler(this.btnTestMM_Click);
            // 
            // btnTestClass
            // 
            this.btnTestClass.Location = new System.Drawing.Point(30, 113);
            this.btnTestClass.Margin = new System.Windows.Forms.Padding(2);
            this.btnTestClass.Name = "btnTestClass";
            this.btnTestClass.Size = new System.Drawing.Size(174, 27);
            this.btnTestClass.TabIndex = 1;
            this.btnTestClass.Text = "Test Class";
            this.btnTestClass.UseVisualStyleBackColor = true;
            this.btnTestClass.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtNum
            // 
            this.txtNum.Location = new System.Drawing.Point(132, 84);
            this.txtNum.Margin = new System.Windows.Forms.Padding(2);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(75, 22);
            this.txtNum.TabIndex = 2;
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(34, 84);
            this.lblNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(96, 17);
            this.lblNum.TabIndex = 3;
            this.lblNum.Text = "Enter Number";
            // 
            // pic1
            // 
            this.pic1.Location = new System.Drawing.Point(226, 21);
            this.pic1.Margin = new System.Windows.Forms.Padding(2);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(1155, 762);
            this.pic1.TabIndex = 5;
            this.pic1.TabStop = false;
            // 
            // btnGMMND
            // 
            this.btnGMMND.Location = new System.Drawing.Point(30, 341);
            this.btnGMMND.Margin = new System.Windows.Forms.Padding(2);
            this.btnGMMND.Name = "btnGMMND";
            this.btnGMMND.Size = new System.Drawing.Size(174, 31);
            this.btnGMMND.TabIndex = 6;
            this.btnGMMND.Text = "GMM ND";
            this.btnGMMND.UseVisualStyleBackColor = true;
            this.btnGMMND.Click += new System.EventHandler(this.btnGMMND_Click);
            // 
            // txtNumClusters
            // 
            this.txtNumClusters.Location = new System.Drawing.Point(163, 313);
            this.txtNumClusters.Margin = new System.Windows.Forms.Padding(2);
            this.txtNumClusters.Name = "txtNumClusters";
            this.txtNumClusters.Size = new System.Drawing.Size(44, 22);
            this.txtNumClusters.TabIndex = 7;
            this.txtNumClusters.Text = "3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 316);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "number of Clusters";
            // 
            // btnTennisGMM
            // 
            this.btnTennisGMM.Location = new System.Drawing.Point(30, 409);
            this.btnTennisGMM.Margin = new System.Windows.Forms.Padding(2);
            this.btnTennisGMM.Name = "btnTennisGMM";
            this.btnTennisGMM.Size = new System.Drawing.Size(174, 31);
            this.btnTennisGMM.TabIndex = 6;
            this.btnTennisGMM.Text = "GMM Tennis";
            this.btnTennisGMM.UseVisualStyleBackColor = true;
            this.btnTennisGMM.Click += new System.EventHandler(this.btnTennisGMM_Click);
            // 
            // btnTennisSwarm
            // 
            this.btnTennisSwarm.Location = new System.Drawing.Point(30, 467);
            this.btnTennisSwarm.Margin = new System.Windows.Forms.Padding(2);
            this.btnTennisSwarm.Name = "btnTennisSwarm";
            this.btnTennisSwarm.Size = new System.Drawing.Size(174, 31);
            this.btnTennisSwarm.TabIndex = 6;
            this.btnTennisSwarm.Text = "GMM Tennis Swarm";
            this.btnTennisSwarm.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1402, 798);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNumClusters);
            this.Controls.Add(this.btnTennisSwarm);
            this.Controls.Add(this.btnTennisGMM);
            this.Controls.Add(this.btnGMMND);
            this.Controls.Add(this.pic1);
            this.Controls.Add(this.lblNum);
            this.Controls.Add(this.txtNum);
            this.Controls.Add(this.btnTestClass);
            this.Controls.Add(this.btnTestMM);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTestMM;
        private System.Windows.Forms.Button btnTestClass;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.PictureBox pic1;
        private System.Windows.Forms.Button btnGMMND;
        private System.Windows.Forms.TextBox txtNumClusters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTennisGMM;
        private System.Windows.Forms.Button btnTennisSwarm;
    }
}

