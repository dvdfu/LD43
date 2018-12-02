using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public static Score instance;
    public enum PlayerID { P1, P2 };
    const uint WINNING_SCORE = 30;

    [SerializeField] Text uiPlayer1Score;
    [SerializeField] Text uiPlayer2Score;
    [SerializeField] Text uiWinner;

    uint player1Score;
    uint player2Score;
    bool gameOver;

    public void CollectPoint(PlayerID player) {
        if (gameOver) return;
        switch (player) {
            case PlayerID.P1:
                player1Score++;
                uiPlayer1Score.text = player1Score.ToString();
                if (player1Score == WINNING_SCORE) {
                    DeclareWinner(player);
                }
                break;
            case PlayerID.P2:
                player2Score++;
                uiPlayer2Score.text = player2Score.ToString();
                if (player2Score == WINNING_SCORE) {
                    DeclareWinner(player);
                }
                break;
        }
    }

    public void DeclareWinner(PlayerID player) {
        if (gameOver) return;
        gameOver = true;
        uiWinner.text = player.ToString() + " won the game!";
        uiWinner.gameObject.SetActive(true);
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        player1Score = 0;
        player2Score = 0;
        gameOver = false;
        uiPlayer1Score.text = "0";
        uiPlayer2Score.text = "0";
        uiWinner.gameObject.SetActive(false);
    }
}
