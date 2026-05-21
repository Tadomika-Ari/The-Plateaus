using Terraria;
using Terraria.ID;
using SubworldLibrary;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.Chat;
using Terraria.Utilities;
using Humanizer;
using ThePlateaus.Content.TheColosseum;

namespace ThePlateaus.Content.Items.FragmentOfMoon
{
    public class MoonFragment : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 200;
            Item.consumable = false;
            Item.uniqueStack = true;
            Item.rare = ItemRarityID.Purple;
        }
        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (Main.dayTime == true)
                {
                    Main.NewText("Le soleil vous regarde");
                    SubworldSystem.Exit();
                }
                if (Main.dayTime == false)
                {
                    Main.NewText("The Moon is watching you");
                    SubworldSystem.Enter<TheColosseumWorld>();
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.ManaCrystal, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}