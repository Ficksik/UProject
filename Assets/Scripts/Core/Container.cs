using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface IContainer
    {
        T Resolve<T>() where T : class,ITypeProvider;
    }

    public abstract class Container : MonoBehaviour, IContainer, IDisposable
    {
        private Dictionary<Type, ITypeProvider> _typeProviders = new();
        private List<IUpdatable> _updatables = new();
        private List<ManagedMonoBeh> _manageMonoBehs = new();
        private List<IStartable> _startables = new();

        public abstract void Init();

        // find the necessary types on the stage and write them down
        protected void BindChildren()
        {
            var go = gameObject;
            Debug.Log("BindChildren in container "+go.scene.name+"/"+go.name);
            var children = GetComponentsInChildren<ITypeProvider>(true);
            for (var i = 0; i < children.Length; i++)
            {
                Bind(children[i]);
            }

            _updatables.AddRange(GetComponentsInChildren<IUpdatable>(true));
            _startables.AddRange(GetComponentsInChildren<IStartable>(true));
        }

        // we initialize objects by passing them a container
        protected void InitializeChildren()
        {
            foreach (var child in GetComponentsInChildren<ManagedMonoBeh>(true))
            {
                _manageMonoBehs.Add(child);
                child.StartInitialize(this);
            }

            InitializeEnd();
        }

        private void InitializeEnd()
        {
            for (int i = 0; i < _startables.Count; i++)
            {
                if (_startables[i] == null) continue;
                _startables[i].CoreStart();
            }
            _startables = null;
        }

        // by calling update only here I manage to call it
        // + this is an indulgence for unity
        // + it's more convenient to profile
        private void Update()
        {
            for (int i = 0; i < _updatables.Count; i++)
            {
                if (_updatables[i] == null) continue;
                if (_updatables[i].Enabled)
                {
                    _updatables[i].CoreUpdate();
                }
            }
        }

        // add object in container
        public void Bind(ITypeProvider typeProvider)
        {
            foreach (var type in typeProvider.GetTypes())
            {
                try
                {
                    _typeProviders.Add(type, typeProvider);
                }
                catch (Exception e)
                {
                    var error = type != null ?  $"Duplicate type {type.Name}\n" : string.Empty;
                    Debug.LogError(error+e);
                }
            }
        }

        // resolve object by type
        public virtual T Resolve<T>() where T : class,ITypeProvider
        {
            return Resolve(typeof(T)) as T;
        }

        private object Resolve(Type t)
        {
            if (!_typeProviders.ContainsKey(t)) return null;
            return _typeProviders[t];
        }
        
        // clear container before change scene
        public void Dispose()
        {
            _updatables.Clear();
            _typeProviders.Clear();
            try
            {
                foreach (var child in _manageMonoBehs)
                {
                    child.Dispose();
                }
            }
            catch (Exception e)
            {
               Debug.LogError(e);
            }
            _manageMonoBehs.Clear();
        }
    }
}