using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThePlateaus.Content.Tiles;

namespace ThePlateaus.Content.Items
{
    public class OldDespairItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<OldDespairTiles>();
        }
    }
}