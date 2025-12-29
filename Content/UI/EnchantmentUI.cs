using Iced.Intel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using ThePlateaus.Content.Items;
using ThePlateaus.Content.Items.Runes;

namespace ThePlateaus.Content.UI
{
    public class EnchantmentUI : UIState
    {
        private UIPanel mainPanel;
        private UIText title;
        private Item[] WeaponSlotArray;
        private Item[] RuneSlotArray;
        
        public override void OnInitialize()
        {
            mainPanel = new UIPanel();
            mainPanel.Width.Set(400, 0);
            mainPanel.Height.Set(300, 0);
            mainPanel.HAlign = 0.5f;
            mainPanel.VAlign = 0.5f;
            Append(mainPanel);

            title = new UIText("Enchantment Table");
            title.HAlign = 0.5f;
            title.Top.Set(20, 0);
            mainPanel.Append(title);

            var enchantButton = new UITextPanel<string>("Enchant Weapon");
            enchantButton.Width.Set(200, 0);
            enchantButton.Height.Set(40, 0);
            enchantButton.HAlign = 0.5f;enchantButton.Height.Set(40, 0);
            enchantButton.HAlign = 0.5f;
            enchantButton.VAlign = 0.7f;
            enchantButton.OnLeftClick += EnchantWeapon;
            mainPanel.Append(enchantButton);

            var CloseButton = new UITextPanel<string>("Close");
            CloseButton.Width.Set(70, 0);
            CloseButton.Height.Set(40, 0);
            CloseButton.HAlign = 0.95f;
            CloseButton.VAlign = 0.05f;
            CloseButton.OnLeftClick += CloseUI;
            mainPanel.Append(CloseButton);

            WeaponSlotArray = new Item[1];
            WeaponSlotArray[0] = new Item();
            var ItemSlots = new UIItemSlot(WeaponSlotArray, 0, ItemSlot.Context.InventoryItem);
            ItemSlots.Width.Set(100, 0);
            ItemSlots.Height.Set(100, 0);
            ItemSlots.HAlign = 0.30f;
            ItemSlots.VAlign = 0.4f;
            mainPanel.Append(ItemSlots);

            RuneSlotArray = new Item[1];
            RuneSlotArray[0] = new Item();
            var ItemSlots2 = new UIItemSlot(RuneSlotArray, 0, ItemSlot.Context.InventoryItem);
            ItemSlots2.Width.Set(100, 0);
            ItemSlots2.Height.Set(100, 0);
            ItemSlots2.HAlign = 0.70f;
            ItemSlots2.VAlign = 0.40f;
            mainPanel.Append(ItemSlots2);
        }

        private void EnchantWeapon(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            Item iteminslot = WeaponSlotArray[0];
            Item runeinslot = RuneSlotArray[0];

            if (!iteminslot.IsAir && !runeinslot.IsAir && runeinslot.ModItem is RuneStone rune)
            {
                RuneType type = rune.runeType;
                int value = rune.statValue;
                int statutEnchant = 0;
                if (type == RuneType.None)
                {
                    Main.NewText("Rune with no stat ? Strange...", Color.Orange);
                }
                switch (type)
                {
                    case RuneType.Health:
                        Main.NewText($"Weapon enchanted with +{value} and {type}");
                        var globalItem = iteminslot.GetGlobalItem<EnchantedWeaponData>();
                        globalItem.bonusHealth += value;
                        statutEnchant += 1;
                        break;
                    case RuneType.Damage:
                        Main.NewText($"Weapon enchanted with +{value} and {type}");
                        iteminslot.damage += value;
                        statutEnchant += 1;
                        break;
                    case RuneType.Regeneration:
                        Main.NewText($"Weapon enchanted with +{value} and {type}");
                        var globalItemRegen = iteminslot.GetGlobalItem<EnchantedWeaponData>(); 
                        globalItemRegen.bonusRegen += value;
                        statutEnchant += 1;
                        break;
                    case RuneType.Defense:
                        Main.NewText($"Weapon enchanted with +{value} and {type}");
                        var globalItemDef = iteminslot.GetGlobalItem<EnchantedWeaponData>();
                        globalItemDef.bonusDefense += value;
                        statutEnchant += 1;
                        break;
                    case RuneType.CritChance:
                        Main.NewText($"Weapon enchanted with +{value} and {type}");
                        iteminslot.crit += value;
                        statutEnchant += 1;
                        break;
                    case RuneType.Awakening:
                        Main.NewText($"Your weapon is now a true Weapon !");
                        statutEnchant += 1;
                        break;
                }
                if (statutEnchant != 0)
                {
                    runeinslot.TurnToAir();
                    statutEnchant = 0;
                }
                else
                {
                    Main.NewText("Not Work ?");
                }
                
            }
            else
            {
                Main.NewText("No valid weapon equipped!", Color.Red);
            }
        }
        private void CloseUI(UIMouseEvent evt, UIElement listeningElement)
        {
            ModContent.GetInstance<EnchantmentUISystem>().ToggleUI();
            Main.playerInventory = false;
        }
    }

    public class EnchantmentUISystem : ModSystem
    {
        public UserInterface EnchantmentInterface;
        public EnchantmentUI enchantmentUI;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                EnchantmentInterface = new UserInterface();
                enchantmentUI = new EnchantmentUI();
                enchantmentUI.Activate();
            }
        }

        public override void Unload()
        {
            EnchantmentInterface = null;
            enchantmentUI = null;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (EnchantmentInterface?.CurrentState != null)
            {
                EnchantmentInterface.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ThePlateaus: Enchantment UI",
                    delegate
                    {
                        if (EnchantmentInterface?.CurrentState != null)
                        {
                            EnchantmentInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
        public void ToggleUI()
        {
            if (EnchantmentInterface.CurrentState == null)
            {
                EnchantmentInterface.SetState(enchantmentUI);
                Main.playerInventory = true;
            }
            else
            {
                EnchantmentInterface.SetState(null);
            }
        }
    }
}