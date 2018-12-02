using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : Player {
    const float STUN_DURATION = 0.3f;

    [SerializeField] Score.PlayerID id;
    [SerializeField] float attackCooldown = 0.5f;
    [SerializeField] float axeThrowSlowdown = 0.5f;

	[SerializeField] KeyCode attackKey = KeyCode.RightShift;
	[SerializeField] GameObject axeThrowPrefab;
    [SerializeField] GameObject axeSwingPrefab;
    [SerializeField] GameObject stunEffect;

    bool hasAxe;
    bool isStunned;
	Vector2 previousDirection;
    Timer attackCooldownTimer;
    Tween stunTween;

	protected override void Awake() {
        base.Awake();
        hasAxe = true;
        isStunned = false;
        attackCooldownTimer = new Timer(attackCooldown, true);
		previousDirection = Vector2.right;
        stunTween = new Tween(this);
	}

	void Update() {
        attackCooldownTimer.update(Time.deltaTime);
        if (hasAxe && !isStunned) {
            if (Input.GetKeyDown(attackKey)) {
                if (attackCooldownTimer.isDone()) {
                    speed = maxSpeed * axeThrowSlowdown;
                }
            } else if (Input.GetKeyUp(attackKey)) {
                speed = maxSpeed;
                hasAxe = false;
                attackCooldownTimer.start();
                GameObject axeThrow = Instantiate(axeThrowPrefab, transform.position, Quaternion.identity);
                axeThrow.GetComponent<AxeThrow>().Throw(id, previousDirection);
                Physics2D.IgnoreCollision(
                    GetComponent<Collider2D>(),
                    axeThrow.GetComponent<Collider2D>()
                );
            }
        }

		Vector2 move = GetMoveVector();
		if (move != Vector2.zero) previousDirection = move;
	}

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Weapon") &&
            !isStunned &&
            collision.gameObject.GetComponent<AxeThrow>().GetID() != id) {
                
            stunEffect.SetActive(true);
            stunEffect.GetComponent<SimpleAnimation>().PlayLoop();
            isStunned = true;
            canMove = false;
            stunTween.Start(STUN_DURATION, (float progress) => {}, () => {
                stunEffect.SetActive(false);
                isStunned = false;
                canMove = true;
            });
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("AxePickup") && !hasAxe) {
            hasAxe = true;
            Destroy(other.gameObject);
        } else if (other.gameObject.CompareTag("Drumstick")) {
            Score.instance.CollectPoint(id);
            Destroy(other.gameObject);
        } 
    }
}
