using System.Collections;
using UnityEngine;
using System.IO;
using Vuforia;

public class ImgManager : MonoBehaviour
{
    public static ImgManager Instance {  get; private set; }

    public Texture2D ImgTarget { get; private set; }
    public float printedTargetSize;
    public string targetName;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //VuforiaApplication.Instance.OnVuforiaStarted += CreateImageTargetFromSideloadedTexture;
    }

    public void LoadTargetImage(string p)
    {
        StartCoroutine(LoadImage(p));
    }

    public void CreateImageTargetFromSideloadedTexture()
    {
        var mTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(
            ImgTarget,
            printedTargetSize,
            targetName);
        mTarget.gameObject.AddComponent<DefaultObserverEventHandler>();
        Debug.Log("Instant Image Target created " + mTarget.TargetName);
    }

    private IEnumerator LoadImage(string path)
    {
        byte[] fileData = File.ReadAllBytes(path);
        ImgTarget = new Texture2D(2, 2);
        ImgTarget.LoadImage(fileData);

        targetName = Path.GetFileNameWithoutExtension(path);

        yield return null;
    }
}
