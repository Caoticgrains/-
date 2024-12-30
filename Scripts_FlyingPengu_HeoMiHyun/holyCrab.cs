using UnityEngine;

public class holyCrab : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void LaunchCrab(Vector2 direction, float shotForce)
    {
        _rigidbody2D.velocity = Vector2.zero; // 이전 속도 초기화
        _rigidbody2D.AddForce(direction * shotForce, ForceMode2D.Impulse); // 던지는 힘 적용
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌 처리 (예: 목표에 맞추면 점수 증가 등)
        if (collision.gameObject.CompareTag("Target"))
        {
            Debug.Log("목표를 맞췄습니다!");
            // 추가적인 처리 (점수 증가, 애니메이션 등)
        }
    }
}