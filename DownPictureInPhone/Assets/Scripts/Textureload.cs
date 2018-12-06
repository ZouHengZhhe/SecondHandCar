using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Textureload : MonoBehaviour
{
    public string url = "http://jrdcar.com/files/b80df797-582b-4cf4-926c-9f92e58c6465.jpg";
    public GameObject go;
    private string path;
    public Image _image;
    public Image[] images = new Image[6];
    public List<string> names = new List<string>();
    public List<string> urls = new List<string>();
    public List<string> paths = new List<string>();

    int num = 0;

    private void Start()
    {
        paths.Add("");
        paths.Add("");
        paths.Add("");
        paths.Add("");
        paths.Add("");
        paths.Add("");
        urls.Add("http://jrdcar.com/files/b80df797-582b-4cf4-926c-9f92e58c6465.jpg");
        urls.Add("http://jrdcar.com/files/97f367aa-a54b-4f9b-9da6-f67ea6608a4b.jpg");
        urls.Add("http://jrdcar.com/files/83c5fff5-ea01-4334-87d5-1e56e7b13f44.jpg");
        urls.Add("http://jrdcar.com/files/658e0f1b-daef-4109-b171-b6a6ddfb2c3a.jpg");
        urls.Add("http://jrdcar.com/files/4fd3abf7-d6be-4dfd-95e5-4d1ef7acb5cf.jpg");
        urls.Add("http://jrdcar.com/files/26b04c38-da25-4985-840a-fa32d71d0ea2.jpg");

        names.Add("b80df797-582b-4cf4-926c-9f92e58c6465.jpg");
        names.Add("97f367aa-a54b-4f9b-9da6-f67ea6608a4b.jpg");
        names.Add("83c5fff5-ea01-4334-87d5-1e56e7b13f44.jpg");
        names.Add("658e0f1b-daef-4109-b171-b6a6ddfb2c3a.jpg");
        names.Add("4fd3abf7-d6be-4dfd-95e5-4d1ef7acb5cf.jpg");
        names.Add("26b04c38-da25-4985-840a-fa32d71d0ea2.jpg");

        for(int i = 0; i < urls.Count; i++)
        {
            StartCoroutine(UploadPNG(names[i], "Work",i));
        }
        
    }

    private void Update()
    {
        if(num==6)
        {
            ShowNativeTexture();
            num = 0;
        }
    }

    /// <summary>
    /// 下载图片
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private IEnumerator UploadPNG(string fileName, string dic,int index)
    {
        WWW www = new WWW(urls[index]);
        yield return www;
        //print(www.texture==null);
        byte[] bytes = www.texture.EncodeToJPG();
        //path = PathForFile(fileName, dic);//移动平台的判断
        string str= PathForFile(fileName, dic);
        print(str);
        paths[index]=PathForFile(fileName, dic);

        print("文件" + paths[index]);

        //SaveNativeFile(bytes, path);//保存图片到本地
        SaveNativeFile(bytes, paths[index]);//保存图片到本地
        num++;
    }

    /// <summary>
    /// 判断平台
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public string PathForFile(string filename, string dic)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)   //IOS
        {
            string path = Application.persistentDataPath.Substring(0, Application.persistentDataPath.Length - 5);
            path = path.Substring(0, path.LastIndexOf('/'));
            path = Path.Combine(path, "Documents");
            path = Path.Combine(path, dic);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return Path.Combine(path, filename);
        }
        else if (Application.platform == RuntimePlatform.Android)   //Android
        {
            string path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            path = Path.Combine(path, dic);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return Path.Combine(path, filename);
        }
        else  //Windows
        {
            string path = Application.dataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            path = Path.Combine(path, dic);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return Path.Combine(path, filename);
        }
    }

    /// <summary>
    /// 在本地保存文件
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="path"></param>
    public void SaveNativeFile(byte[] bytes, string path)
    {
        FileStream fs = new FileStream(path, FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Flush();
        fs.Close();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(200, 200, 120, 30), "Click"))
        {
            ShowNativeTexture();
        }
    }

    /// <summary>
    /// 显示图片
    /// </summary>
    public void ShowNativeTexture()
    {
        //go.GetComponent<MeshRenderer>().material.mainTexture = GetNativeFile(path);
        //_image.sprite= GetSprite(path);
        for(int i = 0;i<6;i++)
        {
            images[i].sprite = GetSprite(paths[i]);
        }
    }

    /// <summary>
    /// 获取到本地的图片
    /// </summary>
    /// <param name="path"></param>
    public Texture2D GetNativeFile(string path)
    {
        print(path);
        try
        {
            var pathName = path;
            var bytes = ReadFile(pathName);
            int width = Screen.width;
            int height = Screen.height;
            var texture = new Texture2D(width, height);
            texture.LoadImage(bytes);
            return texture;
        }
        catch
        {
        }
        return null;
    }

    public Sprite GetSprite(string path)
    {
        try
        {
            var pathName = path;
            var bytes = ReadFile(pathName);
            int width = Screen.width;
            int height = Screen.height;
            var texture = new Texture2D(width, height);
            texture.LoadImage(bytes);
            Sprite m_sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
            print(m_sprite.name);
            return m_sprite;
        }
        catch
        {
        }
        return null;
    }

    public byte[] ReadFile(string filePath)
    {
        var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        fs.Seek(0, SeekOrigin.Begin);
        var binary = new byte[fs.Length];
        fs.Read(binary, 0, binary.Length);
        fs.Close();
        return binary;
    }
}