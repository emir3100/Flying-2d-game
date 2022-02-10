using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    private Camera playerCamera;

    private void Awake()
    {
        playerCamera = Camera.main;
        PutColliders();
    }

    private void PutColliders()
    {
        GameObject left = new GameObject("LeftCollider", typeof(BoxCollider2D));
        GameObject right = new GameObject("RightCollider", typeof(BoxCollider2D));
        GameObject top = new GameObject("TopCollider", typeof(BoxCollider2D));
        GameObject bottom = new GameObject("BottomCollider", typeof(BoxCollider2D));

        left.transform.SetParent(transform);
        right.transform.SetParent(transform);
        top.transform.SetParent(transform);
        bottom.transform.SetParent(transform);

        Vector2 leftBottomCorner = playerCamera.ViewportToWorldPoint(Vector3.zero);
        Vector2 rightTopCorner = playerCamera.ViewportToWorldPoint(Vector3.one);

        Debug.LogWarning(leftBottomCorner);
        Debug.Log(rightTopCorner);

        left.transform.position = new Vector2(
            leftBottomCorner.x - 0.5f,
            playerCamera.transform.position.y
            );
        right.transform.position = new Vector2(
            rightTopCorner.x + 0.5f,
            playerCamera.transform.position.y
            );
        top.transform.position = new Vector2(
            playerCamera.transform.position.x,
            rightTopCorner.y + 0.5f
            );
        bottom.transform.position = new Vector2(
            playerCamera.transform.position.x,
            leftBottomCorner.y - 0.5f
            );

        left.transform.localScale = new Vector3(1, Mathf.Abs(rightTopCorner.y - leftBottomCorner.y));
        right.transform.localScale = new Vector3(1, Mathf.Abs(rightTopCorner.y - leftBottomCorner.y));
        top.transform.localScale = new Vector3(Mathf.Abs(rightTopCorner.x - leftBottomCorner.x), 1);
        bottom.transform.localScale = new Vector3(Mathf.Abs(rightTopCorner.x - leftBottomCorner.x), 1);
    }
}
