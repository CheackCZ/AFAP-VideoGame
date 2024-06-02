using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class Formule
    {
        public String Name {  get; set; }
        public Sprite Image {  get; set; }

        public Formule(string name, Sprite image)
        {
            Name = name;
            Image = image;
        }
    }
}
