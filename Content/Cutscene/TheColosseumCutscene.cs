using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThePlateaus.Content.TheColosseum;
using Terraria.Audio;

namespace ThePlateaus.Content.Cutscene
{
    public class ColosseumCutsceneSystem : ModSystem
    {
        public enum CutsceneStep
        {
            CameraMove,
            NPCWalk,
            Dialogue,
            ChangeTime,
            CameraMoveFinal,
            End
        }
        public static bool IsActive;
        public static CutsceneStep Step;
        public static int Timer;
        public static Vector2 CameraTarget;
        public static Vector2 CurrentCamPos;
        public static Vector2 ChangeTimeCam;
        public static Vector2 ChangeTimeCamFinal;
        public static bool ChangeCamInit;
        public static Vector2 FinalCamStart;
        public static Vector2 FinalCamTarget;
        public static bool FinalCamInit;
        public static int NpcIndex = -1;
        public static int PlayerWhoAmI = -1;
        public static double GetTime;
        public static void StartCutscene(Player player)
        {
            if (Main.netMode == NetmodeID.Server)
                return;
            IsActive = true;
            Step = CutsceneStep.CameraMove;
            Timer = 0;
            PlayerWhoAmI = player.whoAmI;
            CurrentCamPos = Main.screenPosition;
            CameraTarget = Main.screenPosition;
            NpcIndex = FindAnyTownNpc();
            if (NpcIndex < 0 && Main.netMode != NetmodeID.Server)
                Main.NewText("Cutscene: aucun town NPC trouvé, fallback caméra joueur.");
        }
        public override void ModifyScreenPosition()
        {
            if (!IsActive)
                return;
            CurrentCamPos = Vector2.Lerp(CurrentCamPos, CameraTarget, 0.08f);
            Main.screenPosition = CurrentCamPos;
        }
        public override void PostUpdateEverything()
        {
            if (!IsActive)
                return;
            Timer++;
            switch (Step)
            {
                case CutsceneStep.CameraMove:
                    if (NpcIndex >= 0 && Main.npc[NpcIndex].active)
                    {
                        NPC npc = Main.npc[NpcIndex];
                        CameraTarget = npc.Center - new Vector2(Main.screenWidth * 0.5f, Main.screenHeight * 0.5f);
                    }
                    else if (PlayerWhoAmI >= 0)
                    {
                        Player p = Main.player[PlayerWhoAmI];
                        CameraTarget = (p.Center + new Vector2(280f, -40f)) - new Vector2(Main.screenWidth * 0.5f, Main.screenHeight * 0.5f);
                    }
                    if (Timer >= 120)
                    {
                        Step = CutsceneStep.NPCWalk;
                        Timer = 0;
                    }
                    break;
                case CutsceneStep.NPCWalk:
                    if (NpcIndex >= 0 && Main.npc[NpcIndex].active && PlayerWhoAmI >= 0)
                    {
                        NPC npc = Main.npc[NpcIndex];
                        Player p = Main.player[PlayerWhoAmI];
                        float dir = System.Math.Sign(p.Center.X - npc.Center.X);
                        npc.velocity.X = dir * 1.5f;
                        npc.direction = dir >= 0 ? 1 : -1;
                        npc.spriteDirection = npc.direction;
                        CameraTarget = npc.Center - new Vector2(Main.screenWidth * 0.5f, Main.screenHeight * 0.5f);
                    }
                    if (Timer >= 150)
                    {
                        if (NpcIndex >= 0 && Main.npc[NpcIndex].active)
                            Main.npc[NpcIndex].velocity.X = 0f;
                        Step = CutsceneStep.Dialogue;
                        Timer = 0;
                    }
                    break;
                case CutsceneStep.Dialogue:
                    if (Timer == 1 && Main.netMode != NetmodeID.Server)
                        Main.NewText("Welcome to the Colosseum.\n You keep coming back, time and time again, but with the same result.\n Let your trial begin.\n");
                    if (Timer >= 120)
                    {
                        Step = CutsceneStep.ChangeTime;
                        Timer = 0;
                        ChangeCamInit = false; // <- important
                    }
                    break;

                case CutsceneStep.ChangeTime:
                    if (!ChangeCamInit)
                    {
                        ChangeTimeCam = CurrentCamPos;

                        if (PlayerWhoAmI >= 0) {
                            Player p = Main.player[PlayerWhoAmI];
                            Vector2 skyFocus = p.Center + new Vector2(0f, -700f);
                            ChangeTimeCamFinal = skyFocus - new Vector2(Main.screenWidth * 0.5f, Main.screenHeight * 0.5f);
                            if (ChangeTimeCamFinal.Y < 0f)
                                ChangeTimeCamFinal.Y = 0f;
                        }
                        else {
                            ChangeTimeCamFinal = Main.screenPosition;
                        }
                        ChangeCamInit = true;
                    }
                    float tChange = MathHelper.Clamp(Timer / 90f, 0f, 1f);
                    CameraTarget = Vector2.SmoothStep(ChangeTimeCam, ChangeTimeCamFinal, tChange);
                    GetTime = Main.time;
                    if (tChange >= 1f && Vector2.Distance(CurrentCamPos, ChangeTimeCamFinal) <= 1.5f)
                    {
                        CurrentCamPos = ChangeTimeCamFinal;
                        Main.screenPosition = ChangeTimeCamFinal;
    
                        SoundEngine.PlaySound(
                            new SoundStyle("ThePlateaus/Assets/Music/inter"),
                            Main.LocalPlayer.Center
                        );
                        Main.dayTime = false;
                        Step = CutsceneStep.CameraMoveFinal;
                        Timer = 0;
                    }
                    break;
                case CutsceneStep.CameraMoveFinal:
                    if (!FinalCamInit)
                    {
                        FinalCamStart = CurrentCamPos;
                        if (PlayerWhoAmI >= 0) {
                            Player p = Main.player[PlayerWhoAmI];
                            FinalCamTarget = p.Center - new Vector2(Main.screenWidth * 0.5f, Main.screenHeight * 0.5f);
                        } else {
                            FinalCamTarget = Main.screenPosition;
                        }
                        FinalCamInit = true;
                    }

                    float t = MathHelper.Clamp(Timer / 60f, 0f, 1f);
                    CameraTarget = Vector2.SmoothStep(FinalCamStart, FinalCamTarget, t);

                    if (t >= 1f && Vector2.Distance(CurrentCamPos, FinalCamTarget) <= 1.5f)
                    {
                        CurrentCamPos = FinalCamTarget;
                        Main.screenPosition = FinalCamTarget;

                        Step = CutsceneStep.End;
                        Timer = 0;
                    }
                    break;
                case CutsceneStep.End:
                    if (NpcIndex >= 0 && Main.npc[NpcIndex].active)
                    {
                        Main.npc[NpcIndex].active = false;
                    }
                    IsActive = false;
                    Timer = 0;
                    NpcIndex = -1;
                    PlayerWhoAmI = -1;
                    ColosseumTimerSystem.StartTimer = true;
                    SystemBossSpawn.StartBossSystem = true;
                    break;
            }
            
        }
        private static int FindAnyTownNpc()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.townNPC && npc.friendly)
                    return i;
            }
            return -1;
        }
    }
    public class ColosseumCutscenePlayer : ModPlayer
    {
        public override void PreUpdate()
        {
            if (!ColosseumCutsceneSystem.IsActive)
                return;
            Player.frozen = true;
            Player.controlUseItem = false;
            Player.controlJump = false;
            Player.controlLeft = false;
            Player.controlRight = false;
            Player.velocity = Vector2.Zero;
        }
    }

    public class StartCutsceneCommand : ModCommand
    {
        public override CommandType Type => CommandType.Chat;
        public override string Command => "cutscene";
        public override string Usage => "/cutscene";
        public override string Description => "Lance la cinématique de test";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            ColosseumCutsceneSystem.StartCutscene(caller.Player);
        }
    }
}