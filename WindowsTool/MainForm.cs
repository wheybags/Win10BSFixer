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
      updatesKiller = new UpdatesKiller(true);

      InitializeComponent();

      SettingsCheckedListBox.Items.Add("Force disable windows updates", true);
    }
  }
}
