﻿namespace WindowsTool
{
  partial class MainForm
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

