using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExTools
{
    [RequireComponent(typeof(Image))]
    public class ExButton : UIBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        enum TransitionType
        {
            None = 0,
            Color = 1,
            Animation = 2,
        }

        [SerializeField] Image _image;
        [SerializeField] TransitionType _transitionType;
        [SerializeField] ColorTransition _colorTransition;
        [SerializeField] AnimationTransition _animationTransition;

        public Action onTapCallback;
        public Action onHoldCallback;

        private const float HOLD_THRESHOLD_TIME = 0.4f;
        private const float IMAGE_COLOR_FADE_TIME = 0.1f;

        private float _lastClickTime;
        private bool _isHold;

        protected override void Awake()
        {
            base.Awake();
            _image.CrossFadeColor(_colorTransition.NormalColor, 0F, true, true);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isHold = true;
            _lastClickTime = Time.time;
            _image.CrossFadeColor(_colorTransition.PressedColor, IMAGE_COLOR_FADE_TIME, true, true);

            Invoke("OnHold", HOLD_THRESHOLD_TIME);
        }

        public void OnPointerUp(PointerEventData eventData)
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

        /// <summary>
        /// タップイベント後処理
        /// </summary>
        private void TapPostProcess()
        {
            _isHold = false;
            CancelInvoke();
            _image.CrossFadeColor(_colorTransition.NormalColor, IMAGE_COLOR_FADE_TIME, true, true);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // ホバー時処理
            if (!_isHold)
                _image.CrossFadeColor(_colorTransition.HoverColor, IMAGE_COLOR_FADE_TIME, true, true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isHold)
                _image.CrossFadeColor(_colorTransition.NormalColor, IMAGE_COLOR_FADE_TIME, true, true);
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