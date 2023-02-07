using System;
using UnityEngine;

namespace Model.Infos
{
    [Serializable]
    public class ColorInfo
    {
        [SerializeField]  private int _id;
        [SerializeField] private Color _color;
        [SerializeField] private string _name;
        
        public int Id => _id;
        public Color Color => _color;
        public string Name => _name; 
    }
}