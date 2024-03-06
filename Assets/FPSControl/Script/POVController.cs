using System;
using UnityEngine;

public class POVController : MonoBehaviour
{
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _body;
    [SerializeField] private float _sensX = 5f;
    [SerializeField] private float _sensY = 5f;
    
    private float _yRotation, _xRotation;
    
    // リコイル
    [SerializeField] private float _returnSpeed = 1;
    [SerializeField] private float _snappiness = 6;
    private Vector3 _recoilTargetRotation;
    private Vector3 _currentRecoilRotation;
    private Vector3 _returnTarget;

    private void Update()
    {
        Look();
    }

    private void FixedUpdate()
    {
        ReflectsRecoil();
    }

    private void Look()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X") * _sensX,
            Input.GetAxis("Mouse Y") * _sensY);

        if (mouseInput.magnitude != 0)
        {
            _returnTarget = _currentRecoilRotation;
        }

        _xRotation -= mouseInput.y;
        _yRotation += mouseInput.x;
        _yRotation %= 360; // 絶対値が大きくなりすぎないように

        // 上下の視点移動量をClamp
        Vector3 currentRot = _currentRecoilRotation;

        if (_xRotation + currentRot.x > 90f) // 手動Clamp
        {
            currentRot.x -= _xRotation + currentRot.x - 90;
        }
        else if (_xRotation + currentRot.x < -90f)
        {
            currentRot.x += -90f - (_xRotation + currentRot.x);
        }
        
        // 頭、体の向きの適用
        _head.transform.localRotation = Quaternion.Euler(_xRotation + currentRot.x, 0, 0);
        _body.transform.localRotation = Quaternion.Euler(0, _yRotation + currentRot.y, 0);
    }

    /// <summary>指定したリコイルを設定する</summary>
    public void Recoil(Vector2 recoil)
    {
        _recoilTargetRotation += new Vector3(-recoil.y, recoil.x, 0);
    }

    /// <summary>リコイルを反映させる</summary>
    void ReflectsRecoil()
    {
        _recoilTargetRotation = Vector3.Slerp(_recoilTargetRotation, _returnTarget, _returnSpeed * Time.fixedDeltaTime);
        _currentRecoilRotation = Vector3.Slerp(_currentRecoilRotation, _recoilTargetRotation, _snappiness * Time.fixedDeltaTime);
    }
}