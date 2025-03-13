using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public List<Powerup> powerups;

    private List<Powerup> removedPowerupQueue;

    // Start is called before the first frame update
    void Start()
    {
        powerups = new List<Powerup>();

        removedPowerupQueue = new List<Powerup>();
    }

    // Update is called once per frame
    void Update()
    {
        DecrementPowerupTimers();
    }

    public void LateUpdate()
    {
        ApplyRemovedPowerupsQueue();
    }

    public void Add (Powerup powerupToAdd)
    {
        powerupToAdd.Apply(this);

        powerups.Add(powerupToAdd);
    }

    public void Remove (Powerup powerupToRemove)
    {
        powerupToRemove.Remove(this);

        removedPowerupQueue.Add(powerupToRemove);
    }

    public void DecrementPowerupTimers()
    {
        foreach (Powerup powerup in powerups)
        {
            if (!powerup.isPermanent)
            {
                powerup.duration -= Time.deltaTime;

                if (powerup.duration <= 0)
                {
                    Remove(powerup);
                }
            }
            
        }
    }

    private void ApplyRemovedPowerupsQueue()
    {
        foreach (Powerup powerup in removedPowerupQueue)
        {
            powerups.Remove(powerup);
        }

        removedPowerupQueue.Clear();
    }
}
