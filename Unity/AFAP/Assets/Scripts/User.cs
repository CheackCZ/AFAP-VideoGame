using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class User
    {
        public string Username { get; set; }
        public DateTime Log { get; set; }
        public string TimeLapse { get; set; }
        public Formule SelectedFormule { get; set; }
        public Map SelectedMap { get; set; }

        public User(string username, DateTime log, string timeLapse,
                    Sprite selectedCarImage, string selectedCarName,
                    Sprite selectedMapImage, string selectedMapName)
        {
            Username = username;
            Log = log;
            TimeLapse = timeLapse;
            SelectedFormule = new Formule(selectedCarName, selectedCarImage);
            SelectedMap = new Map(selectedMapName, selectedMapImage);
        }

        public User() {}
    }
}
