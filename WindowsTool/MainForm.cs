using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win10BSFixer
{
  public partial class MainForm : Form
  {
    UpdatesKiller updatesKiller;

    public MainForm()
    {
      InitializeComponent();

      updatesKiller = new UpdatesKiller(Settings.Instance.data.KillWindowsUpdate);
      SetupSettingsToggles();
    }
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);

      if (updatesKiller != null)
        updatesKiller.Dispose();
    }

    private void SetupSettingsToggles()
    {
      SetterDictionary.Clear();
      SettingsCheckedListBox.Items.Clear();

      var settings = Settings.Instance.data;
      AddSettingsItem("Force disable windows updates", settings.KillWindowsUpdate, x => { settings.KillWindowsUpdate = x; updatesKiller.Enabled = x; });
    }

    private Dictionary<int, Action<bool>> SetterDictionary = new Dictionary<int, Action<bool>>();
    private void AddSettingsItem(string text, bool defaultValue, Action<bool> setAction)
    {
      int index = SettingsCheckedListBox.Items.Add(text, defaultValue);
      SetterDictionary.Add(index, setAction);
    }

    private void SettingsCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      if (SetterDictionary.ContainsKey(e.Index))
      {
        SetterDictionary[e.Index](e.NewValue == CheckState.Checked);
        Settings.Instance.Save();
      }
    }

    bool RunUpdateClicked = false;
    private void UpdateNowButton_Click(object sender, EventArgs e)
    {
      if (RunUpdateClicked)
      {
        RunUpdateClicked = false;
        updatesKiller.Enabled = Settings.Instance.data.KillWindowsUpdate;
        UpdateNowButton.Text = "Run Windows Update now";
        SettingsCheckedListBox.Enabled = true;
        ResetButton.Enabled = true;
      }
      else
      {
        if (updatesKiller.Enabled)
        {
          DialogResult result = MessageBox.Show("This program will freeze for about 10 seconds.\n\n" +
            "You will need to come back to this program to let it know when you are done with updates so it can turn the update blocker back on.\n\n" +
            "The next time you boot your pc the block will start again, so if you update and the pc reboots, it is all automatic.",
            "!!! READ THIS !!!", MessageBoxButtons.OKCancel);

          if (result != DialogResult.OK)
            return;

          UpdateNowButton.Text = "Click here when finished updating";

          RunUpdateClicked = true;
          updatesKiller.EnsureUpdatesAllowed();
          SettingsCheckedListBox.Enabled = false;
          ResetButton.Enabled = false;
        }

        Process.Start("ms-settings:windowsupdate");
      }
    }

    private void ResetButton_Click(object sender, EventArgs e)
    {
      Settings.Instance.Reset();
      SetupSettingsToggles();

      for (int i = 0; i < SettingsCheckedListBox.Items.Count; i++)
        SettingsCheckedListBox_ItemCheck(null, new ItemCheckEventArgs(i, SettingsCheckedListBox.GetItemCheckState(i), SettingsCheckedListBox.GetItemCheckState(i)));
    }
  }
}
