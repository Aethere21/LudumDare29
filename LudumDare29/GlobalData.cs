﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LudumDare29
{
    public class GlobalData
    {
        private static PlayerData mPlayerData = new PlayerData(); 

        public static PlayerData PlayerData
        {
            get { return mPlayerData; }
        }

    }
}
