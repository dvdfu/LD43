using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour {
    const float NUDGE_FORCE = 50;

    [SerializeField] GameObject drumstickPrefab;
    [SerializeField] GameObject poofPrefab;

    SpriteRenderer spriteRenderer;
    Rigidbody2D body;
    Vector2 dir;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dir = Vector2.zero;
    }

    void FixedUpdate() {
        // body.MovePosition(body.position + rollDirection * rollSpeed * Time.deltaTime);
        body.AddForce(dir);
    }

    void Update() {
        dir = moveAwayFromPlayers();
        if (body.velocity.x > 0.1f) spriteRenderer.flipX = false;
        if (body.velocity.x < -0.1f) spriteRenderer.flipX = true;
    }

    Vector2 moveAwayFromPlayers() {
        List<GameObject> players = GameManager.Instance.GetPlayers();
        Vector2 pos = transform.position;
        Vector2 dir = Vector2.zero;

        foreach (GameObject player in players) {
            Vector2 pPos = player.transform.position;
            float dist = Vector2.Distance(pos, pPos);
            if (dist < 45) dir += (pos - pPos) * (6000f / dist / dist);
        }

        return dir;
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
