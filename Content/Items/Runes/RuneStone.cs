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

namespace ThePlateaus.Content.Items.Runes
{
    public class RuneStone : ModItem
    {
        public RuneType runeType = RuneType.None;
        public int statValue = 0;
        private bool hasGeneratedStats = false;
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ItemRarityID.Gray;
            Item.value = Item.sellPrice(silver: 5);
            Item.maxStack = 1;
        }
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
        private void GenerateRandomStats()
        {
            int roll = Main.rand.Next(100);

            if (roll < 60)
            {
                Item.rare = ItemRarityID.Blue;
                runeType = (RuneType)Main.rand.Next(1, 4);
                statValue = Main.rand.Next(5, 15);
            }
            else if (roll < 90 && roll > 60)
            {
                Item.rare = ItemRarityID.Purple;
                runeType = (RuneType)Main.rand.Next(4, 6);
                if (runeType == RuneType.CritChance)
                {
                    statValue = Main.rand.Next(5, 15);
                }
                if (runeType == RuneType.Defense)
                {
                    statValue = Main.rand.Next(10, 20);
                }
            }
            else
            {
                Item.rare = ItemRarityID.Orange;
                runeType = RuneType.Awakening;
                statValue = 1;
            }
        }
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
                    color = new Color(200, 100, 255);
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
                case RuneType.Awakening:
                    color = new Color(255, 200, 50);
                    text = "Awakens a weapon's true power";
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
            recipe.AddIngredient(ItemID.DirtBlock, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
    public enum RuneType
    {
        None,
        Health, // Rareté : Bleu
        Damage, // Rareté : Bleu
        Regeneration, // Rareté : Bleu
        CritChance, // Rareté : Bleu
        Defense, // Rareté : Violet
        Awakening // Rareté : Or
    }
}