using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour
{

    private TrackTimeHandler timer;
    private bool hitCheckpoint1 = false;
    private bool hitCheckpoint2 = false;
    private bool hitCheckpoint3 = false;

    private void Awake()
    {
        timer = GameObject.FindGameObjectWithTag("HUD").GetComponent<TrackTimeHandler>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Checkpoint-1"))
        {
            hitCheckpoint1 = true;
            if (PlayerPrefs.GetInt("mode") == 1)
            {
                timer.setFastestSector(1);
            }
        }

        if (collision.gameObject.tag.Equals("Checkpoint-2"))
        {
            hitCheckpoint2 = true;
            if (PlayerPrefs.GetInt("mode") == 1)
            {
                timer.setFastestSector(2);
            }
        }

        if (collision.gameObject.tag.Equals("Checkpoint-3"))
        {
            hitCheckpoint3 = true;
            if (PlayerPrefs.GetInt("mode") == 1)
            {
                timer.setFastestSector(3);
            }
        }
    }

    public bool didHitAllCheckpoints()
    {
        if (hitCheckpoint1 && hitCheckpoint2 && hitCheckpoint3)
        {
            return true;
        }

        return false;
    }

    public void resetAllHitCheckpoints()
    {
        hitCheckpoint1 = false;
        hitCheckpoint2 = false;
        hitCheckpoint3 = false;
    }
}
