using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Person

{
    public PersonData[] stranger;

    public string selfAloneFlag;

    public string retCode;
}


[System.Serializable]
public class PersonData
{
    public string userName;

    public string nickName;

    public float lastX;

    public float lastY;

    public float lastZ;

    public float lastAngleX;

    public float lastAngleY;

    public float lastAngleZ;

    public string mimsId;

    public string userId;

    public string avatar;

    public string aloneFlag;

}

[System.Serializable]
public class PlayerPos
{
    public float x;

    public float y;

    public float z;

    public float ax;

    public float ay;

    public float az;

    public string sceneid;

    public string userId;
}


[System.Serializable]
public class PlayerData
{
    public PersonData selfInfo;

    public string retCode;
}