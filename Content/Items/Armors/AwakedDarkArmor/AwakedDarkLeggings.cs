using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using ThePlateaus.Content.Items.Ores;

namespace ThePlateaus.Content.Items.Armors.AwakedDarkArmor
{
    [AutoloadEquip(EquipType.Legs)]
    public class AwakedDarkLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(gold: 0);
            Item.defense = 20;
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
            tooltips.Add(new TooltipLine(Mod, "DarkLeggingsAwak", "Archive : The plateaus are fragments of the world, caused by a titanic clash between two entities... \nOne capable of creating and destroying, the other being the origin of everything."));
        }
    }
}