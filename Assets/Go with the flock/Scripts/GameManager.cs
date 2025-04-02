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
}