using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PowerUps
{
    NumberOfCanons = 0
}

public class PowerUpManager : MonoBehaviour, IReset
{
    public PowerUpArray[] powerUpLevel;
    private const int MAXPOWERPOINTS = 5;
    private int currentPowerPoints = 0;
    public event Action<int> RecivedPowerPoint;

    public void AddPowerPoint() => currentPowerPoints++;

    public void ResetValues()
    {
        for (int i = 0; i < powerUpLevel.Length; i++)
        {
            powerUpLevel[i].powerUpLevel = 0;
        }
        currentPowerPoints = 0;
    }

    public void PowerUp(int pointsToSpend)
    {
        if (currentPowerPoints >= pointsToSpend)
        {
            if (powerUpLevel[pointsToSpend - 1].powerUpLevel != powerUpLevel[pointsToSpend - 1].maximumLevel)
            {
                powerUpLevel[pointsToSpend - 1].powerUpLevel++;
                currentPowerPoints -= pointsToSpend;
                RecivedPowerPoint?.Invoke(currentPowerPoints);
            }
        }
        Debug.Log(currentPowerPoints);
    }

    public void HasPowerUp(PowerUps powerUp)
    {

    }

    public int GetUpgradedLevel(PowerUps powerUpType)
    {
        return powerUpLevel[(int)powerUpType].powerUpLevel;
    }

    public void IncreasePowerPoints()
    {
        if (currentPowerPoints == MAXPOWERPOINTS)
            return;
        currentPowerPoints++;
        RecivedPowerPoint?.Invoke(currentPowerPoints);
    }

    [Serializable]
    public class PowerUpArray
    {
        public int maximumLevel;
        public int powerUpLevel;
    }
}