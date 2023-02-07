using Core;

namespace Ui.UiList
{
    public abstract class CellBase : ManagedMonoBeh
    {
        protected IContainer Container { get; private set; }

        protected override void Initialize(IContainer container)
        {
            Container = container;
        }

        protected override void Suspend()
        {
            Container = null;
        }
    }
    public abstract class ChildCellBase<THolder> : CellBase, IParantable<THolder>
    {
        protected THolder Parent { get; private set; }
        public void SetParent(THolder holder)
        {
            Parent = holder;
        }
    }
}