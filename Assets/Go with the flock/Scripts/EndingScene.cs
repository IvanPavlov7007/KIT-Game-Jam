using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
    
public class EndingScene : MonoBehaviour
{
    public AudioClip bgMusic;
    AudioSource bgm;
    private void Start()
    {
        InputManager.Instance.onAccept += Accept;
        if(bgMusic != null)
        bgm = AudioManager.Instance.playLoop(bgMusic);
    }

    // Update is called once per frame
    void Accept()
    {
        if(bgm != null)
            AudioManager.Instance.fadeOut(bgm, 2f);
        SceneManager.LoadScene("Main");
    }
}