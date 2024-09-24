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
        ClearTexture(); // 초기화 시 텍스처를 한 번 지워줍니다.
    }

    // 텍스처를 지우는 메서드
    public void ClearTexture()
    {
        Color32[] resetColorArray = texture.GetPixels32();

        for (int i = 0; i < resetColorArray.Length; i++)
        {
            resetColorArray[i] = Color.white; // 모든 픽셀을 흰색으로 설정
        }

        texture.SetPixels32(resetColorArray);
        texture.Apply();
    }

    // 버튼을 눌렀을 때 호출될 메서드
    public void ClearWhiteboard()
    {
        ClearTexture();
    }
}
