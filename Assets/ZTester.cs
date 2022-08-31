using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZTester : MonoBehaviour
{
    private void Start()
    {
        //PlayerPrefsImprove.SetStructObject(new ModelData("name",10));
    }
    struct ModelData
    {
        public string name;
        public int score;

        public ModelData(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }
}
