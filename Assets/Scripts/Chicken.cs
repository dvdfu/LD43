using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour {
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

    void Die() {
        Vector3 position = transform.position;
        GameObject poof = Instantiate(poofPrefab, position, Quaternion.identity);
        poof.GetComponent<SimpleAnimation>().PlayOnce(() => {
            Instantiate(drumstickPrefab, position, Quaternion.identity);
            GameManager.Instance.SpawnChicken();
        });
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Weapon") Die();
    }
}
