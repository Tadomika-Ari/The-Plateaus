using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using System;
using Terraria.ID;
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
    }
}