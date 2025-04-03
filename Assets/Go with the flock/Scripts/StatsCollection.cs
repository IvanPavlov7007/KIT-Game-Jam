using System.Collections;
using UnityEngine;

[System.Serializable]
public struct StatsCollection
{
    public int attack;
    public int additionalAttack;

    public int view;
    public int additionalView;

    public int speed;
    public int additionalSpeed;

    public int maxHealth;
    public int health;
    public int additionalHealth;

    public static StatsCollection operator + (StatsCollection a, StatsCollection b)
    {
        a.additionalAttack += b.additionalAttack;
        a.additionalSpeed += b.additionalSpeed;
        a.additionalView += b.additionalView;
        a.health += b.health;
        a.additionalHealth += b.additionalHealth;
        a.maxHealth += b.maxHealth;
        return a;
    }

    public static StatsCollection operator -(StatsCollection stats)
    {
        stats.additionalAttack = -stats.additionalAttack;
        stats.additionalSpeed -= stats.additionalSpeed;
        stats.additionalView -= stats.additionalView;
        stats.health -= stats.health;
        stats.additionalHealth -= stats.additionalHealth;
        stats.maxHealth -= stats.maxHealth;
        return stats;
    }
}