using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ThePlateaus.Content.Items.Ressource
{
    public class DemonicHorn : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ItemRarityID.Gray;
            Item.value = Item.sellPrice(silver: 5);
            Item.maxStack = 999;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "HornTooltip", "Just a strange horn")
            {
            OverrideColor = new Microsoft.Xna.Framework.Color(100, 255, 100)
            });
        }
    }
}