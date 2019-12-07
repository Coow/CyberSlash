using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.StatsSystem
{
    public class StatBaseModifierListUI : ListManagerUI
    {
        public ListManagerUI Arguments;

        public override bool Add(IUIBaseObject element)
        {
            var res = base.Add(element);
            if (res)
            {
                ((StatBaseModifierTemplateUI)element).FunctionChanged += StatBaseModifierListUI_FunctionChanged; ;
            }

            return res;
        }

        public override bool Remove(IUIBaseObject element)
        {
            var res = base.Remove(element);
            if (res)
            {
                ((StatBaseModifierTemplateUI)element).FunctionChanged -= StatBaseModifierListUI_FunctionChanged;
            }

            return res;
        }

        private void StatBaseModifierListUI_FunctionChanged(IStatBaseModifier modifier)
        {
            Arguments.gameObject.SetActive(true);
            Arguments.Clear();
            foreach (var arg in modifier.Map)
            {
                Arguments.Add(arg.Value);
            }
        }

        protected override void Element_Selected(object sender)
        {
            base.Element_Selected(sender);
            StatBaseModifierListUI_FunctionChanged(((IUIBaseObject)sender).BaseObject as IStatBaseModifier);
        }
    }
}
