using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExTools
{
    [RequireComponent(typeof(Image))]
    public class ExButton : Selectable
    {
        public Action onTapCallback;
        public Action onHoldCallback;

        private const float HOLD_THRESHOLD_TIME = 0.4f;
        private const float IMAGE_COLOR_FADE_TIME = 0.1f;

        private float _lastClickTime;
        private bool _isHold;

        protected override void Awake()
        {
            base.Awake();
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            _isHold = true;
            _lastClickTime = Time.time;

            Invoke("OnHold", HOLD_THRESHOLD_TIME);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

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
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
        }
    }
}
