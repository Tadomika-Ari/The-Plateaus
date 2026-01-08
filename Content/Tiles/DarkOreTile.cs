using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace ThePlateaus.Content.Tiles
{
	public class DarkOreTile : ModTile
	{
		public override void SetStaticDefaults() {
			TileID.Sets.Ore[Type] = true;
			TileID.Sets.FriendlyFairyCanLureTo[Type] = true;
			Main.tileSpelunker[Type] = true;
			Main.tileOreFinderPriority[Type] = 510;
			Main.tileShine2[Type] = true;
			Main.tileShine[Type] = 975;
			Main.tileMergeDirt[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileMerge[Type][Type] = true;

			LocalizedText name = CreateMapEntryName();
			AddMapEntry(new Color(152, 171, 198), name);

			DustType = DustID.Platinum;
			VanillaFallbackOnModDeletion = TileID.Silver;
			HitSound = SoundID.Tink;
			MineResist = 4f;
			MinPick = 110;
		}
		public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor) {
			sightColor = Color.Blue;
			return true;
		}
	}
	public class DarkOreSystem : ModSystem
	{
		public static LocalizedText DarkOrePassMessage { get; private set; }
		public static LocalizedText BlessedWithExampleOreMessage { get; private set; }

		public override void SetStaticDefaults() {
			DarkOrePassMessage = Mod.GetLocalization($"WorldGen.{nameof(DarkOrePassMessage)}");
			BlessedWithExampleOreMessage = Mod.GetLocalization($"WorldGen.{nameof(BlessedWithExampleOreMessage)}");
		}
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) {
			int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
			if (ShiniesIndex != -1) {

				tasks.Insert(ShiniesIndex + 1, new DarkOrePass("Dark Mod Ores", 237.4298f));
			}
		}
	}

	public class DarkOrePass : GenPass
	{
		public DarkOrePass(string name, float loadWeight) : base(name, loadWeight) {
		}

		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) {
			progress.Message = DarkOreSystem.DarkOrePassMessage.Value;

			for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 7E-05); k++) {
				int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
				int minY = Main.UnderworldLayer - 600;
				int maxY = Main.UnderworldLayer - 50;
				int y = WorldGen.genRand.Next(minY, maxY);

				Tile tile = Framing.GetTileSafely(x, y);
				if (tile.HasTile && (tile.TileType == TileID.Stone || tile.TileType == TileID.Ash)) {
					WorldGen.TileRunner(x, y, WorldGen.genRand.Next(4, 7), WorldGen.genRand.Next(3, 6), ModContent.TileType<DarkOreTile>());
				}
			}
		}
	}
}