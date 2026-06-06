using System.Collections.Generic;
using UnityEngine;

public class UINavigator : MonoBehaviour
{
    private Stack<UIPanel> _history;

    void Awake()
    {
        _history = new Stack<UIPanel>();
    }

    public void OpenPanel(UIPanel newPanel)
    {
        if (_history.Count > 0)
        {
            _history.Peek().Hide();
        }

        _history.Push(newPanel);            
        newPanel.Show();
    }

    public void GoBack()
    {
        if (_history.Count <= 1) return;

        UIPanel current = _history.Pop();
        current.Hide();

        UIPanel previous = _history.Peek();
        previous.Show();
    }
}
