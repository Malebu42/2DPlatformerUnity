using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    Text text;

    public static int lifeAmount = 3;
    void Start()
    {
        text = GetComponent<Text> ();

    }

    // Update is called once per frame
    void Update()
    {
        text.text = lifeAmount.ToString();
    }
}
