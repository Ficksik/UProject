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

        protected override void Initialize(IContainer container)
        {
            _transform = transform;
            
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
            _meshRenderer.sharedMaterial.color = colorInfo.Color;
        }

        public void CoreUpdate()
        {
            _transform.Rotate(new Vector3(0, _speedRotate, 0) * Time.deltaTime);
        }
    }
}