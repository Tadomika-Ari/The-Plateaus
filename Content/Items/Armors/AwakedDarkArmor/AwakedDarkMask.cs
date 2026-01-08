using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using ThePlateaus.Content.Items.Ores;
using System.Collections.Generic;
using ThePlateaus.Content.Items.Runes;
using ThePlateaus.Content.Items.Armors.DarkArmor;
using ThePlateaus.Content.Items.Ressource;

namespace ThePlateaus.Content.Items.Armors.AwakedDarkArmor
{
    [AutoloadEquip(EquipType.Head)]
    public class AwakedDarkMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(gold : 15);
            Item.defense = 25;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.25f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "DarkMaskBonus1", "Increases melee damage by 25%")
            {
            OverrideColor = new Microsoft.Xna.Framework.Color(100, 255, 100)
            });

            tooltips.Add(new TooltipLine(Mod, "DarkMaskLore", "Archive: Replica of an ancient mask belonging to a famous warrior whose name has been lost."));
        }
    }
}