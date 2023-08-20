using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float speed = 0f;
    private bool isWalking;

    // Update is called once per frame
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * Time.deltaTime * speed;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * speed);
        isWalking = moveDir != Vector3.zero;
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
