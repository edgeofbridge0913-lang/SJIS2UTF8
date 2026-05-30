using System;
using System.Drawing;
using System.Windows.Forms;

namespace SJIS2UTF8Converter
{
    public enum OverwriteDecision
    {
        Yes,
        No,
        YesAll,
        Cancel
    }

    public static class OverwriteDialog
    {
        public static OverwriteDecision AskOverwrite(string filePath)
        {
            using var form = new Form();
            form.Text = "上書き確認";
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.ClientSize = new Size(580, 170);
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.ShowIcon = false;
            form.ShowInTaskbar = false;

            var label = new Label();
            label.AutoSize = false;
            label.Location = new Point(12, 12);
            label.Size = new Size(556, 90);
            label.Text = "出力先に同名ファイルが存在します。\r\n\r\n" + filePath + "\r\n\r\nこのファイルを上書きしますか？";

            var btnYes = new Button();
            btnYes.Text = "はい";
            btnYes.Size = new Size(110, 32);
            btnYes.Location = new Point(30, 112);
            btnYes.DialogResult = DialogResult.Yes;

            var btnNo = new Button();
            btnNo.Text = "いいえ";
            btnNo.Size = new Size(110, 32);
            btnNo.Location = new Point(160, 112);
            btnNo.DialogResult = DialogResult.No;

            var btnYesAll = new Button();
            btnYesAll.Text = "以降すべて上書き";
            btnYesAll.Size = new Size(140, 32);
            btnYesAll.Location = new Point(290, 112);
            btnYesAll.DialogResult = DialogResult.OK;

            var btnCancel = new Button();
            btnCancel.Text = "キャンセル";
            btnCancel.Size = new Size(110, 32);
            btnCancel.Location = new Point(450, 112);
            btnCancel.DialogResult = DialogResult.Cancel;

            form.Controls.Add(label);
            form.Controls.Add(btnYes);
            form.Controls.Add(btnNo);
            form.Controls.Add(btnYesAll);
            form.Controls.Add(btnCancel);
            form.AcceptButton = btnYes;
            form.CancelButton = btnCancel;

            var result = form.ShowDialog();
            return result switch
            {
                DialogResult.Yes => OverwriteDecision.Yes,
                DialogResult.No => OverwriteDecision.No,
                DialogResult.OK => OverwriteDecision.YesAll,
                _ => OverwriteDecision.Cancel,
            };
        }
    }
}
