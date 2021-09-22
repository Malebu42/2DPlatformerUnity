using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin_script : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {
        ScoreTextScript.coinAmount += 1;
        Destroy (gameObject);
    }

 
}
