using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Ui.UiList
{
    public interface IPopulatable<TData>
    {
        TData Data { get; }
        void Populate(TData data);
        void DePopulate();
    }

    public interface IParantable<in TParent>
    {
        void SetParent(TParent parent);
    }

    public class UIList<TCell, TData> : ManagedMonoBeh where TCell : Component, IPopulatable<TData>  
    {

        [SerializeField] private TCell _cellPrefab;
        [SerializeField] private GameObject _group;
        private Transform _root;
        [SerializeField] protected GameObject _emptyListMessage;
        private readonly List<TCell> _items = new ();

        bool _initialCounted;
        int _siblingOffset;

        protected override void Initialize(IContainer c)
        {
            UpdateEmptyListMessagae();
            if (_group == null)
            {
                _root = gameObject.transform;
            }
            else
            {
                _root = _group.transform;
            }
        }

        protected override void Suspend()
        {
            Clear();
        }

        public void AddData(TData data)
        {
            if(!_initialCounted)
            {
                _initialCounted = true;
                _siblingOffset = _root.childCount;
            }
            TCell cell = GetNextCell(data);
            _items.Add(cell);
        
            cell.Populate( data);
        
            ReapplySiblingIndex();
            UpdateEmptyListMessagae();
            OnDataAdd(data, cell);
        }

        //call after AddData
        protected virtual void OnDataAdd(TData data, TCell cell) {}

        public void Remove(TData data)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (data.Equals(_items[i].Data))
                {
                    var cell = _items[i];
                    _items.RemoveAt(i);
                    RecycleCell(cell);
                    i--;
                }
            }
            ReapplySiblingIndex();
            UpdateEmptyListMessagae();
        }

        public void RemoveAtIndex(int index)
        {
            var cell = _items[index];
            _items.RemoveAt(index);
            RecycleCell(cell);
            
            ReapplySiblingIndex();
            UpdateEmptyListMessagae();
        }

        private void ReapplySiblingIndex()
        {
            for(int i=0; i < _items.Count; i++)
            {
                _items[i].transform.SetSiblingIndex(i+ _siblingOffset);
            }
        }
        private void RecycleCell(TCell cell)
        {
            cell.DePopulate();
            Destroy(cell.gameObject);
        }
        public virtual void Clear()
        {
            for(int i=0; i < _items.Count; i++)
            {
                RecycleCell(_items[i]);
            }
            _items.Clear();
            UpdateEmptyListMessagae();
        }

        private TCell GetNextCell(TData data)
        {
            var prefab = GetPrefabOverride(data) ?? _cellPrefab;
        
            // create a prefab instance
            var cell = Instantiate(prefab, _root, false);

            // remove (Clone) part from the name
            cell.name = cell.name.Replace("(Clone)", "");

            // set parent
            //cell.transform.localScale = Vector3.one;
            ProcessCell(cell);
            return cell;
        }

        protected virtual void ProcessCell(TCell cell) {}
        private void UpdateEmptyListMessagae()
        {
            if(_emptyListMessage == null) return;

            if (_items == null || _items.Count == 0 )
            {
                _emptyListMessage.SetActive( true);
            }
            else
            {
                _emptyListMessage.SetActive( false);
            }
        }

        protected virtual TCell GetPrefabOverride(TData data)
        {
            return null;
        }
    }
    
    public class UIListParent<TCell, TData, THolder> : UIList<TCell, TData> where TCell : Component,
        IPopulatable<TData>, IParantable<THolder>
        where THolder :  UIListParent<TCell, TData, THolder>
    {
        protected override void ProcessCell(TCell cell)
        {
            cell.SetParent(this as THolder);
        }
    }
}