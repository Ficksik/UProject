using Core;
using Model;
using UniRx;
using UnityEngine;

namespace Ui.UiView.ColorPalette
{
    public class ColorPalette : ManagedMonoBeh, IStartable
    {
        [SerializeField] private ListColors _listColors;
        private ModelController _model;

        protected override void Initialize(IContainer container)
        {
            _model = container.Resolve<ModelController>();
            _listColors.OnClick.Subscribe(ClickColor).AddTo(LifetimeDisposable);
        }

        private void ClickColor(int id)
        {
            var selectedObject = 1; //testObject
            _model.State.SimpleObjects.UpdateVisual(selectedObject,id);
        }
        public void CoreStart()
        {
            UpdateView();
        }
        private void UpdateView()
        {
            _listColors.Clear();
            var info = _model.Info.SimpleObjectsInfo;
            var colors = info.Colors;
            for (int i = 0; i < colors.Count; i++)
            {
                _listColors.AddData(colors[i]);
            }
        }
    }
}