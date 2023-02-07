using System;
using System.Collections.Generic;
using Core;

namespace Model
{
    public class ModelController : ITypeProvider
    {
        public GameInfo Info;
        public GameState State;

        public ModelController(GameInfo info, GameState state)
        {
            Info = info;
            State = state;
        }

        public IEnumerable<Type> GetTypes()
        {
            yield return typeof(ModelController);
        }
    }
}