using Core;

namespace Model.States
{
    public class SimpleObjectState : IIdentifiable
    {
        private int _id;
        private int _visualId;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int VisualId
        {
            get => _visualId;
            set => _visualId = value;
        }
    }
}