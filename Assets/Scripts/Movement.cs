using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	[SerializeField] float speed = 100.0f;
	[SerializeField] KeyCode left;
	[SerializeField] KeyCode right;
	[SerializeField] KeyCode up;
	[SerializeField] KeyCode down;
	
	Rigidbody2D body;

	void Start () {
		body = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
		Vector2 move = Vector2.zero;
		if (Input.GetKey(left)) move.x--;
		if (Input.GetKey(right)) move.x++;
		if (Input.GetKey(up)) move.y++;
		if (Input.GetKey(down)) move.y--;

		move = move.normalized * Time.deltaTime * speed;
		body.MovePosition(body.position + move);
	}
}
