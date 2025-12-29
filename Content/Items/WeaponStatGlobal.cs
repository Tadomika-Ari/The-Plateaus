using Terraria;
using Terraria.IO;
using System.Collections.Generic;
using Terraria.ModLoader;
using Steamworks;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;

namespace ThePlateaus.Content.Items
{
    public class EnchantedWeaponData : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public int bonusHealth = 0;
        public int bonusRegen;
        public int bonusDefense;
        public int Damage;

        public override void UpdateEquip(Item item, Player player)
        {
            player.statLifeMax2 += bonusHealth;
            player.lifeRegen += bonusRegen;
            player.statDefense += bonusDefense;
        }
        public override void SaveData(Item item, TagCompound tag)
        {
            tag["bonusHealth"] = bonusHealth;
            tag["bonusLifeRegen"] = bonusRegen;
            tag["bonusDefense"] = bonusDefense;
        }
        public override void LoadData(Item item, TagCompound tag)
        {
            bonusHealth = tag.GetInt("bonusHealth");
            bonusRegen = tag.GetInt("bonusLifeRegen");
            bonusDefense = tag.GetInt("bonusDefense");
        }
        public override void ModifyTooltips(Item item, List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            if (bonusHealth > 0)
                tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "BonusHealth", $"Bonus Health : {bonusHealth}"));
            if (bonusRegen > 0)
                tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "BonusRegen", $"Bonus Regen : {bonusRegen}"));
            if (bonusDefense > 0)
                tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "BonusDefense", $"Bonus Defense : {bonusDefense}"));
        }
    }
}