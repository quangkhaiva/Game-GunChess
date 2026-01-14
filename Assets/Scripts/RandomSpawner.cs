using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; // Th? vi?n A* Pathfinding

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // M?ng ch?a các Prefab quái
    public GameObject warningPrefab; // Prefab d?u X ??
    public int enemiesPerWave = 3; // S? l??ng quái spawn m?i ??t
    public float spawnInterval = 5f; // Th?i gian gi?a các l?n spawn
    public float warningDuration = 2f; // Th?i gian hi?n d?u X ?? tr??c khi quái xu?t hi?n

    private GridGraph gridGraph;

    private void Start()
    {
        // L?y tham chi?u ??n GridGraph
        gridGraph = AstarPath.active.data.gridGraph;

        // B?t ??u spawn quái
        InvokeRepeating(nameof(SpawnEnemies), 2f, spawnInterval);
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            // L?y v? trí ng?u nhiên trên b?n ??
            Vector3 randomPosition = GetRandomPositionOnMap();

            // Hi?n d?u X ?? và spawn quái sau 2 giây
            StartCoroutine(ShowWarningAndSpawn(randomPosition));
        }
    }

    private IEnumerator ShowWarningAndSpawn(Vector3 position)
    {
        // Hi?n d?u X ??
        GameObject warning = Instantiate(warningPrefab, position, Quaternion.identity);

        // ??i trong 2 giây
        yield return new WaitForSeconds(warningDuration);

        // Ch?n ng?u nhiên lo?i quái
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);

        // Spawn quái t?i v? trí
        Instantiate(enemyPrefabs[randomEnemyIndex], position, Quaternion.identity);

        // Xóa d?u X ??
        Destroy(warning);
    }

    private Vector3 GetRandomPositionOnMap()
    {
        // Danh sách các node kh? d?ng (walkable)
        var walkableNodes = new List<GraphNode>();

        // Duy?t qua t?t c? các node trong GridGraph
        for (int x = 0; x < gridGraph.width; x++)
        {
            for (int z = 0; z < gridGraph.depth; z++)
            {
                var node = gridGraph.GetNode(x, z);

                // Ki?m tra n?u node là walkable
                if (node.Walkable)
                {
                    walkableNodes.Add(node);
                }
            }
        }

        // Ch?n ng?u nhiên m?t node t? danh sách các node kh? d?ng
        if (walkableNodes.Count > 0)
        {
            var randomNode = walkableNodes[Random.Range(0, walkableNodes.Count)];

            // Chuy?n ??i node sang v? trí trong th? gi?i
            return (Vector3)randomNode.position;
        }

        // N?u không có node kh? d?ng, tr? v? v? trí m?c ??nh (0, 0, 0)
        return Vector3.zero;
    }
}
