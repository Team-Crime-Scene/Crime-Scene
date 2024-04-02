using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenshotSystem : MonoBehaviour
{

    //카메라가 달려있는 오브젝트에 이 컴포넌트를 같이 부착해야 사용가능

    [SerializeField] string folderName = "ScreenShots";
    [SerializeField] string fileName = "MyScreenShot";
    [SerializeField] string extName = "png";

    [SerializeField]
    public bool isTakeScreenshot;


    public List<string> screenshots = new List<string>();  //스크린샷 경로 저장용 List 나중에 AlbumManager 등으로 옮길 것


    /***********************************************************************
    *                               Fields & Properties
    ***********************************************************************/
    #region Properties
    private Texture2D _imageTexture; // imageToShow의 소스 텍스쳐


    //경로 설정
    private string RootPath
    {
        get
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            return Application.dataPath;
#elif UNITY_ANDROID
            return $"/storage/emulated/0/DCIM/{Application.productName}/";
            //return Application.persistentDataPath;
#endif
        }
    }
    private string FolderPath => $"{RootPath}/{folderName}";
    private string TotalPath => $"{FolderPath}/{fileName}_{DateTime.Now.ToString("MMdd_HHmmss")}.{extName}"; //일단 작명 규칙은 현재 시간참조

    private string lastSavedPath;

    #endregion

    /***********************************************************************
    *                               Unity Events
    ***********************************************************************/

    // 카메라가 랜더링 마친 이후임을 보장하기 위해 OnPostRender()에서 호출
    // .. 인데 OnPostRender는 시네머신 컴포넌트에서는 호출 안해줌
    // 메인카메라에 달아주자
    private void OnPostRender()
    {
        if ( !isTakeScreenshot )
            return;
        Debug.Log("Call ScreenShot");
        ScreenShot();
        isTakeScreenshot = false;
    }

    /***********************************************************************
    *                               Methods
    ***********************************************************************/

    private void ScreenShot()
    {
        string totalPath = TotalPath; // 프로퍼티 참조 시 시간에 따라 이름이 결정되므로 캐싱
        screenshots.Add(totalPath);

        Texture2D screenTex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        Rect area = new Rect(0f, 0f, Screen.width, Screen.height); 

        // 현재 스크린으로부터 지정 영역의 픽셀들을 텍스쳐에 저장
        screenTex.ReadPixels(area, 0, 0); //OnPostRender() 혹은 코루틴의 EndOfFrame이 아니면 에러 뜸

        bool succeeded = true;
        try
        {
            if ( Directory.Exists(FolderPath) == false )
            {
                Directory.CreateDirectory(FolderPath);
            }
            // 스크린샷 저장
            File.WriteAllBytes(totalPath, screenTex.EncodeToPNG()); //Texture2D => PNG 
        }
        catch ( Exception e ) // 예외처리
        {
            succeeded = false;
            Debug.LogWarning($"Screen Shot Save Failed : {totalPath}");
            Debug.LogWarning(e);
        }
        Destroy(screenTex); //가비지 제거

        if ( succeeded )
        {
            Debug.Log($"Screen Shot Saved : {totalPath}");
            StartCoroutine(ScreenshotAnimation());
            lastSavedPath = totalPath; // 최근 경로에 저장
        }
    }

    IEnumerator ScreenshotAnimation()
    {
        Debug.Log("아무튼 개쩌는 스크린샷 효과,");
        yield return null;
    }

}