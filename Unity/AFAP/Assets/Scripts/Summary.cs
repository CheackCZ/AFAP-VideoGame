using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Summary : MonoBehaviour
    {
        public TMP_Text textUsername;
        public TMP_Text textMap;
        public TMP_Text textCar;

        public Image imgFormula;
        public Image imgTrack;

        private void Awake()
        {
            textUsername.text = PlayerPrefs.GetString("Username");

            textMap.text = PlayerPrefs.GetString("MapName").ToString();
            textCar.text = PlayerPrefs.GetString("CarName").ToString();

            string imageMap = PlayerPrefs.GetString("MapImage");
            imgTrack = Resources.Load<Image>("imgs/Maps/" + imageMap);


            string imageCar = PlayerPrefs.GetString("CarImage");
            imgFormula = Resources.Load<Image>("imgs/Formulas/" + imageCar);
        }
    }
}
