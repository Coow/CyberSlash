using System;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanics.FunctionsSystem
{
    public class FunctionBaseTemplateUI : UIBaseObject<IFunctionBase>
    {
        [SerializeField]
        Color expressionErrorColor = new Color(1, 0, 0, 0.25f);
        [SerializeField]
        Color expressionNormalColor = new Color(0, 0, 0, 0);
        [SerializeField]
        string defaultName = @"New function";
        [SerializeField]
        string defaultExpression = @"a + b";

        public Image expressionBackground;
        public TMP_Text idLabel;
        public EditableLabelUi nameInput;
        public EditableLabelUi expressionInput;

        protected override void Start()
        {
            base.Start();
            nameInput.ValueChanged += NameInput_ValueChanged;
            nameInput.FocusGained += Input_FocusGained;
            expressionInput.ValueChanged += ExpressionInput_ValueChanged;
            expressionInput.FocusGained += Input_FocusGained;
        }

        private void Input_FocusGained(EditableLabelUi sender)
        {
            IsSelected = true;
        }
        
        public override void CreateNewObject()
        {
            Object = new FunctionBase { Name = defaultName, Expression = defaultExpression };
        }

        private void ExpressionInput_ValueChanged(string newValue)
        {
            Object.Expression = newValue;
        }

        private void NameInput_ValueChanged(string newValue)
        {
            Object.Name = newValue;
        }

        protected override void Object_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IFunctionBase.Id):
                    UpdateId(Object.Id.ToString());
                    break;
                case nameof(IFunctionBase.Name):
                    UpdateName(Object.Name);
                    break;
                case nameof(IFunctionBase.Expression):
                    UpdateExpression(Object.Expression);
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

        private void UpdateExpression(string expression)
        {
            var res = FunctionTools.CheckSyntax(Object);
            if (res.Result)
            {
                expressionInput.Value = expression;
            }
            else
            {
                Debug.Log($"Syntax error found for function {Object.Name}({Object.Id}) : {res.ErrorMessage}");
            }

            expressionBackground.color = res.Result ? expressionNormalColor : expressionErrorColor;
        }

        protected override void UpdateVisuals()
        {
            UpdateSelection(IsSelected);
            UpdateId(Object?.Id.ToString() ?? Guid.Empty.ToString());
            UpdateName(Object?.Name ?? defaultName);
            UpdateExpression(Object?.Expression ?? defaultExpression);
        }
    }
}
