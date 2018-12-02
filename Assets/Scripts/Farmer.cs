using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : Player {
	[SerializeField] GameObject axeThrowPrefab;
    [SerializeField] GameObject axeSwingPrefab;
    [SerializeField] SpriteRenderer axeCarry;
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
        attackCooldownTimer = new Timer(playerData.attackCooldown, true);
		previousDirection = Vector2.right;
        stunTween = new Tween(this);
	}

	void Update() {
        attackCooldownTimer.update(Time.deltaTime);
        if (hasAxe && !isStunned) {
            if (Input.GetKeyDown(playerData.attackBindings.attack)) {
                if (attackCooldownTimer.isDone()) {
                    speed = maxSpeed * playerData.axeThrowSlowdown;
                }
            } else if (Input.GetKeyUp(playerData.attackBindings.attack)) {
                speed = maxSpeed;
                hasAxe = false;
                axeCarry.enabled = false;
                attackCooldownTimer.start();
                GameObject axeThrow = Instantiate(axeThrowPrefab, transform.position, Quaternion.identity);
                axeThrow.GetComponent<AxeThrow>().Throw(playerData.id, previousDirection);
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
            collision.gameObject.GetComponent<AxeThrow>().GetID() != playerData.id) {
                
            stunEffect.SetActive(true);
            stunEffect.GetComponent<SimpleAnimation>().PlayLoop();
            isStunned = true;
            canMove = false;
            stunTween.Start(playerData.stunDuration, (float progress) => {}, () => {
                stunEffect.SetActive(false);
                isStunned = false;
                canMove = true;
            });
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("AxePickup") && !hasAxe) {
            hasAxe = true;
            axeCarry.enabled = true;
            Destroy(other.gameObject);
        } else if (other.gameObject.CompareTag("Drumstick")) {
            Score.instance.CollectPoint(playerData.id);
            Destroy(other.gameObject);
        } 
    }
}
