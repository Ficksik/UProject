using System;
using System.Threading.Tasks;
using Extensions;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GameFlowController
    {
        private ModelController _modelController;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void StartGame()
        {
            Debug.Log("StartGame");
            var controller = new GameFlowController();
            controller.Start();
        }

        private async void Start()
        {
            await Login();
            ActivateScene();
        }

        private async Task Login()
        {
            //TODO: later they will be received in response from the server to the user's login
            var info = await ResourcesExtensions.LoadTaskAsync<GameInfo>(GameInfo.Path);
            var state = new GameState();
            //

            _modelController = new ModelController(info,state);
        }

        private void ActivateScene()
        {
            var currentScene = SceneManager.GetActiveScene();
            var container = currentScene.ResolveContainer();
            if (container == null)
            {
                throw new Exception("Container not found in scene");
            }

            // add the modelController to the container so everyone can get it
            container.Bind(_modelController);
            container.Init();
        }
    }
}