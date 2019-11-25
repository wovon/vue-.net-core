using System;
using System.Collections.Generic;
using System.Text;
using V.NetCore.Infrastructure;
using V.NetCore.Repository.Domain;

namespace V.NetCore.App.ModelViews
{
    public class ButtonView
    {
        public ButtonView()
        {
            Sort = 1000;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Label { get; set; }

        public string Icon { get; set; }

        public string Explain { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int Sort { get; set; }

        public static implicit operator ButtonView(Button button)
        {
            return button.MapTo<ButtonView>();
        }

        public static implicit operator Button(ButtonView view)
        {
            return view.MapTo<Button>();
        }

        public List<MenuButton> MenuButtons { get; set; }
    }
}
