using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExTools
{
    [Serializable]
    public class ColorTransition
    {
        [SerializeField] private Color normalColor = new Color(1, 1, 1, 1);
        [SerializeField] private Color hoverColor = new Color(0.85F, 0.85F, 0.85F, 1);
        [SerializeField] private Color pressedColor = new Color(0.7F, 0.7F, 0.7F, 1);

        public Color NormalColor { get => normalColor; set => normalColor = value; }
        public Color HoverColor { get => hoverColor; set => hoverColor = value; }
        public Color PressedColor { get => pressedColor; set => pressedColor = value; }
    }

    [Serializable]
    public class AnimationTransition
    {
        [SerializeField] string normalName = "normal";
        [SerializeField] string hoverName = "hover";
        [SerializeField] string pressedName = "pressed";

        public string NormalName { get => normalName; set => normalName = value; }
        public string HoverName { get => hoverName; set => hoverName = value; }
        public string PressedName { get => pressedName; set => pressedName = value; }
    }

    [Serializable]
    public class SpriteSwapTransition
    {
        [SerializeField] Sprite normalSprite;
        [SerializeField] Sprite hoverSprite;
        [SerializeField] Sprite pressedSprite;

        public Sprite NormalSprite { get => normalSprite; set => normalSprite = value; }
        public Sprite HoverSprite { get => hoverSprite; set => hoverSprite = value; }
        public Sprite PressedSprite { get => pressedSprite; set => pressedSprite = value; }
    }
}