using System.Collections;
using UnityEngine;

// 10초 동안 모든 좀비를 멈추게 하는 시계 아이템
public class ClockItem : MonoBehaviour, IItem {
    public void Use(GameObject target) {
        // 아이템 획득 시 바로 파괴되면 코루틴이 멈추므로
        // 비주얼과 충돌을 끄고 자가 파괴를 10초 지연하여 수행
        StartCoroutine(TimeStopRoutine());
    }

    private IEnumerator TimeStopRoutine() {
        // 콜라이더와 렌더러 비활성화하여 획득 연출 처리
        Collider collider = GetComponent<Collider>();
        if (collider != null) collider.enabled = false;

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = false;
        }

        // 시간 정지 발동
        Zombie.isTimeStopped = true;

        // 씬 내의 모든 활성화된 좀비들의 애니메이션을 즉시 멈춤
        Zombie[] zombies = FindObjectsByType<Zombie>(FindObjectsSortMode.None);
        for (int i = 0; i < zombies.Length; i++)
        {
            Animator animator = zombies[i].GetComponent<Animator>();
            if (animator != null)
            {
                animator.speed = 0f;
            }
        }

        // 10초 대기
        yield return new WaitForSeconds(10f);

        // 시간 정지 해제
        Zombie.isTimeStopped = false;

        // 좀비 애니메이션 복원
        zombies = FindObjectsByType<Zombie>(FindObjectsSortMode.None);
        for (int i = 0; i < zombies.Length; i++)
        {
            Animator animator = zombies[i].GetComponent<Animator>();
            if (animator != null)
            {
                animator.speed = 1f;
            }
        }

        // 게임오브젝트 완전 파괴
        Destroy(gameObject);
    }
}
