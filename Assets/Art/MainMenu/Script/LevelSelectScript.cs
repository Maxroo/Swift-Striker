using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;
    public Slider Slider;
    public int Option;

    [SerializeField]
    private KeyCode _AdvanceKey;

    private Image _IR;

    private void Start()
    {
        _IR = GetComponent<Image>();
    }

    private void Update()
    {
        if (Slider.value == 0)
        {
            _IR.sprite = Sprite1;
            Option = 0;
        }
        if (Slider.value == 1)
        {
            _IR.sprite = Sprite2;
            Option = 1;
        }
        if (Slider.value == 2)
        {
            _IR.sprite = Sprite3;
            Option = 2;
        }

        if (Input.GetKeyDown(_AdvanceKey))
        {
            switch (Option)
            {
                case 0:
                    SceneManager.LoadScene("Level001");
                    break;

                case 1:
                    SceneManager.LoadScene("Level002");
                    break;

                case 2:
                    SceneManager.LoadScene("Level003");
                    break;


            }


        }
    }
}

