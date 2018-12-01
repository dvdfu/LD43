using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimation : MonoBehaviour {
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] frames;
    [SerializeField] float frameDuration = 0.1f;
    [SerializeField] bool looping = true;
    [SerializeField] bool destroyOnComplete = false;

    Tween tween;
    bool playing;

    public void PlayOnce(Tween.OnFinish onFinish) {
        playing = true;
        spriteRenderer.enabled = true;
        tween.Start(frameDuration,
            (float x) => {
                int index = Mathf.FloorToInt(x * frames.Length);
                spriteRenderer.sprite = frames[index];
            },
            () => {
                if (destroyOnComplete) {
                    Destroy(gameObject);
                } else {
                    playing = false;
                    onFinish();
                }
            }
        );
    }

    public void PlayLoop() {
        playing = true;
        spriteRenderer.enabled = true;
        tween.Start(frameDuration,
            (float x) => {
                int index = Mathf.FloorToInt(x * frames.Length);
                spriteRenderer.sprite = frames[index];
            }, () => {
                if (playing) {
                    PlayLoop();
                }
            }
        );
    }

    public void Stop() {
        tween.Stop();
        playing = false;
    }

    void Awake() {
        tween = new Tween(this);
        if (looping) {
            PlayLoop();
        }
    }
}
