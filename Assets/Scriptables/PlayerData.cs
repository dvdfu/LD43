using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MovementBindings {
	[SerializeField] public KeyCode left;
	[SerializeField] public KeyCode right;
	[SerializeField] public KeyCode up;
	[SerializeField] public KeyCode down;
}

[System.Serializable]
public struct AttackBindings {
    [SerializeField] public KeyCode attack;
}

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
public class PlayerData : ScriptableObject {
    public Score.PlayerID id;

    [Header("Movement")]
    public MovementBindings movementBindings;

    [Header("Attack")]
    public AttackBindings attackBindings;

    public float attackCooldown = 0.5f;
    public float axeThrowSlowdown = 0.5f;

    public float stunDuration = 0.8f;
}
