using UnityEngine;

public class CorrosionSlimeBullet : MonoBehaviour
{
    public GameObject spawnObject;

    public void SpawnEnemyEvent()
    {
        var slime = Instantiate(spawnObject, transform.position, Quaternion.identity);
        slime.transform.localScale = new Vector3(1.7f, 1.7f, 0);
        slime.GetComponent<SpriteRenderer>().sortingOrder += 1;

        var enemy = slime.GetComponent<Enemy>();
        enemy.AttackRadius = 100;

        Destroy(gameObject);
    }
}
