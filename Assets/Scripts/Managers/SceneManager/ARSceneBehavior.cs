using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARSceneBehavior : MonoBehaviour
{
    void Start()
    {
        ObjManager.Instance.LoadModel();
        ImgManager.Instance.CreateImageTargetFromSideloadedTexture();
    }
}
