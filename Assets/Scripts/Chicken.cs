using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour {
    [SerializeField] GameObject drumstickPrefab;

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
        Instantiate(drumstickPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
        GameManager.Instance.SpawnChicken();
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Weapon") Die();
    }
}
