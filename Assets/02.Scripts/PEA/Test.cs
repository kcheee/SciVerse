using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //for (int i = 0; i < btn.onClick.GetPersistentEventCount(); i++)
            //{
            //    //print(btn.onClick.GetPersistentTarget(i).name);
            //    //print(btn.onClick.GetPersistentListenerState(i));
            //    //print(btn.onClick.GetPersistentMethodName(i));
            //}
            btn.onClick.Invoke();
        }
    }
}
