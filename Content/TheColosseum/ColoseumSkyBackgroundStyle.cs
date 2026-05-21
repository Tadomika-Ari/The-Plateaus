using Terraria;
using Terraria.ModLoader;

namespace ThePlateaus.Content.TheColosseum
{
    public class ColosseumSkyBackgroundStyle : ModSurfaceBackgroundStyle
    {
        private static int SkySlot =>
            BackgroundTextureLoader.GetBackgroundSlot("ThePlateaus/Content/TheColosseum/Backgrounds/ColoseumSky");
        public override int ChooseFarTexture() => SkySlot;
        public override int ChooseMiddleTexture() => SkySlot;
        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            scale = 1f;
            parallax = 0.35;
            a = 0f;
            b = 0f;
            return SkySlot;
        }
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
        }
    }
}