using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private bool maxScoreReached = false;
    private float radius;
    private Vector2 direction;
    public Vector3 startPosition;

    private Score_board scoreboard;
    private bool isResetting = false;

    private void Start()
    {
        startPosition = transform.position;
        direction = Vector2.one.normalized;
        radius = transform.localScale.x / 2;

        scoreboard = GameObject.FindObjectOfType<Score_board>();
        if (scoreboard == null)
            Debug.LogError("Score_board not found in scene!");
    }

    private void Update()
    {
        if (!isResetting)
            transform.Translate(direction * speed * Time.deltaTime);

        // Bounce off top and bottom
        if (transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0)
            direction.y = -direction.y;
        if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0)
            direction.y = -direction.y;

        // Right player scores
        if (!isResetting && transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0 && !maxScoreReached)
            ScorePoint(false);

        // Left player scores
        if (!isResetting && transform.position.x > GameManager.topRight.x - radius && direction.x > 0 && !maxScoreReached)
            ScorePoint(true);
    }

    private void ScorePoint(bool leftPlayer)
    {
        if (scoreboard == null) return;

        if (leftPlayer)
            scoreboard.AddLeftScore();
        else
            scoreboard.AddRightScore();

        if ((leftPlayer && scoreboard.left_player < scoreboard.maxscore) ||
            (!leftPlayer && scoreboard.right_player < scoreboard.maxscore))
        {
            StartCoroutine(Reset());
        }
        else
        {
            maxScoreReached = true;
        }
    }

    // Handles both triggers and collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandlePaddleCollision(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandlePaddleCollision(collision.gameObject);
    }

    private void HandlePaddleCollision(GameObject paddleObj)
    {
        if (paddleObj.CompareTag("PaddleLeft") || paddleObj.CompareTag("PaddleRight"))
        {
            Paddle paddle = paddleObj.GetComponent<Paddle>();
            if (paddle == null)
            {
                Debug.LogWarning("Paddle script not found on " + paddleObj.name);
                return;
            }

            bool isRight = paddle.isRight;

            // Reflect the ball
            if ((isRight && direction.x > 0) || (!isRight && direction.x < 0))
                direction.x = -direction.x;
        }
    }

    private IEnumerator Reset()
    {
        isResetting = true;

        yield return new WaitForSeconds(1f);

        transform.position = startPosition;
        direction = Vector2.one.normalized;

        isResetting = false;
    }

    public void RestartPosition()
    {
        transform.position = startPosition;
        direction = Vector2.one.normalized;
    }

    public void ResetScoreFlag()
    {
        maxScoreReached = false;
    }
}
