using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ThePlateaus.Content.Projectiles;
using ThePlateaus.Content.Players;
using Mono.CompilerServices.SymbolWriter;
using ThePlateaus.Content.Items.Ores;
using ThePlateaus.Content.Items.Ressource;
using System.Collections.Generic;

namespace ThePlateaus.Content.Items.Weapons
{ 
	// Scythe Despair
	public class DespairAwaked : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(gold: 15);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			
		}

		// Combo system + Dash
        public override bool? UseItem(Player player)
        {
            if (player.itemAnimation == player.itemAnimationMax)
			{
				var comboPlayer = player.GetModPlayer<ComboPlayer>();

				comboPlayer.comboCount++;
				comboPlayer.comboTimer = 60; // 1 seconde

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

		public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "DespairAwaked", "A true relic of forgotten legends. A weapon that has been completely awakened. You can even see runes engraved on the inside.")
            {
            OverrideColor = new Microsoft.Xna.Framework.Color(100, 255, 100)
            });
        }

	}
}
