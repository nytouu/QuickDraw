using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
	[SerializeField] private int player1Score;
	[SerializeField] private int player2Score;

	public TextMeshProUGUI scoreText;

	void Start(){
		player1Score = 0;
		player2Score = 0;
	}

	public void IncreaseScore(Player player){
		switch (player){
			case Player.One: 
				player1Score++;
				break;
			case Player.Two:
				player2Score++;
				break;
		}

		UpdateScoreText();
	}

	private void UpdateScoreText(){
		scoreText.text = "player 1 : " + player1Score + " - " + player2Score + " : player 2";
	}
}
