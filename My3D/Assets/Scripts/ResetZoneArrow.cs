using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetZoneArrow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform resetZone;

    [Header("Settings")]
    [SerializeField] private float showDistance = 5f;
    [SerializeField] private float hoverHeight = 3f;
    [SerializeField] private float rotateSpeed = 5f;

    private GameObject arrowObj;

    void Start()
    {
        // 화살표 오브젝트 자동 생성
        // 자식으로 붙임
        arrowObj = CreateArrow();
        arrowObj.SetActive(false);

        arrowObj = CreateArrow();
        Debug.Log(arrowObj == null ? "arrowObj 생성 실패" : "arrowObj 생성 성공");
        arrowObj.SetActive(false);
    }

    void Update()
    {
        if (player == null || resetZone == null)
        {
            return;
        }

        float distance = Vector3.Distance(player.position, resetZone.position);

        if (distance >= showDistance)
        {
            arrowObj.SetActive(true);

            // 플레이어 머리 위에 위치
            arrowObj.transform.position = player.position + Vector3.up * hoverHeight;

            // 리셋존 방향으로 회전
            Vector3 dir = resetZone.position - player.position;
            dir.y = 0;

            if (dir != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir);
                arrowObj.transform.rotation = Quaternion.Slerp(
                    arrowObj.transform.rotation,
                    targetRot,
                    Time.deltaTime * rotateSpeed
                );
            }
        }
        else
        {
            arrowObj.SetActive(false);
        } // if - else
    }

    private GameObject CreateArrow()
    {
        // 부모 오브젝트
        GameObject arrow = new GameObject("ResetZoneArrow");

        // ── 화살표 몸통 (얇은 Cube) ──
        GameObject body = GameObject.CreatePrimitive(PrimitiveType.Cube);
        body.name = "Body";
        body.transform.SetParent(arrow.transform);
        body.transform.localPosition = new Vector3(0, 0, 0f);
        body.transform.localScale = new Vector3(0.15f, 0.15f, 0.6f);
        Destroy(body.GetComponent<Collider>());
        SetColor(body, Color.yellow);

        // ── 화살표 머리 (찌그러진 Cube를 45도 회전) ──
        GameObject head = GameObject.CreatePrimitive(PrimitiveType.Cube);
        head.name = "Head";
        head.transform.SetParent(arrow.transform);
        head.transform.localPosition = new Vector3(0, 0, 0.45f);
        head.transform.localRotation = Quaternion.Euler(0, 45, 0);
        head.transform.localScale = new Vector3(0.25f, 0.15f, 0.25f);
        Destroy(head.GetComponent<Collider>());
        SetColor(head, Color.yellow);

        return arrow;
    } // CreateArrow

    private void SetColor(GameObject obj, Color color)
    {
        MeshRenderer mr = obj.GetComponent<MeshRenderer>();
        if (mr != null)
        {
            mr.material.color = color;
        }
    } // SetColor
} // ResetZoneArrow