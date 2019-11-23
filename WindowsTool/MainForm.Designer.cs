namespace WindowsTool
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.SettingsCheckedListBox = new System.Windows.Forms.CheckedListBox();
      this.SuspendLayout();
      // 
      // SettingsCheckedListBox
      // 
      this.SettingsCheckedListBox.FormattingEnabled = true;
      this.SettingsCheckedListBox.Location = new System.Drawing.Point(12, 12);
      this.SettingsCheckedListBox.Name = "SettingsCheckedListBox";
      this.SettingsCheckedListBox.Size = new System.Drawing.Size(558, 441);
      this.SettingsCheckedListBox.TabIndex = 0;
      this.SettingsCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.SettingsCheckedListBox_ItemCheck);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.ClientSize = new System.Drawing.Size(800, 469);
      this.Controls.Add(this.SettingsCheckedListBox);
      this.Name = "MainForm";
      this.Text = "Windows Tool";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.CheckedListBox SettingsCheckedListBox;
  }
}

