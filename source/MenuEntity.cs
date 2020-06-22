using System;
using System.Collections.Generic;

namespace Tergie.source
{
    public class MenuEntity: Entity
    {
        public event ItemSelectedCallback ItemSelected;
        public delegate void ItemSelectedCallback(MenuEntity sender, string item);
        
        public MenuEntity(): base()
        {
            KeyPressed += (sender, info) =>
            {
                if (info.Key == ConsoleKey.DownArrow)
                {
                    _selectedItem = (_selectedItem + 1) % _items.Count;
                    Draw();
                }
                else if (info.Key == ConsoleKey.UpArrow)
                {
                    _selectedItem = _selectedItem - 1;
                    if (_selectedItem < 0)
                        _selectedItem = _items.Count - 1;
                    Draw();
                }
                else if (info.Key == ConsoleKey.Enter)
                    ItemSelected.Invoke(this,_items[_selectedItem]);
            };
        }

        public void AddItem(string item)
        {
            _items.Add(item);
            Draw();
        }

        public void RemoveItem(string item)
        {
            _items.Remove(item);
            Draw();
        }
        
        // private stuff
        private List<string> _items = new List<string>();
        private int _selectedItem = 0;

        private void Draw()
        {
            int height = _items.Count;
            int width = 0;
            foreach (var item in _items)
                if (item.Length > width)
                    width = item.Length;
            width += 6;
            height *= 2;
            Characters = new char[height,width];
            
            Utils.SetChar(Characters,Utils.TransparentChar); // clear
            for (int i = 0; i < _items.Count; i++)
            {
                Utils.Blit(_items[i], Characters, new Vector2I(6, i*2), false);
            }
            if (_items.Count > 0)
                Utils.Blit("====>",Characters,new Vector2I(0,_selectedItem*2),false );
        }
    }
}