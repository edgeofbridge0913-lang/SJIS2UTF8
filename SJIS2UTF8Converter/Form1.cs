using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hnx8.ReadJEnc;

namespace SJIS2UTF8Converter;

public partial class Form1 : Form
{
    private bool _overwriteAll;
    private bool _cancelRequested;

    public Form1()
    {
        InitializeComponent();
    }

    private void btnBrowseSource_Click(object sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog();
        dialog.Description = "変換元（入力）フォルダを選択してください。";
        dialog.SelectedPath = txtSourceFolder?.Text ?? string.Empty;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            txtSourceFolder!.Text = dialog.SelectedPath;
        }
    }

    private void btnBrowseDestination_Click(object sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog();
        dialog.Description = "保存先（出力）フォルダを選択してください。";
        dialog.SelectedPath = txtDestinationFolder?.Text ?? string.Empty;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            txtDestinationFolder!.Text = dialog.SelectedPath;
        }
    }

    private void FolderTextBox_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop)!;
            if (files.Length == 1 && Directory.Exists(files[0]))
            {
                e.Effect = DragDropEffects.Copy;
                return;
            }
        }
        e.Effect = DragDropEffects.None;
    }

    private void FolderTextBox_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop)!;
            if (files.Length == 1 && Directory.Exists(files[0]))
            {
                if (sender is TextBox textBox)
                {
                    textBox.Text = files[0];
                }
            }
        }
    }

    private void btnStart_Click(object sender, EventArgs e)
    {
        if (!ValidatePaths(out var sourceDir, out var destinationDir))
        {
            return;
        }

        _overwriteAll = false;
        _cancelRequested = false;
        btnStart.Enabled = false;
        AppendLog("変換処理を開始します...");

        try
        {
            ProcessFiles(sourceDir, destinationDir, chkRecursive.Checked);
            if (!_cancelRequested)
            {
                AppendLog("処理が完了しました。");
            }
        }
        catch (Exception ex)
        {
            AppendLog($"致命的なエラー: {ex.Message}");
        }
        finally
        {
            btnStart.Enabled = true;
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        txtLog.Clear();
    }

    private bool ValidatePaths(out DirectoryInfo sourceDir, out DirectoryInfo destinationDir)
    {
        sourceDir = null!;
        destinationDir = null!;
        var sourcePath = txtSourceFolder?.Text ?? string.Empty;
        var destinationPath = txtDestinationFolder?.Text ?? string.Empty;

        if (string.IsNullOrWhiteSpace(sourcePath))
        {
            MessageBox.Show("変換元フォルダを指定してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (string.IsNullOrWhiteSpace(destinationPath))
        {
            MessageBox.Show("保存先フォルダを指定してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        sourceDir = new DirectoryInfo(sourcePath);
        if (!sourceDir.Exists)
        {
            MessageBox.Show("変換元フォルダが存在しません。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        destinationDir = new DirectoryInfo(destinationPath);
        if (!destinationDir.Exists)
        {
            destinationDir.Create();
        }

        if (string.Equals(sourceDir.FullName.TrimEnd(Path.DirectorySeparatorChar), destinationDir.FullName.TrimEnd(Path.DirectorySeparatorChar), StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show("変換元と保存先に同じフォルダは指定できません。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        return true;
    }

    private void ProcessFiles(DirectoryInfo sourceDir, DirectoryInfo destinationDir, bool recursive)
    {
        var searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        var targetExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".c", ".h", ".txt" };
        var files = sourceDir.EnumerateFiles("*.*", searchOption)
            .Where(file => targetExtensions.Contains(file.Extension))
            .OrderBy(file => file.FullName)
            .ToList();

        if (!files.Any())
        {
            AppendLog("対象ファイルが見つかりませんでした。" );
            return;
        }

        foreach (var sourceFile in files)
        {
            if (_cancelRequested)
            {
                AppendLog("処理はキャンセルされました。" );
                break;
            }

            var relativePath = Path.GetRelativePath(sourceDir.FullName, sourceFile.FullName);
            var destinationFilePath = Path.Combine(destinationDir.FullName, relativePath);
            var destinationFolder = Path.GetDirectoryName(destinationFilePath);
            if (!string.IsNullOrEmpty(destinationFolder) && !Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            if (File.Exists(destinationFilePath) && !_overwriteAll)
            {
                var decision = OverwriteDialog.AskOverwrite(destinationFilePath);
                if (decision == OverwriteDecision.Cancel)
                {
                    AppendLog("上書き確認でキャンセルされました。" );
                    _cancelRequested = true;
                    break;
                }
                else if (decision == OverwriteDecision.No)
                {
                    AppendLog($"スキップ: {relativePath}");
                    continue;
                }
                else if (decision == OverwriteDecision.YesAll)
                {
                    _overwriteAll = true;
                }
            }

            try
            {
                ProcessSingleFile(sourceFile.FullName, destinationFilePath);
            }
            catch (Exception ex)
            {
                AppendLog($"エラー: {relativePath} - {ex.Message}");
            }
        }
    }

    private void ProcessSingleFile(string sourcePath, string destinationPath)
    {
        var relativePath = Path.GetFileName(sourcePath);
        byte[] fileBytes = File.ReadAllBytes(sourcePath);

        if (fileBytes.Length == 0)
        {
            File.WriteAllBytes(destinationPath, fileBytes);
            AppendLog($"空ファイルをコピーしました: {relativePath}");
            return;
        }

        var reader = new FileReader(new FileInfo(sourcePath));
        var charCode = reader.Read(new FileInfo(sourcePath));
        var isSjis = charCode.Equals(CharCode.SJIS);

        if (isSjis)
        {
            var text = Encoding.GetEncoding("shift_jis").GetString(fileBytes);
            var utf8Encoding = new UTF8Encoding(rdoBomWith.Checked);
            File.WriteAllText(destinationPath, text, utf8Encoding);
            AppendLog($"変換成功: {relativePath} (SJIS → UTF-8{(rdoBomWith.Checked ? " BOMあり" : " BOMなし")})");
        }
        else
        {
            File.Copy(sourcePath, destinationPath, true);
            AppendLog($"SJIS以外のためコピーのみ: {relativePath} ({charCode})");
        }
    }

    private void AppendLog(string message)
    {
        txtLog.AppendText($"[{DateTime.Now:yyyy/MM/dd HH:mm:ss}] {message}{Environment.NewLine}");
        txtLog.SelectionStart = txtLog.TextLength;
        txtLog.ScrollToCaret();
        Application.DoEvents();
    }
}
