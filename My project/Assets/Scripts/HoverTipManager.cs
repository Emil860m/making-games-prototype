using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoverTipManager : MonoBehaviour
{
    public TextMeshProUGUI tipText;
    public RectTransform tipWindow;

    public static Action<string, Vector2> OnMouseHover;
    public static Action OnMouseExit;

    //Subscribe to events
    private void OnEnable()
    {
        OnMouseHover += ShowTip;
        OnMouseExit += HideTip;
    }
    // Unsubscribe to events
    private void OnDisable()
    {
        OnMouseHover -= ShowTip;
        OnMouseExit -= HideTip;
    }

    private void Start()
    {
        HideTip();
    }
    private void ShowTip(string tip, Vector2 mousePos)
    {
        tipText.text = tip;
        tipWindow.sizeDelta = new Vector2(tipText.preferredWidth > 200 ? 200 : tipText.preferredWidth, tipText.preferredHeight);
        tipWindow.gameObject.SetActive(true);
        tipWindow.transform.position = new Vector3(mousePos.x + tipWindow.sizeDelta.x * 2, mousePos.y, 0);


    }
    private void HideTip()
    {
        tipText.text = default;
        tipWindow.gameObject.SetActive(false);
    }
}
