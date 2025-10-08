using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class ShowTheObject : MonoBehaviour
{
    public static ShowTheObject instance {  get; private set; }

    [SerializeField]
    private GameObject objToShow;
    [SerializeField]
    private GameObject parent;

    private void Awake()
    {
        instance = this;
    }

    public void Show()
    {
        objToShow.SetActive(true);
        Debug.Log("Show!");
    }

    public void Hide()
    {
        objToShow.SetActive(false);
        Debug.Log("Hide!");
    }
}
