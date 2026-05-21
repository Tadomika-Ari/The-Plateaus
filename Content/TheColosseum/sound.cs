using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SubworldLibrary;
using ThePlateaus.Content.TheColosseum;

namespace ThePlateaus.Content.TheColoseum
{
    public class ClockSound: ModSceneEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public override bool IsSceneEffectActive(Player player)
        {
            return SubworldSystem.IsActive<TheColosseumWorld>();
        }

        public override int Music => MusicID.Boss1;
    }
}