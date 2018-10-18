using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    #region Fields

    private IInventorySlot _slot;

    /// <summary>
    /// The GameObject displaying the Item
    /// </summary>
    public ItemUI Front;

    /// <summary>
    /// The GameObject displaying the slot background
    /// </summary>
    public Image Background;

    /// <summary>
    /// The GameObject displaying the amount
    /// </summary>
    public TextMeshProUGUI Text;

    #endregion

    #region Properties

    /// <summary>
    /// The slot object
    /// </summary>
    public IInventorySlot Slot => _slot;

    #endregion

    #region Private

    /// <summary>
    /// Reacts to changes with slot properties
    /// </summary>
    /// <param name="sender">The sender of the event</param>
    /// <param name="e">The event data</param>
    private void Slot_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        UpdateVisuals();
    }

    /// <summary>
    /// Updates the display of the slot
    /// </summary>
    private void UpdateVisuals()
    {
        Front.SetItem(_slot.Item);
        SetAmount(_slot.Amount, _slot.Item.MaxStack);
        SetActiveParts(!Slot.Empty);
    }

    /// <summary>
    /// Activates or deactivates the display of the item
    /// </summary>
    /// <param name="active">Should the item be displayed</param>
    private void SetActiveParts(bool active = true)
    {
        Front.gameObject.SetActive(active);
        Text.gameObject.SetActive(active);
    }

    /// <summary>
    /// Formats the display of the amount inside the slot
    /// </summary>
    /// <param name="amount">The amount in the slot stack</param>
    /// <param name="max">The max stack size of the item</param>
    private void SetAmount(int amount, int max)
    {
        if (max > 1)
        {
            Text.text = $"{amount}/{max}";
        }
        else
        {
            Text.text = "";
        }
    }
    
    #endregion

    /// <summary>
    /// Sets up the display properly based on slot data
    /// </summary>
    /// <param name="slot">The slot to display</param>
    public void Set(IInventorySlot slot)
    {
        if (_slot != null)
        {
            _slot.PropertyChanged -= Slot_PropertyChanged;
        }

        _slot = slot;
        _slot.PropertyChanged += Slot_PropertyChanged;

        UpdateVisuals();
    }
}
