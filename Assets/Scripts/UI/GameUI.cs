using UnityEngine;

public class GameUI : MonoBehaviour
{
    private static GameUI m_Instance;
    
    public GameObject YourTurnPanel;
    public GameObject StartingGamePanel;
    public GameObject WinPanel;

    public static GameUI instance
    {
        get { return m_Instance; }
    }

    private void Awake()
    {
        m_Instance = this;
    }

    public void ShowYourTurnPanel()
    {
        YourTurnPanel.SetActive(true);
    }

    public void ShowGameStartingPanel()
    {
        StartingGamePanel.SetActive(true);
    }

    public void ShowWinPanel()
    {
        WinPanel.SetActive(true);
    }
}
