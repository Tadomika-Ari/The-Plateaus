using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using ThePlateaus.Content.UI;

namespace ThePlateaus.Content.Tiles
{
    public class EnchantmentTable : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(120, 85, 60), Language.GetText("Enchantment Table"));
            DustType = DustID.WoodFurniture;
            AdjTiles = new int[] { TileID.Tables };
        }

        public override bool RightClick(int i, int j)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                ModContent.GetInstance<EnchantmentUISystem>().ToggleUI();
            }
            return true;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<Content.Items.EnchantmentTableItem>();
        }
    }
}