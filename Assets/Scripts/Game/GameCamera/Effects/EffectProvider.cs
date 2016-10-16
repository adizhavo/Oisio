using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace Oisio
{
    public class EffectProvider : MonoBehaviour
    {

        public static EffectProvider instance
        {
            private set;
            get;
        }

        [SerializeField] private VignetteAndChromaticAberration vignetteAndChroma;

        private IEffect[] availableEffects;

        private void Awake()
        {
            instance = this;
            availableEffects = InitEffects();
        }

        private void Update()
        {
            foreach(IEffect effect in availableEffects)
                effect.FrameFeed();
        }

        protected virtual IEffect[] InitEffects()
        {
            return new IEffect[]
            {
                new HitEffect(vignetteAndChroma)
            };
        }

        public void DisplayEffect<T>() where T : IEffect
        {
            foreach(IEffect effect in availableEffects)
                if (effect is T)
                    effect.Display();
        }
    }
}