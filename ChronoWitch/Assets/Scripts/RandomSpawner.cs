using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private int[] itemRarity;
    public float Radius = 2;


    public void Start()
    {
        SpawnObjectAtRandom();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
            //SpawnObjectAtRandom();
    }

    public void SpawnObjectAtRandom()
    {
        Vector3 randomPos = transform.position + Random.insideUnitSphere*Radius;
        GameObject itemPrefab = GetRandomItem();
        Instantiate(itemPrefab, randomPos, Quaternion.identity);
        Debug.Log("spawn " + itemPrefab);
    }

    private GameObject GetRandomItem()
    {
        int totalRarity = 0;
        foreach (int rarity in itemRarity)
        {
            totalRarity += rarity;
        }

        int randomRarity = Random.Range(0, totalRarity);
        int currentRarity = 0;

        for (int i = 0; i < itemPrefabs.Length; i++)
        {
            currentRarity += itemRarity[i];
            if (randomRarity < currentRarity)
            {
                return itemPrefabs[i];
            }
        }

        return itemPrefabs[0]; // fallback
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }
}
