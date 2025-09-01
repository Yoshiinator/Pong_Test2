using UnityEngine;

public class HelpWindow : MonoBehaviour
{
    public GameObject helpPanel;

    private bool isOpen = false;

    private void Start()
    {
        if (helpPanel != null)
            helpPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleHelp();
        }
    }

    public void ToggleHelp()
    {
        if (helpPanel == null) return;

        isOpen = !isOpen;
        helpPanel.SetActive(isOpen);

        Time.timeScale = isOpen ? 0f : 1f;
    }

    // Optional: button to close help panel
    public void CloseHelp()
    {
        if (helpPanel == null) return;

        isOpen = false;
        helpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
