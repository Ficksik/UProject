using Core;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

namespace Extensions
{
    public static class SceneExtensions
    {
        [CanBeNull]
        public static Container ResolveContainer(this Scene scene)
        {
            const string containerName = "Container";
            var objs = scene.GetRootGameObjects();
            for (var i = 0; i < objs.Length; i++)
            {
                var go = objs[i];
                if (go.name == containerName)
                {
                    return go.GetComponent<Container>();
                }
            }

            return null;
        }
    }
}