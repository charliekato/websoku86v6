namespace websoku86v6
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblServerName = new Label();
            lblHtmlPath = new Label();
            lblIndexFile = new Label();
            lblPrgResult = new Label();
            lblRanking = new Label();
            txtBoxServerName = new TextBox();
            txtBoxHtmlPath = new TextBox();
            txtBoxIndexFile = new TextBox();
            txtBoxPrgResult = new TextBox();
            txtBoxRanking = new TextBox();
            btnRun = new Button();
            btnAutoRun = new Button();
            lblInterval = new Label();
            txtBoxInterval = new TextBox();
            txtBoxScoreFile = new TextBox();
            lblScoreFile = new Label();
            lblTitle = new Label();
            btnQuit = new Button();
            lblAutoRun = new Label();
            txtBoxKeyFile = new TextBox();
            lblKeyFile = new Label();
            btnConfirmServer = new Button();
            btnKeyFile = new Button();
            txtBoxHostName = new TextBox();
            lblHostName = new Label();
            lblNote = new Label();
            txtBoxUserName = new TextBox();
            lblUserName = new Label();
            txtBoxPort = new TextBox();
            lblPort = new Label();
            btnBrowse = new Button();
            toolTip1 = new ToolTip(components);
            toolTip2 = new ToolTip(components);
            lblYouTube = new Label();
            txtBoxYouTube = new TextBox();
            chkBoxInitSend = new CheckBox();
            lblSQLServer = new Label();
            lblHTMLFiles = new Label();
            txtBoxEventNo = new TextBox();
            lblEventNo = new Label();
            pictureBox3 = new PictureBox();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblServerName
            // 
            lblServerName.AutoSize = true;
            lblServerName.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblServerName.Location = new Point(110, 130);
            lblServerName.Margin = new Padding(2, 0, 2, 0);
            lblServerName.Name = "lblServerName";
            lblServerName.Size = new Size(189, 33);
            lblServerName.TabIndex = 0;
            lblServerName.Text = "Server Name";
            // 
            // lblHtmlPath
            // 
            lblHtmlPath.AutoSize = true;
            lblHtmlPath.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblHtmlPath.Location = new Point(110, 700);
            lblHtmlPath.Margin = new Padding(2, 0, 2, 0);
            lblHtmlPath.Name = "lblHtmlPath";
            lblHtmlPath.Size = new Size(196, 33);
            lblHtmlPath.TabIndex = 1;
            lblHtmlPath.Text = "Html file path";
            // 
            // lblIndexFile
            // 
            lblIndexFile.AutoSize = true;
            lblIndexFile.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblIndexFile.Location = new Point(110, 250);
            lblIndexFile.Margin = new Padding(2, 0, 2, 0);
            lblIndexFile.Name = "lblIndexFile";
            lblIndexFile.Size = new Size(216, 33);
            lblIndexFile.TabIndex = 2;
            lblIndexFile.Text = "index file name";
            // 
            // lblPrgResult
            // 
            lblPrgResult.AutoSize = true;
            lblPrgResult.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblPrgResult.Location = new Point(110, 310);
            lblPrgResult.Margin = new Padding(2, 0, 2, 0);
            lblPrgResult.Name = "lblPrgResult";
            lblPrgResult.Size = new Size(303, 33);
            lblPrgResult.TabIndex = 3;
            lblPrgResult.Text = "Program形式結果File";
            // 
            // lblRanking
            // 
            lblRanking.AutoSize = true;
            lblRanking.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblRanking.Location = new Point(110, 370);
            lblRanking.Margin = new Padding(2, 0, 2, 0);
            lblRanking.Name = "lblRanking";
            lblRanking.Size = new Size(177, 33);
            lblRanking.TabIndex = 4;
            lblRanking.Text = "Ranking File";
            // 
            // txtBoxServerName
            // 
            txtBoxServerName.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtBoxServerName.Location = new Point(450, 125);
            txtBoxServerName.Margin = new Padding(2, 5, 2, 5);
            txtBoxServerName.Name = "txtBoxServerName";
            txtBoxServerName.Size = new Size(303, 39);
            txtBoxServerName.TabIndex = 5;
            // 
            // txtBoxHtmlPath
            // 
            txtBoxHtmlPath.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtBoxHtmlPath.Location = new Point(450, 695);
            txtBoxHtmlPath.Margin = new Padding(2, 5, 2, 5);
            txtBoxHtmlPath.Name = "txtBoxHtmlPath";
            txtBoxHtmlPath.Size = new Size(726, 39);
            txtBoxHtmlPath.TabIndex = 6;
            // 
            // txtBoxIndexFile
            // 
            txtBoxIndexFile.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtBoxIndexFile.Location = new Point(450, 245);
            txtBoxIndexFile.Margin = new Padding(2, 5, 2, 5);
            txtBoxIndexFile.Name = "txtBoxIndexFile";
            txtBoxIndexFile.Size = new Size(303, 39);
            txtBoxIndexFile.TabIndex = 7;
            // 
            // txtBoxPrgResult
            // 
            txtBoxPrgResult.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtBoxPrgResult.Location = new Point(450, 305);
            txtBoxPrgResult.Margin = new Padding(2, 5, 2, 5);
            txtBoxPrgResult.Name = "txtBoxPrgResult";
            txtBoxPrgResult.Size = new Size(303, 39);
            txtBoxPrgResult.TabIndex = 8;
            // 
            // txtBoxRanking
            // 
            txtBoxRanking.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtBoxRanking.Location = new Point(450, 365);
            txtBoxRanking.Margin = new Padding(2, 5, 2, 5);
            txtBoxRanking.Name = "txtBoxRanking";
            txtBoxRanking.Size = new Size(303, 39);
            txtBoxRanking.TabIndex = 9;
            // 
            // btnRun
            // 
            btnRun.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnRun.Location = new Point(196, 1027);
            btnRun.Margin = new Padding(2, 5, 2, 5);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(162, 91);
            btnRun.TabIndex = 10;
            btnRun.Text = "作成";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // btnAutoRun
            // 
            btnAutoRun.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnAutoRun.Location = new Point(998, 1027);
            btnAutoRun.Margin = new Padding(2, 5, 2, 5);
            btnAutoRun.Name = "btnAutoRun";
            btnAutoRun.Size = new Size(154, 91);
            btnAutoRun.TabIndex = 11;
            btnAutoRun.Text = "開始";
            btnAutoRun.UseVisualStyleBackColor = true;
            btnAutoRun.Click += btnAutoRun_Click;
            // 
            // lblInterval
            // 
            lblInterval.AutoSize = true;
            lblInterval.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblInterval.Location = new Point(526, 1056);
            lblInterval.Margin = new Padding(2, 0, 2, 0);
            lblInterval.Name = "lblInterval";
            lblInterval.Size = new Size(259, 33);
            lblInterval.TabIndex = 12;
            lblInterval.Text = "自動実行間隔(分)";
            // 
            // txtBoxInterval
            // 
            txtBoxInterval.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtBoxInterval.Location = new Point(829, 1051);
            txtBoxInterval.Margin = new Padding(2, 5, 2, 5);
            txtBoxInterval.Name = "txtBoxInterval";
            txtBoxInterval.Size = new Size(67, 39);
            txtBoxInterval.TabIndex = 13;
            txtBoxInterval.Text = "5";
            // 
            // txtBoxScoreFile
            // 
            txtBoxScoreFile.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtBoxScoreFile.Location = new Point(450, 425);
            txtBoxScoreFile.Margin = new Padding(2, 5, 2, 5);
            txtBoxScoreFile.Name = "txtBoxScoreFile";
            txtBoxScoreFile.Size = new Size(303, 39);
            txtBoxScoreFile.TabIndex = 17;
            // 
            // lblScoreFile
            // 
            lblScoreFile.AutoSize = true;
            lblScoreFile.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblScoreFile.Location = new Point(110, 430);
            lblScoreFile.Margin = new Padding(2, 0, 2, 0);
            lblScoreFile.Name = "lblScoreFile";
            lblScoreFile.Size = new Size(151, 33);
            lblScoreFile.TabIndex = 16;
            lblScoreFile.Text = "Score File";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("MS UI Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblTitle.Location = new Point(299, 29);
            lblTitle.Margin = new Padding(2, 0, 2, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(472, 54);
            lblTitle.TabIndex = 18;
            lblTitle.Text = "WEB速報作成ツール";
            // 
            // btnQuit
            // 
            btnQuit.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnQuit.Location = new Point(1044, 14);
            btnQuit.Margin = new Padding(2, 5, 2, 5);
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new Size(162, 91);
            btnQuit.TabIndex = 21;
            btnQuit.Text = "終了";
            btnQuit.UseVisualStyleBackColor = true;
            btnQuit.Click += btnQuit_Click;
            // 
            // lblAutoRun
            // 
            lblAutoRun.AutoSize = true;
            lblAutoRun.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblAutoRun.Location = new Point(998, 989);
            lblAutoRun.Margin = new Padding(2, 0, 2, 0);
            lblAutoRun.Name = "lblAutoRun";
            lblAutoRun.Size = new Size(143, 33);
            lblAutoRun.TabIndex = 22;
            lblAutoRun.Text = "自動実行";
            // 
            // txtBoxKeyFile
            // 
            txtBoxKeyFile.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtBoxKeyFile.Location = new Point(453, 755);
            txtBoxKeyFile.Margin = new Padding(2, 5, 2, 5);
            txtBoxKeyFile.Name = "txtBoxKeyFile";
            txtBoxKeyFile.Size = new Size(726, 39);
            txtBoxKeyFile.TabIndex = 24;
            // 
            // lblKeyFile
            // 
            lblKeyFile.AutoSize = true;
            lblKeyFile.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblKeyFile.Location = new Point(110, 760);
            lblKeyFile.Margin = new Padding(2, 0, 2, 0);
            lblKeyFile.Name = "lblKeyFile";
            lblKeyFile.Size = new Size(201, 33);
            lblKeyFile.TabIndex = 23;
            lblKeyFile.Text = "秘密鍵ファイル";
            // 
            // btnConfirmServer
            // 
            btnConfirmServer.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnConfirmServer.Location = new Point(946, 200);
            btnConfirmServer.Margin = new Padding(3, 4, 3, 4);
            btnConfirmServer.Name = "btnConfirmServer";
            btnConfirmServer.Size = new Size(170, 69);
            btnConfirmServer.TabIndex = 25;
            btnConfirmServer.Text = "接続確認";
            btnConfirmServer.UseVisualStyleBackColor = true;
            btnConfirmServer.Click += btnConfirmServer_Click;
            // 
            // btnKeyFile
            // 
            btnKeyFile.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnKeyFile.Location = new Point(1075, 811);
            btnKeyFile.Margin = new Padding(3, 4, 3, 4);
            btnKeyFile.Name = "btnKeyFile";
            btnKeyFile.Size = new Size(104, 64);
            btnKeyFile.TabIndex = 28;
            btnKeyFile.Text = "選択";
            btnKeyFile.UseVisualStyleBackColor = true;
            btnKeyFile.Click += btnKeyFile_Click;
            // 
            // txtBoxHostName
            // 
            txtBoxHostName.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtBoxHostName.Location = new Point(450, 575);
            txtBoxHostName.Margin = new Padding(2, 5, 2, 5);
            txtBoxHostName.Name = "txtBoxHostName";
            txtBoxHostName.Size = new Size(303, 39);
            txtBoxHostName.TabIndex = 30;
            // 
            // lblHostName
            // 
            lblHostName.AutoSize = true;
            lblHostName.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblHostName.Location = new Point(110, 580);
            lblHostName.Margin = new Padding(2, 0, 2, 0);
            lblHostName.Name = "lblHostName";
            lblHostName.Size = new Size(164, 33);
            lblHostName.TabIndex = 29;
            lblHostName.Text = "Host Name";
            // 
            // lblNote
            // 
            lblNote.AutoSize = true;
            lblNote.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblNote.Location = new Point(222, 506);
            lblNote.Name = "lblNote";
            lblNote.Size = new Size(894, 33);
            lblNote.TabIndex = 31;
            lblNote.Text = "-------- Webサーバーにアップロードする場合は以下も設定の事----";
            // 
            // txtBoxUserName
            // 
            txtBoxUserName.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtBoxUserName.Location = new Point(450, 635);
            txtBoxUserName.Margin = new Padding(2, 5, 2, 5);
            txtBoxUserName.Name = "txtBoxUserName";
            txtBoxUserName.Size = new Size(303, 39);
            txtBoxUserName.TabIndex = 33;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblUserName.Location = new Point(110, 640);
            lblUserName.Margin = new Padding(2, 0, 2, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(164, 33);
            lblUserName.TabIndex = 32;
            lblUserName.Text = "User Name";
            // 
            // txtBoxPort
            // 
            txtBoxPort.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtBoxPort.Location = new Point(953, 596);
            txtBoxPort.Margin = new Padding(2, 5, 2, 5);
            txtBoxPort.Name = "txtBoxPort";
            txtBoxPort.Size = new Size(76, 39);
            txtBoxPort.TabIndex = 35;
            txtBoxPort.Text = "22";
            toolTip1.SetToolTip(txtBoxPort, "sshのポート番号");
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblPort.Location = new Point(845, 596);
            lblPort.Margin = new Padding(2, 0, 2, 0);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(73, 33);
            lblPort.TabIndex = 34;
            lblPort.Text = "Port";
            // 
            // btnBrowse
            // 
            btnBrowse.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnBrowse.Location = new Point(953, 397);
            btnBrowse.Margin = new Padding(3, 4, 3, 4);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(142, 67);
            btnBrowse.TabIndex = 36;
            btnBrowse.Text = "確認";
            toolTip1.SetToolTip(btnBrowse, "ブラウザーで確認する");
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // lblYouTube
            // 
            lblYouTube.AutoSize = true;
            lblYouTube.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblYouTube.Location = new Point(110, 931);
            lblYouTube.Margin = new Padding(2, 0, 2, 0);
            lblYouTube.Name = "lblYouTube";
            lblYouTube.Size = new Size(201, 33);
            lblYouTube.TabIndex = 37;
            lblYouTube.Text = "YouTube URL";
            // 
            // txtBoxYouTube
            // 
            txtBoxYouTube.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtBoxYouTube.Location = new Point(453, 925);
            txtBoxYouTube.Margin = new Padding(2, 5, 2, 5);
            txtBoxYouTube.Name = "txtBoxYouTube";
            txtBoxYouTube.Size = new Size(726, 39);
            txtBoxYouTube.TabIndex = 38;
            // 
            // chkBoxInitSend
            // 
            chkBoxInitSend.AutoSize = true;
            chkBoxInitSend.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            chkBoxInitSend.Location = new Point(110, 849);
            chkBoxInitSend.Margin = new Padding(3, 4, 3, 4);
            chkBoxInitSend.Name = "chkBoxInitSend";
            chkBoxInitSend.Size = new Size(418, 37);
            chkBoxInitSend.TabIndex = 40;
            chkBoxInitSend.Text = "  CSS, JS ファイルも転送する";
            chkBoxInitSend.UseVisualStyleBackColor = true;
            // 
            // lblSQLServer
            // 
            lblSQLServer.AutoSize = true;
            lblSQLServer.Location = new Point(778, 125);
            lblSQLServer.Name = "lblSQLServer";
            lblSQLServer.Size = new Size(234, 32);
            lblSQLServer.TabIndex = 42;
            lblSQLServer.Text = "<--SQL Serverの名前";
            // 
            // lblHTMLFiles
            // 
            lblHTMLFiles.AutoSize = true;
            lblHTMLFiles.Location = new Point(845, 339);
            lblHTMLFiles.Name = "lblHTMLFiles";
            lblHTMLFiles.Size = new Size(206, 32);
            lblHTMLFiles.TabIndex = 43;
            lblHTMLFiles.Text = "HTML形式のFile名";
            // 
            // txtBoxEventNo
            // 
            txtBoxEventNo.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtBoxEventNo.Location = new Point(450, 185);
            txtBoxEventNo.Margin = new Padding(2, 5, 2, 5);
            txtBoxEventNo.Name = "txtBoxEventNo";
            txtBoxEventNo.Size = new Size(55, 39);
            txtBoxEventNo.TabIndex = 45;
            txtBoxEventNo.TextAlign = HorizontalAlignment.Right;
            // 
            // lblEventNo
            // 
            lblEventNo.AutoSize = true;
            lblEventNo.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblEventNo.Location = new Point(110, 190);
            lblEventNo.Margin = new Padding(2, 0, 2, 0);
            lblEventNo.Name = "lblEventNo";
            lblEventNo.Size = new Size(143, 33);
            lblEventNo.TabIndex = 44;
            lblEventNo.Text = "大会番号";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.image1;
            pictureBox3.Location = new Point(795, 255);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(33, 195);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 48;
            pictureBox3.TabStop = false;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(txtBoxEventNo);
            panel1.Controls.Add(lblEventNo);
            panel1.Controls.Add(lblHTMLFiles);
            panel1.Controls.Add(lblSQLServer);
            panel1.Controls.Add(chkBoxInitSend);
            panel1.Controls.Add(txtBoxYouTube);
            panel1.Controls.Add(lblYouTube);
            panel1.Controls.Add(btnBrowse);
            panel1.Controls.Add(txtBoxPort);
            panel1.Controls.Add(lblPort);
            panel1.Controls.Add(txtBoxUserName);
            panel1.Controls.Add(lblUserName);
            panel1.Controls.Add(lblNote);
            panel1.Controls.Add(txtBoxHostName);
            panel1.Controls.Add(lblHostName);
            panel1.Controls.Add(btnKeyFile);
            panel1.Controls.Add(btnConfirmServer);
            panel1.Controls.Add(txtBoxKeyFile);
            panel1.Controls.Add(lblKeyFile);
            panel1.Controls.Add(lblAutoRun);
            panel1.Controls.Add(btnQuit);
            panel1.Controls.Add(lblTitle);
            panel1.Controls.Add(txtBoxScoreFile);
            panel1.Controls.Add(lblScoreFile);
            panel1.Controls.Add(txtBoxInterval);
            panel1.Controls.Add(lblInterval);
            panel1.Controls.Add(btnAutoRun);
            panel1.Controls.Add(btnRun);
            panel1.Controls.Add(txtBoxRanking);
            panel1.Controls.Add(txtBoxPrgResult);
            panel1.Controls.Add(txtBoxIndexFile);
            panel1.Controls.Add(txtBoxHtmlPath);
            panel1.Controls.Add(txtBoxServerName);
            panel1.Controls.Add(lblRanking);
            panel1.Controls.Add(lblPrgResult);
            panel1.Controls.Add(lblIndexFile);
            panel1.Controls.Add(lblHtmlPath);
            panel1.Controls.Add(lblServerName);
            panel1.Location = new Point(0, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1341, 1270);
            panel1.TabIndex = 49;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1331, 1205);
            Controls.Add(panel1);
            Margin = new Padding(2, 5, 2, 5);
            Name = "Form1";
            Text = "(C)一般社団法人大津市水泳協会";
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblServerName;
        private System.Windows.Forms.Label lblHtmlPath;
        private System.Windows.Forms.Label lblIndexFile;
        private System.Windows.Forms.Label lblPrgResult;
        private System.Windows.Forms.Label lblRanking;
        private System.Windows.Forms.TextBox txtBoxServerName;
        private System.Windows.Forms.TextBox txtBoxHtmlPath;
        private System.Windows.Forms.TextBox txtBoxIndexFile;
        private System.Windows.Forms.TextBox txtBoxPrgResult;
        private System.Windows.Forms.TextBox txtBoxRanking;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnAutoRun;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.TextBox txtBoxInterval;
        private System.Windows.Forms.TextBox txtBoxScoreFile;
        private System.Windows.Forms.Label lblScoreFile;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Label lblAutoRun;
        private System.Windows.Forms.TextBox txtBoxKeyFile;
        private System.Windows.Forms.Label lblKeyFile;
        private System.Windows.Forms.Button btnConfirmServer;
        private System.Windows.Forms.Button btnKeyFile;
        private System.Windows.Forms.TextBox txtBoxHostName;
        private System.Windows.Forms.Label lblHostName;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtBoxUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtBoxPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Label lblYouTube;
        private System.Windows.Forms.TextBox txtBoxYouTube;
        private System.Windows.Forms.CheckBox chkBoxInitSend;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblSQLServer;
        private System.Windows.Forms.Label lblHTMLFiles;
        private System.Windows.Forms.TextBox txtBoxEventNo;
        private System.Windows.Forms.Label lblEventNo;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Panel panel1;
    }
}
