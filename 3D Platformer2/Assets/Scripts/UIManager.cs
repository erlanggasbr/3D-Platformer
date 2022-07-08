using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image blackscreen, healthImage;
    public float fadeSpeed = 2f;
    public bool fadeToBlack, fadeFromBlack;

    public Text coinText;

    public GameObject pauseScreen;

    private void Awake() 
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeToBlack)
        {
            blackscreen.color = new Color(blackscreen.color.r, blackscreen.color.g, blackscreen.color.b, Mathf.MoveTowards(blackscreen.color.a, 1f, fadeSpeed*Time.deltaTime));

            if(blackscreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        if(fadeFromBlack)
        {
            blackscreen.color = new Color(blackscreen.color.r, blackscreen.color.g, blackscreen.color.b, Mathf.MoveTowards(blackscreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (blackscreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
    }

    public void Resume()
    {
        GameManager.instance.PauseUnpause();
    }


    public void Option()
    {
        Debug.Log("Nampilan menu Option");
    }


    public void TitleScreen()
    {
        Debug.Log("Title Screen");
    }
}
