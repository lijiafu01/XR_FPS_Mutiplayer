using UnityEngine;

public class Test : MonoBehaviour
{
    public float moveSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        // Di chuyển ô tô theo trục x và z
        float moveHorizontal = Input.GetAxis("Horizontal");//A D move
        float moveVertical = Input.GetAxis("Vertical");//WS move

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
