using UnityEngine;

public class GameInitScreenView : MonoBehaviour
{

    [SerializeField]
    private Canvas _canvas;

    public void Show()
    {
        _canvas.enabled = true;
    }

    public void Hide()
    {
        _canvas.enabled = false;
    }

    private void FixedUpdate()
    {
        if (_canvas.enabled && Input.anyKey)
        {
            Hide();
        }
    }
}
