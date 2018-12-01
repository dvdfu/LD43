using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSwing : MonoBehaviour {
    const float OFFSET = 20;

    public void Swing(Vector3 pivot, Vector2 direction) {
        Vector2 offset = direction.normalized * OFFSET;
        transform.Translate(offset);
        transform.Rotate(Vector3.forward, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        GetComponent<SimpleAnimation>().PlayOnce(() => Destroy(gameObject));
    }
}
