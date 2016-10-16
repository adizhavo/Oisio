using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace Oisio
{
    public class HitEffect : IEffect
    {
        private VignetteAndChromaticAberration vignetteAndChroma;

        public HitEffect(VignetteAndChromaticAberration vignetteAndChroma)
        {
            this.vignetteAndChroma = vignetteAndChroma;
        }

        public void Display()
        {
            vignetteAndChroma.intensity = .35f;
            vignetteAndChroma.blur = 1f;
            vignetteAndChroma.chromaticAberration = 10f;
        }

        public void FrameFeed()
        {
            vignetteAndChroma.intensity = Mathf.Lerp(vignetteAndChroma.intensity, 0.2f, Time.deltaTime);
            vignetteAndChroma.blur = Mathf.Lerp(vignetteAndChroma.blur, 0.2f, Time.deltaTime);
            vignetteAndChroma.chromaticAberration = Mathf.Lerp(vignetteAndChroma.chromaticAberration, 0f, Time.deltaTime);
        }
    }
}