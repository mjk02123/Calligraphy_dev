using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearButton : MonoBehaviour
{
    public Whiteboard whiteboard; // Whiteboard ��ũ��Ʈ�� ����

    void Start()
    {
        var button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ClearWhiteboard);
        }
        else
        {
            Debug.LogError("Button component not found on the GameObject.");
        }
    }

    void ClearWhiteboard()
    {
        if (whiteboard != null)
        {
            whiteboard.ClearTexture();
        }
        else
        {
            Debug.LogWarning("Whiteboard reference is null. Cannot clear texture.");
        }
    }
}