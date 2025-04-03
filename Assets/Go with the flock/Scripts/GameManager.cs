using System.Collections;
using UnityEngine;
using Pixelplacement;
public class GameManager : Singleton<GameManager>
{
    public PlayerController playerController;
    public AnimalMovement firstAnimal;
    public Rect spawnRect = new Rect(-100f, -100f, 200f, 200f);
    void Start()
    {
        playerController.currentTarget = firstAnimal;

        SpawnManager.Instance.SpawnAnimals(spawnRect);
    }

    public void buildFlock(params AnimalAI[] animalAIs)
    {
        var flock = new GameObject("flock").AddComponent<Flock>();
        flock.createFlock(animalAIs);

        Vector3 sumPos = Vector3.zero;

        foreach (var ai in animalAIs)
        {
            if (playersControl(ai))
            {
                playerController.currentTarget = flock;
            }
            sumPos += ai.transform.position;
        }
        flock.assignInitialPosition(sumPos / animalAIs.Length);
    }

    private bool playersControl(AnimalAI ai)
    {
        return playerController.currentTarget == ai.AnimalMovement;
    }

    private bool playersControl(BaseMovement movement)
    {
        return playerController.currentTarget == movement;
    }
}