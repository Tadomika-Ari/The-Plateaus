using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;
using System.IO;
namespace ThePlateaus.Content.Projectiles.RingAncientWitch
{
    public class RuneRing: ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Magic;
            Projectile.damage = 10;
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.timeLeft = 20;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
        }
        public override void AI()
        {
            if (Main.netMode == NetmodeID.Server)
                return;
            int count = 5;
            float radius = 100f;
            float rotation = Projectile.ai[0];

            for (int i = 0; i < count; i++)
            {
                float angle = MathHelper.TwoPi * i / count + rotation;
                Vector2 offset = new Vector2((float)System.Math.Cos(angle), (float)System.Math.Sin(angle)) * radius;
                Dust d = Dust.NewDustPerfect(Projectile.Center + offset, DustID.Ice, offset * 0.03f);
                d.noGravity = true;
                d.noLight = false;
                Lighting.AddLight(Projectile.Center + offset, 0.2f, 0.6f, 0.2f);
            }
            Projectile.ai[0] += 0.2f;
        }
    }
}