using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtilities
{
    public static Camera mainCamera;

    //获取鼠标的世界位置
    public static Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null) mainCamera = Camera.main;//自动获取带有mainCarema tag的物体

        Vector3 mouseScreenPosition = Input.mousePosition;//获取鼠标的屏幕位置

        mouseScreenPosition.x = Mathf.Clamp(mouseScreenPosition.x, 0f, Screen.width);
        mouseScreenPosition.y = Mathf.Clamp(mouseScreenPosition.y, 0f, Screen.height);

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        worldPosition.z = 0f;

        return worldPosition;
    }

    //获取向量的角度
    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);//arctan(y / x), 弧度制，1派2派那样的

        float degrees = radians * Mathf.Rad2Deg;//* (派 / 180),转换成角度制

        return degrees;
    }

    public static AimDirection GetAimDirection(float angleDegrees)
    {
        AimDirection aimDirection;

        if (-90f <= angleDegrees && angleDegrees <= 90f)
        {
            aimDirection = AimDirection.right;
        }
        else if (90f < angleDegrees && angleDegrees <= 180f || -180f <= angleDegrees && angleDegrees < -90f)
        {
            aimDirection = AimDirection.left;
        }
        else
        {
            aimDirection = AimDirection.right;
        }

        return aimDirection;
    }


    //debug检查空字符串,如果含有空字符串,就返回true
    public static bool ValidateCheckEmptyString(Object thisObject, string fieldName, string stringToCheck)
    {
        if (stringToCheck == "")
        {
            Debug.Log(fieldName + " is empty and must contain a value in object " + thisObject.name.ToString());
            return true;
        }
        return false;
    }

    //检查物体是否为空
    public static bool ValidateCheckNullValue(Object thisObject, string fieldName, UnityEngine.Object objectToCheck)
    {
        if (objectToCheck == null)
        {
            Debug.Log(fieldName + " is empty and must contain a value in object " + thisObject.name.ToString());
            return true;
        }
        return false;
    }

    //检查列表是否为空或含有null值,返回是否存在这些错误
    public static bool ValidateCheckEnumerableValues(Object thisObject, string fieldName, IEnumerable enumerableObjectToCheck)
    {
        bool error = false;
        int count = 0;

        if (enumerableObjectToCheck == null)
        {
            Debug.Log(fieldName + "is null in object" + thisObject.name.ToString());
            return true;
        }

        foreach (var item in enumerableObjectToCheck)
        {
            if (item == null)
            {
                Debug.Log(fieldName + " has null values in object " + thisObject.name.ToString());
                error = true;
            }
            else
            {
                count++;
            }
        }

        if (count == 0)
        {
            Debug.Log(fieldName + " has no values in object " + thisObject.name.ToString());
            error = true;
        }

        return error;
    }

    //检查整数是否为正数
    public static bool ValidateCheckPositiveValue(Object thisObject, string fieldName, int valueToCheck, bool isZeroAllowed)
    {
        bool error = false;

        if (isZeroAllowed)
        {
            if (valueToCheck <= 0)
            {
                Debug.Log(fieldName + " must contain a postive value or zero in object " + thisObject.name.ToString());
                error = true;
            }
        }
        else
        {
            if (valueToCheck < 0)
            {
                Debug.Log(fieldName + " must contain a postive value in object " + thisObject.name.ToString());
                error = true;
            }
        }

        return error;
    }

    //同上,float版
    public static bool ValidateCheckPositiveValue(Object thisObject, string fieldName, float valueToCheck, bool isZeroAllowed)
    {
        bool error = false;

        if (isZeroAllowed)
        {
            if (valueToCheck < 0f)
            {
                Debug.Log(fieldName + " must contain a postive value or zero in object " + thisObject.name.ToString());
                error = true;
            }
        }
        else
        {
            if (valueToCheck <= 0f)
            {
                Debug.Log(fieldName + " must contain a postive value in object " + thisObject.name.ToString());
                error = true;
            }
        }

        return error;
    }

    //检测是否是一个合法的正数范围
    public static bool ValidateCheckPositiveRange(Object thisObject, string fieldNameMinimum, float valueToCheckMinimum, 
        string fieldNameMaximum, float valueToCheckMaximum, bool isZeroAllowed)
    {
        bool error = false;
        if (valueToCheckMinimum > valueToCheckMaximum)
        {
            Debug.Log(fieldNameMinimum + " must be less than or equal to " + fieldNameMaximum + "in object " + thisObject.name.ToString());
            error = true;
        }

        if (ValidateCheckPositiveValue(thisObject, fieldNameMinimum, valueToCheckMinimum, isZeroAllowed)) error = true;
        if (ValidateCheckPositiveValue(thisObject, fieldNameMaximum, valueToCheckMaximum, isZeroAllowed)) error = true;

        return error;
    }

    //传入玩家的世界坐标位置,返回距离玩家位置最近的出生点
    //Vector3Int表示网格位置的坐标
    //Vector3 表示真实位置的坐标
    //grid的CellToWorld可以把对于这个网格位置的坐标转换成真实世界的坐标
    public static Vector3 GetSpawnPositionNearestToPlayer(Vector3 playerPosition)
    {
        Room currentRoom = GameManager.Instance.GetCurrentRoom();

        Grid grid = currentRoom.instantiatedRoom.grid;

        Vector3 nearestSpawnPosition = new Vector3(10000f, 10000f, 0f);

        foreach (Vector2Int spawnPositionGrid in currentRoom.spawnPositionArray)
        {
            //转化成世界坐标后再与传入的玩家世界坐标比较
            Vector3 spawnPositionWorld = grid.CellToWorld((Vector3Int)spawnPositionGrid);

            if (Vector3.Distance(spawnPositionWorld, playerPosition) < Vector3.Distance(nearestSpawnPosition, playerPosition))
            {
                nearestSpawnPosition = spawnPositionWorld;
            }
        }

        return nearestSpawnPosition;
    }
}
