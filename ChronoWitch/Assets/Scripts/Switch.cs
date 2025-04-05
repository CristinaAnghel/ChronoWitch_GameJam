using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Switch : MonoBehaviour
{
    public Image R;
    public Image L;
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text pov;
    public TMP_Text controls;
    public TMP_Text controls1;
    int index;

    void Start()
    {


    }

    void Update()
    {
        if (index == 1)
        {
            text1.gameObject.SetActive(false);
            text2.gameObject.SetActive(false);
            pov.gameObject.SetActive(false);
            controls.gameObject.SetActive(true);
            controls1.gameObject.SetActive(true);
        }

        if (index == 0)
        {
            text1.gameObject.SetActive(true);
            text2.gameObject.SetActive(true);
            pov.gameObject.SetActive(true);
            controls.gameObject.SetActive(false);
            controls1.gameObject.SetActive(false);
        }
    }

    public void Right()
    {
        index = 1;
        L.gameObject.SetActive(true);
        R.gameObject.SetActive(false);
    }

    public void Left()
    {
        index = 0;
        L.gameObject.SetActive(false);
        R.gameObject.SetActive(true);
    }
}