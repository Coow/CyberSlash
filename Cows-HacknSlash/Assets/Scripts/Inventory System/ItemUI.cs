using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    #region Fields

    private IInventoryItem _item;
    
    public SlotUI Slot;
    public Image Image;
    public TextMeshProUGUI Text;

    #endregion

    #region Properties

    public IInventoryItem Item => _item;

    #endregion

    #region Private

    private void Awake()
    {
        Slot = transform.parent.GetComponent<SlotUI>();
    }

    private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(InventoryItem.Image))
        {
            SetImage();
        }
    }

    private void SetImage()
    {
        if (_item == null || string.IsNullOrEmpty(_item.Image))
        {
            Image.sprite = null;
        }
        else
        {
            Image.sprite = Resources.Load<Sprite>(_item.Image);
        }
    }
    
    #endregion

    public void SetItem(IInventoryItem item)
    {
        if (_item != null)
        {
            _item.PropertyChanged -= Item_PropertyChanged;
        }

        _item = item;
        if (item != null)
        {
            _item.PropertyChanged += Item_PropertyChanged;
        }

        SetImage();
    }
}
