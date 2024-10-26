using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowMouse : MonoBehaviour
{
    private bool _chasing = false;
    private float timer = 5f;
    private Rigidbody2D _rb;
    private Vector3 _mousePosition;
    private Vector2 _position;
    private Animator _floater;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _position = this.GetComponent<RectTransform>().position;
        _floater = this.GetComponent<Animator>();
        _floater.enabled = false;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.F))
            _chasing = !_chasing;
        if (_chasing)
        {
            _floater.enabled = false;
            Camera.main.transform.position = Input.mousePosition;
            _mousePosition = Input.mousePosition;
            _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
            _position = Vector2.Lerp(transform.position, _mousePosition, 0.1f);
            if (Input.GetKey(KeyCode.E))
            {
                if (this.transform.localScale.x < 2.5f)
                    this.transform.localScale += new Vector3(0.04f, 0.01f, 0.01f);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                if (this.transform.localScale.x > 0.8f)
                    this.transform.localScale -= new Vector3(0.04f, 0.01f, 0.01f);
            }
        }
        else if (timer <= 0)
            _floater.enabled = true;
    }

    private void FixedUpdate() 
    {
        _rb.MovePosition(_position);
    }
}
