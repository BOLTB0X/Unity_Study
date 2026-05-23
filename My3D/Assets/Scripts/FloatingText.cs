using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [Header("Text Settings")]
    [SerializeField] private float floatDistance = 2f;
    [SerializeField] private float floatDuration = 1.0f;
    [SerializeField] private Color textColor = Color.yellow;
    [SerializeField] private float fontSize = 5f;

    private TextMeshPro tmp;

    void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
        if (tmp == null)
            tmp = gameObject.AddComponent<TextMeshPro>();

        tmp.text = "+1";
        tmp.fontSize = fontSize;
        tmp.color = textColor;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontStyle = FontStyles.Bold;

        RectTransform rt = GetComponent<RectTransform>();
        if (rt != null) rt.sizeDelta = new Vector2(2f, 1f);
    } // Awake

    public void Play(Vector3 startPos)
    {
        transform.position = startPos + new Vector3(0, 0, -10.5f);
        StartCoroutine(FloatRoutine(startPos));
    } // Play

    private IEnumerator FloatRoutine(Vector3 startPos)
    {
        Vector3 endPos = startPos + Vector3.up * floatDistance;
        float elapsed = 0f;

        while (elapsed < floatDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / floatDuration;

            // 위로 이동
            transform.position = Vector3.Lerp(startPos, endPos, t);

            // 후반 50%부터 페이드아웃
            float alpha = t < 0.5f ? 1f : 1f - ((t - 0.5f) / 0.5f);
            Color c = tmp.color;
            c.a = alpha;
            tmp.color = c;

            // 빌보드
            if (Camera.main != null)
                transform.rotation = Camera.main.transform.rotation;

            yield return null;
        }

        Destroy(gameObject);
    } // FloatRoutine
} // FloatingText
