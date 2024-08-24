using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    // Start is called before the first frame update

    // Phương thức này được gọi khi có một đối tượng va chạm với Collider của DeathBarrier newu61 IsTrigger được bật
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu đối tượng có tag là "Player"
        if (other.CompareTag("Player"))
        {
            // Tắt đối tượng Player
            other.gameObject.SetActive(false);

            // Gọi phương thức ResetLevel của GameManager sau 3 giây
            GameManager.Instance.ResetLevel(3f);
        }
        else
        {
            // Nếu đối tượng không phải là Player, phá hủy đối tượng đó
            Destroy(other.gameObject);
        }
    }
}
