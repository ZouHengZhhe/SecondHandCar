using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGetTexture : MonoBehaviour
{
    public string str = "http://jrdcar.com/files/b80df797-582b-4cf4-926c-9f92e58c6465.jpg";

    public Image image;

    // Use this for initialization
    void Start () {
        StartCoroutine(DownloadTexture());
	}
	
    IEnumerator DownloadTexture()
    {
        WWW www = new WWW(str);

        yield return  www;

        Texture2D tex2d = www.texture;
        Debug.Log(tex2d == null);
        Sprite m_sprite = Sprite.Create(tex2d, new Rect(0, 0, tex2d.width, tex2d.height), new Vector2(0, 0));
        Debug.Log(m_sprite == null);
        image.sprite = m_sprite;
    }
}
