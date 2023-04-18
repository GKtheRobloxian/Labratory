using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaBars;
    public Image staminaFill;
    public TMP_Text staminaText;
    GameObject player;
    PlayerController control;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        control = player.GetComponent<PlayerController>();
        staminaBars.maxValue = control.maxStamina;
        staminaBars.value = control.stamina;
        staminaBars.fillRect.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(float value)
    {
        staminaBars.fillRect.gameObject.SetActive(true);
        staminaBars.value = value;
    }

    public void SetText(float value)
    {
        float a;
        a = Mathf.Floor(value);
        staminaText.text = "STM: " + a;
    }
}
