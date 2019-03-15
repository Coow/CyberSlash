using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public delegate void FocusGainedEvent(EditableLabelUi sender);
public delegate void FocusLostEvent(EditableLabelUi sender);
public delegate void ValueChangedEvent(string newValue);

public class EditableLabelUi : MonoBehaviour
{
    #region Fields

    private string _value;

    public TMP_Text Label;
    public TMP_InputField Input;
    public Button Activator;
    public bool reactToKeyPress;

    #endregion

    #region Properties

    public string Value
    {
        get
        {
            return _value;
        }

        set
        {
            SetValue(value);
        }
    }

    #endregion

    #region Initialization

    private void Start()
    {
        Value = Label.text;
    }

    #endregion

    #region Events

    public event ValueChangedEvent ValueChanged;
    public event FocusGainedEvent FocusGained;
    public event FocusLostEvent FocusLost;

    #endregion

    #region Actions

    private bool SetValue(string value)
    {
        //Check if the field needs updating
        if (EqualityComparer<string>.Default.Equals(_value, value))
        {
            return false;
        }

        _value = value;
        Label.text = _value;

        return true;
    }

    private void ChangeState(bool state)
    {
        Input.gameObject.SetActive(state);
        Label.gameObject.SetActive(!state);
        Activator.gameObject.SetActive(!state);

        if (state)
        {
            Input.ActivateInputField();
            FocusGained?.Invoke(this);
        }
        else
        {
            FocusLost?.Invoke(this);
        }
    }
    
    public void OnEditBegin()
    {
        Input.text = Value;
        ChangeState(true);
    }

    public void OnEditEnd()
    {
        ChangeState(false);

        if (SetValue(Input.text))
        {
            //Let listeners know the value has been changed
            ValueChanged?.Invoke(_value);
        }
    }

    public void OnValueChanged()
    {
        if (reactToKeyPress)
        {
            if(SetValue(Input.text))
            {
                ValueChanged?.Invoke(_value);
            }
        }
    }
    
    #endregion

}
