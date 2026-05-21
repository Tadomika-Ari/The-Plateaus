using Terraria;
using Terraria.ID;
using SubworldLibrary;
using Terraria.WorldBuilding;
using Terraria.ModLoader;
using Terraria.IO;
using System.Collections.Generic;
using System;
using Terraria.DataStructures;
using Terraria.Localization;
using StructureHelper.API;
using System.Threading;
using ThePlateaus.Content.Npc.TheEmperor;
using ThePlateaus.Content.Cutscene;

namespace ThePlateaus.Content.TheColosseum
{
    public class TheColosseumWorld : Subworld
    {
        public override int Width => 3000;
        public override int Height => 2000;
        public override bool ShouldSave => false;
	    public override bool NoPlayerSaving => false;
        public override List<GenPass> Tasks => new List<GenPass>()
	    {
		    new ExampleGenPass(Mod)
	    };
        public override void OnLoad()
	    { 
		    Main.dayTime = true;
 
            Main.worldSurface = Main.maxTilesY + 200;
            Main.rockLayer = Main.maxTilesY + 400;
	    }
    }
    public class ExampleGenPass : GenPass
    {
        private readonly Mod _mod;
	    public ExampleGenPass(Mod mod) : base("Terrain", 1)
        {
            _mod = mod;
        }
	    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
	    {
		    progress.Message = "Generating The Colosseum...";
            int islandWidth = 700;
            int islandHeight = 100;
            int centerX = Main.maxTilesX / 2;
            int centerY = 400;
            int startX = centerX - islandWidth / 2;
            Main.spawnTileX = islandWidth + 470;
            Main.spawnTileY = centerY;
            for (int x = startX; x < startX + islandWidth; x++)
            {
                float edgeDistance = Math.Abs(x - centerX) / (float)(islandWidth / 2);
                int localHeight = (int)(islandHeight * (1f - edgeDistance * edgeDistance));
                for (int y = centerY; y < centerY + localHeight; y++)
                {
                    WorldGen.PlaceTile(x, y, TileID.Dirt, forced: true);
                }
                WorldGen.PlaceTile(x, centerY, TileID.Grass, forced: true);
            }
            for (int x = startX; x < startX + islandWidth; x++)
            {
                float edgeDistance = Math.Abs(x - centerX) / (float)(islandWidth / 2);
                int localHeight = (int)(islandHeight * (1f - edgeDistance * edgeDistance));
                int surfaceY = centerY;
                Tile tile = Main.tile[x, surfaceY];
                if (tile.HasTile && tile.TileType == TileID.Dirt)
                {
                    tile.TileType = TileID.Grass;
                }
            }
            int coliseeZone = 300;
            int treeStartX = startX + 10;
            int treeEndX = startX + islandWidth - 10;
            for (int x = treeStartX; x < treeEndX; x += WorldGen.genRand.Next(4, 9))
            {
                bool inColiseeZone = x > centerX - coliseeZone / 2 && x < centerX + coliseeZone / 2;
                if (inColiseeZone) continue;
                WorldGen.GrowTree(x, centerY);
            }
            int startColosseumX = centerX - 150;
            int startColosseumY = centerY - 56;
            Generator.GenerateStructure("Content/TheColosseum/Structure/TheColosseumFinal", new Point16(startColosseumX, startColosseumY), _mod);
            int spawnX = ((startX + islandWidth / 2) + 100) * 16;
            int spawnY = (centerY - 1) * 16;
            NPC.NewNPC(new EntitySource_WorldGen(), spawnX, spawnY, ModContent.NPCType<TheEmperor>());
	    }
    }
    public class UpdateSubworldSystem : ModSystem
    {
        private bool shown;
	    public override void PreUpdateWorld()
	    {
	        if (SubworldSystem.IsActive<TheColosseumWorld>())
	        {
                if (!shown && Main.netMode != NetmodeID.Server)
                {
                    Main.NewText("Hello world");
                    shown = true;
               }
		        Wiring.UpdateMech();
		        TileEntity.UpdateStart();
		        foreach (TileEntity te in TileEntity.ByID.Values)
		        {
			        te.Update();
		        }
		        TileEntity.UpdateEnd();
		        if (++Liquid.skipCount > 1)
		        {
			        Liquid.UpdateLiquid();
			        Liquid.skipCount = 0;
		        }
	        }
            else
            {
                shown = false;
            }
        }
    }
    public class ColoseumTimeSystem : ModSystem
    {
    public static bool DoNightTransition;

