using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    void Update()
    {

    }

    public void MenuOpen(bool isOpen)
    {
        anim.SetBool("isOpen", isOpen);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    // 음량 조절
    public void VolumeControl()
    {

    }

    // 메인홀로 이동
    public void GoMainHall()
    {
        GameManager.instance.ChangeScene(0);
    }

    // 종료
    public void Quit()
    {
        Application.Quit();
    }
}
