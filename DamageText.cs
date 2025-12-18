using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float moveSpeed; // 텍스트 이동 속도
    public float alphaSpeed; // 투명화 속도
    TextMeshPro text;
    Color alpha;
    public float damage;
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = damage.ToString("F0");
        alpha = text.color;
        Invoke("DestroyObject", 1f);
    }

    
    void Update()
    {
        transform.Translate(new Vector2(0,moveSpeed * Time.deltaTime));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
