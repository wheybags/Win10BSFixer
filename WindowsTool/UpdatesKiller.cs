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
  class UpdatesKiller
  {
    public UpdatesKiller(bool enabled)
    {
      this.Enabled = enabled;
      WorkerThread = new Thread(new ThreadStart(KillLoop));
      WorkerThread.Start();
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

          if (!value)
          {
            while (!ReEnableServiceDone)
              Thread.Sleep(500);
          }
        }
      }
    }

    private volatile bool _Enabled = false;
    private volatile bool ReEnableServiceDone = false;
    private Thread WorkerThread;

    private void KillLoop()
    {
      ServiceController updateService = new ServiceController("wuauserv");

      while (true)
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
          updateService.Start();
          updateService.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));

          if (updateService.Status == ServiceControllerStatus.Running)
            ReEnableServiceDone = true;
        }

        Thread.Sleep(10 * 1000);
      }
    }
  }
}
