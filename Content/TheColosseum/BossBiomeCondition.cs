using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SubworldLibrary;

namespace ThePlateaus.Content.TheColosseum
{
    public class ColoseumBossBiomePlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            if (!SubworldSystem.IsActive<TheColosseumWorld>())
                return;

            bool eowAlive =
                NPC.AnyNPCs(NPCID.EaterofWorldsHead) ||
                NPC.AnyNPCs(NPCID.EaterofWorldsBody) ||
                NPC.AnyNPCs(NPCID.EaterofWorldsTail);

            bool bocAlive =
                NPC.AnyNPCs(NPCID.BrainofCthulhu) ||
                NPC.AnyNPCs(NPCID.Creeper);

            if (eowAlive) {
                Player.ZoneCorrupt = true;
                Player.ZoneCrimson = false;
            }
            else if (bocAlive) {
                Player.ZoneCrimson = true;
                Player.ZoneCorrupt = false;
            }
        }
    }
}