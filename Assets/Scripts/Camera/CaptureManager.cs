using UnityEngine;
using System.Collections;
using RenderHeads.Media.AVProMovieCapture;

public class CaptureManager : MonoBehaviour
{
    [SerializeField]
    private string screenshotDirectory = "Screenshots";

    [SerializeField]
    private string videoDirectory = "Captures";

    private int pictureCount;
    private CaptureFromScreen videoCap;
    private string objName = "*Capture Manager*";
    private GameObject obj;

    void Awake() {
        obj = GameObject.Find(objName);
        if (obj == null) {
            obj = gameObject;
            obj.name = objName;
            DontDestroyOnLoad(obj);
            System.IO.Directory.CreateDirectory(screenshotDirectory);
            pictureCount = 0;
            videoCap = gameObject.AddComponent<CaptureFromScreen>();
            videoCap._useMediaFoundationH264 = true;
            videoCap._outputFolderPath = videoDirectory;
            videoCap._autoFilenameExtension = "mp4";
        } else if (obj != gameObject) {
            Destroy(gameObject);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F1)) {
            pictureCount++;
            ScreenCapture.CaptureScreenshot(screenshotDirectory + "/screenshot" + System.DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".png");
        }
        if (Input.GetKeyDown(KeyCode.F2)) {
            if (videoCap.IsCapturing()) {
                videoCap.StopCapture();
            } else {
                videoCap.StartCapture();
            }
        }
    }
}
