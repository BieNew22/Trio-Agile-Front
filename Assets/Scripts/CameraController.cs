using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector2 DragOrigin;
    bool IsDragging = false;

    public float zoomSpeed = 10.0f;  // Ȯ��/��� �ӵ�
    public float minZoom = 5.0f;    // �ּ� Ȯ�� ����
    public float maxZoom = 30.0f;   // �ִ� ��� ����

    void Start()
    {
        Camera.main.transform.position = new Vector3(20, 9, Camera.main.transform.position.z);
        //transform.position = new Vector2(20, 9);
        //Camera.main.transform.size
        Camera.main.orthographicSize = 10.0f;
    }

    void Update()
    {
        // ���� Ȯ�� ��� ���
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");

        Camera.main.orthographicSize -= scrollData * zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);


        // ī�޶� ������ �巡�׷� �̵� ��Ŵ.
        if (Input.GetMouseButtonDown(0))    // ���콺 Ŭ����
        {
            DragOrigin = Input.mousePosition;
            IsDragging = true;
        }

        if (Input.GetMouseButtonUp(0))      // ���콺 ���� ��
        {
            IsDragging = false;
        }

        if (IsDragging)
        {
            Vector3 diff = Camera.main.ScreenToWorldPoint(DragOrigin) - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += diff;
            DragOrigin = Input.mousePosition;

        }
    }
}
