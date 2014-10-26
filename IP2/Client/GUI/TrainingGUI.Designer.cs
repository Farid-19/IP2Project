namespace Client.GUI
{
    partial class TrainingGUI
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataPULSE = new System.Windows.Forms.Label();
            this.dataENERGY = new System.Windows.Forms.Label();
            this.dataPOWER = new System.Windows.Forms.Label();
            this.dataPOWERPCT = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dataTIME = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dataDISTANCE = new System.Windows.Forms.Label();
            this.dataSPEED = new System.Windows.Forms.Label();
            this.dataRPM = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._graph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.setPowerLabel = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this._graph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataPULSE
            // 
            this.dataPULSE.AutoSize = true;
            this.dataPULSE.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataPULSE.Location = new System.Drawing.Point(634, 187);
            this.dataPULSE.Name = "dataPULSE";
            this.dataPULSE.Size = new System.Drawing.Size(39, 37);
            this.dataPULSE.TabIndex = 44;
            this.dataPULSE.Text = "P";
            this.dataPULSE.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dataENERGY
            // 
            this.dataENERGY.AutoSize = true;
            this.dataENERGY.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataENERGY.Location = new System.Drawing.Point(474, 187);
            this.dataENERGY.Name = "dataENERGY";
            this.dataENERGY.Size = new System.Drawing.Size(61, 37);
            this.dataENERGY.TabIndex = 43;
            this.dataENERGY.Text = "----";
            this.dataENERGY.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dataPOWER
            // 
            this.dataPOWER.AutoSize = true;
            this.dataPOWER.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataPOWER.Location = new System.Drawing.Point(330, 187);
            this.dataPOWER.Name = "dataPOWER";
            this.dataPOWER.Size = new System.Drawing.Size(50, 37);
            this.dataPOWER.TabIndex = 42;
            this.dataPOWER.Text = "---";
            this.dataPOWER.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dataPOWERPCT
            // 
            this.dataPOWERPCT.AutoSize = true;
            this.dataPOWERPCT.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataPOWERPCT.Location = new System.Drawing.Point(332, 141);
            this.dataPOWERPCT.Name = "dataPOWERPCT";
            this.dataPOWERPCT.Size = new System.Drawing.Size(45, 29);
            this.dataPOWERPCT.TabIndex = 41;
            this.dataPOWERPCT.Text = "-%";
            this.dataPOWERPCT.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(591, 174);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 13);
            this.label11.TabIndex = 40;
            this.label11.Text = "PULSE";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(447, 174);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 39;
            this.label10.Text = "ENERGY";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(311, 174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 38;
            this.label9.Text = "POWER";
            // 
            // dataTIME
            // 
            this.dataTIME.AutoSize = true;
            this.dataTIME.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataTIME.Location = new System.Drawing.Point(441, 120);
            this.dataTIME.Name = "dataTIME";
            this.dataTIME.Size = new System.Drawing.Size(106, 55);
            this.dataTIME.TabIndex = 37;
            this.dataTIME.Text = "--:--";
            this.dataTIME.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(447, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "TIME";
            // 
            // dataDISTANCE
            // 
            this.dataDISTANCE.AutoSize = true;
            this.dataDISTANCE.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataDISTANCE.Location = new System.Drawing.Point(588, 57);
            this.dataDISTANCE.Name = "dataDISTANCE";
            this.dataDISTANCE.Size = new System.Drawing.Size(71, 37);
            this.dataDISTANCE.TabIndex = 35;
            this.dataDISTANCE.Text = "--.--";
            this.dataDISTANCE.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dataSPEED
            // 
            this.dataSPEED.AutoSize = true;
            this.dataSPEED.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataSPEED.Location = new System.Drawing.Point(468, 57);
            this.dataSPEED.Name = "dataSPEED";
            this.dataSPEED.Size = new System.Drawing.Size(71, 37);
            this.dataSPEED.TabIndex = 34;
            this.dataSPEED.Text = "---.-";
            this.dataSPEED.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dataRPM
            // 
            this.dataRPM.AutoSize = true;
            this.dataRPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataRPM.Location = new System.Drawing.Point(330, 57);
            this.dataRPM.Name = "dataRPM";
            this.dataRPM.Size = new System.Drawing.Size(50, 37);
            this.dataRPM.TabIndex = 33;
            this.dataRPM.Text = "---";
            this.dataRPM.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(591, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "DISTANCE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(447, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "SPEED";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(311, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "RPM";
            // 
            // _graph
            // 
            chartArea2.Name = "ChartArea1";
            this._graph.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this._graph.Legends.Add(legend2);
            this._graph.Location = new System.Drawing.Point(85, 247);
            this._graph.Name = "_graph";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.DarkBlue;
            series3.Legend = "Legend1";
            series3.Name = "SPEED";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.DarkRed;
            series4.Legend = "Legend1";
            series4.Name = "PULSE";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            this._graph.Series.Add(series3);
            this._graph.Series.Add(series4);
            this._graph.Size = new System.Drawing.Size(839, 343);
            this._graph.TabIndex = 45;
            // 
            // setPowerLabel
            // 
            this.setPowerLabel.AutoSize = true;
            this.setPowerLabel.Location = new System.Drawing.Point(200, 178);
            this.setPowerLabel.Name = "setPowerLabel";
            this.setPowerLabel.Size = new System.Drawing.Size(72, 13);
            this.setPowerLabel.TabIndex = 47;
            this.setPowerLabel.Text = "SET POWER";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(130, 172);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown1.TabIndex = 46;
            this.numericUpDown1.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // TrainingGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 622);
            this.Controls.Add(this.setPowerLabel);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this._graph);
            this.Controls.Add(this.dataPULSE);
            this.Controls.Add(this.dataENERGY);
            this.Controls.Add(this.dataPOWER);
            this.Controls.Add(this.dataPOWERPCT);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dataTIME);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dataDISTANCE);
            this.Controls.Add(this.dataSPEED);
            this.Controls.Add(this.dataRPM);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TrainingGUI";
            this.Text = "Training";
            ((System.ComponentModel.ISupportInitialize)(this._graph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dataPULSE;
        private System.Windows.Forms.Label dataENERGY;
        private System.Windows.Forms.Label dataPOWER;
        private System.Windows.Forms.Label dataPOWERPCT;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label dataTIME;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label dataDISTANCE;
        private System.Windows.Forms.Label dataSPEED;
        private System.Windows.Forms.Label dataRPM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart _graph;
        private System.Windows.Forms.Label setPowerLabel;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}