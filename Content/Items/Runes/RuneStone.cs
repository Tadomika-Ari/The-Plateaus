using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System;
using Terraria.GameContent.Bestiary;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Text;
using Steamworks;
using Microsoft.Build.Tasks;
using Terraria.DataStructures;
using System.Linq.Expressions;
using ThePlateaus.Content.Items.Ores;

namespace ThePlateaus.Content.Items.Runes
{
    public class RuneStone : ModItem
    {
        public RuneType runeType = RuneType.None;
        public int statValue = 0;
        public int ValueAwaked = 0;
        private bool hasGeneratedStats = false;
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ItemRarityID.Gray;
            Item.value = Item.sellPrice(silver: 5);
            Item.maxStack = 1;
        }
        // All update compatible with cheat mod
        public override bool OnPickup(Player player)
        {
            if (!hasGeneratedStats && runeType == RuneType.None)
            {
                GenerateRandomStats();
                hasGeneratedStats = true;
            }
            return base.OnPickup(player);
        }
        public override void OnCreated(ItemCreationContext context)
        {
            if (!hasGeneratedStats && runeType == RuneType.None)
            {
                GenerateRandomStats();
                hasGeneratedStats = true;
            }
        }
        public override void UpdateInventory(Player player)
        {
            if (!hasGeneratedStats && runeType == RuneType.None)
            {
                GenerateRandomStats();
                hasGeneratedStats = true;
            }
        }
        // Generator for stat
        private void GenerateRandomStats()
        {
            int roll = Main.rand.Next(100);

            if (roll < 70)
            {
                Item.rare = ItemRarityID.Blue;
                runeType = (RuneType)Main.rand.Next(1, 4);
                statValue = Main.rand.Next(5, 15);
            }
            else if (roll < 90 && roll > 70)
            {
                Item.rare = ItemRarityID.Purple;
                runeType = (RuneType)Main.rand.Next(4, 7);
                if (runeType == RuneType.CritChance)
                {
                    statValue = Main.rand.Next(5, 15);
                }
                if (runeType == RuneType.Defense)
                {
                    statValue = Main.rand.Next(10, 20);
                }
                if (runeType == RuneType.ImmuneLava)
                {
                    statValue = 1;
                }
            }
            else
            {
                Item.rare = ItemRarityID.Orange;
                runeType = RuneType.Awakening;
                statValue = 1;
                ValueAwaked = 1;
            }
        }
        // Text for describle
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Color color = Color.White;
            string text = "";

            switch (runeType)
            {
                case RuneType.Health:
                    color = new Color(100, 255, 100);
                    text = $"+{statValue} Max Health";
                    break;
                case RuneType.Regeneration:
                    color = new Color(100, 255, 100);
                    text = $"+{statValue} Life Regeneration";
                    break;
                case RuneType.Damage:
                    color = new Color(200, 255, 100);
                    text = $"+{statValue}% Damage";
                    break;
                case RuneType.CritChance:
                    color = new Color(200, 100, 255);
                    text = $"+{statValue}% Critical Strike Chance";
                    break;
                case RuneType.Defense:
                    color = new Color (200, 100, 255);
                    text = $"+{statValue} Defence";
                    break;
                case RuneType.ImmuneLava:
                    color = new Color (200, 100, 255);
                    text = "A stone who can't destroy on lava... Very strange";
                    break;
                case RuneType.Awakening:
                    color = new Color(255, 200, 50);
                    text = "Strange Stone use for awakens a weapon's or armor's true power";
                    break;
            }

            if (!string.IsNullOrEmpty(text))
            {
                tooltips.Add(new TooltipLine(Mod, "RuneEffect", text)
                {
                    OverrideColor = color
                });
            }
        }
        // Save Section
        public override void SaveData(TagCompound tag)
        {
            tag["runeType"] = (int)runeType;
            tag["statValue"] = statValue;
        }
        public override void LoadData(TagCompound tag)
        {
            runeType = (RuneType)tag.GetInt("runeType");
            statValue = tag.GetInt("statValue");
        }
        public override bool CanStack(Item item2)
        {
            if (item2.ModItem is RuneStone other)
            {
                return runeType == other.runeType && statValue == other.statValue;
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<DarkBar>(2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
    // All type of rune
    public enum RuneType
    {
        None,
        Health,
        Damage,
        Regeneration,
        CritChance,
        Defense,
        ImmuneLava,
        Awakening
    }
}