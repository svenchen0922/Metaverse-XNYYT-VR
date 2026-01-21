using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KeyRorate2 : MonoBehaviour
{
    float m_time = 0.5f;
    Transform m_trans;

    Vector3 _vec3;
    void Start()
    {
        _vec3.Set(0, 360, 0);


        m_trans = transform;
        MyPingPong(m_trans.localPosition.y, m_trans.localPosition.y + 0.1f);

        transform.DOLocalRotate(_vec3, 2f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }

    private void MyPingPong(float from, float to)
    {
        m_trans.DOLocalMoveY(to, m_time).OnComplete(() => MyPingPong(to, from));
    }

}
