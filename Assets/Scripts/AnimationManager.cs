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

	[Header("Dash")]
	public Transform player1DashTarget;
	public Transform player2DashTarget;
	public float dashSpeed;

	public void DashPlayer(Player player){
		switch (player){
			case Player.One:
				StartCoroutine(Dash(player1DashTarget.position));
				break;
			case Player.Two:
				StartCoroutine(Dash(player2DashTarget.position));
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

	private IEnumerator Dash(Vector3 target){
		Vector3 originalPosition = transform.position;

		while (transform.localPosition.x != target.x){
			float x = transform.localPosition.x + dashSpeed;
			float y = transform.localPosition.y + dashSpeed;

			transform.localPosition = new Vector3(x, y, originalPosition.z);

			yield return null;
		}

		yield return new WaitForSeconds(0.5f);
		transform.localPosition = originalPosition;
	}
}
