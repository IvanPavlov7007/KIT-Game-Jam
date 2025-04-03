using System.Collections;
using UnityEngine;
using Pixelplacement;
public class GameManager : Singleton<GameManager>
{
    public PlayerController playerController;
    public AnimalBase firstAnimal;
    public Rect spawnRect = new Rect(-100f, -100f, 200f, 200f);
    void Start()
    {
        playerController.currentTarget = firstAnimal;

        SpawnManager.Instance.SpawnAnimals(spawnRect);
    }

    public Process initNewProcess(Mind a, Mind b)
    {
        Process process = new GameObject("process between " + a.name + " and " + b.name).AddComponent<Process>();
        process.InitProcess(a, b);
        return process;
    }

    public void resolveBypass(Process process, Mind mindA, Mind mindB)
    {
        mindA.StopProcess();
        mindB.StopProcess();
        Destroy(process.gameObject);
    }

    public void resolveAlliance(Process process, Mind mindA, Mind mindB)
    {
        Mind mainMind = mindA;
        /* TODO
         * if player
         * mainMind =
         */


         /*
          * mindA add animals from mindB
          * 
          * Animals[] animals from mindB.flock 
          * 
          * mindA: recalculateStats and controls
          * 
          * flockB
          * mindB destory 
          * 
          * 
          * 
          * */
    }

    public void resolveFight(Process process, FightResult result1, FightResult result2)
    {

    }    

    public void buildFlock(params FlockingMember[] animalAIs)
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

    private bool playersControl(FlockingMember ai)
    {
        return playerController.currentTarget == ai.AnimalBase;
    }

    private bool playersControl(BaseMovement movement)
    {
        return playerController.currentTarget == movement;
    }

    public Mind provideAnyMind(FlockingMember flockingMember)
    {

    }
}