using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ThePlateaus.Content.Items.Ores
{
    public class ExampleBar : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(silver: 20);
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<ExampleOre>(3); // 3 minerais = 1 lingot
            recipe.AddTile(TileID.Furnaces); // Craft au furnace
            recipe.Register();
        }
    }
}
