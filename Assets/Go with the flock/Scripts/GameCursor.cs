using System.Collections;
using UnityEngine;
using Pixelplacement;

public class GameCursor : Singleton<GameCursor>
{
    public RectTransform cursorSpriteUI;

    [SerializeField]
    private float defaultCursorScale = default;
    [SerializeField]
    private float pressedCursorScale = default;

    private void Update()
    {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            cursorSpriteUI.parent as RectTransform, // usually the canvas or a container
            Input.mousePosition,
            null, // null for Overlay canvas
            out localPos
        );

        cursorSpriteUI.anchoredPosition = localPos;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        if (InputManager.Instance.mousePressed)
        {
            Tween.LocalScale(cursorSpriteUI, Vector2.one * defaultCursorScale, 0.05f, 0f, Tween.EaseInOut);
        }
        else if (InputManager.Instance.mousePressed)
        {
            Tween.LocalScale(cursorSpriteUI, Vector2.one * pressedCursorScale, 0.05f, 0f, Tween.EaseInOut);
        }
    }
}