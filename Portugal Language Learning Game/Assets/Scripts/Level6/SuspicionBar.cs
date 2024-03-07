using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionBar : MonoBehaviour
{
    public Slider healthSlider;
    public float minHealth = 0f;
    public float health;
    public float smoothSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        health = minHealth;   
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSlider.value != health)
        {
            //healthSlider.value = health;
            healthSlider.value = Mathf.Lerp(healthSlider.value, health, smoothSpeed * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }

    }


    void TakeDamage(int damage)
    {
        health += damage;
        health = Mathf.Max(health, 0f);
    }

}
