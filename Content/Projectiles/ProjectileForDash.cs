using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ThePlateaus.Content.Projectiles
{
    public class ProjectileForDash : ModProjectile
    {
        // Dash Hitbox on projectile use by Despair
        public override string Texture => "Terraria/Images/Projectile_0";
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.aiStyle = -1;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 30;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center;
            Projectile.rotation = player.velocity.ToRotation();
        }
    }
}