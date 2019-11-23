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
      this.UpdateNowButton = new System.Windows.Forms.Button();
      this.ResetButton = new System.Windows.Forms.Button();
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
      // UpdateNowButton
      // 
      this.UpdateNowButton.Location = new System.Drawing.Point(577, 13);
      this.UpdateNowButton.Name = "UpdateNowButton";
      this.UpdateNowButton.Size = new System.Drawing.Size(305, 42);
      this.UpdateNowButton.TabIndex = 1;
      this.UpdateNowButton.Text = "Run Windows Update now";
      this.UpdateNowButton.UseVisualStyleBackColor = true;
      this.UpdateNowButton.Click += new System.EventHandler(this.UpdateNowButton_Click);
      // 
      // ResetButton
      // 
      this.ResetButton.Location = new System.Drawing.Point(576, 61);
      this.ResetButton.Name = "ResetButton";
      this.ResetButton.Size = new System.Drawing.Size(305, 42);
      this.ResetButton.TabIndex = 2;
      this.ResetButton.Text = "Reset Settings";
      this.ResetButton.UseVisualStyleBackColor = true;
      this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(894, 458);
      this.Controls.Add(this.ResetButton);
      this.Controls.Add(this.UpdateNowButton);
      this.Controls.Add(this.SettingsCheckedListBox);
      this.Name = "MainForm";
      this.Text = "Windows Tool";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.CheckedListBox SettingsCheckedListBox;
    private System.Windows.Forms.Button UpdateNowButton;
    private System.Windows.Forms.Button ResetButton;
  }
}

