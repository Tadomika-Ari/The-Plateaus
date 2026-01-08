using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using ThePlateaus.Content.Items.Runes;
using ThePlateaus.Content.Items.Ressource;

namespace ThePlateaus.Content.Players
{
    public class GlobalLoot : GlobalNPC
    {
        private static bool twinsBossDefeated = false;
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            //For Strange Stone
            if (npc.type == NPCID.WallofFlesh)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StrangeStone>(), 2, 1, 1));
            }

            // For Demon Horn
            if (npc.type == NPCID.TheDestroyer)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DemonicHorn>(), 2, 1, 1));
            }
            if (npc.type == NPCID.SkeletronPrime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DemonicHorn>(), 2, 1, 1));
            }
            if (npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.MissingTwin(), ModContent.ItemType<DemonicHorn>(), 2, 1, 1));
            }
        }
    }
}