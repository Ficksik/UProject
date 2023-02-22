using System;
using Controllers;
using Core;
using Model;
using Model.Infos;
using Model.States;
using UniRx;
using UnityEngine;

namespace View
{
    public class SimpleObjectView : ManagedMonoBeh, IStartable, IUpdatable
    {
        [SerializeField] private int _id;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private float _speedRotate;
            
        private SimpleObjectsInfo _simpleObjectsInfo;
        private SimpleObjectsController _simpleObjects;
        private Transform _transform;
        private MaterialPropertyBlock _propBlock;
        private static readonly int ColorProp = Shader.PropertyToID("_Color");

        protected override void Initialize(IContainer container)
        {
            _transform = transform;
            
            _propBlock = new MaterialPropertyBlock();
            _meshRenderer.GetPropertyBlock(_propBlock);
            
            var model = container.Resolve<ModelController>();

            _simpleObjectsInfo  = model.Info.SimpleObjectsInfo;
            
            _simpleObjects =model.State.SimpleObjects;
            _simpleObjects.OnObjectChange.Subscribe(StateUpdate).AddTo(LifetimeDisposable);
        }
        public void CoreStart()
        {
            var state = _simpleObjects.GetObjectState(_id);
            StateUpdate(state);
        }
        private void StateUpdate(SimpleObjectState state)
        {
            if (state.Id != _id) return;
            var colorInfo = _simpleObjectsInfo.GetInfo(state.VisualId);
            if (colorInfo == null)
            {
                Debug.LogError("colorInfo not found");
                return;
            }
            
            // Assign our new value.
            _propBlock.SetColor(ColorProp, colorInfo.Color);
            // Apply the edited values to the renderer.
            _meshRenderer.SetPropertyBlock(_propBlock);
        }

        public void CoreUpdate()
        {
            _transform.Rotate(new Vector3(0, _speedRotate, 0) * Time.deltaTime);
        }
    }
}