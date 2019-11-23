using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;

namespace Win10BSFixer
{
  class Settings
  {
    private static Settings _Instance;

    public static Settings Instance
    {
      get
      { 
        if (_Instance == null) 
          _Instance = new Settings();
        return _Instance;
      }
    }

    private string SettingsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + "\\.win10bsfixer.cfg";

    private Settings()
    {
      Load();
    }

    public void Load()
    {
      if (!File.Exists(SettingsPath))
      {
        Save();
        return;
      }

      var formatter = new SoapFormatter();
      using (Stream stream = new FileStream(SettingsPath, FileMode.Open, FileAccess.Read))
      {
        data = (Data)formatter.Deserialize(stream);
      }
    }

    public void Save()
    {
      var formatter = new SoapFormatter();
      using (Stream stream = new FileStream(SettingsPath, FileMode.OpenOrCreate, FileAccess.Write))
      {
        formatter.Serialize(stream, data);
      }
    }

    public void Reset()
    {
      data = new Data();
    }

    [Serializable]
    public class Data
    {
      public bool KillWindowsUpdate = true;
      public bool StartMinimised = false;
    }

    public Data data = new Data();
  }
}
