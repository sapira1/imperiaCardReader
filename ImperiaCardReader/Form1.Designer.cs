namespace ImperiaCardReader
{
    partial class ImperiaCardReader
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.ButtonGo = new System.Windows.Forms.Button();
            this.ButtonGetMap = new System.Windows.Forms.Button();
            this.UserURL = new System.Windows.Forms.TextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.World = new System.Windows.Forms.Label();
            this.WorldNum = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.WorldNum)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonGo
            // 
            this.ButtonGo.AutoSize = true;
            this.ButtonGo.Location = new System.Drawing.Point(579, 12);
            this.ButtonGo.Name = "ButtonGo";
            this.ButtonGo.Size = new System.Drawing.Size(75, 23);
            this.ButtonGo.TabIndex = 0;
            this.ButtonGo.Text = "Go";
            this.ButtonGo.UseVisualStyleBackColor = true;
            this.ButtonGo.Click += new System.EventHandler(this.ButtonGo_Click);
            // 
            // ButtonGetMap
            // 
            this.ButtonGetMap.AutoSize = true;
            this.ButtonGetMap.Location = new System.Drawing.Point(753, 12);
            this.ButtonGetMap.Name = "ButtonGetMap";
            this.ButtonGetMap.Size = new System.Drawing.Size(75, 23);
            this.ButtonGetMap.TabIndex = 1;
            this.ButtonGetMap.Text = "Map";
            this.ButtonGetMap.UseVisualStyleBackColor = true;
            this.ButtonGetMap.Click += new System.EventHandler(this.ButtonGetMap_Click);
            // 
            // UserURL
            // 
            this.UserURL.Location = new System.Drawing.Point(13, 14);
            this.UserURL.Name = "UserURL";
            this.UserURL.Size = new System.Drawing.Size(560, 20);
            this.UserURL.TabIndex = 2;
            this.UserURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserURL_KeyDown);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(0, 40);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(840, 480);
            this.webBrowser1.TabIndex = 3;
            this.webBrowser1.Url = new System.Uri("http://www.imperiaonline.org/", System.UriKind.Absolute);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.FileDownload += new System.EventHandler(this.webBrowser1_FileDownload);
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            // 
            // World
            // 
            this.World.AutoSize = true;
            this.World.Location = new System.Drawing.Point(660, 17);
            this.World.Name = "World";
            this.World.Size = new System.Drawing.Size(38, 13);
            this.World.TabIndex = 4;
            this.World.Text = "World:";
            // 
            // WorldNum
            // 
            this.WorldNum.Location = new System.Drawing.Point(704, 14);
            this.WorldNum.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.WorldNum.Name = "WorldNum";
            this.WorldNum.Size = new System.Drawing.Size(43, 20);
            this.WorldNum.TabIndex = 6;
            // 
            // ImperiaCardReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(844, 521);
            this.Controls.Add(this.World);
            this.Controls.Add(this.UserURL);
            this.Controls.Add(this.ButtonGo);
            this.Controls.Add(this.ButtonGetMap);
            this.Controls.Add(this.WorldNum);
            this.Controls.Add(this.webBrowser1);
            this.MaximizeBox = false;
            this.Name = "ImperiaCardReader";
            this.Text = "ImperiaCardReader";
            ((System.ComponentModel.ISupportInitialize)(this.WorldNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonGo;
        private System.Windows.Forms.Button ButtonGetMap;
        private System.Windows.Forms.TextBox UserURL;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label World;
        private System.Windows.Forms.NumericUpDown WorldNum;
    }
}

