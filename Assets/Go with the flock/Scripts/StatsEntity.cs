using System.Collections;
using UnityEngine;

[System.Serializable]
public struct StatsEntity
{
    public float attack;
    public float additionalAttack;

    public float view;
    public float additionalView;

    public float speed;
    public float additionalSpeed;

    public float maxHealth;
    public float health;
    public float additionalHealth;

    public static StatsEntity operator + (StatsEntity a, StatsEntity b)
    {
        a.additionalAttack += b.additionalAttack;
        a.additionalSpeed += b.additionalSpeed;
        a.additionalView += b.additionalView;
        a.health += b.health;
        a.additionalHealth += b.additionalHealth;
        a.maxHealth += b.maxHealth;
        return a;
    }

    public static StatsEntity operator -(StatsEntity stats)
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