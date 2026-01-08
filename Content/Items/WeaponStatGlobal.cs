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
        public int bonusRegen = 0;
        public int bonusDefense = 0;
        public int bonusDamage = 0;
        public int bonusCritChance = 0;

        // Update on equipe stat in accessory slot
        public override void UpdateEquip(Item item, Player player)
        {
            player.statLifeMax2 += bonusHealth;
            player.lifeRegen += bonusRegen;
            player.statDefense += bonusDefense;
            player.GetDamage(DamageClass.Generic) += bonusDamage / 100f;
            player.GetCritChance(DamageClass.Generic) += bonusCritChance / 100f;
        }
        // Update in hold Item. For Healt check RuneHealtStat in player folder
        public override void HoldItem(Item item, Player player)
        {
            player.lifeRegen += bonusRegen;
            player.statDefense += bonusDefense;
            player.GetDamage(DamageClass.Generic) += bonusDamage / 100f;
            player.GetCritChance(DamageClass.Generic) += bonusCritChance / 100f;
        }
        // Save section
        public override void SaveData(Item item, TagCompound tag)
        {
            tag["bonusHealth"] = bonusHealth;
            tag["bonusLifeRegen"] = bonusRegen;
            tag["bonusDefense"] = bonusDefense;
            tag["bonusDamage"] = bonusDamage;
            tag["bonusCritChance"] = bonusCritChance;
        }
        public override void LoadData(Item item, TagCompound tag)
        {
            bonusHealth = tag.GetInt("bonusHealth");
            bonusRegen = tag.GetInt("bonusLifeRegen");
            bonusDefense = tag.GetInt("bonusDefense");
            bonusDamage = tag.GetInt("bonusDamage");
            bonusCritChance = tag.GetInt("bonusCritChance");
        }
        public override void ModifyTooltips(Item item, List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            if (bonusHealth > 0)
                tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "BonusHealth", $"Bonus Health : {bonusHealth}"));
            if (bonusRegen > 0)
                tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "BonusRegen", $"Bonus Regen : {bonusRegen}"));
            if (bonusDefense > 0)
                tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "BonusDefense", $"Bonus Defense : {bonusDefense}"));
            if (bonusDamage > 0)
                tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "BonusDamage", $"Bonus Damage : {bonusDamage} %"));
            if (bonusCritChance > 0)
                tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "BonusCritChance", $"Bonus Crit Chance : {bonusCritChance} %"));
        }
    }
}