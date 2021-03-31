
namespace KMedoidsClustering
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
            this.btnLoadDataPoints = new System.Windows.Forms.Button();
            this.btnInitializeData = new System.Windows.Forms.Button();
            this.lblK = new System.Windows.Forms.Label();
            this.txtNumClusters = new System.Windows.Forms.TextBox();
            this.btnDoKMedoidClustering = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.pic1 = new System.Windows.Forms.PictureBox();
            this.btnRedrawClusters = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadDataPoints
            // 
            this.btnLoadDataPoints.Location = new System.Drawing.Point(12, 12);
            this.btnLoadDataPoints.Name = "btnLoadDataPoints";
            this.btnLoadDataPoints.Size = new System.Drawing.Size(140, 40);
            this.btnLoadDataPoints.TabIndex = 0;
            this.btnLoadDataPoints.Text = "Load Data Points";
            this.btnLoadDataPoints.UseVisualStyleBackColor = true;
            this.btnLoadDataPoints.Click += new System.EventHandler(this.btnLoadDataPoints_Click);
            // 
            // btnInitializeData
            // 
            this.btnInitializeData.Location = new System.Drawing.Point(12, 58);
            this.btnInitializeData.Name = "btnInitializeData";
            this.btnInitializeData.Size = new System.Drawing.Size(140, 40);
            this.btnInitializeData.TabIndex = 0;
            this.btnInitializeData.Text = "Initialize Data";
            this.btnInitializeData.UseVisualStyleBackColor = true;
            this.btnInitializeData.Click += new System.EventHandler(this.btnInitializeData_Click);
            // 
            // lblK
            // 
            this.lblK.AutoSize = true;
            this.lblK.Location = new System.Drawing.Point(77, 115);
            this.lblK.Name = "lblK";
            this.lblK.Size = new System.Drawing.Size(17, 17);
            this.lblK.TabIndex = 1;
            this.lblK.Text = "K";
            // 
            // txtNumClusters
            // 
            this.txtNumClusters.Location = new System.Drawing.Point(100, 112);
            this.txtNumClusters.Name = "txtNumClusters";
            this.txtNumClusters.Size = new System.Drawing.Size(52, 22);
            this.txtNumClusters.TabIndex = 2;
            this.txtNumClusters.Text = "3";
            // 
            // btnDoKMedoidClustering
            // 
            this.btnDoKMedoidClustering.Location = new System.Drawing.Point(12, 154);
            this.btnDoKMedoidClustering.Name = "btnDoKMedoidClustering";
            this.btnDoKMedoidClustering.Size = new System.Drawing.Size(140, 40);
            this.btnDoKMedoidClustering.TabIndex = 0;
            this.btnDoKMedoidClustering.Text = "K-Medoid";
            this.btnDoKMedoidClustering.UseVisualStyleBackColor = true;
            this.btnDoKMedoidClustering.Click += new System.EventHandler(this.btnDoKMedoidClustering_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(12, 200);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(204, 653);
            this.txtResult.TabIndex = 3;
            // 
            // pic1
            // 
            this.pic1.Location = new System.Drawing.Point(231, 12);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(1046, 841);
            this.pic1.TabIndex = 4;
            this.pic1.TabStop = false;
            // 
            // btnRedrawClusters
            // 
            this.btnRedrawClusters.Location = new System.Drawing.Point(648, 859);
            this.btnRedrawClusters.Name = "btnRedrawClusters";
            this.btnRedrawClusters.Size = new System.Drawing.Size(140, 40);
            this.btnRedrawClusters.TabIndex = 0;
            this.btnRedrawClusters.Text = "Redraw Clusters";
            this.btnRedrawClusters.UseVisualStyleBackColor = true;
            this.btnRedrawClusters.Click += new System.EventHandler(this.btnRedrawClusters_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 903);
            this.Controls.Add(this.pic1);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtNumClusters);
            this.Controls.Add(this.lblK);
            this.Controls.Add(this.btnRedrawClusters);
            this.Controls.Add(this.btnDoKMedoidClustering);
            this.Controls.Add(this.btnInitializeData);
            this.Controls.Add(this.btnLoadDataPoints);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadDataPoints;
        private System.Windows.Forms.Button btnInitializeData;
        private System.Windows.Forms.Label lblK;
        private System.Windows.Forms.TextBox txtNumClusters;
        private System.Windows.Forms.Button btnDoKMedoidClustering;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.PictureBox pic1;
        private System.Windows.Forms.Button btnRedrawClusters;
    }
}

