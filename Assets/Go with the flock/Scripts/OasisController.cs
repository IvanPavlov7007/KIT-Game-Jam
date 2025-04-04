using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OasisController : MonoBehaviour
{
    public float distanceToOasis = 10f;
    Transform oasis;
    void Update()
    {
        if (GameManager.Instance.playerMind != null)
        {
            var flock = GameManager.Instance.playerMind.flock;
            var dist = flock.position - (Vector2) oasis.transform.position;
            if (dist.magnitude < distanceToOasis)
            {
                showEnding(flock);
            }
        }
    }

    void showEnding(Flock flock)
    {
        if(flock.animalsInFlock.Count > 1)
        {
            SceneManager.LoadScene("GoodEnding");
        }
        else
            SceneManager.LoadScene("LoneEnding");
    }


}