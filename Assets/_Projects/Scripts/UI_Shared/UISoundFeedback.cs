using UnityEngine;
using UnityEngine.EventSystems;

public class UISoundFeedback : MonoBehaviour, IPointerDownHandler, ISubmitHandler
{
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = FindAnyObjectByType<AudioManager>();

        if (_audioManager == null)
        {
            Debug.LogWarning("AudioManager not found", this);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TriggerSound();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        TriggerSound();
    }

    private void TriggerSound()
    {
        if (_audioManager != null)
        {
            _audioManager.PlayUIClick();
        }
    }
}
