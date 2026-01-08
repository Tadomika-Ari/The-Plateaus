using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.IO;

namespace ThePlateaus.Content.Players
{
    public class ComboPlayer : ModPlayer
    {
        public int comboCount = 0;
        public int comboTimer = 0;
        public int dashImmuneTimer = 0;
        // File for combo for Despair
        public override void ResetEffects()
        {
            if (comboTimer > 0)
                comboTimer--;
            if (comboTimer == 0)
                comboCount = 0;
        
            if (dashImmuneTimer > 0)
            {
                dashImmuneTimer--;
                Player.immune = true;
                Player.immuneTime = 2;
            }
        }
    }
}