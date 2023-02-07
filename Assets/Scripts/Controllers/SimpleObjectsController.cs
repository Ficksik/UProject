using System.Collections.Generic;
using Model.States;

namespace Controllers
{
    public class SimpleObjectsController : ObjectListController<SimpleObjectState>
    {
        public SimpleObjectsController(List<SimpleObjectState> source) : base(source)
        {
        }

        public void UpdateVisual(int selectedObject, int visualId)
        {
            var state = GetObjectState(selectedObject);
            state.VisualId = visualId;
            Update(state); // local state update

            // rpc server call for example
            // UpdateVisualAsync();
        }
    }
}