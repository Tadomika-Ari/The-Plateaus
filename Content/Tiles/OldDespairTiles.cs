using Terraria;
using Terraria.ID;
using Terraria.IO;
using System; 
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.GameContent.UI;
using Terraria.DataStructures;
using Terraria.Localization;
using ThePlateaus.Content.Items;
using ThePlateaus.Content.Items.Weapons;
using System.Collections.Generic;

namespace ThePlateaus.Content.Tiles
{
    public class OldDespairTiles: ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
            TileObjectData.addTile(Type);
            DustType = DustID.Stone;
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            yield return new Item(ModContent.ItemType<OldDespair>());
        }
    }
}