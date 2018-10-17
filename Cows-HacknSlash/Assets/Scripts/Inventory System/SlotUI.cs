using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    #region Fields

    private IInventorySlot _slot;

    public ItemUI Front;
    public Image Background;
    public TextMeshProUGUI Text;

    #endregion

    #region Properties

    public IInventorySlot Slot => _slot;

    #endregion

    #region Private

    private void UpdateVisuals()
    {
        Front.SetItem(_slot.Item);
        SetAmount(_slot.Amount, _slot.Item.MaxStack);
        SetActiveParts(!Slot.Empty);
    }

    private void Slot_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        UpdateVisuals();
    }

    private void Unset()
    {
        SetActiveParts(false);
    }

    private void SetActiveParts(bool active = true)
    {
        Front.gameObject.SetActive(active);
        Text.gameObject.SetActive(active);
    }

    private void SetAmount(int amount, int max)
    {
        Text.text = $"{amount}/{max}";
    }
    
    #endregion

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
