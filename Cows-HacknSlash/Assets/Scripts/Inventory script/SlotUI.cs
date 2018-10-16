using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public ItemUI Front;
    public Image Background;
    public TextMeshProUGUI Text;
    public InventoryUI Inventory;

    [SerializeField]
    private InventorySlot _slot;

    public InventorySlot Slot
    {
        get { return _slot; }
    }

    private void Awake()
    {
        Inventory = transform.parent.GetComponent<InventoryUI>();
    }

    public void Set(InventorySlot slot)
    {
        if (_slot != null)
        {
            _slot.PropertyChanged -= Slot_PropertyChanged;
        }
        _slot = slot;
        _slot.PropertyChanged += Slot_PropertyChanged;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        SetActiveParts(Slot.Id >= 0);
        Front.SetItem(_slot.Item);
        SetAmount();

    }

    private void Slot_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        UpdateVisuals();
    }

    public void Unset()
    {
        SetActiveParts(false);
    }

    private void SetActiveParts(bool active = true)
    {
        Front.gameObject.SetActive(active);
        Text.gameObject.SetActive(active);
    }

    public void SetAmount()
    {
        Text.text = $"{_slot.Amount}/{_slot.Item.MaxStack}";
    }
}
