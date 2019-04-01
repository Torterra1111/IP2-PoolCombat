using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingDamageController : MonoBehaviour
{
    public float moveAmount;
    public float moveSpeed;
    public float floatingDuration;

    void Start()
    {
        Destroy(gameObject, floatingDuration);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, moveAmount * (moveSpeed * Time.deltaTime));
    }

    public void SetText(string text)
    {
        Text damageText = GetComponentInChildren<Text>();
        damageText.text = text;
    }
}
