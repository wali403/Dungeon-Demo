using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCursor : MonoBehaviour
{
    private void Awake()
    {
        //禁用自带鼠标
        Cursor.visible = false;
    }

    private void Update()
    {
        //更新自身位置为鼠标位置
        transform.position = Input.mousePosition;
    }
}
