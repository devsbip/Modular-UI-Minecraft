using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderHoverVisuals : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Dependencies")]
    [SerializeField] private Image _backgroundImage;

    [Header("Settings")]
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _highlightedSprite;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _backgroundImage.sprite = _highlightedSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _backgroundImage.sprite = _defaultSprite;
    }
}
