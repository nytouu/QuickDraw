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
	public float minimumTime;
	private float timing;

	private bool canClick;

	private AudioSource source;
	public AudioClip yo;
	public AudioClip signal;

	public List<SpriteRenderer> playerSprites;

    void Start()
    {
		source = GetComponent<AudioSource>();

		attacking = new List<bool>();
		penalty = new List<float>();
		delay = new List<float>();
		InitializeGame();
    }

    void Update()
    {
		handleClicks();
		if (Time.time >= timing && !canClick){
			canClick = true;

			source.clip = signal;
			source.Play();
		}

		for (int player = 0; player < PLAYER_COUNT; player++){
			// check if attacking before correct timing
			if (attacking[player] && !canClick && penalty[player] == 0f){
				attacking[player] = false;
				penalty[player] = cooldown;

				playerSprites[player].color = Color.red;

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
			if (attacking[player] && canClick && penalty[player] == 0f){
				Debug.Log("Player " + player + " attacked !");
				delay[player] = Time.time - timing;
			}

			// check winner
			if (delay[0] != float.MaxValue || delay[1] != float.MaxValue){
				if (delay[0] == delay[1] && delay[0] != 0f){
					Debug.LogError("Tied game");
				}
				if (delay[0] < delay[1]){
					Debug.Log("Player 1 won");
				} else {
					Debug.Log("Player 2 won");
				}
			}
		}

		Reset(attacking, false);
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
		source.clip = yo;
		source.Play();

		// generate click time
		timing = Time.time + minimumTime + Random.Range(0, 10f);
		Debug.Log("Correct timing :" + timing + "sec");

		canClick = false;
		Reset(penalty, 0f);
		Reset(delay, float.MaxValue);
		Reset(attacking, false);
	}

	private void Reset<T>(List<T> list, T item){
		for (int player = 0; player < PLAYER_COUNT; player++){
			list.Insert(player, item);
		}
	}
}
