using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    const float ROUND_DURATION_SEC = 60;

    [SerializeField] Text uiTimer;
    
    float timeRemainingSec;

    void Awake() {
        timeRemainingSec = ROUND_DURATION_SEC;
    }

    void FixedUpdate() {
        if (timeRemainingSec > Time.deltaTime) {
            timeRemainingSec -= Time.deltaTime;
        } else {
            timeRemainingSec = 0;
        }

        uiTimer.text = Mathf.CeilToInt(timeRemainingSec).ToString();
    }
}
