using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour {
    const float NUDGE_FORCE = 50;

    [SerializeField] GameObject drumstickPrefab;
    [SerializeField] GameObject poofPrefab;

    Rigidbody2D body;

    void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        // body.MovePosition(body.position + rollDirection * rollSpeed * Time.deltaTime);
    }

    void Update() {

    }

    void Die(Vector2 direction) {
        Vector3 position = transform.position;
        GameObject poof = Instantiate(poofPrefab, position, Quaternion.identity);
        poof.GetComponent<SimpleAnimation>().PlayOnce(() => {
            GameObject drumstick = Instantiate(drumstickPrefab, position, Quaternion.identity);
            drumstick.GetComponent<Rigidbody2D>().velocity = direction * NUDGE_FORCE;
            GameManager.Instance.SpawnChicken();
        });
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Weapon") {
            Die(-col.gameObject.GetComponent<AxeThrow>().GetDirection());
        }
    }
}
