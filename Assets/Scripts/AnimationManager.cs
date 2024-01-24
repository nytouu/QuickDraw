using System.Collections;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
	[Header("Shake")]
	public Shake player1Shake;
	public Shake player2Shake;

	[Header("Sprites")]
	public SpriteRenderer player1Sprite;
	public SpriteRenderer player2Sprite;

	[Header("Player Animators")]
	public Animator player1Animator;
	public Animator player2Animator;

	[Header("Dash")]
	public Transform player1DashTarget;
	public Transform player2DashTarget;
	public Dash player1Dash;
	public Dash player2Dash;
	public float dashSpeed;
	public float stayAfterDash;

	[Header("Win titles")]
	public SpriteRenderer player1Title;
	public SpriteRenderer player2Title;

	public void DashPlayer(Player player){
		switch (player){
			case Player.One:
				player1Animator.Play("Start");
				StartCoroutine(player1Dash.Play(player1DashTarget, dashSpeed, 0.15f, stayAfterDash));
				break;
			case Player.Two:
				player2Animator.Play("Start");
				StartCoroutine(player2Dash.Play(player2DashTarget, -dashSpeed, 0.15f, stayAfterDash));
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

	public void MoveTitle(Player player){
		switch (player){
			case Player.One:
				StartCoroutine(Title(player1Title, player2Title.transform, 1f, 0.2f));
				break;
			case Player.Two:
				StartCoroutine(Title(player2Title, player1Title.transform, 1f, -0.2f));
				break;
		}
	}

	private IEnumerator Title(SpriteRenderer title, Transform target, float duration, float speed){
		Vector3 originalPosition = title.transform.position;
		float elapsed = 0f;

		// immonde
		while (elapsed < duration / 2f){
			float x = title.transform.position.x + speed;

			title.transform.localPosition = new Vector3(x, originalPosition.y, originalPosition.z);

			elapsed += Time.deltaTime;

			yield return null;
		}
		while (elapsed < duration){
			float x = title.transform.position.x + speed / 100f;

			title.transform.localPosition = new Vector3(x, originalPosition.y, originalPosition.z);

			elapsed += Time.deltaTime;

			yield return null;
		}
		while (elapsed < duration + 3f){
			float x = title.transform.position.x + speed;

			title.transform.localPosition = new Vector3(x, originalPosition.y, originalPosition.z);

			elapsed += Time.deltaTime;

			yield return null;
		}
		title.transform.localPosition = originalPosition;
	}
}
