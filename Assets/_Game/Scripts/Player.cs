using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 12f;
    [SerializeField] float _turnSpeed = 3f;

    Rigidbody _rb = null;

    public GameObject _player;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        TurnPlayer();
    }

    // use forces to build momentum forward/backward
    void MovePlayer()
    {
        // S/Down = -1, W/Up = 1, None = 0. Scale by moveSpeed
        float moveAmountThisFrame = Input.GetAxisRaw("Vertical") * _moveSpeed;
        // combine our direction with our calculated amount
        Vector3 moveDirection = transform.forward * moveAmountThisFrame;
        // apply the movement to the physics object
        _rb.AddForce(moveDirection);
    }
    void TurnPlayer()
    {
        // A/Left = -1, D/Right = 1, None = 0. Scale by turnSpeed
        float turnAmountThisFrame = Input.GetAxisRaw("Horizontal") * _turnSpeed;
        // specify an axis to apply our turn amount (x,y,z) as a rotation
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        // spin the rigidbody
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }

    public void ActivateInvis(GameObject _player)
    {
        _player.SetActive(false);
    }

    public void DeActivateInvis(GameObject _player)
    {
        _player.SetActive(true);
    }

    public void SetMoveSpeed(float newSpeedAdjustment)
    {
        _moveSpeed = newSpeedAdjustment;
        // speed flash
    }

    public void SetShield(GameObject _shield)
    {
        _shield.SetActive(true);

    }

    public void DeActivateShield(GameObject _shield)
    {
        _shield.SetActive(false);
    }

    public void ScaleGrowth(GameObject _player)
    {
        _player.transform.localScale = new Vector2(4f, 4f);

    }

    public void ScaleShrink(GameObject _player)
    {
        _player.transform.localScale = new Vector2(1f, 1f);
    }
}
