using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
	public CameraShake shake;

	public SpriteRenderer player1Sprite;
	public SpriteRenderer player2Sprite;

	public Transform player1DashTarget;
	public Transform player2DashTarget;

	public float dashDuration;

	public void CameraShake(){
		StartCoroutine(shake.Shake(0.15f, 0.1f));
	}

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

	private IEnumerator Dash(Vector3 target){
		Vector3 originalPosition = transform.position;
		float elapsed = 0f;
		float startTime = Time.time;

		while (elapsed < dashDuration){
			float x = Mathf.Lerp(originalPosition.x, target.x, startTime + (elapsed / dashDuration));
			float y = Mathf.Lerp(originalPosition.y, target.y, startTime + (elapsed / dashDuration));

			transform.localPosition = new Vector3(x, y, originalPosition.z);

			elapsed += Time.deltaTime;
			yield return null;
		}

		yield return new WaitForSeconds(0.5f);
		transform.localPosition = originalPosition;
	}
}
