using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using System.Collections.Generic;
using Terraria.ID;
using ThePlateaus.Content.Items.Runes;

namespace ThePlateaus.Content.Players
{
    public class GlobalLoot : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.WallofFlesh)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StrangeStone>(), 2, 1, 1 ));
        }
    }
}