    public static void StartNightTransition()
    {
        DoNightTransition = true;
    }

    public override void PostUpdateWorld()
    {
        if (!SubworldSystem.IsActive<TheColosseumWorld>())
            return;

        if (Main.netMode == NetmodeID.MultiplayerClient)
            return;

        if (!DoNightTransition)
            return;

        if (Main.dayTime) {
            Main.time += 240.0;
            if (Main.time >= 54000.0) {
                Main.dayTime = false;
                Main.time = 0.0;
            }
        }
        else {
            Main.time += 90.0;
            if (Main.time >= 9000.0) {
                Main.time = 9000.0;
                DoNightTransition = false;
            }
        }
    }
    }

    public class ColosseumTimerSystem : ModSystem
    {
        public static int TimerTicks;
        private bool wasinColoseum;
        public static bool StartTimer = false;
        public override void PreUpdateWorld()
        {
            bool inColoseum = SubworldSystem.IsActive<TheColosseumWorld>();
            if (inColoseum)
            {
                if (!wasinColoseum)
                    TimerTicks = 0;
                if (!Main.gamePaused && StartTimer)
                    TimerTicks++;
            }
            wasinColoseum = inColoseum;
        }
        public static string GetFormattedTime()
        {
            int totalSeconds = TimerTicks / 60;
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            int centiseconds = (TimerTicks % 60) * 100 / 60;
            return $"{minutes:00}:{seconds:00}.{centiseconds:00}";
        }
    }
    public class PlayerLimit : ModPlayer
    {
        private bool shownOkOk;
        public override void PostUpdate()
        {
            if (!SubworldSystem.IsActive<TheColosseumWorld>()) {
                shownOkOk = false;
                return;
            }
            if (Player.whoAmI != Main.myPlayer)
                return;
            float killY = (Main.maxTilesY - 50) * 16f;
            if (Player.Bottom.Y >= killY)
            {
                Player.KillMe(
                    PlayerDeathReason.ByCustomReason(NetworkText.FromLiteral($"{Player.name} fell into the void.")),
                    999999,
                    0
                );
            }
            float targetX = 1500f * 16f;
            if (!shownOkOk && Math.Abs(Player.Center.X - targetX) <= 24f)
            {
                ColosseumCutsceneSystem.StartCutscene(Player);
                shownOkOk = true;
            }
        }
        public override void OnRespawn()
        {
            if (!SubworldSystem.IsActive<TheColosseumWorld>())
                return;
            SystemBossSpawn.indexBoss = 0;
            SystemBossSpawn.StartBossSystem = false;
            ColosseumTimerSystem.StartTimer = false;
            ColosseumTimerSystem.TimerTicks = 0;
            SubworldSystem.Exit();
        }
    }
    public class TimerDisplay : InfoDisplay
    {
        public bool Shown = false;
        public static LocalizedText Timer;
        public override string Texture => "Terraria/Images/Projectile_0";
        public override void SetStaticDefaults()
        {
            Timer = this.GetLocalization("Timer test");
        }
        public override bool Active()
        {
            return SubworldSystem.IsActive<TheColosseumWorld>();
        }
        public override string DisplayValue(ref Microsoft.Xna.Framework.Color displayColor, ref Microsoft.Xna.Framework.Color displayShadowColor)
        {
            displayColor = Microsoft.Xna.Framework.Color.White;
            return $"Timer: {ColosseumTimerSystem.GetFormattedTime()}"; 
        }
    }
}