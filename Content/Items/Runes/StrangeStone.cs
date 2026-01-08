using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using System;
using Terraria.ID;
using System.Collections.Generic;
using System.Drawing;

namespace ThePlateaus.Content.Items.Runes
{
    public class StrangeStone : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(copper: 10);
            Item.maxStack = 1;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "tooltipeStrangeStone", "A strange stone with unknown engraved design."));
            tooltips.Add(new TooltipLine(Mod, "OtherLineStrangeStone", "Maybe someone can tell me more about it..."));
        }
    }
}