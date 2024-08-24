using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{

    // DAY 4
    public float speed = 1f;
    public Vector2 direction = Vector2.left;

    private Vector2 velocity;
    private new Rigidbody2D rigidbody; // Tham chiếu đến thành phần Rigidbody2D của đối tượng

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();  // Lấy thành phần Rigidbody2D từ đối tượng
        enabled = false; // Vô hiệu hóa script này khi đối tượng được tạo
    }
    private void OnBecameVisible() // kích hoạt camera và fixupdate and update // Được gọi khi đối tượng trở nên hiển thị trong camera
    {
        enabled = true; // Kích hoạt script này
    }

    private void OnBecameInvisible() // Được gọi khi đối tượng không còn hiển thị trong camera
    {
        enabled= false; // Vô hiệu hóa script này
    }

    private void OnEnable() // Được gọi khi MonoBehaviour được kích hoạt. Gọi WakeUp trên Rigidbody2D để đảm bảo rằng nó đang hoạt động.
    {
        rigidbody.WakeUp();
    }
    private void OnDisable() //  Được gọi khi MonoBehaviour bị vô hiệu hóa. Đặt vận tốc của Rigidbody2D về 0 và gọi Sleep để đưa nó vào trạng thái nghỉ. 
    {
        rigidbody.velocity = Vector2.zero; // Đặt vận tốc của Rigidbody2D về 0
        rigidbody.Sleep(); // Đưa Rigidbody2D vào trạng thái nghỉ
    }
    //tốc độ left-right Goomba
    private void FixedUpdate()
    { 
        velocity.x = direction.x * speed; // Tính toán vận tốc theo hướng x
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime; //project setting --> Physics2D // Cộng thêm trọng lực vào vận tốc theo hướng y


        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime); // Di chuyển đối tượng dựa trên vận tốc

        if (rigidbody.Raycast(direction))// Kiểm tra va chạm theo hướng di chuyển
        {
            direction = -direction; // Đảo ngược hướng di chuyển nếu có va chạm
        }

        if (rigidbody.Raycast(Vector2.down)) // Kiểm tra va chạm theo hướng xuống
        {
            velocity.y = Mathf.Max(velocity.y, 0f); // Đặt vận tốc y tối thiểu là 0 nếu có va chạm theo hướng xuống
        }
    }

}
