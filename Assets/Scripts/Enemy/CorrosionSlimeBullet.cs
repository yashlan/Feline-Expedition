using UnityEngine;

public class CorrosionSlimeBullet : MonoBehaviour
{
    public GameObject spawnObject;
    static int order = 0;

    public void SpawnEnemyEvent()
    {
        var slime = Instantiate(spawnObject, transform.position, Quaternion.identity);
        slime.transform.localScale = new Vector3(1.7f, 1.7f, 0);
        order++;
        slime.GetComponent<SpriteRenderer>().sortingOrder = order;

        var enemy = slime.GetComponent<Enemy>();
        enemy.AttackRadius = 100;

        Invoke(nameof(Destroy), 0.05f);

    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
