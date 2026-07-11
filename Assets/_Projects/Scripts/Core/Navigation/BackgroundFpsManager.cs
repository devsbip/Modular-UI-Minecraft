using UnityEngine;
using UnityEngine.InputSystem;

public class BackgroundFpsManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private GameSettingsSO _gameSettings;

    private float _afkTimer;
    private bool _isReduced;
    private bool _isWindowedFocused = true;

    private const float BUFFER_MINUTE = 60f;
    private const float MAIN_COUNTDOWN = 540f;
    private const float TOTAL_AFK_THRESHOLD = BUFFER_MINUTE + MAIN_COUNTDOWN; // 10 Minutes.

    private void Update()
    {
        if (!_isWindowedFocused) return;

        if (_gameSettings.BackgroundMode != BackgroundFpsMode.Afk) return;

        if (HasPlayerInput())
        {
            ResetAfkState();
            return;
        }

        ProcessAfkCountdown();
    }

    private void OApplicationFocus(bool focus)
    {
        _isWindowedFocused = focus;

        if (!focus)
        {
            ApplyReducedFramerate();
        }
        else
        {
            ResetAfkState();
        }
    }

    private bool HasPlayerInput()
    {
        bool hasKeyboardInput = Keyboard.current != null && Keyboard.current.anyKey.isPressed;
        bool hasMouseMovement = Mouse.current != null && Mouse.current.delta.ReadValue() != Vector2.zero;
        bool hasMouseClicked = Mouse.current != null && (Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed);

        return hasKeyboardInput || hasMouseMovement || hasMouseClicked;
    }

    private void ProcessAfkCountdown()
    {
        if (_isReduced) return;

        _afkTimer += Time.unscaledDeltaTime;

        if (_afkTimer >= TOTAL_AFK_THRESHOLD)
        {
            ApplyReducedFramerate();
        }
    }

    private void ResetAfkState()
    {
        _afkTimer = 0f;

        if (_isReduced)
        {
            RestoreOriginalFramerate();
        }
    }

    private void ApplyReducedFramerate()
    {
        _isReduced = true;
        Application.targetFrameRate = 30;
    }

    private void RestoreOriginalFramerate()
    {
        _isReduced = false;
        int savedIndex = _gameSettings.MaxFramerate;

        if (savedIndex == 22)
        {
            Application.targetFrameRate = -1;
        }
        else
        {
            Application.targetFrameRate = 30 + (savedIndex * 10);
        }
    }
}

