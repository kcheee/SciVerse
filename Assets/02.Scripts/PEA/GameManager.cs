using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Menu menu;

    private bool isMenuOpened = false;

    public Button chemistryBtn;
    public Button biologyBtn;
    public Button earthScienceBtn;

    private void Awake()
    {
        if(instance!= null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        chemistryBtn?.onClick.AddListener(() => ChangeScene(3));
        biologyBtn?.onClick.AddListener(() => ChangeScene(2));
        earthScienceBtn?.onClick.AddListener(()=> ChangeScene(1));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuOpened = !isMenuOpened;
            MenuOpen(isMenuOpened);
        }
    }

    public void MenuOpen(bool isOpen)
    {
        if (isOpen)
        {
            menu.gameObject.SetActive(true);
        }

        menu.MenuOpen(isOpen);
    }

    public void ChangeScene(int buiildIndex)
    {
        SceneManager.LoadScene(buiildIndex);
    }

}
