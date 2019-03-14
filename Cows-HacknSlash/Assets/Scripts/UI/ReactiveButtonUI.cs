using System;
using TMPro;
using UnityEngine;

public delegate void ReactiveButtonClickedEvent(ReactiveButtonUI sender);

public class ReactiveButtonUI : MonoBehaviour
{
    public TMP_Text Label;
    public event ReactiveButtonClickedEvent Clicked;
    public int Id;

    public string Text
    {
        get
        {
            return Label.text;
        }
        set
        {
            Label.text = value;
        }
    }

    public void ButtonClicked()
    {
        Clicked?.Invoke(this);
    }
}
