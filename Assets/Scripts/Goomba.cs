using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;


/// <summary>
/// DAY 5
/// </summary>
public class Goomba : MonoBehaviour
{

    public Sprite flatSprite;

    //
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            // // Lấy thông tin của đối tượng Player từ va chạm
            Player player = collision.gameObject.GetComponent<Player>(); // DAY 5


            if (player.startpower) // DAY 6
            {
                Hit();
            } else if (collision.transform.DotTest(transform, Vector2.down)) // Kiểm tra va chạm có xảy ra từ phía dưới của Goomba không
            {
                Flatten(); // Nếu va chạm từ dưới lên, gọi hàm Flatten() để làm phẳng Goomba
            }
            else // DAY 5
            {
                player.Hit();// Nếu không phải va chạm từ dưới lên, gọi hàm Hit() của Player
            }


        }
    }

    // DAY 5
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem đối tượng va chạm có layer là "Shell" hay không
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit(); //Nếu có va chạm với "Shell", gọi hàm Hit() để xử lý
        }
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false; // Tắt component AnimatedSprite của Goomba
        GetComponent<DeathAnimation>().enabled = true; // Bật component DeathAnimation để chơi animation khi chết
        Destroy(gameObject, 0.5f); // Hủy đối tượng Goomba sau 0.5 giây
    }

    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false; // Tắt Collider2D của Goomba
        GetComponent<EntityMovement>().enabled = false; // Tắt component EntityMovement để Goomba không di chuyển
        GetComponent<AnimatedSprite>().enabled = false; // Tắt component AnimatedSprite của Goomba
        GetComponent<SpriteRenderer>().sprite = flatSprite; //Đổi sprite của Goomba thành sprite phẳng
        Destroy(gameObject, 3f); // Hủy đối tượng Goomba sau 3 giây
    }
    

}
