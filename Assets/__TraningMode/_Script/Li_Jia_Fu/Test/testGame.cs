using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class testGame : MonoBehaviour
{
    public ConnectionManager ConnectionManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Kiểm tra nếu phím "M" được nhấn xuống
        if (Input.GetKeyDown(KeyCode.A))
        {
            ConnectionManager.JoinRoom();
        }
    }
}
