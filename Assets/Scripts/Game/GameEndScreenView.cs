using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEndScreenView : MonoBehaviour
{

    [SerializeField]
    private Canvas _canvas;
    
    [SerializeField]
    public Button Button;
    
    [SerializeField]
    public TextMeshProUGUI Text;

    public void Show(string text)
    {
        Text.text = text;
        _canvas.enabled = true;
    }

    public void Hide()
    {
        _canvas.enabled = false;
    }
}
