namespace SJIS2UTF8Converter;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private Label lblSourceFolder = null!;
    private TextBox txtSourceFolder = null!;
    private Button btnBrowseSource = null!;
    private Label lblDestinationFolder = null!;
    private TextBox txtDestinationFolder = null!;
    private Button btnBrowseDestination = null!;
    private CheckBox chkRecursive = null!;
    private GroupBox grpBom = null!;
    private RadioButton rdoBomNone = null!;
    private RadioButton rdoBomWith = null!;
    private Button btnStart = null!;
    private Button btnClear = null!;
    private TextBox txtLog = null!;

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
        components = new System.ComponentModel.Container();
        lblSourceFolder = new Label();
        txtSourceFolder = new TextBox();
        btnBrowseSource = new Button();
        lblDestinationFolder = new Label();
        txtDestinationFolder = new TextBox();
        btnBrowseDestination = new Button();
        chkRecursive = new CheckBox();
        grpBom = new GroupBox();
        rdoBomNone = new RadioButton();
        rdoBomWith = new RadioButton();
        btnStart = new Button();
        btnClear = new Button();
        txtLog = new TextBox();
        grpBom.SuspendLayout();
        SuspendLayout();
        // 
        // lblSourceFolder
        // 
        lblSourceFolder.AutoSize = true;
        lblSourceFolder.Location = new Point(12, 15);
        lblSourceFolder.Name = "lblSourceFolder";
        lblSourceFolder.Size = new Size(120, 20);
        lblSourceFolder.Text = "変換元（入力）フォルダ";
        // 
        // txtSourceFolder
        // 
        txtSourceFolder.AllowDrop = true;
        txtSourceFolder.Location = new Point(12, 40);
        txtSourceFolder.Name = "txtSourceFolder";
        txtSourceFolder.Size = new Size(660, 27);
        txtSourceFolder.DragEnter += FolderTextBox_DragEnter;
        txtSourceFolder.DragDrop += FolderTextBox_DragDrop;
        // 
        // btnBrowseSource
        // 
        btnBrowseSource.Location = new Point(680, 37);
        btnBrowseSource.Name = "btnBrowseSource";
        btnBrowseSource.Size = new Size(120, 30);
        btnBrowseSource.Text = "参照...";
        btnBrowseSource.UseVisualStyleBackColor = true;
        btnBrowseSource.Click += btnBrowseSource_Click;
        // 
        // lblDestinationFolder
        // 
        lblDestinationFolder.AutoSize = true;
        lblDestinationFolder.Location = new Point(12, 80);
        lblDestinationFolder.Name = "lblDestinationFolder";
        lblDestinationFolder.Size = new Size(120, 20);
        lblDestinationFolder.Text = "保存先（出力）フォルダ";
        // 
        // txtDestinationFolder
        // 
        txtDestinationFolder.AllowDrop = true;
        txtDestinationFolder.Location = new Point(12, 105);
        txtDestinationFolder.Name = "txtDestinationFolder";
        txtDestinationFolder.Size = new Size(660, 27);
        txtDestinationFolder.DragEnter += FolderTextBox_DragEnter;
        txtDestinationFolder.DragDrop += FolderTextBox_DragDrop;
        // 
        // btnBrowseDestination
        // 
        btnBrowseDestination.Location = new Point(680, 102);
        btnBrowseDestination.Name = "btnBrowseDestination";
        btnBrowseDestination.Size = new Size(120, 30);
        btnBrowseDestination.Text = "参照...";
        btnBrowseDestination.UseVisualStyleBackColor = true;
        btnBrowseDestination.Click += btnBrowseDestination_Click;
        // 
        // chkRecursive
        // 
        chkRecursive.AutoSize = true;
        chkRecursive.Location = new Point(12, 150);
        chkRecursive.Name = "chkRecursive";
        chkRecursive.Size = new Size(280, 24);
        chkRecursive.Text = "サブフォルダを再帰的に処理する";
        chkRecursive.UseVisualStyleBackColor = true;
        // 
        // grpBom
        // 
        grpBom.Controls.Add(rdoBomNone);
        grpBom.Controls.Add(rdoBomWith);
        grpBom.Location = new Point(12, 185);
        grpBom.Name = "grpBom";
        grpBom.Size = new Size(260, 95);
        grpBom.TabIndex = 7;
        grpBom.TabStop = false;
        grpBom.Text = "UTF-8 の BOM";
        // 
        // rdoBomNone
        // 
        rdoBomNone.AutoSize = true;
        rdoBomNone.Checked = true;
        rdoBomNone.Location = new Point(20, 30);
        rdoBomNone.Name = "rdoBomNone";
        rdoBomNone.Size = new Size(90, 24);
        rdoBomNone.Text = "BOMなし";
        rdoBomNone.UseVisualStyleBackColor = true;
        // 
        // rdoBomWith
        // 
        rdoBomWith.AutoSize = true;
        rdoBomWith.Location = new Point(20, 60);
        rdoBomWith.Name = "rdoBomWith";
        rdoBomWith.Size = new Size(86, 24);
        rdoBomWith.Text = "BOMあり";
        rdoBomWith.UseVisualStyleBackColor = true;
        // 
        // btnStart
        // 
        btnStart.Location = new Point(680, 185);
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(120, 40);
        btnStart.Text = "変換開始";
        btnStart.UseVisualStyleBackColor = true;
        btnStart.Click += btnStart_Click;
        // 
        // btnClear
        // 
        btnClear.Location = new Point(680, 235);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(120, 40);
        btnClear.Text = "ログクリア";
        btnClear.UseVisualStyleBackColor = true;
        btnClear.Click += btnClear_Click;
        // 
        // txtLog
        // 
        txtLog.Location = new Point(12, 290);
        txtLog.Multiline = true;
        txtLog.Name = "txtLog";
        txtLog.ReadOnly = true;
        txtLog.ScrollBars = ScrollBars.Vertical;
        txtLog.Size = new Size(788, 200);
        txtLog.TabIndex = 10;
        // 
        // Form1
        // 
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(812, 505);
        Controls.Add(lblSourceFolder);
        Controls.Add(txtSourceFolder);
        Controls.Add(btnBrowseSource);
        Controls.Add(lblDestinationFolder);
        Controls.Add(txtDestinationFolder);
        Controls.Add(btnBrowseDestination);
        Controls.Add(chkRecursive);
        Controls.Add(grpBom);
        Controls.Add(btnStart);
        Controls.Add(btnClear);
        Controls.Add(txtLog);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = true;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "SJIS → UTF-8 文字コード一括変換 Ver.1.0";
        grpBom.ResumeLayout(false);
        grpBom.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}
