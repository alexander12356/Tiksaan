using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TurnManager : NetworkBehaviour
{
    public byte currentTurn = 0;

    [Server]
    public void StartGame()
    {
        NextTurn();
    }

    [Server]
    public void NextTurn()
    {
        FindObjectOfType<TeamManager>().playerSessions[currentTurn].RpcStartTurn();

        currentTurn++;

        if (currentTurn > 1)
        {
            currentTurn = 0;
        }
    }
}
