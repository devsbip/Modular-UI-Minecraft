using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderHoverVisuals : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Slider Components")]
    [SerializeField] private Image _background;
    [SerializeField] private Sprite _default;
    [SerializeField] private Sprite _highlighted;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _background.sprite = _highlighted;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _background.sprite = _default;
    }
}
