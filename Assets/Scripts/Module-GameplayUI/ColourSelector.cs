using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Agate.MVC.Core;

namespace TankU.GameplayUI
{
    public class ColourSelector : MonoBehaviour
    {
        public Color MainColour, SubColour;
        //public List<Color> MainColour = new List<Color>();
        //public List<Color> SubColour = new List<Color>();
        public Button PlayButton;
        [HideInInspector]
        public int ColorIndex;
        private Button[] ColorButton;
        private bool IsMainSelected, isSubSelected;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (IsMainSelected && isSubSelected)
            {
                PlayButton.gameObject.SetActive(true);
            }
        }
        public void MainColor(Image MainImg)
        {
            MainColour = MainImg.color;
            PublishSubscribe.Instance.Publish<PubSub.ColourIn>(new PubSub.ColourIn());
            IsMainSelected = true;
        }
        public void SubColor(Image SubImg)
        {
            SubColour = SubImg.color;
            PublishSubscribe.Instance.Publish<PubSub.ColourIn>(new PubSub.ColourIn());
            isSubSelected = true;
        }
    }
}

