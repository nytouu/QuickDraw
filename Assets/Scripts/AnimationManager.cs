using UnityEngine;

public class AnimationManager : MonoBehaviour
{
	[Header("Shake")]
	public Shake player1Shake;
	public Shake player2Shake;

	[Header("Sprites")]
	public SpriteRenderer player1Sprite;
	public SpriteRenderer player2Sprite;

	[Header("Dash")]
	public Transform player1DashTarget;
	public Transform player2DashTarget;
	public Dash player1Dash;
	public Dash player2Dash;
	public float dashSpeed;

	public void DashPlayer(Player player){
		switch (player){
			case Player.One:
				StartCoroutine(player1Dash.Play(player1DashTarget, dashSpeed, 0.15f));
				break;
			case Player.Two:
				StartCoroutine(player2Dash.Play(player2DashTarget, -dashSpeed, 0.15f));
				break;
		}
	}

	public void ShakePlayer(Player player){
		switch (player){
			case Player.One:
				StartCoroutine(player1Shake.Play(0.15f, 0.10f));
				break;
			case Player.Two:
				StartCoroutine(player2Shake.Play(0.15f, 0.10f));
				break;
		}
	}
}
