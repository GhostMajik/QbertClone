using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class arrowsScript : MonoBehaviour {

    public Image arrow1;
    public Image arrow2;

    public Image arrow3;
    public Image arrow4;

    void Start () {
        arrow1.enabled = true;
        arrow2.enabled = true;
        arrow3.enabled = true;
        arrow4.enabled = true;
        InvokeRepeating("Flash", 0.5f, 1.0f);
        InvokeRepeating("Flash2", 1.0f, 1.0f);
	}


    void Flash()
    {
        arrow1.enabled = !arrow1.enabled;
        arrow2.enabled = !arrow2.enabled;
    }

    void Flash2() {
        arrow3.enabled = !arrow3.enabled;
        arrow4.enabled = !arrow4.enabled;
    }
}
