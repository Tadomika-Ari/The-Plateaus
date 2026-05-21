using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace ThePlateaus.Content.Projectiles.Projectiles_test
{
    public class Projectiles_test : ModProjectile
    {
        private const float BeamLength = 900f;
        private const float BeamHitboxThickness = 4f;
        private const float BeamVisualThickness = 4f;
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 20;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.light = 1f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
        public override void AI()
        {
            if (Projectile.velocity.LengthSquared() < 0.001f)
                Projectile.velocity = Vector2.UnitX;

            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float point = 0f;
            Vector2 start = Projectile.Center;
            Vector2 end = start + Projectile.velocity.SafeNormalize(Vector2.UnitX) * BeamLength;

            return Collision.CheckAABBvLineCollision(
                targetHitbox.TopLeft(),
                targetHitbox.Size(),
                start,
                end,
                BeamHitboxThickness,
                ref point
            );
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.MagicPixel.Value;
            Rectangle src = new Rectangle(0, 0, 1, 1); // force 1x1
            Vector2 start = Projectile.Center - Main.screenPosition;
            Vector2 dir = Projectile.velocity.SafeNormalize(Vector2.UnitX);
            Main.EntitySpriteDraw(
                tex,
                start,
                src,
                Color.White,
                dir.ToRotation(),
                new Vector2(0f, 0.5f),
                new Vector2(BeamLength, BeamVisualThickness),
                SpriteEffects.None,
                0
            );

            return false;
        }
    }
}