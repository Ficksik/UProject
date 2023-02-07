using System.Collections.Generic;
using Controllers;
using Model.States;

namespace Model
{
    public class GameState
    {
        public SimpleObjectsController SimpleObjects;
        
        //when creating a GameState, pass the player's state
        //from the server to it and create controllers for them
        public GameState()
        {
            //example
            SimpleObjects = new SimpleObjectsController(new List<SimpleObjectState>());
            //create test state
            SimpleObjects.UpdateVisual(1,0);
        }
    }
}