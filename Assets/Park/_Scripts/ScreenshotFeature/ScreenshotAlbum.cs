using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotAlbum : MonoBehaviour
{
    public static ScreenshotAlbum Instance { get; private set; }
    public List<Screenshot> Screenshots { get; private set; }
    private void Awake()
    {
        if ( Instance == null )
        {
            Instance = this;
            Screenshots = new List<Screenshot>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitAlbum(string folderPath)
    {
        string [] paths = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);
        for ( int i = 0; i < paths.Length; i++ )
        {
            Screenshots.Add(new Screenshot(new ScreenshotData(paths [i]))); //���߿� ���ã�� ���ε� �ҷ��;���
        }
    }

    public void Add(Screenshot screenshot )
    {
        Screenshots.Add( screenshot );
    }

    public void Delete( Screenshot screenshot )
    {
        string path = screenshot.Data.path;
        Screenshots.Remove(screenshot);
        if ( File.Exists(path) )
        {
            try
            {
                // ���� ����
                File.Delete(path);
                Debug.Log("File deleted: " + path);
            }
            catch ( IOException ex )
            {
                Debug.LogError("Error deleting file: " + ex.Message);
            }
        }
        else
        {
            Debug.LogWarning("File does not exist: " + path);
        }
    }

}
