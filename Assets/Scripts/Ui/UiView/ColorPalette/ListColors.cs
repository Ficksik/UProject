using System;
using Model.Infos;
using Ui.UiList;
using UniRx;

namespace Ui.UiView.ColorPalette
{
    public class ListColors : UIListParent<ColorItem,ColorInfo,ListColors>
    {
        private Subject<int> _onClick;
        public IObservable<int> OnClick => _onClick ??= new Subject<int>();

        public void Click(int id)
        {
            _onClick?.OnNext(id);
        }
    }
}