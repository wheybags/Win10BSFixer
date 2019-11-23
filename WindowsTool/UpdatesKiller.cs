using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsTool
{
  class UpdatesKiller : IDisposable
  {
    public UpdatesKiller(bool enabled)
    {
      this.Enabled = enabled;
      WorkerThread = new Thread(new ThreadStart(KillLoop));
      WorkerThread.Start();
    }

    public void Dispose()
    {
      Quit = true;
      while (!QuitDone)
        Thread.Sleep(500);
    }

    public bool Enabled
    {
      get
      {
        return _Enabled;
      }

      set
      {
        if (value != _Enabled)
        {
          ReEnableServiceDone = false;
          _Enabled = value;
        }
      }
    }


    private volatile bool Quit = false;
    private volatile bool QuitDone = false;
    private volatile bool _Enabled = false;
    private volatile bool ReEnableServiceDone = false;
    private Thread WorkerThread;

    private void KillLoop()
    {
      ServiceController updateService = new ServiceController("wuauserv");

      while (!Quit)
      {
        if (Enabled)
        {
          if (updateService.Status != ServiceControllerStatus.StopPending && updateService.Status != ServiceControllerStatus.Stopped)
          {
            try
            {
              updateService.Stop();
            }
            catch (InvalidOperationException ex)
            {
              // If we tried to stop when the service was not running, just ignore
              if (!(ex.InnerException is Win32Exception) || (ex.InnerException as Win32Exception).NativeErrorCode != 1062) // ERROR_SERVICE_NOT_ACTIVE
                throw ex;
            }
          }

          ServiceHelper.ChangeStartMode(updateService, ServiceStartMode.Disabled);
        }

        if (!Enabled && !ReEnableServiceDone)
        {
          ServiceHelper.ChangeStartMode(updateService, ServiceStartMode.Automatic);

          try
          {
            updateService.Start();
          }
          catch (InvalidOperationException ex)
          {
            if (!(ex.InnerException is Win32Exception) || (ex.InnerException as Win32Exception).NativeErrorCode != 1056) // ERROR_SERVICE_ALREADY_RUNNING
              throw ex;
          }

          updateService.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));

          if (updateService.Status == ServiceControllerStatus.Running)
            ReEnableServiceDone = true;
        }

        Thread.Sleep(10 * 1000);
      }

      QuitDone = true;
    }
  }
}
