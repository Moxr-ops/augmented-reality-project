using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class FlipManager : MonoBehaviour
{
    public static FlipManager instance {  get; private set; }

    [SerializeField] private GameObject pageA;
    [SerializeField] private GameObject pageB;
    private float flipThreshold = 85f;
    private float averageFlipSpeed;

    private ImageTargetBehaviour frontImgBehaviour => pageA.GetComponent<ImageTargetBehaviour>();
    private Vector3 posOfSpine => pageA.transform.position - new Vector3(pageA.transform.position.x - frontImgBehaviour.GetSize().x / 2, 0, 0);
    private bool readyToFlip => pageA.transform.rotation.y >= flipThreshold;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
       
    }


}
