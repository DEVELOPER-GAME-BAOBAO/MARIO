using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// DAY 5
/// </summary>
public class Koopa : MonoBehaviour
{

    public Sprite shellSprite; // Sprite của Koopa khi vào vỏ
    public float shellSpeed = 12f; // Tốc độ di chuyển của Koopa khi bị đẩy

    private bool shelled;  // Biến xác định xem Koopa có đang ở trong vỏ không
    private bool pushed = false; // Biến xác định xem Koopa có đang bị đẩy không


    // Phương thức được gọi khi có va chạm giữa Koopa và đối tượng khác
    private void OnCollisionEnter2D(Collision2D collision)
    {

        // Kiểm tra nếu Koopa chưa ở trong vỏ và va chạm với đối tượng có tag là "Player"
        if (!shelled && collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>(); // DAY 5

            if (player.startpower) //DAY 6
            {
                Hit();
            } else if (collision.transform.DotTest(transform, Vector2.down)) // Kiểm tra va chạm từ phía dưới
            {
                EnterShell(); // Nếu va chạm từ dưới lên, gọi hàm EnterShell() để Koopa vào vỏ
            }
            else // DAY 5
            {
                player.Hit(); // Nếu không, gọi phương thức Hit() của Player
            }

        }
    }


    // Phương thức được gọi khi có va chạm với Collider2D khác (vd: Player hoặc Shell)
    // Day 5 and đẩy con rùa đi 
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Nếu Koopa đang ở trong vỏ và va chạm với Player
        if (shelled && other.CompareTag("Player"))
        {

            // Nếu Koopa chưa bị đẩy
            if (!pushed)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction); // Gọi hàm PushShell() để đẩy Koopa theo hướng xác định
            }
            else
            {
                Player player = other.GetComponent<Player>();

                if (player.startpower) //DAY 6
                {
                    Hit();
                }
                else
                {
                    player.Hit(); // Nếu Koopa đã bị đẩy, gọi phương thức Hit() của Player
                }
            }
        } else if (!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell")) // death đẩy rùa dính goomba // DAY 5
        {
            Hit(); // Nếu Koopa không ở trong vỏ và va chạm với đối tượng ở layer "Shell", gọi phương thức Hit()
        }
    }

    // Phương thức xử lý khi Koopa vào vỏ
    private void EnterShell()
    {
        shelled = true; // Đặt biến shelled thành true

        // Tắt các component không cần thiết khi Koopa ở trong vỏ
        //GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false; 
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite; // Đổi sprite của Koopa thành sprite vỏ

    }

    // Phương thức xử lý khi Koopa bị đẩy
    private void PushShell(Vector2 direction)
    {
        pushed = true; // Đặt biến pushed thành true

        GetComponent<Rigidbody2D>().isKinematic = false; // Đặt Rigidbody2D thành không kinematic để Koopa có thể di chuyển

        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized; // Đặt hướng di chuyển của Koopa
        movement.speed = shellSpeed; // Đặt tốc độ di chuyển của Koopa
        movement.enabled = true; // Bật component EntityMovement

        gameObject.layer = LayerMask.NameToLayer("Shell"); //Hiện tượng đẩy theo Goomba cần khắc phục lỗi Goomba --> private void OnTriggerEnter2D(Collider2D other) and Hit để khắc phục // Đặt layer của Koopa thành "Shell"
    }


    // Phương thức xử lý khi Koopa bị "hit"
    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;// Tắt component AnimatedSprite của Koopa
        GetComponent<DeathAnimation>().enabled = true; // Bật component DeathAnimation để chơi animation khi chết
        Destroy(gameObject, 3f); // Hủy đối tượng Koopa sau 3 giây
    }

    // Phương thức được gọi khi Koopa biến mất khỏi khung hình camera
    private void OnBecameInvisible() // rùa sẽ biến mất trong khung hình camera
    {
        if (pushed)
        {
            Destroy(gameObject);
        }
    }
}
