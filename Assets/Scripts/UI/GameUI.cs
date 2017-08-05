using UnityEngine;

public class GameUI : MonoBehaviour
{
    private static GameUI m_Instance;
    
    public GameObject YourTurnPanel;
    public static GameUI instance
    {
        get { return m_Instance; }
    }

    public void Awake()
    {
        m_Instance = this;
    }

    public void ShowYourTurnPanel()
    {
        YourTurnPanel.SetActive(true);
    }
}
