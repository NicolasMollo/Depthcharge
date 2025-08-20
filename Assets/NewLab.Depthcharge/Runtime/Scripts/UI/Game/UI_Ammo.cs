using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Depthcharge.UI
{

    public class UI_Ammo : MonoBehaviour
    {

        private List<Image> ammo = null;

        [SerializeField]
        private GameObject prefabAmmo = null;
        [SerializeField]
        private Transform ammoContainer = null;
        [SerializeField]
        private float ammoTransparency = 1.0f;

        public void SetUp(int ammoCount, float ammoTransparency = 1.0f)
        {
            this.ammoTransparency = ammoTransparency != 1.0f ? ammoTransparency : this.ammoTransparency;
            ammo = new List<Image>();
            Image temporary = null;
            for (int i = 0; i < ammoCount; i++)
            {
                temporary = CreateAmmo();
                ammo.Add(temporary);
            }
        }

        public void AddTransparency()
        {
            float alpha = ammoTransparency;
            Color newColor = default;
            foreach (Image image in ammo)
            {
                if (image.color.a != alpha)
                {
                    newColor = new Color(image.color.r, image.color.g, image.color.b, alpha);
                    image.color = newColor;
                    return;
                }
            }
        }
        public void RemoveTransparency()
        {
            float alpha = 1.0f;
            Color newColor = new Color(
                ammo[0].color.r,
                ammo[0].color.g,
                ammo[0].color.b,
                alpha
                );
            foreach (Image image in ammo)
                image.color = newColor;
        }

        private Image CreateAmmo()
        {
            GameObject ammoObj = Instantiate(prefabAmmo, ammoContainer);
            Image ammoImage = ammoObj.GetComponent<Image>();
            return ammoImage;
        }

    }

}