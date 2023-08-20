using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorScript : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private Animator animator;

    private const string IS_WALKING = "IsWalking";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        animator.SetBool(IS_WALKING, playerController.IsWalking());
    }
}
