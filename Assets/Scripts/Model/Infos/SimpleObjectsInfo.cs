using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Model.Infos
{
    [Serializable]
    public class SimpleObjectsInfo
    {
        [SerializeField] private List<ColorInfo> _colors;
        public IReadOnlyList<ColorInfo> Colors => _colors;

        [CanBeNull]
        public ColorInfo GetInfo(int visualId)
        {
            for (int i = 0; i < _colors.Count; i++)
            {
                if (_colors[i].Id == visualId)
                {
                    return _colors[i];
                }
            }
            return null;
        }
    }
}