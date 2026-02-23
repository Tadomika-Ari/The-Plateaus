using System.Collections.Generic;
using StructureHelper.API;
using Terraria;
using Terraria.WorldBuilding;
using Terraria.ModLoader;
using Terraria.IO;
using Terraria.GameContent.Generation;
using Terraria.DataStructures;

namespace ThePlateaus.Content.Structures
{
    public class OldDespairSpawn : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

            if (genIndex != -1)
            {
                tasks.Insert(genIndex + 1, new PassLegacy("Old Despair Structure", GenerateOldDespair));
            }
        }

        private void GenerateOldDespair(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Placing Old Despair...";

            for (int attempts = 0; attempts < 10000; attempts++)
            {
                int x = WorldGen.genRand.Next(200, Main.maxTilesX - 200);
                int y = WorldGen.genRand.Next((int)Main.worldSurface, (int) Main.rockLayer);
                
                if (IsForestBiome(x, y)) {
                    Generator.GenerateStructure("Content/Structures/OldDespairSpawn", new Point16(x, y), Mod);
                    break;
                }
            }
        }
        private bool IsForestBiome(int x, int y)
        {
            return !WorldGen.oceanDepths(x, y) &&
                   Main.tile[x, y].WallType == 0 &&
                   !WorldGen.SolidTile(x, y);
        }
    }
}