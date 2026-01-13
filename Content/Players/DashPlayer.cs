using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace ThePlateaus.Content.Players
{
    public class DashPlayer : ModPlayer
    {
        private int cooldown = 0;
        public bool CanDash;
        public override void ResetEffects()
        {
            CanDash = false;
            if (cooldown > 0)
            {
                cooldown--;
            }
        }
        public override void PreUpdateMovement()
        {
            if (cooldown > 0)
            {
                return;
            }
            const float DashSpeed = 13f;
            if (Player.mount.Active || Player.frozen || Player.webbed || CanDash == false)
            {
                return;
            }
            if (Player.controlRight && Player.doubleTapCardinalTimer[2] > 0 && Player.doubleTapCardinalTimer[2] < 15)
            {
                DoDash(+1, DashSpeed);
                return;
            }
            if (Player.controlLeft && Player.doubleTapCardinalTimer[3] > 0 && Player.doubleTapCardinalTimer[3] < 15)
            {
                DoDash(-1, DashSpeed);
                return;
            }
        }
        private void DoDash(int dir, float speedDash)
        {
            Player.velocity.X = dir * speedDash;
            cooldown = 30;
        }
    }
}