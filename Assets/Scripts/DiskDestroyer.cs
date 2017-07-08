using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Disk")
        {
            GameSession.Instance.diskManager.RemoveDisk(collision.GetComponent<Disk>());
        }
    }
}
