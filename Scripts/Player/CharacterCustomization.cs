// ==================================
// CharacterCustomization.cs
// Handles player appearance: hair, skin, outfit
// ==================================

using UnityEngine;

namespace ExoPioneer.PlayerSystem
{
    public class CharacterCustomization : MonoBehaviour
    {
        [Header("References")]
        public SkinnedMeshRenderer bodyRenderer;   // Player body material
        public SkinnedMeshRenderer hairRenderer;   // Player hair mesh
        public SkinnedMeshRenderer outfitRenderer; // Player outfit

        [Header("Options")]
        public Material[] skinTones;
        public Material[] hairStyles;
        public Material[] outfits;

        private int skinIndex = 0;
        private int hairIndex = 0;
        private int outfitIndex = 0;

        void Start()
        {
            ApplyCustomization();
        }

        public void NextSkin()
        {
            skinIndex = (skinIndex + 1) % skinTones.Length;
            ApplyCustomization();
        }

        public void NextHair()
        {
            hairIndex = (hairIndex + 1) % hairStyles.Length;
            ApplyCustomization();
        }

        public void NextOutfit()
        {
            outfitIndex = (outfitIndex + 1) % outfits.Length;
            ApplyCustomization();
        }

        private void ApplyCustomization()
        {
            if (skinTones.Length > 0 && bodyRenderer != null)
                bodyRenderer.material = skinTones[skinIndex];

            if (hairStyles.Length > 0 && hairRenderer != null)
                hairRenderer.material = hairStyles[hairIndex];

            if (outfits.Length > 0 && outfitRenderer != null)
                outfitRenderer.material = outfits[outfitIndex];
        }
    }
}
