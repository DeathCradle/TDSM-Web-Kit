﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria_Server;

namespace WebKit.Server.JsonData.Packets
{
    public struct WebCommand : IPacket
    {
        public string GetPacket()
        {
            return PacketId.WEB_COMMAND;
        }

        public Dictionary<String, Object> Process(object[] Data)
        {
            Dictionary<String, Object> array = new Dictionary<String, Object>();

            WebKit WebKit = (WebKit)Data[0];
            string Command = Data[2].ToString();

            Program.commandParser.ParseAndProcess(WebKit.WebSender, Command);
            return array;
        }
    }
}
