using System.Collections;
using UnityEngine;
using Pixelplacement;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroScene : MonoBehaviour
{

    public TextMeshProUGUI[] toHideRenderers;
    public Transform enterText;
    [Space]
    public Transform storyTextTransform;
    public float scrollSpeed;
    public float endTextY;
    [Space]
    public AudioClip bgMusic;

    private bool storyExplanation;

    AudioSource bgm;

    private void Start()
    {
        InputManager.Instance.onAccept += Accept;
        Tween.LocalScale(enterText, enterText.localScale + new Vector3(0f, 1f, 0f), 1f, 0f, Tween.EaseInOut, loop:Tween.LoopType.PingPong);
        bgm = AudioManager.Instance.playLoop(bgMusic);
    }

    public void Update()
    {
    }

    public void Accept()
    {
        StartCoroutine(introAnimation());
    }

    IEnumerator introAnimation()
    {
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync("Main");
        sceneLoad.allowSceneActivation = false;
        Tween.LocalScale(enterText, enterText.localScale + new Vector3(0.2f, 0f, 0f), 0.05f, 0f);
        Tween.LocalScale(enterText, Vector3.one, 0.05f, 0.05f);
        foreach (var r in toHideRenderers)
        {
            Tween.Color(r, Color.clear, 1.5f, 0.1f);
        }
        while (storyTextTransform.localPosition.y < endTextY)
        {
            storyTextTransform.position += Vector3.up * Time.deltaTime * scrollSpeed;
            yield return new WaitForEndOfFrame();
        }
        AudioManager.Instance.fadeOut(bgm, 2f);
        sceneLoad.allowSceneActivation = true;
    }

    private void OnDestroy()
    {
        if(InputManager.Instance != null)
            InputManager.Instance.onAccept -= Accept;
    }
}