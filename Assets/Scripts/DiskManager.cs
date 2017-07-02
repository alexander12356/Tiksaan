using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskManager : MonoBehaviour
{
    [SerializeField]
    private List<Disk> m_Team1;

    [SerializeField]
    private List<Disk> m_Team2;

    public void SetLocalPlayerTeam(byte teamId)
    {
        switch (teamId)
        {
            case 1:
                SetColor(m_Team1);
                break;
            case 2:
                SetColor(m_Team2);
                break;
        }
    }

    private void SetColor(List<Disk> playerTeam)
    {
        for (int i = 0; i < playerTeam.Count; i++)
        {
            playerTeam[i].GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
}
