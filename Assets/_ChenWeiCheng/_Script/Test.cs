using UnityEngine;

public class Test : MonoBehaviour
{
    public float moveSpeed = 5f;

    // 更新被呼叫的次數是每一個影格
    void Update()
    {
        // 根據水平和垂直的輸入來移動汽車
        float moveHorizontal = Input.GetAxis("Horizontal"); // A 和 D 按鍵移動
        float moveVertical = Input.GetAxis("Vertical"); // W 和 S 按鍵移動

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
