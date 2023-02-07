using Model.Infos;
using TMPro;
using Ui.UiList;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

namespace Ui.UiView.ColorPalette
{
    public class ColorItem : ChildCellBase<ListColors>, IPopulatable<ColorInfo>
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;
        private ColorInfo _data;
        public ColorInfo Data => _data;
        public void Populate(ColorInfo data)
        {
            _data = data; 
            _image.color = data.Color;
            _text.text = data.Name;
        }

        [Preserve]
        public void Click()
        {
            Parent.Click(_data.Id);
        }
        
        public void DePopulate()
        {
        }
    }
}