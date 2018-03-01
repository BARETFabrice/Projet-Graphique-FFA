using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    float timeLeft = 3600;

    // Update is called once per frame
    void FixedUpdate () {
        timeLeft -= Time.deltaTime;
        Debug.Log(timeLeft);
    }
}
