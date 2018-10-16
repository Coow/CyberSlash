using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public SlotUI Slot;
    public Image Image;
    public TextMeshProUGUI Text;

    [SerializeField]
    private InventoryItem _item;

    private void Awake()
    {
        Slot = transform.parent.GetComponent<SlotUI>();
    }

    public void SetItem(InventoryItem item)
    {
        if (_item != null)
        {
            _item.PropertyChanged -= _item_PropertyChanged;
        }
        _item = item;
        if (item != null)
        {
            _item.PropertyChanged += _item_PropertyChanged;
            SetImage();
        }
    }

    private void _item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        SetImage();
    }

    public void SetImage()
    {
        Image.sprite = Resources.Load<Sprite>(_item.Image);
    }

}
