using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabOpener : MonoBehaviour
{
    public KeyCode selectKey;
    bool isOpened;
    public GameObject Object;
    // Update is called once per frame
    private void Start()
    {
        Object.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(selectKey))
            OpenTab();
    }

    public void OpenTab()
    {
        Object.SetActive(!Object.activeSelf);
    }
}
