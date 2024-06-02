using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Map
    {
        public String Name { get; set; }
        public Sprite Image { get; set; }

        public Map(string name, Sprite image)
        {
            Name = name;
            Image = image;
        }
    }
}
