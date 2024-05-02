using UnityEngine;
using System.Collections;
using TraningMode;
public class CaptureTransparentScreenshot : MonoBehaviour
{
    public Camera cameraToCapture;
    public RenderTexture renderTexture;

    void Start()
    {
        StartCoroutine(Capture());
    }

    IEnumerator Capture()
    {
        // Chờ đợi cuối frame để đảm bảo đã render xong
        yield return new WaitForEndOfFrame();

        // Lưu render texture vào một texture2D
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        // Chuyển texture thành byte và lưu file
        byte[] imageBytes = texture.EncodeToPNG();
        string directory = @"C:\Users\user\Desktop\DEV_Personal\Zhuan_Ti_Data\image";
        string filename = "SavedTransparentScreen2.png";
        string fullPath = System.IO.Path.Combine(directory, filename);
        System.IO.File.WriteAllBytes(fullPath, imageBytes);


        Debug.Log("Transparent Screenshot saved at D:/SavedTransparentScreen.png");
    }
}
