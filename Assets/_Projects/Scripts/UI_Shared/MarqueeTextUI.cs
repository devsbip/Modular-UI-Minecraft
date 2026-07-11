using System.Collections;
using TMPro;
using UnityEngine;

public class MarqueeTextUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private RectTransform _maskTransform;
    [SerializeField] private TextMeshProUGUI _targetLabel;
    [SerializeField] private TextMeshProUGUI _shadowLabel;
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("Game Feel Settings")]
    [SerializeField] private float _scrollSpeed = 15f;
    [SerializeField] private float _delayBetweenLoops = 1.5f;
    [SerializeField] private bool _scrollLeftToRight = true;
    
    [Header("Padding (Pixel-Perfect Tweaks)")]
    [SerializeField] private float _startPadding = 2.5f; 
    [SerializeField] private float _endPadding = 15f; 

    private RectTransform _textRectTransform;
    private bool _needsScrolling;
    private bool _isWaitingAtEnd; 
    private bool _isMovingForward;
    
    private float _scrollLimit;
    private float _startPositionX;
    private float _currentWaitTime;

    private float _originalY;
    private float _originalShadowY;
    private float _shadowOffsetX;
    
    private bool _isInitialized;
    private Coroutine _refreshCoroutine;

    private void Awake()
    {
        if (_targetLabel != null)
        {
            _textRectTransform = _targetLabel.rectTransform;
            _originalY = _textRectTransform.anchoredPosition.y;

            if (_shadowLabel != null)
            {
                _originalShadowY = _shadowLabel.rectTransform.anchoredPosition.y;
                _shadowOffsetX = _shadowLabel.rectTransform.localPosition.x - _textRectTransform.localPosition.x;
            }
            
            _isInitialized = true;
        }
    }

    private void Start()
    {
        if (_gameSettings != null)
        {
            _gameSettings.OnLanguageChanged += OnLanguageChanged;
        }

        TriggerRefresh();
    }

    private void OnDestroy()
    {
        if (_gameSettings != null)
        {
            _gameSettings.OnLanguageChanged -= OnLanguageChanged;
        }
    }

    private void OnLanguageChanged(GameLanguage newLanguage)
    {
        TriggerRefresh();
    }

    public void TriggerRefresh()
    {
        if (!_isInitialized || !gameObject.activeInHierarchy) return;

        if (_refreshCoroutine != null)
        {
            StopCoroutine(_refreshCoroutine);
        }
        
        _refreshCoroutine = StartCoroutine(EvaluateTextBoundsRoutine());
    }

    private IEnumerator EvaluateTextBoundsRoutine()
    {
        yield return new WaitForEndOfFrame();

        if (_targetLabel == null || _maskTransform == null) yield break;

        _targetLabel.ForceMeshUpdate();

        float textWidth = _targetLabel.preferredWidth;
        float maskWidth = _maskTransform.rect.width;

        if (textWidth > maskWidth)
        {
            _needsScrolling = true;
            _currentWaitTime = _delayBetweenLoops;
            _isWaitingAtEnd = false;
            _isMovingForward = true;

            _targetLabel.alignment = TextAlignmentOptions.Left;
            if (_shadowLabel != null) _shadowLabel.alignment = TextAlignmentOptions.Left;

            _textRectTransform.pivot = new Vector2(0f, 0.5f);
            _textRectTransform.anchorMin = new Vector2(0f, 0.5f);
            _textRectTransform.anchorMax = new Vector2(0f, 0.5f);

            if (_scrollLeftToRight)
            {
                _startPositionX = -(textWidth - maskWidth);
                _scrollLimit = _startPadding; 
            }
            else
            {
                _startPositionX = _startPadding; 
                _scrollLimit = -(textWidth - maskWidth) - _endPadding;
            }

            Vector2 initialPos = _textRectTransform.anchoredPosition;
            initialPos.x = _startPositionX;
            initialPos.y = _originalY; 
            _textRectTransform.anchoredPosition = initialPos;

            if (_shadowLabel != null)
            {
                RectTransform shadowRect = _shadowLabel.rectTransform;
                shadowRect.pivot = new Vector2(0f, 0.5f);
                shadowRect.anchorMin = new Vector2(0f, 0.5f);
                shadowRect.anchorMax = new Vector2(0f, 0.5f);
                
                shadowRect.anchoredPosition = new Vector2(_startPositionX + _shadowOffsetX, _originalShadowY);
            }
        }
        else
        {
            _needsScrolling = false;
            _targetLabel.alignment = TextAlignmentOptions.Center;
            if (_shadowLabel != null) _shadowLabel.alignment = TextAlignmentOptions.Center;

            _textRectTransform.pivot = new Vector2(0.5f, 0.5f);
            _textRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            _textRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            
            _textRectTransform.anchoredPosition = new Vector2(0f, _originalY);
            
            if (_shadowLabel != null)
            {
                _shadowLabel.rectTransform.pivot = new Vector2(0.5f, 0.5f);
                _shadowLabel.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                _shadowLabel.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                _shadowLabel.rectTransform.anchoredPosition = new Vector2(_shadowOffsetX, _originalShadowY);
            }
        }
    }

    private void Update()
    {
        if (!_needsScrolling) return;

        if (_isWaitingAtEnd)
        {
            _currentWaitTime -= Time.unscaledDeltaTime;
            
            if (_currentWaitTime <= 0)
            {
                _isMovingForward = !_isMovingForward;
                _isWaitingAtEnd = false;
            }
            return;
        }

        float currentX = _textRectTransform.anchoredPosition.x;
        float targetLimit = _isMovingForward ? _scrollLimit : _startPositionX;
        float step = _scrollSpeed * Time.unscaledDeltaTime;
        float newX = Mathf.MoveTowards(currentX, targetLimit, step);

        ApplyPositionX(newX);

        if (Mathf.Abs(newX - targetLimit) < 0.001f)
        {
            _isWaitingAtEnd = true;
            _currentWaitTime = _delayBetweenLoops;
        }
    }

    private void ApplyPositionX(float newX)
    {
        Vector2 newPos = _textRectTransform.anchoredPosition;
        newPos.x = newX;
        _textRectTransform.anchoredPosition = newPos;

        if (_shadowLabel != null)
        {
            Vector2 shadowPos = _shadowLabel.rectTransform.anchoredPosition;
            shadowPos.x = newX + _shadowOffsetX;
            _shadowLabel.rectTransform.anchoredPosition = shadowPos;
        }
    }
}