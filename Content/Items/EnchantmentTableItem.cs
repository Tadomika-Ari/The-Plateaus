using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThePlateaus.Content.Items.Runes;

namespace ThePlateaus.Content.Items
{
    public class EnchantmentTableItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Blue;
            Item.createTile = ModContent.TileType<Content.Tiles.EnchantmentTable>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 20);
            recipe.AddIngredient(ItemID.Diamond, 5);
            recipe.AddIngredient<StrangeStone>(1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}