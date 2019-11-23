using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsTool
{
  public partial class MainForm : Form
  {
    UpdatesKiller updatesKiller;

    public MainForm()
    {
      InitializeComponent();

      var settings = Settings.Instance.data;
      updatesKiller = new UpdatesKiller(settings.KillWindowsUpdate);

      AddSettingsItem("Force disable windows updates", settings.KillWindowsUpdate, x => { settings.KillWindowsUpdate = x; updatesKiller.Enabled = x; });
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
  }
}
