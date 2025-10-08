using Dummiesman;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public sealed class ObjManager : MonoBehaviour
{
    public static ObjManager Instance { get; private set; }

    private readonly Dictionary<string, GameObject> _cache = new();

    public string objPath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadModel()
    {
        LoadObjModelAsync(objPath, (loadedObj) =>
        {
            ModelManager.Instance.Initialize(loadedObj);
        });
    }

    public async void LoadObjModelAsync(string objPath, System.Action<GameObject> onFinished = null)
    {
        if (!File.Exists(objPath))
        {
            Debug.LogError($"[ObjManager] File not found: {objPath}");
            onFinished?.Invoke(null);
            return;
        }

        if (_cache.TryGetValue(objPath, out GameObject template))
        {
            onFinished?.Invoke(Instantiate(template));
            return;
        }

        byte[] fileBytes = await Task.Run(() => File.ReadAllBytes(objPath));

        GameObject loaded = null;
        await Task.Yield();

        try
        {
            loaded = new OBJLoader().Load(new MemoryStream(fileBytes));
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
            onFinished?.Invoke(null);
            return;
        }

        loaded.SetActive(false);
        loaded.transform.parent = GameObject.Find(ImgManager.Instance.targetName).transform;
        loaded.SetActive(true);
        _cache[objPath] = loaded;
        onFinished?.Invoke(loaded);
    }

    public void ClearCache()
    {
        foreach (var kv in _cache)
        {
            if (kv.Value) DestroyImmediate(kv.Value, true);
        }
        _cache.Clear();
        Resources.UnloadUnusedAssets();
    }
}
