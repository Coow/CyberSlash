using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SplitUI : MonoBehaviour
{
    #region Fields

    private int _min;
    private int _max;
    private int _amount;
    private IInventorySlot _origin;
    private IInventorySlot _target;

    /// <summary>
    /// The GameObject containing the value display
    /// </summary>
    public TMP_Text Text;

    /// <summary>
    /// The GameObject containing the input field
    /// </summary>
    public TMP_InputField Input;

    /// <summary>
    /// The GameObject containing the slider
    /// </summary>
    public Slider Slider;

    public bool BeginHalf = true;

    #endregion

    #region Private

    /// <summary>
    /// The method that initializes the GameObject properly
    /// </summary>
    private void Start()
    {
        Slider.wholeNumbers = true;
        Unset();
    }

    /// <summary>
    /// The method will properly update the visuals
    /// </summary>
    private void UpdateVisuals()
    {
        UpdateSlider();
        UpdateText();
        UpdateInput();
    }

    /// <summary>
    /// roperly updates the slider display
    /// </summary>
    private void UpdateSlider()
    {
        Slider.minValue = _min;
        Slider.maxValue = _max;
        Slider.value = _amount;
    }

    /// <summary>
    /// Properly updates the value text display
    /// </summary>
    private void UpdateText()
    {
        Text.text = $"{_amount} / {_max}";
    }

    /// <summary>
    /// Properly updates the input field display
    /// </summary>
    private void UpdateInput()
    {
        Input.text = $"{_amount}";
    }

    #endregion

    #region Public

    /// <summary>
    /// Properly sets up everything for the origin and target slots
    /// </summary>
    /// <param name="origin">The slot to split the stack from</param>
    /// <param name="target">The slot to split the stack into</param>
    public void Set(IInventorySlot origin, IInventorySlot target)
    {
        _origin = origin;
        _target = target;
        _amount = !BeginHalf || _origin.Amount == 1 ? 1 : _origin.Amount / 2;
        _max = _origin.Amount;

        UpdateVisuals();
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Reacts to any input field changes and updates the relevant visuals
    /// </summary>
    public void InputChanged()
    {
        _amount = int.Parse(Input.text);
        UpdateVisuals();
    }

    /// <summary>
    /// Reacts to any slider value changes and updates the relevant visuals
    /// </summary>
    public void SliderChanged()
    {
        _amount = (int)Slider.value;
        UpdateVisuals();
    }

    /// <summary>
    /// Applies the selected amount to the slot stacks
    /// </summary>
    public void Validate()
    {
        Debug.Log("Validated");
        _origin.Remove(_amount - _target.Add(_amount));
        Unset();
    }

    /// <summary>
    /// Cancels the stack splitting
    /// </summary>
    public void Cancel()
    {
        Debug.Log("Canclled");
        Unset();
    }

    /// <summary>
    /// Re-initializes the GameObject for the next call
    /// </summary>
    public void Unset()
    {
        _origin = null;
        _target = null;
        _min = 0;
        _max = 0;
        _amount = 0;
        UpdateVisuals();
        gameObject.SetActive(false);
    }
    
    #endregion
}
