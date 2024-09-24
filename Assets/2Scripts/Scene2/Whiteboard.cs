using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 texturesize = new Vector2(2048, 2048);

    void Start()
    {
        var r = GetComponent<Renderer>();
        texture = new Texture2D((int)texturesize.x, (int)texturesize.y);
        r.material.mainTexture = texture;
        ClearTexture(); // �ʱ�ȭ �� �ؽ�ó�� �� �� �����ݴϴ�.
    }

    // �ؽ�ó�� ����� �޼���
    public void ClearTexture()
    {
        Color32[] resetColorArray = texture.GetPixels32();

        for (int i = 0; i < resetColorArray.Length; i++)
        {
            resetColorArray[i] = Color.white; // ��� �ȼ��� ������� ����
        }

        texture.SetPixels32(resetColorArray);
        texture.Apply();
    }

    // ��ư�� ������ �� ȣ��� �޼���
    public void ClearWhiteboard()
    {
        ClearTexture();
    }
}
