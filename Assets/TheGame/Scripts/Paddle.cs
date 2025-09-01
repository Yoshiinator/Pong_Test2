using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    float height;

    public bool isRight;          // side of paddle
    public bool isAI = false;     // if true, paddle moves automatically
    public Vector3 startPosition;

    private int direction = 1;    //computer movement
    private string input;         //player movement

    private void Start()
    {
        height = transform.localScale.y;
        startPosition = transform.position;

        if (!isAI)
        {
            input = isRight ? "PaddleRight" : "PaddleLeft";
        }
    }

    private void Update()
    {
        if (isAI)
        {
            AutoMove();
        }
        else
        {
            PlayerMove();
        }
    }

    private void AutoMove()
    {
        float move = direction * speed * Time.deltaTime;

        if (transform.position.y < GameManager.bottomLeft.y + height / 2 && direction < 0)
            direction = 1; // move up
        else if (transform.position.y > GameManager.topRight.y - height / 2 && direction > 0)
            direction = -1; // move down

        transform.Translate(move * Vector2.up);
    }

    private void PlayerMove()
    {
        float move = Input.GetAxis(input) * Time.deltaTime * speed;

        // restrict movement to screen bounds
        if (transform.position.y < GameManager.bottomLeft.y + height / 2 && move < 0)
            move = 0;
        if (transform.position.y > GameManager.topRight.y - height / 2 && move > 0)
            move = 0;

        transform.Translate(move * Vector2.up);
    }

    public void Init(bool isRightPaddle)
    {
        isRight = isRightPaddle;
        Vector2 pos = Vector2.zero;

        if (isRightPaddle)
        {
            pos = new Vector2(GameManager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x;
        }
        else
        {
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x;
        }

        transform.position = pos;
        startPosition = pos;
    }

    public void Reset()
    {
        Vector2 pos = Vector2.zero;
        pos = startPosition;
    }
}
