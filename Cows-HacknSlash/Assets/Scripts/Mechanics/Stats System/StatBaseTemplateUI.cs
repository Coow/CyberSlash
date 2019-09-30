using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Mechanics.StatsSystem
{
    public class StatBaseTemplateUI : UIBaseObject<IStatBase>
    {
        [SerializeField]
        string defaultName = @"New function";

        public TMP_Text idLabel;
        public EditableLabelUi nameInput;

        protected override void Start()
        {
            base.Start();
            nameInput.ValueChanged += NameInput_ValueChanged;
            nameInput.FocusGained += Input_FocusGained;
        }

        private void Input_FocusGained(EditableLabelUi sender)
        {
            IsSelected = true;
        }

        private void NameInput_ValueChanged(string newValue)
        {
            Object.Name = newValue;
        }

        public override void CreateNewObject()
        {
            Object = new StatBase { Name = defaultName };
        }

        protected override void Object_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IStatBase.Id):
                    UpdateId(Object.Id.ToString());
                    break;
                case nameof(IStatBase.Name):
                    UpdateName(Object.Name);
                    break;
                default:
                    UpdateVisuals();
                    break;
            }
        }

        private void UpdateId(string id)
        {
            idLabel.text = id;
        }

        private void UpdateName(string name)
        {
            nameInput.Value = name;
        }

        protected override void UpdateVisuals()
        {
            UpdateSelection(IsSelected);
            UpdateId(Object?.Id.ToString() ?? Guid.Empty.ToString());
            UpdateName(Object?.Name ?? defaultName);
        }
    }
}
