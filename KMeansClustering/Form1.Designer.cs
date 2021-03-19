
namespace KMeansClustering
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
            this.btnLoadDataFile = new System.Windows.Forms.Button();
            this.btnInitializeData = new System.Windows.Forms.Button();
            this.btnDoKMeans = new System.Windows.Forms.Button();
            this.btnDoKMeansPlus = new System.Windows.Forms.Button();
            this.btnKMeansPlusMinVar = new System.Windows.Forms.Button();
            this.txtNumOfClusters = new System.Windows.Forms.TextBox();
            this.lblNumClusters = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.pic1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadDataFile
            // 
            this.btnLoadDataFile.Location = new System.Drawing.Point(12, 12);
            this.btnLoadDataFile.Name = "btnLoadDataFile";
            this.btnLoadDataFile.Size = new System.Drawing.Size(148, 40);
            this.btnLoadDataFile.TabIndex = 0;
            this.btnLoadDataFile.Text = "Load Data File";
            this.btnLoadDataFile.UseVisualStyleBackColor = true;
            // 
            // btnInitializeData
            // 
            this.btnInitializeData.Location = new System.Drawing.Point(12, 58);
            this.btnInitializeData.Name = "btnInitializeData";
            this.btnInitializeData.Size = new System.Drawing.Size(148, 40);
            this.btnInitializeData.TabIndex = 0;
            this.btnInitializeData.Text = "Initialize Data";
            this.btnInitializeData.UseVisualStyleBackColor = true;
            // 
            // btnDoKMeans
            // 
            this.btnDoKMeans.Location = new System.Drawing.Point(12, 143);
            this.btnDoKMeans.Name = "btnDoKMeans";
            this.btnDoKMeans.Size = new System.Drawing.Size(148, 40);
            this.btnDoKMeans.TabIndex = 0;
            this.btnDoKMeans.Text = "Do KMeans";
            this.btnDoKMeans.UseVisualStyleBackColor = true;
            // 
            // btnDoKMeansPlus
            // 
            this.btnDoKMeansPlus.Location = new System.Drawing.Point(12, 189);
            this.btnDoKMeansPlus.Name = "btnDoKMeansPlus";
            this.btnDoKMeansPlus.Size = new System.Drawing.Size(148, 42);
            this.btnDoKMeansPlus.TabIndex = 0;
            this.btnDoKMeansPlus.Text = "Do KMeans ++ Clustering";
            this.btnDoKMeansPlus.UseVisualStyleBackColor = true;
            // 
            // btnKMeansPlusMinVar
            // 
            this.btnKMeansPlusMinVar.Location = new System.Drawing.Point(12, 237);
            this.btnKMeansPlusMinVar.Name = "btnKMeansPlusMinVar";
            this.btnKMeansPlusMinVar.Size = new System.Drawing.Size(148, 42);
            this.btnKMeansPlusMinVar.TabIndex = 0;
            this.btnKMeansPlusMinVar.Text = "Do KMeans ++ with Min Variance";
            this.btnKMeansPlusMinVar.UseVisualStyleBackColor = true;
            // 
            // txtNumOfClusters
            // 
            this.txtNumOfClusters.Location = new System.Drawing.Point(127, 104);
            this.txtNumOfClusters.Name = "txtNumOfClusters";
            this.txtNumOfClusters.Size = new System.Drawing.Size(33, 22);
            this.txtNumOfClusters.TabIndex = 1;
            this.txtNumOfClusters.Text = "3";
            // 
            // lblNumClusters
            // 
            this.lblNumClusters.AutoSize = true;
            this.lblNumClusters.Location = new System.Drawing.Point(13, 107);
            this.lblNumClusters.Name = "lblNumClusters";
            this.lblNumClusters.Size = new System.Drawing.Size(112, 17);
            this.lblNumClusters.TabIndex = 2;
            this.lblNumClusters.Text = "Num of Clusters:";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(12, 286);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(148, 379);
            this.txtResult.TabIndex = 3;
            // 
            // pic1
            // 
            this.pic1.Location = new System.Drawing.Point(167, 12);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(1025, 653);
            this.pic1.TabIndex = 4;
            this.pic1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 677);
            this.Controls.Add(this.pic1);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.lblNumClusters);
            this.Controls.Add(this.txtNumOfClusters);
            this.Controls.Add(this.btnKMeansPlusMinVar);
            this.Controls.Add(this.btnDoKMeansPlus);
            this.Controls.Add(this.btnDoKMeans);
            this.Controls.Add(this.btnInitializeData);
            this.Controls.Add(this.btnLoadDataFile);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadDataFile;
        private System.Windows.Forms.Button btnInitializeData;
        private System.Windows.Forms.Button btnDoKMeans;
        private System.Windows.Forms.Button btnDoKMeansPlus;
        private System.Windows.Forms.Button btnKMeansPlusMinVar;
        private System.Windows.Forms.TextBox txtNumOfClusters;
        private System.Windows.Forms.Label lblNumClusters;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.PictureBox pic1;
    }
}

