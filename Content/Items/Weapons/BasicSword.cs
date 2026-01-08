using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ThePlateaus.Content.Projectiles;

namespace ThePlateaus.Content.Items.Weapons
{ 
	// Troll Weapon
	public class BasicSword : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 200;
			Item.DamageType = DamageClass.Melee;
			Item.width = 60;
			Item.height = 60;
			Item.useTime = 5;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

			Item.shoot = ModContent.ProjectileType<Projectiles_test>();
			Item.shootSpeed = 12f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LunarBar, 12);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
