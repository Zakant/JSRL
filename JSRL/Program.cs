﻿using JSRL.Robotics;
using Jint;
using Jint.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.Display.Animation;
using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.Display.Menus;
using MonoBrickFirmware.FileSystem;

namespace JSRL
{
    class Program
    {
        static MenuContainer container;
        static Menu mainMenu = new Menu("JSRL");

        static void Main(string[] args)
        {
            Console.WriteLine("JSRL - Version 0.0.2");
            Console.WriteLine("Startup args:");
            for (int i = 0; i < args.Length; i++)
                Console.WriteLine($"{i.ToString()}: {args[i]}");

            Startup(); // Startup the engine and prepare everything.

            if (args.Length >= 1)
            {
                try
                {
                    Console.WriteLine("Direct launch mode.");
                    Console.WriteLine($"Starting now {args[0]}!");
                    JsrlProgramManager.Instance.RunProgram(JsrlProgram.fromPath(args[0]), null);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    EngineFactory.Shutdown();
                }
                Console.WriteLine("Execution completed!");
                return;
            }

            // Creating the main manue
            mainMenu.AddItem(new ItemWithJsrlProgramList());
            mainMenu.AddItem(new ItemWithDialog<JsrlAboutDialog>(new JsrlAboutDialog(), "About"));
            mainMenu.AddItem(new ItemWithTurnOff());

            container = new MenuContainer(mainMenu);
            container.Show(true);

        }

        public static void Startup()
        {
            Lcd.Clear();
            Lcd.WriteTextBox(Font.MediumFont, new Rectangle(new Point(0, Lcd.Height / 3), new Point(Lcd.Width, Lcd.Height / 2)), "Starting JSRL", true, Lcd.Alignment.Center);
            Lcd.WriteTextBox(Font.SmallFont, new Rectangle(new Point(0, (Lcd.Height / 3) * 2 + 20), new Point(Lcd.Width, Lcd.Height - 1)), "This might take a while!", true, Lcd.Alignment.Center);
            var startupAnim = new ProgressAnimation(new Rectangle(new Point(20, Lcd.Height / 2 + 10), new Point(Lcd.Width - 20, Lcd.Height / 2 + 20)));
            startupAnim.Start();
            Console.WriteLine("Checking if script folder exists...");
            JsrlProgramManager.Instance.CreateProgramFolder();
            Console.WriteLine("Done!");


            Console.WriteLine("Setting up engine now.. this might take a minute or two!");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            EngineFactory.CreateEngine().Execute("var a = 42").Destroy();
            sw.Stop();
            Console.WriteLine($"Engine ready after {sw.ElapsedMilliseconds}ms!");
            startupAnim.Stop();
            Lcd.Clear();
        }
    }
}
