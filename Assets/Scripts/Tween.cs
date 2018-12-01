using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween {
    public delegate void OnUpdate(float progress);
    public delegate void OnFinish();

    MonoBehaviour mb;
    Coroutine coroutine;

    public Tween(MonoBehaviour mb) {
        this.mb = mb;
    }

    public void Start(float duration, OnUpdate onUpdate, OnFinish onFinish) {
        Stop();
        mb.StartCoroutine(Animate(duration, onUpdate, onFinish));
    }

    public void Stop() {
        if (coroutine != null) {
            mb.StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    IEnumerator Animate(float duration, OnUpdate onUpdate, OnFinish onFinish) {
        float t = 0;
        while (t < duration) {
            if (onUpdate != null) onUpdate(t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        if (onFinish != null) onFinish();
    }
}