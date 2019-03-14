using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UI;
using UnityEngine;

namespace Mechanics.StatsSystem
{
    public class ModifierDependencyUI : UIBaseObject<IModifierDependency>
    {
        [SerializeField]
        string defaultName = @"New modifier";

        public TMP_Text idLabel;
        public TMP_Text nameInput;
        public DropDownUI statsDropdown;

        protected override void Start()
        {
            statsDropdown.ValueChanged += StatsDropdown_ValueChanged;

            base.Start();

        }

        private void StatsDropdown_ValueChanged(object sender, string value)
        {
            Object.Stat = ((DropDownUI)sender).SelectedItem as IStatBase;
        }

        public override void CreateNewObject()
        {
            Object = new ModifierDependency { Name = defaultName };
        }

        protected override void Object_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IModifierDependency.Id):
                    UpdateId(Object.Id.ToString());
                    break;
                case nameof(IModifierDependency.Name):
                    UpdateName(Object.Name);
                    break;
                case nameof(IModifierDependency.Stat):
                    UpdateStat(Object.Stat);
                    break;
                default:
                    UpdateVisuals();
                    break;
            }
        }

        private void UpdateStat(IStatBase stat)
        {
            statsDropdown.ClearOptions();
            statsDropdown.SetOptions(SceneDataManager.instance.GetByType(StatBase.ObjectType), e => e.Name, @"No stat chosen");
            statsDropdown.SelectItem(stat);
        }

        private void UpdateId(string id)
        {
            idLabel.text = id;
        }

        private void UpdateName(string name)
        {
            nameInput.text = name;
        }


        protected override void UpdateVisuals()
        {
            UpdateSelection(IsSelected);
            UpdateStat(Object?.Stat);
            UpdateId(Object?.Id.ToString() ?? Guid.Empty.ToString());
            UpdateName(Object?.Name ?? defaultName);
        }
    }
}
