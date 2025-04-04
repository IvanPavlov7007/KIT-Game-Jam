using System.Collections;
using UnityEngine;

namespace Assets.Go_with_the_flock.Scripts
{
    public class ResolutionSetter : MonoBehaviour
    {
        public GameObject hint;
        public static bool pressed = false;
        private void Start()
        {
            InputManager.Instance.onFullScreen += onFullSwitch;
        }

        private void Update()
        {
            hint.SetActive(!pressed);
        }

        void onFullSwitch()
        {
            pressed = true;
            Screen.fullScreen = !Screen.fullScreen;
        }

        private void OnDestroy()
        {
            InputManager.Instance.onFullScreen -= onFullSwitch;
        }
    }
}