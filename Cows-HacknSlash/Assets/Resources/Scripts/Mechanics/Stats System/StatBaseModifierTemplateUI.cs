using Mechanics.FunctionsSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanics.StatsSystem
{
    public delegate void FunctionChangedEvent(IStatBaseModifier modifier);

    public class StatBaseModifierTemplateUI : UIBaseObject<IStatBaseModifier>
    {
        [SerializeField]
        Color functionErrorColor = new Color(1, 0, 0, 1);
        [SerializeField]
        Color functionNormalColor = new Color(1, 1, 1, 1);
        [SerializeField]
        string defaultName = @"New modifier";

        public Image functionBackground;
        public TMP_Text idLabel;
        public EditableLabelUi nameInput;
        public DropDownUI functionsDropdown;

        public event FunctionChangedEvent FunctionChanged;

        protected override void Start()
        {
            base.Start();
            nameInput.ValueChanged += NameInput_ValueChanged;
            nameInput.FocusGained += Input_FocusGained;

            functionsDropdown.ValueChanged += FunctionsDropdown_ValueChanged;
        }

        private void FunctionsDropdown_ValueChanged(object sender, string value)
        {
            Object.Function = ((DropDownUI)sender).SelectedItem as IFunctionBase;
            FunctionChanged?.Invoke(Object);
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
            Object = new StatBaseModifier { Name = defaultName };
        }

        protected override void Object_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IStatBaseModifier.Id):
                    UpdateId(Object.Id.ToString());
                    break;
                case nameof(IStatBaseModifier.Name):
                    UpdateName(Object.Name);
                    break;
                case nameof(IStatBaseModifier.Function):
                    UpdateFunction(Object.Function);
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

        private void UpdateArguments()
        {
        }

        private void UpdateFunction(IFunctionBase function)
        {
            functionsDropdown.ClearOptions();
            functionsDropdown.SetOptions(SceneDataManager.instance.GetByType(FunctionBase.ObjectType), e => $"{e.Name} ({((IFunctionBase)e).Expression})", @"No functions selected");
            functionsDropdown.SelectItem(function);
            functionBackground.color = function == null ? functionErrorColor : functionNormalColor;
        }

        private void UpdateName(string name)
        {
            nameInput.Value = name;
        }

        protected override void UpdateVisuals()
        {
            UpdateSelection(IsSelected);
            UpdateFunction(Object?.Function);
            UpdateId(Object?.Id.ToString() ?? Guid.Empty.ToString());
            UpdateName(Object?.Name ?? defaultName);
        }
    }
}
