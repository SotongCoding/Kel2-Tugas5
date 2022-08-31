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
        [SerializeField] private List<Button> mainColorBtn;
        [SerializeField] private List<Button> subColorBtn;
        public Button PlayButton;
        [HideInInspector]
        public int ColorIndex;
        private Button[] ColorButton;
        private bool IsMainSelected, isSubSelected;
        //private int PlayerReady;
        [SerializeField]
        private GameplayUI _GameplayReady;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (IsMainSelected && isSubSelected && _GameplayReady.PlayerReady == 4)
            {
                PlayButton.gameObject.SetActive(true);
                //PlayerReady = +1;
                //Debug.Log("Ready Amount: " + PlayerReady);
                //if (PlayerReady == 2)
                //{
                //    PlayButton.gameObject.SetActive(true);
                //}

            }
        }
        public void MainColor(Image MainImg)
        {
            MainColour = MainImg.color;
            PublishSubscribe.Instance.Publish<PubSub.ColourIn>(new PubSub.ColourIn());
            IsMainSelected = true;
            _GameplayReady.PlayerReady++;
        }
        public void SubColor(Image SubImg)
        {
            SubColour = SubImg.color;
            PublishSubscribe.Instance.Publish<PubSub.ColourIn>(new PubSub.ColourIn());
            isSubSelected = true;
            _GameplayReady.PlayerReady++;
        }

        public void SetColorBtn(int playerWinAmount)
        {
            int unlockAmount = Mathf.RoundToInt(playerWinAmount / 3);
            int maxIndex = Mathf.Clamp(unlockAmount, 1, mainColorBtn.Count);
            for (int i = 1; i <= maxIndex; i++)
            {
                mainColorBtn[i].interactable = true;
                subColorBtn[i].interactable = true;
            }
        }

    }
}

