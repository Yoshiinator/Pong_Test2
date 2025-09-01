using UnityEngine;
using TMPro;

public class Score_board : MonoBehaviour
{
    public int left_player = 0;
    public int right_player = 0;
    public int maxscore;

    [SerializeField] private TextMeshProUGUI leftScoreText;
    [SerializeField] private TextMeshProUGUI rightScoreText;

    public GameObject Win;
    public GameObject Lose;
    public GameObject Resultpanel;

    private void Start()
    {
        Resultpanel.SetActive(false);
        Win.SetActive(false);
        Lose.SetActive(false);
        UpdateUI(); // show 0-0 at the start
    }

    public void AddLeftScore()
    {
        left_player++;
        UpdateUI();
    }

    public void AddRightScore()
    {
        right_player++;
        UpdateUI();
    }

    public void Gameover()
    {
        if (left_player == maxscore)
        {
            Time.timeScale = 0f;
            Resultpanel.SetActive(true);
            Win.SetActive(true);
        }
        if (right_player == maxscore)
        {
            Time.timeScale = 0f;
            Resultpanel.SetActive(true);
            Lose.SetActive(true);
        }
    }

    private void UpdateUI()
    {
        if (leftScoreText != null)
            leftScoreText.text = left_player.ToString();
        if (rightScoreText != null)
            rightScoreText.text = right_player.ToString();

        Gameover();
    }

    public void Reset()
    {
        Resultpanel.SetActive(false);
        Win.SetActive(false);
        Lose.SetActive(false);

        left_player = 0;
        right_player = 0;

        UpdateUI();
    }
}
