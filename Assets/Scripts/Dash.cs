using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
	public IEnumerator Play(Transform target, float speed, float duration, float stayTime){
		Vector3 originalPosition = transform.position;
		float elapsed = 0f;

		while (elapsed < duration){
			float x = transform.position.x + speed;

			transform.localPosition = new Vector3(x, originalPosition.y, originalPosition.z);

			elapsed += Time.deltaTime;

			yield return null;
		}

		yield return new WaitForSeconds(stayTime);
		transform.localPosition = originalPosition;
	}
}
