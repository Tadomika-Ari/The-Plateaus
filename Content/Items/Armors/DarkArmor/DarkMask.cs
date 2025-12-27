using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using ThePlateaus.Content.Items.Ores;
using System.Collections.Generic;

namespace ThePlateaus.Content.Items.Armors.DarkArmor
{
    [AutoloadEquip(EquipType.Head)]
    public class DarkMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.value = Item.sellPrice(gold : 15);
            Item.defense = 15;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.10f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "DarkMaskBonus1", "Increases melee damage by 10%")
            {
            OverrideColor = new Microsoft.Xna.Framework.Color(100, 255, 100)
            });

            tooltips.Add(new TooltipLine(Mod, "DarkMaskLore", "Archive: Replica of an ancient mask belonging to a famous warrior whose name has been lost.")
            {
            OverrideColor = new Microsoft.Xna.Framework.Color(150, 150, 150) // Gris (lore)
            });
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<DarkBar>(20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}