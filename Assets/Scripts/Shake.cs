using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
	public IEnumerator Play(float duration, float intensity){
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		sprite.color = Color.red;

		Vector3 originalPosition = transform.position;
		float elapsed = 0f;

		while (elapsed < duration){
			float x = originalPosition.x + Random.Range(-1f, 1f) * intensity;
			float y = originalPosition.y + Random.Range(-1f, 1f) * intensity;

			transform.localPosition = new Vector3(x, y, originalPosition.z);

			elapsed += Time.deltaTime;

			yield return null;
		}

		sprite.color = Color.white;
		transform.localPosition = originalPosition;
	}
}
