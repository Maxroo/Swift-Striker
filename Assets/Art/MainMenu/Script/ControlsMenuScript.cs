using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlsMenuScript : MonoBehaviour
{
    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;
    public Sprite Sprite4;
    public Sprite Sprite5;
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

        if (Slider.value == 3)
        {
            _IR.sprite = Sprite4;
            Option = 3;
        }

                if (Slider.value == 4)
        {
            _IR.sprite = Sprite5;
            Option = 4;
        }

        if (Input.GetKeyDown(_AdvanceKey))
        {
            SceneManager.LoadScene("ArenSelect");
        }
    }
}
