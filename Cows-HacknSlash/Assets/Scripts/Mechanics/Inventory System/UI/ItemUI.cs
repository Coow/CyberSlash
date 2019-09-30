using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    #region Fields

    private IInventoryItem _item;
    
    /// <summary>
    /// The GameObject containing the image of the item
    /// </summary>
    public Image Image;
    
    #endregion

    #region Properties

    /// <summary>
    /// The item object
    /// </summary>
    public IInventoryItem Item => _item;

    #endregion

    #region Private

    /// <summary>
    /// Reacts to changes with item properties
    /// </summary>
    /// <param name="sender">The sender of the event</param>
    /// <param name="e">The event data</param>
    private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(InventoryItem.Image))
        {
            SetImage();
        }
    }

    /// <summary>
    /// Changes the image based on item data
    /// </summary>
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

    /// <summary>
    /// Sets up the display properly based on item data
    /// </summary>
    /// <param name="item">The item to display</param>
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
