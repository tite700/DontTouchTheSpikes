using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public enum GAME_STATE { menu, play, gameOver };

	[SerializeField] private GameObject startingUI;
	[SerializeField] private int gravityForce;
	[SerializeField] private float jumpForce;
	[SerializeField] private GameObject Wall;
	[SerializeField] private GameObject Spike;
	[SerializeField] private GameObject backgroundScore;
	[SerializeField] private Text maxScoreText;
	[SerializeField] private Text gamesPlayedText;
	
    public GAME_STATE gameState;
    public int score;
    public int maxScore;
    public int gamesPlayed;
    
    private Rigidbody2D rb;
    
    
    private void Awake()
    {
	    rb = GetComponent<Rigidbody2D>();
	    rb.gravityScale = 0;
	    maxScore = PlayerPrefs.GetInt("maxScore", 0);
	    maxScoreText.text = maxScore.ToString();
	    gamesPlayed = PlayerPrefs.GetInt("gamesPlayed", 0);
	    gamesPlayedText.text = gamesPlayed.ToString();
    }

    // Use this for initialization
    void Start()
    {
		gameState = GAME_STATE.menu;
		startingUI.SetActive(true);
		backgroundScore.SetActive(false);
    }
    
    private void JumpRight()
	{
	    rb.velocity = Vector2.up * jumpForce;
	    rb.velocity += Vector2.right * jumpForce/3;
	}

    private void JumpLeft()
    {
	    rb.velocity = Vector2.up * jumpForce;
	    rb.velocity += Vector2.left * jumpForce/3;
    }

    //if it collides with walls, it faces the other way
    private void OnCollisionEnter2D(Collision2D other)
	{
		
	    if (other.gameObject.layer == Wall.layer)
	    {
		    score++;
		    if (score > maxScore)
		    {
			    maxScore = score;
		    }
		    if (transform.rotation.x == 0)
		    {
			    transform.rotation = Quaternion.Euler(0, 180, 0);
			    rb.velocity = Vector2.left * jumpForce / 3;
		    }
		    else
		    {
			    transform.rotation = Quaternion.Euler(0, 0, 0);
			    rb.velocity = Vector2.right * jumpForce / 3;
		    }
	    }

	    if (other.gameObject.layer == Spike.layer)
	    {
		    gameState = GAME_STATE.gameOver;
		    gamesPlayed++;
		    PlayerPrefs.SetInt("maxScore", maxScore);
		    PlayerPrefs.SetInt("gamesPlayed", gamesPlayed);
	    }
	}
    
    // Update is called once per frame
    void Update()
    {
	    
	    if (gameState == GAME_STATE.menu)
	    {
		    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
		    {
			    gameState = GAME_STATE.play;
			    startingUI.SetActive(false);
			    backgroundScore.SetActive(true);
			    rb.gravityScale = gravityForce;
			    JumpRight();

		    }
	    }
	    
	    if (gameState == GAME_STATE.play)
	    {
		    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
		    {
			    if (transform.rotation.x == 0)
			    {
				    JumpRight();
			    }
			    else
			    {
				    JumpLeft();
			    }
		    }
	    }
    }
}
