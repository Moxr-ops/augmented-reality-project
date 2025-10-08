using NativeFilePickerNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuBehavior : MonoBehaviour
{
    private UIDocument _doc;
    private Button _objBtn, _imgBtn, _startBtn;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
        _objBtn = _doc.rootVisualElement.Q<Button>("ChooseOBJ");
        _imgBtn = _doc.rootVisualElement.Q<Button>("ChooseIMG");
        _startBtn = _doc.rootVisualElement.Q<Button>("Start");

        _objBtn.clicked += () => NativeFilePicker.PickFile(p =>
        {
            if (!string.IsNullOrEmpty(p)) ObjManager.Instance.objPath = p;
        }, ".obj");

        _imgBtn.clicked += () => NativeFilePicker.PickFile(p =>
        {
            if (!string.IsNullOrEmpty(p)) ImgManager.Instance.LoadTargetImage(p);
        }, ".jpg");

        _startBtn.clicked += OnStartBtnClicked;
    }

    private void OnStartBtnClicked()
    {
        ImgManager.Instance.CreateImageTargetFromSideloadedTexture();
        ObjManager.Instance.LoadModel();

        this.gameObject.SetActive(false);
    }
}
