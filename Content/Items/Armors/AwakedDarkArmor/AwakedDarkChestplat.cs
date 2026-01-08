using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using ThePlateaus.Content.Items.Ores;
using Microsoft.Xna.Framework;

namespace ThePlateaus.Content.Items.Armors.AwakedDarkArmor
{
    [AutoloadEquip(EquipType.Body)]
    public class AwakedDarkChestplat : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 20;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.25f;
            player.GetModPlayer<AwakedDarkArmorPlayer>().hasDarkChestplate = true;
            if (Main.rand.NextBool(3))
            {
                //Particul for style
                Vector2 position = player.Center + new Vector2(
                    Main.rand.Next(-20, 21),
                    Main.rand.Next(-30, 10)
                );
                Dust dust = Dust.NewDustDirect(
                    position, 0, 0,
                    DustID.Smoke,
                    -1f, 1f,
                    100,
                    Color.Black,
                    1.0f
                );
                dust.noGravity = false;
                dust.velocity.Y *= 0.3f;
                dust.velocity.X *= 0.1f;
                dust.fadeIn = 1f;
            }
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "DarkMaskBonus1", "Increases melee damage by 25%")
            {
            OverrideColor = new Microsoft.Xna.Framework.Color(100, 255, 100)
            });
            tooltips.Add(new TooltipLine(Mod, "Awaked Dark Chesplate", "20 % to dodge any attack for short time")
            {
            OverrideColor = new Microsoft.Xna.Framework.Color(100, 255, 100)
            });
            tooltips.Add(new TooltipLine(Mod, "DarkMaskLore", "Archive: It is said that a man climbed the plateaus...\n But most say that this is a myth. However, this replica of his coat is proof of his past existence."));
        }
    }
    public class AwakedDarkArmorPlayer : ModPlayer
    {
        public bool hasDarkChestplate = false;
        public override void ResetEffects()
        {
            hasDarkChestplate = false;
        }
        public override bool ConsumableDodge(Player.HurtInfo info) // Dodge Mechanic
        {
            if (hasDarkChestplate && Main.rand.NextFloat() < 0.20f)
            {
                for (int i = 0; i < 30; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2Circular(8, 8);
                    Dust dust = Dust.NewDustDirect(
                        Player.position, 
                        Player.width, 
                        Player.height, 
                        DustID.Shadowflame, 
                        velocity.X, 
                        velocity.Y, 
                        100, 
                        Color.Purple, 
                        1.5f
                    );
                    dust.noGravity = true;
                }
                CombatText.NewText(Player.getRect(), Color.Purple, "DODGED!", true);

                Player.SetImmuneTimeForAllTypes(20);
                return true;
            }
            return false;
        }
    }
}
