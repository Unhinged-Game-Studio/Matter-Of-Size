using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePause : MonoBehaviour
{
    private bool paused = false;

    void Start()
    {
        this.gameObject.GetComponent<CanvasGroup>().LeanAlpha(0f, 0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused == false)
            {
                this.gameObject.GetComponent<CanvasGroup>().LeanAlpha(1f, 0.5f);
                this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                paused = true;
            }
            else if (paused == true)
            {
                this.gameObject.GetComponent<CanvasGroup>().LeanAlpha(0f, 0.5f);
                this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                paused = false;
            }
        }
    }
}
