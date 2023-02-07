using System.Threading.Tasks;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace Extensions
{
    public static class ResourcesExtensions
    {
        public static async Task<T> LoadTaskAsync<T>(string path) where T : Object
        {
            var request = Resources.LoadAsync<T>(path);
            while (!request.isDone)
            {
               await Observable.NextFrame();
            }
            if (request.asset == null)
            {
                Debug.LogError("'" + path + "' load failed");
                return null;
            }

            return (T)request.asset;
        }
    }
}