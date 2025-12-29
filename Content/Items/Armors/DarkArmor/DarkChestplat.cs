using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using ThePlateaus.Content.Items.Ores;

namespace ThePlateaus.Content.Items.Armors.DarkArmor
{
    [AutoloadEquip(EquipType.Body)]
    public class DarkChestplat : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 20;
            Item.value = Item.sellPrice(gold: 5);
        }
        public override void UpdateArmorSet(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.15f;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "DarkMaskBonus1", "Increases melee damage by 15%")
            {
            OverrideColor = new Microsoft.Xna.Framework.Color(100, 255, 100)
            });

            tooltips.Add(new TooltipLine(Mod, "DarkMaskLore", "Archive: It is said that a man climbed the plateaus...\n But most say that this is a myth. However, this replica of his coat is proof of his past existence.")
            {
            OverrideColor = new Microsoft.Xna.Framework.Color(150, 150, 150)
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
