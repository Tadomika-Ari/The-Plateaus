using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ThePlateaus.Content.Items.Ores
{
    public class ExampleOre : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(silver: 5); // Prix de vente
            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            
            // Pour placer le tile en cliquant
            Item.createTile = ModContent.TileType<Content.Tiles.ExampleOreTile>();
        }
    }
}