using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	const int PLAYER_COUNT = 2;

	private List<bool> attacking;
	private List<float> penalty;
	private List<float> delay;

	public float cooldown;
	private float timing;

    void Start()
    {
		attacking = new List<bool>();
		penalty = new List<float>();
		delay = new List<float>();
		InitializeGame();
    }

    void Update()
    {
		handleClicks();

		for (int player = 0; player < PLAYER_COUNT; player++){
			// check if attacking before correct timing
			if (attacking[player] && Time.time < timing && penalty[player] == 0f){
				attacking[player] = false;
				penalty[player] = cooldown;

				Debug.Log("Player " + player + " clicked too early !");
			}

			// apply cooldown on affected players
			if (penalty[player] > 0f){
				penalty[player] -= Time.deltaTime;

				if (penalty[player] < 0f){
					penalty[player] = 0f;
				}

				Debug.Log("Cooldown remaining for player " + player + ": " + penalty[player]);
			}

			// valid player attack
			if (attacking[player] && Time.time >= timing && penalty[player] == 0f){
				delay[player] = Time.time - timing;
			}

			ResetElements(attacking, false);
		}
    }

	private void handleClicks(){
		if (Input.GetKeyDown(KeyCode.Space)){
			attacking[0] = true;
		}
		if (Input.GetKeyDown(KeyCode.Return)){
			attacking[1] = true;
		}
	}

	private void InitializeGame(){
		// generate click time
		timing = Time.time + 5f + Random.Range(0, 10f);
		Debug.Log("Correct timing :" + timing + "sec");

		ResetElements(penalty, 0f);
		ResetElements(delay, 0f);
		ResetElements(attacking, false);
	}

	private void ResetElements<T>(List<T> list, T item){
		for (int player = 0; player < PLAYER_COUNT; player++){
			list.Insert(player, item);
		}
	}
}
