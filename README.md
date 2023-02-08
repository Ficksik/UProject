I advise you to start watching with `GameFlowController`, since the game starts with it.
[GameFlowController Link](https://github.com/Ficksik/UProject/blob/d25ed8a6721fedcdb8c25deaf1f9a22cfff5399c/Assets/Scripts/Core/GameFlowController.cs)

The project has a `ModelController` that knows to store state and balance information. It is added to the container, which can easily get any `ManagedMonoBeh` object on the stage and get the `ModelController` from it.
The colors are taken from the information and created from perfabs in the UI palette.
After clicking on the color of the user interface, the state of the object changes. View object reacts to state change by changing color

![alt text](https://github.com/Ficksik/UProject/blob/2ea07cc44867092accfc9c55b8bd5ad9e52c5f2a/ExampleView.png?raw=true)
