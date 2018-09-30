using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour {

    public Text baseHPDisplay;
    public Slider baseHPSlider;
    public static MainPanel Instance;

	// Use this for initialization
	void Start () {
        Instance = this;
	}

    public void SetBaseHPDisplay(int current, int max)
    {
        baseHPDisplay.text = current.ToString() + " / " + max.ToString();
        baseHPSlider.value = ((float)current / (float)max); 
    }
}
