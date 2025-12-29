using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ThePlateaus.Content.Projectiles;
using ThePlateaus.Content.Players;
using Mono.CompilerServices.SymbolWriter;
using ThePlateaus.Content.Items.Ores;

namespace ThePlateaus.Content.Items.Weapons
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class Despair : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.ThePlateaus.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 250;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			
		}

        public override bool? UseItem(Player player)
        {
            if (player.itemAnimation == player.itemAnimationMax)
			{
				var comboPlayer = player.GetModPlayer<ComboPlayer>();

				comboPlayer.comboCount++;
				comboPlayer.comboTimer = 60;

				if (comboPlayer.comboCount >= 3)
				{
					Vector2 dashDirection = Vector2.Normalize(Main.MouseWorld - player.Center);
					if (dashDirection.LengthSquared() < 0.001f)
						dashDirection = Vector2.UnitX * player.direction;
				
					if (player.CheckMana(20, pay: true))
					{
						player.velocity = dashDirection * 15f;
						comboPlayer.comboCount = 0;
						comboPlayer.dashImmuneTimer = 30;

						Projectile.NewProjectile(
							player.GetSource_ItemUse(Item),
							player.Center,
							Vector2.Zero,
							ModContent.ProjectileType<ProjectileForDash>(),
							60,
							Item.knockBack,
							player.whoAmI
						);
					
					}
				}
			}
			return base.UseItem(player);
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<DarkBar>(12);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
