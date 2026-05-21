using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ThePlateaus.Content.Cutscene;
using SubworldLibrary;
using ThePlateaus.Content.TheColosseum;

namespace ThePlateaus.Content.Items.DebugObject
{
    public class DebugObject: ModItem
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
                SystemBossSpawn.StartBossSystem = true;
            }
            return true;
        }     
    }
}