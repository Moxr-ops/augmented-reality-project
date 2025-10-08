using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

public class ModelManager : MonoBehaviour
{
    #region µ¥Àý
    public static ModelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    [SerializeField] private UIDocument _doc;

    private Transform _target;
    private readonly Dictionary<string, Slider> _sliders = new();
    private bool _isUndo;

    private void Start()
    {
        _doc = GetComponent<UIDocument>();
        if (_doc == null) _doc = FindObjectOfType<UIDocument>();
        if (_doc == null) throw new NullReferenceException("ÕÒ²»µ½ UIDocument£¡");

        CreateOrBindSliders();
        SetEnabled(false);
    }

    public void Initialize(GameObject loadedObj)
    {
        if (loadedObj == null) return;
        _target = loadedObj.transform;
        SyncSliderValues();
        SetEnabled(true);
    }

    public void Clear()
    {
        _target = null;
        SetEnabled(false);
    }

    private void CreateOrBindSliders()
    {
        var root = _doc.rootVisualElement;

        BindSlider(root, "xPos", v => SetPosition(Axis.X, v));
        BindSlider(root, "yPos", v => SetPosition(Axis.Y, v));
        BindSlider(root, "zPos", v => SetPosition(Axis.Z, v));

        BindSlider(root, "xRot", v => SetRotation(Axis.X, v));
        BindSlider(root, "yRot", v => SetRotation(Axis.Y, v));
        BindSlider(root, "zRot", v => SetRotation(Axis.Z, v));

        BindSlider(root, "uniformScale", v => SetUniformScale(v));
    }

    private void BindSlider(VisualElement root, string name, Action<float> setter)
    {
        var slider = root.Q<Slider>(name);
        if (slider == null)
        {
            slider = new Slider(name, -10, 10) { name = name };
            root.Add(slider);
        }
        slider.RegisterValueChangedCallback(evt =>
        {
            if (_isUndo || _target == null) return;
            RecordUndo(name);
            setter(evt.newValue);
        });
        _sliders[name] = slider;
    }

    private enum Axis { X, Y, Z }

    private void SetPosition(Axis axis, float value)
    {
        Vector3 local = _target.localPosition;
        local[(int)axis] = value;
        _target.localPosition = local;
    }

    private void SetRotation(Axis axis, float value)
    {
        var r = _target.localEulerAngles;
        r[(int)axis] = value;
        _target.localEulerAngles = r;
    }

    private void SetUniformScale(float value)
    {
        _target.localScale = Vector3.one * value;
    }

    private void RecordUndo(string operation)
    {
#if UNITY_EDITOR
        Undo.RecordObject(_target, $"Model {operation}");
#endif
    }

    private void SyncSliderValues()
    {
        if (_target == null) return;

        _sliders["xPos"].value = _target.position.x;
        _sliders["yPos"].value = _target.position.y;
        _sliders["zPos"].value = _target.position.z;

        var r = _target.localEulerAngles;
        _sliders["xRot"].value = NormalizeAngle(r.x);
        _sliders["yRot"].value = NormalizeAngle(r.y);
        _sliders["zRot"].value = NormalizeAngle(r.z);

        _sliders["uniformScale"].value = _target.localScale.x;


    }

    private static float NormalizeAngle(float a)
    {
        a %= 360f;
        return a < 0 ? a + 360f : a;
    }

    private void SetEnabled(bool on)
    {
        foreach (var s in _sliders.Values) s.SetEnabled(on);
    }
}
