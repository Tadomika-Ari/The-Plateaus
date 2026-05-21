using SubworldLibrary;
using Terraria;
using Terraria.ModLoader;

namespace ThePlateaus.Content.TheColosseum
{
    public class ColosseumSceneEffect : ModSceneEffect
    {
        public override bool IsSceneEffectActive(Player player)
            => SubworldSystem.IsActive<TheColosseumWorld>();
        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle
            => ModContent.GetInstance<ColosseumSkyBackgroundStyle>();
        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle
            => null;
    }
}