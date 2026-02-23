using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ThePlateaus.Content.Projectiles;
using ThePlateaus.Content.Players;
using System.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;
using ThePlateaus.Content.Items.Ores;
using ThePlateaus.Content.Items.Ressource;

namespace ThePlateaus.Content.Items.Weapons
{
    public class OldDespair : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 40;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = Item.buyPrice(silver: 20);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
        }
		public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "OldDespair", "An ancient and unique weapon. It seems to have lost its power...")
            {
            OverrideColor = new Microsoft.Xna.Framework.Color(100, 255, 100)
            });
        }
    }
}