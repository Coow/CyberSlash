using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace UI
{
    public delegate void ValueChangedEvent(object sender, string value);

    public class DropDownUI : TMP_Dropdown
    {
        private string _defaultNullChoice = @"Please select an item from the list";
        private List<IBaseObject> _options;
        private IBaseObject _selectedItem;
        private Func<IBaseObject, string> _transformer;

        public event ValueChangedEvent ValueChanged;
        public IBaseObject SelectedItem => _selectedItem;

        protected override void Start()
        {
            onValueChanged.AddListener(delegate { ValueChangedHandler(); });
        }

        private void ValueChangedHandler()
        {
            _selectedItem = value == 0 ? null : _options.ElementAt(value - 1);
            ValueChanged?.Invoke(this, options[value].text);
        }

        public void SetOptions(IEnumerable<IBaseObject> options, Func<IBaseObject, string> objectToString, string defaultChoice)
        {
            _options = options.ToList();
            _transformer = objectToString;

            //Add default/null choice
            var o = options.Select(e => _transformer.Invoke(e)).ToList();
            o.Insert(0, string.IsNullOrEmpty(defaultChoice) ? _defaultNullChoice : defaultChoice);


            AddOptions(o);
        }

        public void SelectItem(IBaseObject item)
        {
            int selectionIndex = 0;

            if (item != null)
            {
                selectionIndex = _options.IndexOf(item) + 1;
            }

            value = selectionIndex;
        }
    }
}
