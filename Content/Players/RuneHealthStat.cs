using Terraria;
using Terraria.ModLoader;
using ThePlateaus.Content.Items;

namespace ThePlateaus.Content.Players
{
    public class EnchantedWeaponPlayer : ModPlayer
    {
        public override void PostUpdateEquips()
        {
            Item heldItem = Player.HeldItem;
            if (!heldItem.IsAir && heldItem.TryGetGlobalItem(out EnchantedWeaponData enchant))
            {
                if (enchant.bonusHealth > 0)
                {
                    // Max health stat is here because different calcul for healt with holdItem
                    Player.statLifeMax2 += enchant.bonusHealth;
                }
            }
        }
    }
}