using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    private Animator anim;
    private PhotonView view;

    private Vector3 initialScale;

    void Start()
    {
        anim = GetComponent<Animator>();
        view = GetComponent<PhotonView>();

        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            Move();
        }
    }

    void Move()
    {
        Vector2 moveInput = new Vector2(x: Input.GetAxisRaw("Horizontal"), y: Input.GetAxisRaw("Vertical"));
        Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
        transform.position += (Vector3)moveAmount;

        if (moveAmount != Vector2.zero)
        {
            anim.SetBool(name: "Walk", value: true);
        } else
        {
            anim.SetBool(name: "Walk", value: false);
        }

        transform.localScale = moveInput.x switch
        {
            < 0 => new Vector3(-initialScale.x, initialScale.y, initialScale.z),
            > 0 => new Vector3(initialScale.x, initialScale.y, initialScale.z),
            _ => transform.localScale
        };
    }
}
