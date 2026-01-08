using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ThePlateaus.Content.Projectiles
{
    public class Projectiles_test : ModProjectile
    {
        // Just a troll projectile
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.CloneDefaults(ProjectileID.FrostWave);
            AIType = ProjectileID.ChlorophyteBullet;
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.damage = 60;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
        }
    }
}