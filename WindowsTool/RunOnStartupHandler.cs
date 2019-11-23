using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.TaskScheduler;

namespace Win10BSFixer
{
  class RunOnStartupHandler
  {
    static string BinaryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + "\\Win10BSFixer.exe";
    static string ShortcutPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Start Menu\Programs\Startup\Win10BSFixer.lnk";
    static string ScheduledTaskName = "Win10BSFixer";

    static public bool Installed()
    {
      return File.Exists(ShortcutPath);
    }

    static public void InstallAndScheduleForStartup()
    {
      string thisBinaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;

      if (thisBinaryPath != BinaryPath)
        File.Copy(thisBinaryPath, BinaryPath, true);

      using (TaskService taskService = new TaskService())
      {
        if (taskService.GetTask(ScheduledTaskName) != null)
          taskService.RootFolder.DeleteTask(ScheduledTaskName);

        TaskDefinition taskDefinition = taskService.NewTask();
        taskDefinition.Actions.Add(new ExecAction(BinaryPath));
        taskDefinition.Settings.DisallowStartIfOnBatteries = false;
        taskDefinition.Principal.RunLevel = TaskRunLevel.Highest;

        taskService.RootFolder.RegisterTaskDefinition(ScheduledTaskName, taskDefinition);
      }

      if (!File.Exists(ShortcutPath))
      {
        Directory.CreateDirectory(Path.GetDirectoryName(ShortcutPath));

        var shell = new IWshRuntimeLibrary.WshShell();
        var shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(ShortcutPath);

        shortcut.TargetPath = "schtasks";
        shortcut.Arguments = "/run /tn " + ScheduledTaskName;
        shortcut.Save();
      }
    }

    static public void Uninstall()
    {
      // Don't delete the binary for now because deleting a running binary is hard on windows

      using (TaskService taskService = new TaskService())
      {
        if (taskService.GetTask(ScheduledTaskName) != null)
          taskService.RootFolder.DeleteTask(ScheduledTaskName);

      }

      if (File.Exists(ShortcutPath))
        File.Delete(ShortcutPath);
    }
  }
}
