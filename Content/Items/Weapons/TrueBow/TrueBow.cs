using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.IO;
using ThePlateaus.Content.Projectiles.OrbitalBow;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Reflection;

namespace ThePlateaus.Content.Items.Weapons.TrueBow
{
    public class TrueBow : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Ranged;
            Item.height = 40;
            Item.width = 40;
            Item.useTime = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<Projectiles.OrbitalBow.TrueBowArrow>();
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Arrow;
            Item.UseSound = SoundID.Item1;
            Item.knockBack = 5; 
            Item.useAnimation = 40;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = false;
            Item.value = Item.buyPrice(gold: 5); 
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int[] orbitalTypes =
            {
                ModContent.ProjectileType<TrueBowOrbital>(),
                ModContent.ProjectileType<SpectreOrbital>(),
                ModContent.ProjectileType<TsunamiOrbital>()
            };
            foreach (Projectile proj in Main.projectile)
            {
                if (!proj.active || proj.owner != player.whoAmI)
                    continue;
                bool isOrbital = false;
                for (int i = 0; i < orbitalTypes.Length; i++)
                {
                    if (proj.type == orbitalTypes[i])
                    {
                        isOrbital = true;
                        break;
                    }
                }
                if (!isOrbital)
                    continue;
                float bowAngle = proj.rotation - MathHelper.PiOver2;
                Vector2 bowVelocity = bowAngle.ToRotationVector2() * Item.shootSpeed;
                Projectile.NewProjectile(
                    source,
                    proj.Center,
                    bowVelocity,
                    ModContent.ProjectileType<TrueBowArrow>(),
                    damage,
                    knockback,
                    player.whoAmI
                );
            }
            return false;
        }
        public override void HoldItem(Player player)
        {
            int[] orbitalTypes =
            {
                ModContent.ProjectileType<TrueBowOrbital>(),
                ModContent.ProjectileType<SpectreOrbital>(),
                ModContent.ProjectileType<TsunamiOrbital>()
            };
            for (int i = 0; i < orbitalTypes.Length; i++)
            {
                int type = orbitalTypes[i];
                bool exists = false;
                foreach (Projectile proj in Main.projectile)
                {
                    if (proj.active && proj.owner == player.whoAmI && proj.type == type)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    Projectile.NewProjectile(
                        player.GetSource_ItemUse(Item),
                        player.Center,
                        Vector2.Zero,
                        type,
                        0,
                        0f,
                        player.whoAmI,
                        ai0: i
                    );
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Phantasm);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}