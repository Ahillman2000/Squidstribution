using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCam : MonoBehaviour
{
    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;
    private UI ui;
    BoxCollider[] myColliders;

    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
        ui = GameObject.Find("UI").GetComponent<UI>();
        myColliders = gameObject.GetComponents<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            cam1.enabled = false;
            cam2.enabled = true;
            StartCoroutine(WaitAfterTrigger(20));
            if (myColliders[0].enabled)
            {
                myColliders[0].enabled = false;
            }
            else if (myColliders[1].enabled)
            {
                myColliders[1].enabled = false;
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            cam1.enabled = !cam1.enabled;
            cam2.enabled = !cam2.enabled;
        }

        if(!cam1.enabled)
        {
            cam1.GetComponent<ClickToScreen>().enabled = false;
        }
        else
        {
            cam1.GetComponent<ClickToScreen>().enabled = true;
        }

        if (!cam2.enabled)
        {
            cam2.GetComponent<ClickToScreen>().enabled = false;
            ui.newsOn = false;

        }
        else
        {
            cam2.GetComponent<ClickToScreen>().enabled = true;
            ui.newsOn = true;
        }
    }

    IEnumerator WaitAfterTrigger(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        cam2.enabled = false;
        cam1.enabled = true;
    }
}
