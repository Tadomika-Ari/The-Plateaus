using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ThePlateaus.Content.Items.Weapons.TrueBow;

namespace ThePlateaus.Content.Projectiles.OrbitalBow
{
    public class SpectreOrbital : ModProjectile
    {
        private const float OrbitDistance = 150f;
        private const float AngularSpread = 5f;
        private const float RotationLerpSpeed = 0.15f;
        private const int MaxLifetime = 10;

        private const float SpriteRotationOffset = MathHelper.PiOver2; 

        private int BowIndex => (int)Projectile.ai[0];

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 56;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = MaxLifetime;
            Projectile.penetrate = -1;
            Projectile.hide = false;
            Projectile.alpha = 0;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active ||
                player.HeldItem.type != ModContent.ItemType<TrueBow>())
            {
                Projectile.Kill();
                return;
            }
            Projectile.timeLeft = MaxLifetime;

            Vector2 toCursor = Main.MouseWorld - player.Center;
            float cursorAngle = toCursor.ToRotation();
            float spreadRad = MathHelper.ToRadians(AngularSpread);
            float indexOffset = (BowIndex - 1) * spreadRad;
            float targetAngle = cursorAngle + indexOffset;
            float currentAngle = Projectile.rotation - SpriteRotationOffset;
            float smoothAngle = LerpAngle(currentAngle, targetAngle, RotationLerpSpeed);
            Vector2 behindDir = -toCursor.SafeNormalize(Vector2.UnitX);
            Vector2 lateralDir = behindDir.RotatedBy(MathHelper.PiOver2);
            float lateralOffset = (BowIndex - 1) * 80f;

            Projectile.Center = player.Center
                + behindDir * OrbitDistance
                + lateralDir * lateralOffset;
            Projectile.rotation = smoothAngle + SpriteRotationOffset;

            if (Main.rand.NextBool(4))
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position, Projectile.width, Projectile.height,
                    20,
                    newColor: new Color(150, 220, 255, 80),
                    Scale: 0.6f
                );
                dust.noGravity = true;
                dust.velocity *= 0.3f;
            }

            Lighting.AddLight(Projectile.Center, 0.3f, 0.6f, 1.0f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

            Color drawColor = lightColor * 0.85f;
            drawColor.A = 180;

            Main.EntitySpriteDraw(
                texture,
                Projectile.Center - Main.screenPosition,
                null,
                drawColor,
                Projectile.rotation,
                texture.Size() / 2f,
                Projectile.scale,
                SpriteEffects.None,
                0
            );
            Color glowColor = new Color(100, 180, 255, 60);
            Main.EntitySpriteDraw(
                texture,
                Projectile.Center - Main.screenPosition,
                null,
                glowColor,
                Projectile.rotation,
                texture.Size() / 2f,
                Projectile.scale * 1.08f,
                SpriteEffects.None,
                0
            );
            return false;
        }
        private static float LerpAngle(float from, float to, float t)
        {
            float diff = MathHelper.WrapAngle(to - from);
            return from + diff * t;
        }
    }
}