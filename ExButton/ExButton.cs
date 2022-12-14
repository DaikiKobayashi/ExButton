using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExTools
{
    [AddComponentMenu("UI/ExButton")]
    [RequireComponent(typeof(Image))]
    public class ExButton : Selectable
    {
        enum TransitionType
        {
            None = 0,
            Color = 1,
            Animation = 2,
            SpriteSwap = 3,
        }

        [SerializeField] Image _image;
        [SerializeField] Animator _animator;
        [SerializeField] TransitionType _transitionType;
        [SerializeField] ColorTransition _colorTransition;
        [SerializeField] AnimationTransition _animationTransition;
        [SerializeField] SpriteSwapTransition _spriteSwapTransition;

        public Action onTapCallback;
        public Action onHoldCallback;

        private const float HOLD_THRESHOLD_TIME = 0.4f;
        private const float IMAGE_COLOR_FADE_TIME = 0.1f;

        private float _lastClickTime;
        private bool _isHold;

        protected override void Awake()
        {
            base.Awake();

            SetNormal();
        }

        public override void OnSelect(BaseEventData eventData)
        {
            print("on select.");
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            _isHold = true;
            _lastClickTime = Time.time;
            SetPressed();

            Invoke("OnHold", HOLD_THRESHOLD_TIME);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (Time.time - _lastClickTime < HOLD_THRESHOLD_TIME)
                OnTap();
        }

        private void OnTap()
        {
            if (_isHold == false)
                return;

            onTapCallback?.Invoke();

            TapPostProcess();
        }

        private void OnHold()
        {
            if (_isHold == false)
                return;

            onHoldCallback?.Invoke();

            TapPostProcess();
        }

        private void TapPostProcess()
        {
            _isHold = false;
            CancelInvoke();
            SetNormal();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isHold)
                SetHover();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (!_isHold)
                SetNormal();
        }


        private void SetNormal()
        {
            switch (_transitionType)
            {
                case TransitionType.Color:
                    _image.CrossFadeColor(_colorTransition.NormalColor, IMAGE_COLOR_FADE_TIME, true, true);
                    break;
                case TransitionType.Animation:
                    PlayAnimation(_animationTransition.NormalName);
                    break;
                case TransitionType.SpriteSwap:
                    _image.sprite = _spriteSwapTransition.NormalSprite;
                    break;
                default:
                    break;
            }
        }

        private void SetHover()
        {
            switch (_transitionType)
            {
                case TransitionType.Color:
                    _image.CrossFadeColor(_colorTransition.HoverColor, IMAGE_COLOR_FADE_TIME, true, true);
                    break;
                case TransitionType.Animation:
                    PlayAnimation(_animationTransition.HoverName);
                    break;
                case TransitionType.SpriteSwap:
                    _image.sprite = _spriteSwapTransition.HoverSprite;
                    break;
                default:
                    break;
            }
        }

        private void SetPressed()
        {
            switch (_transitionType)
            {
                case TransitionType.Color:
                    _image.CrossFadeColor(_colorTransition.PressedColor, IMAGE_COLOR_FADE_TIME, true, true);
                    break;
                case TransitionType.Animation:
                    PlayAnimation(_animationTransition.PressedName);
                    break;
                case TransitionType.SpriteSwap:
                    _image.sprite = _spriteSwapTransition.PressedSprite;
                    break;
                default:
                    break;
            }
        }

        private void PlayAnimation(string animName)
        {
            if (_animator == null)
            {
                Debug.LogWarning("Please set 'Animator Component", this);
                return;
            }

            _animator.Play(animName, 0, 0F);
        }

#if UNITY_EDITOR
        protected override void Reset()
        {
            TryGetComponent(out _image);
            _transitionType = TransitionType.Color;
        }
#endif
    }
}
