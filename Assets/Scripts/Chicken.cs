using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour {
    const float NUDGE_FORCE = 50;
    const float SWEAT_SPEED_THRESHOLD = 300;

    [SerializeField] GameObject drumstickPrefab;
    [SerializeField] GameObject poofPrefab;
    [SerializeField] ParticleSystem sweatParticles;

    SpriteRenderer spriteRenderer;
    Rigidbody2D body;
    Vector2 dir;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dir = Vector2.zero;
        sweatParticles.Stop();
    }

    void FixedUpdate() {
        // body.MovePosition(body.position + rollDirection * rollSpeed * Time.deltaTime);
        body.AddForce(dir);
    }

    void Update() {
        dir = moveAwayFromPlayers();
        if (body.velocity.x > 0.1f) spriteRenderer.flipX = false;
        if (body.velocity.x < -0.1f) spriteRenderer.flipX = true;

        if (sweatParticles.isPlaying) {
            if (body.velocity.sqrMagnitude < SWEAT_SPEED_THRESHOLD) {
                sweatParticles.Stop();
            }
        } else {
            if (body.velocity.sqrMagnitude > SWEAT_SPEED_THRESHOLD) {
                sweatParticles.Play();
            }
        }
    }

    Vector2 moveAwayFromPlayers() {
        List<GameObject> players = GameManager.Instance.GetPlayers();
        Vector2 pos = transform.position;
        Vector2 dir = Vector2.zero;

        foreach (GameObject player in players) {
            Vector2 pPos = player.transform.position;
            float dist = Vector2.Distance(pos, pPos);
            if (dist < 80) dir += (pos - pPos) * (6000f / dist / dist);
        }

        return dir;
    }

    void Die(Vector2 direction) {
        Vector3 position = transform.position;
        GameObject poof = Instantiate(poofPrefab, position, Quaternion.identity);
        poof.GetComponent<SimpleAnimation>().PlayOnce(() => {
            GameObject drumstick = Instantiate(drumstickPrefab, position, Quaternion.identity);
            drumstick.GetComponent<Rigidbody2D>().velocity = direction.normalized * NUDGE_FORCE;
            GameManager.Instance.SpawnChicken();
        });
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Weapon")) {
            Die(-col.gameObject.GetComponent<AxeThrow>().GetDirection());
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Explosion")) {
            Die(transform.position - col.transform.position);
        }
    }
}
