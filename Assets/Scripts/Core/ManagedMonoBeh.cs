using System;
using Extensions;
using UnityEngine;

namespace Core
{
    // inherit from it to get the container and everything in it
    public abstract class ManagedMonoBeh : MonoBehaviour, IDisposable, IEnabled
    {
        // subscriptions for the life of the object until it is either destroy or dispose
        private CompositeDisposableNonAlloc _lifetimeDisposable;
        private GameObject _gameObject;
        public bool Enabled
        {
            get
            {
                _gameObject ??= gameObject;
                return _gameObject.activeSelf;
            }
        }

        protected CompositeDisposableNonAlloc LifetimeDisposable =>
            _lifetimeDisposable ??= new CompositeDisposableNonAlloc();

        public void StartInitialize(IContainer container)
        {
            Initialize(container);
        }

        protected abstract void Initialize(IContainer container);

        private void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            _lifetimeDisposable?.Dispose();
            Suspend();
        }

        protected virtual void Suspend(){}
    }
}