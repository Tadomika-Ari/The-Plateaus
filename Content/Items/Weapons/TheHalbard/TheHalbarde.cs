using Terraria;
using Terraria.ID;
using ThePlateaus;
using Terraria.ModLoader;

namespace ThePlateaus.Content.Items.Weapons.TheHalbard
{
    public class TheHalbarde : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 40;
            Item.scale = 2.5f;
            Item.useAnimation = 40;
            Item.knockBack = 5;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(gold: 1);
            Item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}