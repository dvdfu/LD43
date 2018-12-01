using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimation : MonoBehaviour {
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] frames;
    [SerializeField] float frameDuration = 0.1f;
    [SerializeField] bool looping = true;
    [SerializeField] bool autoStart = true;

    Tween tween;

    public void StartAnimation() {
        spriteRenderer.enabled = true;
        tween.Start(frameDuration,
            (float x) => {
                int index = Mathf.FloorToInt(x * frames.Length);
                spriteRenderer.sprite = frames[index];
            }, () => {
                if (looping) {
                    StartAnimation();
                } else {
                    spriteRenderer.enabled = false;
                }
            }
        );
    }

    void Awake() {
        tween = new Tween(this);
    }

    void Start() {
        if (autoStart) {
            StartAnimation();
        } else {
            spriteRenderer.enabled = false;
        }
    }
}
