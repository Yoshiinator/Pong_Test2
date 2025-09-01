using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    public Paddle paddlePrefab;
    public Ball ballPrefab;

    [Header("Game Area")]
    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    [Header("UI")]
    public Score_board scoreboard;

    private void Start()
    {
        // Get screen bounds
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // Instantiate the Ball
        Ball ballInstance = Instantiate(ballPrefab);

        // Instantiate Left Paddle
        Paddle leftPaddle = Instantiate(paddlePrefab);
        leftPaddle.name = "PaddleLeft";
        leftPaddle.tag = "PaddleLeft";
        leftPaddle.isAI = false;
        leftPaddle.Init(false);

        // Instantiate Right Paddle
        Paddle rightPaddle = Instantiate(paddlePrefab);
        rightPaddle.name = "PaddleRight";
        rightPaddle.tag = "PaddleRight";
        rightPaddle.isAI = true;
        rightPaddle.Init(true);
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void Resetgame()
    {
        Ball ball = Object.FindAnyObjectByType<Ball>();
        ball.RestartPosition();
        ball.ResetScoreFlag();

        Paddle leftpadle = GameObject.FindWithTag("PaddleLeft").GetComponent<Paddle>();
        leftpadle.Reset();

        Paddle rightpadle = GameObject.FindWithTag("PaddleRight").GetComponent<Paddle>();
        rightpadle.Reset();

        if (scoreboard != null)
        {
            scoreboard.Reset();
        }
        else
        {
            Debug.LogError("Scoreboard not assigned in GameManager!");
        }

        Time.timeScale = 1f;
    }
}
