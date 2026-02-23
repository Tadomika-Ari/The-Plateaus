using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using ReLogic.Peripherals.RGB.Razer;
using Steamworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using ThePlateaus.Content.Projectiles.RingAncientWitch;

namespace ThePlateaus.Content.Npc.TheAncientWitch
{
    [AutoloadHead]
    public class TheAncientWitch : ModNPC
    {
		public int NumberOfTimesTalkedTo = 0;
		public int StatutQuest = 1;
        public override void SetStaticDefaults() {
			Main.npcFrameCount[Type] = 25; // The total amount of frames the NPC has

			NPCID.Sets.ExtraFramesCount[Type] = 9; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs. This is the remaining frames after the walking frames.
			NPCID.Sets.DangerDetectRange[Type] = 700; // The amount of pixels away from the center of the NPC that it tries to attack enemies.
			NPCID.Sets.HatOffsetY[Type] = 4; // For when a party is active, the party hat spawns at a Y offset.
			NPCID.Sets.ShimmerTownTransform[Type] = false; // This set says that the Town NPC has a Shimmered form. Otherwise, the Town NPC will become transparent when touching Shimmer like other enemies.

			NPCID.Sets.ShimmerTownTransform[Type] = false; // Allows for this NPC to have a different texture after touching the Shimmer liquid.

			// Connects this NPC with a custom emote.
			// This makes it when the NPC is in the world, other NPCs will "talk about him".
			// By setting this you don't have to override the PickEmote method for the emote to appear.
			//NPCID.Sets.FaceEmote[Type] = ModContent.EmoteBubbleType<ExamplePersonEmote>();

			// Influences how the NPC looks in the Bestiary
			NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers() {
				Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
				Direction = 1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
				// Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
				// If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
			};

			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

			// Set Example Person's biome and neighbor preferences with the NPCHappiness hook. You can add happiness text and remarks with localization (See an example in ExampleMod/Localization/en-US.lang).
			// NOTE: The following code uses chaining - a style that works due to the fact that the SetXAffection methods return the same NPCHappiness instance they're called on.
			NPC.Happiness
				.SetBiomeAffection<ForestBiome>(AffectionLevel.Like) // Example Person prefers the forest.
				.SetBiomeAffection<SnowBiome>(AffectionLevel.Dislike) // Example Person dislikes the snow.
				//.SetBiomeAffection<ExampleSurfaceBiome>(AffectionLevel.Love) // Example Person likes the Example Surface Biome
				.SetNPCAffection(NPCID.Dryad, AffectionLevel.Love) // Loves living near the dryad.
				.SetNPCAffection(NPCID.Guide, AffectionLevel.Like) // Likes living near the guide.
				.SetNPCAffection(NPCID.Merchant, AffectionLevel.Dislike) // Dislikes living near the merchant.
				.SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Hate) // Hates living near the demolitionist.
			; // < Mind the semicolon!

			// This creates a "profile" for ExamplePerson, which allows for different textures during a party and/or while the NPC is shimmered.
			//NPCProfile = new Profiles.StackedNPCProfile(
			//	new Profiles.DefaultNPCProfile(Texture, NPCHeadLoader.GetHeadSlot(HeadTexture), Texture + "_Party"),
			//	new Profiles.DefaultNPCProfile(Texture + "_Shimmer", ShimmerHeadIndex, Texture + "_Shimmer_Party")
			//);

			ContentSamples.NpcBestiaryRarityStars[Type] = 3; // We can override the default bestiary star count calculation by setting this.

			//UpgradedText = this.GetLocalization("Upgraded");
		}
        public override void SetDefaults()
        {
            NPC.width = 14;
            NPC.height = 40;
            NPC.lifeMax = 250;
            NPC.townNPC = true;
            NPC.friendly = true;
			NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.damage = 10;
            NPC.defense = 20;
            NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Wizard;
        }
		public override List<string> SetNPCNameList() {
			return new List<string>() {
				"Rannie",
				"Dorothy",
				"Elena",
			};
		}
		public override string GetChat() {
			WeightedRandom<string> chat = new WeightedRandom<string>();

			int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
			if (partyGirl >= 0 && Main.rand.NextBool(4)) {
				chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExamplePerson.PartyGirlDialogue", Main.npc[partyGirl].GivenName));
			}
			// These are things that the NPC has a chance of telling you when you talk to it.
			chat.Add(Language.GetTextValue("What do you want, Traveler?"));

			NumberOfTimesTalkedTo++;
			if (NumberOfTimesTalkedTo >= 10) {
				// This counter is linked to a single instance of the NPC, so if ExamplePerson is killed, the counter will reset.
				chat.Add(Language.GetTextValue("Tu parles beaucoup trop... Dois je en conclure que tu est partant ?"));
			}

			string chosenChat = chat; // chat is implicitly cast to a string. This is where the random choice is made.

			// Here is some additional logic based on the chosen chat line. In this case, we want to display an item in the corner for StandardDialogue4.
			if (chosenChat == Language.GetTextValue("Mods.ExampleMod.Dialogue.ExamplePerson.StandardDialogue4")) {
				// Main.npcChatCornerItem shows a single item in the corner, like the Angler Quest chat.
				Main.npcChatCornerItem = ItemID.HiveBackpack;
			}

			return chosenChat;
		}

		// Chat button and chat
		public override void SetChatButtons(ref string button, ref string button2) { // What the chat buttons are when you open up the chat UI
			button = "Chat";
		}

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
			if (firstButton)
			{
				Main.npcChatText = ChatSelection();
			}
        }

		public string ChatSelection()
		{
			int r = 0;
			r = Main.rand.Next(4);
			if (r == 0) {
				return "Who am I? I am... a researcher.";
			}
			if (r == 1) {
				return "I'm looking for something... or rather someone.";
			}
			if (r == 2) {
				return "I am a very powerful witch! Runes are mysterious things, and I can decipher them.";
			}
			if (r == 3) {
				return "Runes are often inscribed on strange stones... Bring them to me, I need to decipher them.";
			}
			return  "Oh... that you";
		}

		// AI for the attack action
        public override void PostAI()
        {
			bool danger = false;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC target = Main.npc[i];
				if (!target.friendly && target.damage > 0 && target.life > 0)
				{
					if (Vector2.Distance(NPC.Center, target.Center) < 500f)
					{
						danger = true;
						if (danger && Main.netMode != NetmodeID.Server)
						{
							Projectile.NewProjectile(
								NPC.GetSource_FromAI(),
								target.Center,
								Vector2.Zero,
								ModContent.ProjectileType<RuneRing>(),
								0, 0f, Main.myPlayer
							);
						}
						ApplyFire();
					}
				}
			}
        }
		private void ApplyFire()
		{
			float radius = 500f;

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (!npc.active || npc.friendly)
					continue;
				if (npc.whoAmI == NPC.whoAmI)
					continue;
				if (Vector2.Distance(npc.Center, NPC.Center) <= radius)
					npc.AddBuff(BuffID.Frostburn2, 120);
			}
		}
		//Quest Progress (Not Use)
		public int QuestProgress(int StatutQuest)
		{
			StatutQuest++;
			return StatutQuest;
		}

        // Spawn
        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            return NPC.downedBoss1;
        }
    }
}