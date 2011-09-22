﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Terraria_Server;
using Terraria_Server.Logging;
using Terraria_Server.WorldMod;

namespace WebKit.Server.Utility
{
    public class Utilities
    {
        public static bool RestartServer(WebKit WebKit, string IPOrName)
        {
            /* snip, I 'could' trigger the command, but it requires sender and what not,
             * though the IP should be logged, Fake sender name maybe? 
             * I say it should be logged, thinking in a Server OP sense, that if they dont want 
             * certain OP's to, they can check and whatever.
             */
            try
            {
                WebKit.ServerStatus = "Restarting";
                Terraria_Server.Server.notifyOps("Restarting the Server {" + IPOrName + "}", true);

                NetPlay.StopServer();
                while (NetPlay.ServerUp) { Thread.Sleep(10); }

                ProgramLog.Log("Starting the Server");
                Main.Initialize();
                WorldIO.LoadWorld(Terraria_Server.Server.World.SavePath);
                Program.updateThread = new ProgramThread("Updt", Program.UpdateLoop);
                NetPlay.StartServer();

                return true;
            }
            catch(Exception e)
            {
                ProgramLog.Log(e);
            }

            return false;
        }

        public static bool StopServer(WebKit WebKit, string IPOrName)
        {
            try
            {
                WebKit.ServerStatus = "Exiting";

                Terraria_Server.Server.notifyOps("Exiting on request. {" + IPOrName + "}", false);
                NetPlay.StopServer();
                Statics.Exit = true;

                return true;
            }
            catch (Exception e)
            {
                ProgramLog.Log(e);
            }

            return false;
        }

        //No point adding start

    }
}
