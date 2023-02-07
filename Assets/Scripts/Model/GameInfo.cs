using Model.Infos;
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(fileName = "GameInfo", menuName = "GameInfo")]
    public class GameInfo : ScriptableObject
    {
        public const string Path = "GameInfo";

        [SerializeField]
        private SimpleObjectsInfo _simpleObjectsInfo;
        public SimpleObjectsInfo SimpleObjectsInfo => _simpleObjectsInfo;
    }
}