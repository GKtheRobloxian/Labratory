using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UltraInstinct : MonoBehaviour
{

    public Slider healthBar;
    public Image fill;
    public TMP_Text healthText;
    int healthMax;
    GameObject player;
    PlayerController control;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        control = player.GetComponent<PlayerController>();
        healthBar.maxValue = control.maximumHealth;
        healthBar.value = control.startingHealth;
        healthBar.fillRect.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(float value)
    {
        healthBar.fillRect.gameObject.SetActive(true);
        healthBar.value = value;
        healthText.text = "HP: " + value;
    }
}
