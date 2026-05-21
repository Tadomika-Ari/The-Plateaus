using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ThePlateaus.Content.Cutscene;
using ThePlateaus.Content.TheColosseum;
using SubworldLibrary;

namespace ThePlateaus.Content.TheColosseum
{
    public class SystemBossSpawn : ModPlayer
    {
        public static bool StartBossSystem = false;
        int[] BossID =
        {
            NPCID.KingSlime,
            NPCID.EyeofCthulhu,
            NPCID.BrainofCthulhu,
            NPCID.QueenBee,
            NPCID.Deerclops,
            NPCID.SkeletronHead,
            NPCID.QueenSlimeBoss,
            NPCID.TheDestroyer,
            NPCID.Retinazer,
            NPCID.SkeletronPrime,
            NPCID.Plantera,
            NPCID.Pumpking,
            NPCID.IceQueen,
            NPCID.Golem,
            NPCID.DD2Betsy,
            NPCID.MartianSaucer,
            NPCID.DukeFishron,
            NPCID.HallowBoss,
            NPCID.CultistBoss,
            NPCID.MoonLordCore,
        };
        public static int indexBoss = 0;
        private int nextSpawnSecond = 0;
        bool FirstBoss = false;
        bool IsDeadBoss = false;
        public override void PreUpdate()
        {
            if (!SubworldSystem.IsActive<TheColosseumWorld>() || !StartBossSystem || Player.dead)
                return;
            if (indexBoss >= BossID.Length)
            {
                StartBossSystem = false;
                return;
            }
            int timerTicks = TheColosseum.ColosseumTimerSystem.TimerTicks;
            int timerSeconds = timerTicks / 60;
            if (timerSeconds >= nextSpawnSecond || !AnyBossAlive())
            {
                NPC.SpawnOnPlayer(Player.whoAmI, BossID[indexBoss]);
                indexBoss++;
                IsDeadBoss = false;
                nextSpawnSecond += 60;
            }
        }
        private static bool AnyBossAlive()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.boss)
                    return true;
            }
            return false;
        }
    }
}