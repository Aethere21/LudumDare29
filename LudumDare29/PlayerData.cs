using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LudumDare29
{
    public class PlayerData
    {
        public int score;
        public int health;
        public int gunDamage;
        public string currentLevel;
        public int gunAccuracy; //---
        public int playerDefense;

        public bool FedoraOnHead;

        public Vector2 playerPos;
    }
}
