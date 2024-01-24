using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Player {
	One,
	Two
}

public class GameManager : MonoBehaviour
{
	const int PLAYER_COUNT = 2;

	private List<bool> attacking;
	private List<float> penalty;
	private List<float> delay;

	[Header("Timing")]
	public float cooldown;
	public float minimumTime;
	public float restartDelay;
	private float timing;

	private bool canClick;
	private bool winner;

	[Header("Audio")]
	public AudioClip yo;
	public AudioClip signal;
	public AudioClip slash;
	public AudioClip whoosh;
	private AudioSource source;

	[Header("Sprites")]
	public List<SpriteRenderer> playerSprites;

	private ScoreManager score;
	private AnimationManager animations;

	[Header("Timing Text")]
	public TextMeshProUGUI textPlayer1;
	public TextMeshProUGUI textPlayer2;

    void Start()
    {
		source = GetComponent<AudioSource>();
		score = GetComponent<ScoreManager>();
		animations = GetComponent<AnimationManager>();

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

				source.clip = whoosh;
				source.Play();

				Debug.Log("Player " + player + " clicked too early !");
			}

			// apply cooldown on affected players
			if (penalty[player] > 0f){
				penalty[player] -= Time.deltaTime;

				if (penalty[player] < 0f){
					penalty[player] = 0f;
					playerSprites[player].color = Color.white;
				}

				Debug.Log("Cooldown remaining for player " + player + ": " + penalty[player]);
			}

			// valid player attack
			if (attacking[player] && canClick && penalty[player] == 0f){
				Debug.Log("Player " + player + " attacked !");
				delay[player] = Time.time - timing;
				winner = true;
			}
		}

		// check winner
		if (winner && (delay[0] != float.MaxValue || delay[1] != float.MaxValue) && source.clip != slash){
			winner = false;

			if (delay[0] == delay[1] && delay[0] != 0f){
				// tie
				Debug.LogError("Tied game");

				source.clip = whoosh;
				source.Play();
			} else {
				// someone wins
				if (delay[0] < delay[1]){
					score.IncreaseScore(Player.One);
					animations.DashPlayer(Player.One);
					animations.ShakePlayer(Player.Two);
				} else {
					score.IncreaseScore(Player.Two);
					animations.DashPlayer(Player.Two);
					animations.ShakePlayer(Player.One);
				}

				source.clip = slash;
				source.Play();
			}

			StartCoroutine(UpdateText());
			StartCoroutine(RestartGame());
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
		winner = false;

		Reset(penalty, 0f);
		Reset(delay, float.MaxValue);
		Reset(attacking, false);
	}

	private void Reset<T>(List<T> list, T item){
		for (int player = 0; player < PLAYER_COUNT; player++){
			list.Insert(player, item);
		}
	}

	private IEnumerator RestartGame(){
		yield return new WaitForSeconds(restartDelay);
		InitializeGame();
	}

	private IEnumerator UpdateText(){
		for (int player = 0; player < PLAYER_COUNT; player++){
			if (delay[player] == float.MaxValue){
				delay[player] = 0f;
			} 
		}
		yield return new WaitForSeconds(0.5f);
		textPlayer1.text = ((int)(delay[0] * 100f)).ToString();
		textPlayer2.text = ((int)(delay[1] * 100f)).ToString();

		yield return new WaitForSeconds(1.5f);
		textPlayer1.text = ""; 
		textPlayer2.text = ""; 
	}
}
