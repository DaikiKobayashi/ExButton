using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExButton : UIBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image image;

    // 各アクション時画像に乗算を行う
    [SerializeField] Color hoverColor;
    [SerializeField] Color downColor;


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

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHold = true;
        _lastClickTime = Time.time;
        image.CrossFadeColor(downColor, IMAGE_COLOR_FADE_TIME, true, true);

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
        image.CrossFadeColor(Color.white, IMAGE_COLOR_FADE_TIME, true, true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ホバー時処理
        if (!_isHold)
            image.CrossFadeColor(hoverColor, IMAGE_COLOR_FADE_TIME, true, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isHold)
            image.CrossFadeColor(Color.white, IMAGE_COLOR_FADE_TIME, true, true);
    }
}