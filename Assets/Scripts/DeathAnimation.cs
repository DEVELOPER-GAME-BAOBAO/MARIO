using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;



/// <summary>
///  DAY 5
/// </summary>

public class DeathAnimation : MonoBehaviour
{

    public SpriteRenderer spriteRenderer; // Thành phần SpriteRenderer để hiển thị hình ảnh đối tượng
    public Sprite deadSprite; // hình ảnh sẽ được sử dụng khi đối tượng chết 

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Được gọi khi MonoBehaviour được kích hoạt
    private void OnEnable()
    {
        UpdateSprite(); // Cập nhật hình ảnh của đối tượn
        DisablePhysics(); // Vô hiệu hóa các thành phần vật lý của đối tượng
        StartCoroutine(Animate()); // Bắt đầu hoạt ảnh chết của đối tượng
    }


    // Cập nhật hình ảnh của đối tượng khi chết
    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10; // Đặt thứ tự sắp xếp để đảm bảo hình ảnh chết luôn được hiển thị trên cùng

        if (deadSprite != null)
        {
            spriteRenderer.sprite = deadSprite; // Đặt hình ảnh chết nếu có
        }
    }

    // Vô hiệu hóa tất cả các thành phần vật lý của đối tượng khi chết
    private void DisablePhysics() 
    {
        Collider2D[] colliders = GetComponents<Collider2D>(); // Lấy tất cả các Collider2D từ đối tượng


        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false; // Vô hiệu hóa tất cả các Collider2D
        }

        GetComponent<Rigidbody2D>().isKinematic = true; // Đặt Rigidbody2D thành kinematic để vô hiệu hóa các tính toán vật lý
        //GetComponent<PlayerMovement>().enabled = false;
        //GetComponent<EntityMovement>().enabled = false;*/
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();  // Vô hiệu hóa các thành phần chuyển động của đối tượng nếu có
        EntityMovement entityMovement = GetComponent<EntityMovement>();  // Vô hiệu hóa các thành phần chuyển động của đối tượng nếu có

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if(entityMovement != null)
        {
            entityMovement.enabled = false;
        }
    }

    // Coroutine để quản lý hoạt ảnh chết của đối tượng. Đối tượng sẽ nhảy lên và rơi xuống do trọng lực trong một khoảng thời gian nhất định.
    private IEnumerator Animate()
    {
        float elapsed = 0f; // Thời gian đã trôi qua
        float duration = 3f; // Thời gian hoạt ảnh chết sẽ kéo dài

        float jumpVelocity = 10f; // Vận tốc nhảy ban đầu
        float gravity = -36f; // Trọng lực

        Vector3 velocity = Vector3.up * jumpVelocity; // Vận tốc ban đầu theo hướng lên trên

        while (elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime; // Cập nhật vị trí đối tượng dựa trên vận tốc
            velocity.y += gravity * Time.deltaTime; // Cập nhật vận tốc dựa trên trọng lực
            elapsed += Time.deltaTime; // Cập nhật thời gian đã trôi qua
            yield return null; // Đợi cho khung hình tiếp theo
        }
    }
}
