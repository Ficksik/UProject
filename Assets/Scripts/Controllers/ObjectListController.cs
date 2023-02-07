using System;
using System.Collections.Generic;
using Core;
using UniRx;

namespace Controllers
{
    public abstract class ObjectListController<T> where T :IIdentifiable, new ()
    {
        private Subject<T> _onObjectChange;
        private readonly List<T> _list;
        
        public IObservable<T> OnObjectChange => _onObjectChange ??= new Subject<T>();
        
        //set saved states
        public ObjectListController(List<T> source)
        {
            _list = source;
        }
        
        //get the state, if it doesn't exist, then create it
        public T GetObjectState(int  objId)
        {
            foreach (var conv in _list)
            {
                if (conv.Id == objId)
                {
                    return conv;
                }
            }
            var t = new T(){Id = objId};
            return t;
        }

        private bool IsEquals(T one, T two)
        {
            return one.Id == two.Id;
        }

        // update state, if it doesn't exist, then add it
        protected void Update(T obj)
        {
            for(var i=0; i <  _list.Count; i++)
            {
                if (IsEquals(_list[i], obj))
                {
                    _list[i] = obj;
                    _onObjectChange?.OnNext(obj);
                    return;
                }
            }
            
            _list.Add(obj);
            _onObjectChange?.OnNext(obj);
        }
    }
}