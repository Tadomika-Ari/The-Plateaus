using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using ThePlateaus.Content.Items.Ores;

namespace ThePlateaus.Content.Items.Armors.DarkArmor
{
    [AutoloadEquip(EquipType.Legs)]
    public class DarkLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 15);
            Item.defense = 10;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.25f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "DarkLeggingsBonus1", "Increases move speed by 25%")
            {
            OverrideColor = new Microsoft.Xna.Framework.Color(100, 255, 100)
            });
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<DarkBar>(15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}