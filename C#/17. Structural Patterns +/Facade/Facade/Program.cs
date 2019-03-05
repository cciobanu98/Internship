using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
     class MBR
     {
          public void Load()
          {
               Console.WriteLine("Load MBR");
          }
     }
     class Windows
     {
          public void Start()
          {
               Console.WriteLine("Strat Windows");
          }
          public void Initialize()
          {
               Console.WriteLine("Windows Initialize");
          }
          public void Stop()
          {
               Console.WriteLine("Windows Stop");
          }
     }
     class Application
     {
          public string data = null;
          public void Start()
          {
               Console.WriteLine("Application Start");
          }
          public void LoadOldData()
          {
               Console.WriteLine("Load Old Data");
          }
          public void LoadFromNew()
          {
               Console.WriteLine("New Sesion");
          }
          public void SaveData()
          {
               data = "save";
               Console.WriteLine("Data Save");
          }
          public void ExitApp()
          {
               Console.WriteLine("Exit application");
          }
     }
     class PC
     {
          public MBR mbr;
          public Windows win;
          public Application app;
          public PC(MBR mbr, Windows win, Application app)
          {
               this.mbr = mbr;
               this.win = win;
               this.app = app;
          }
          public void Start()
          {
               mbr.Load();
               win.Start();
               if (app.data == null)
                    app.LoadFromNew();
               else
                    app.LoadOldData();
          }
          public void Stop()
          {
               app.SaveData();
               app.ExitApp();
               win.Stop();
          }
     }
     class Person
     {
          public void PressStartButton(PC pc)
          {
               pc.Start();
          }
          public void PressStopButton(PC pc)
          {
               pc.Stop();
          }
     }
     class Program
     {
          static void Main(string[] args)
          {
               PC pc = new PC(new MBR(), new Windows(), new Application());
               Person p = new Person();
               p.PressStartButton(pc);
               p.PressStopButton(pc);
               Console.WriteLine(new string('*', 50));
               p.PressStartButton(pc);
               Console.ReadKey();
          }
     }
}
