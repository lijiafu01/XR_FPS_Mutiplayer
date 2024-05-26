using UnityEngine;
using System.Collections.Generic;
using TraningMode;



public class RandomEnvironmentSpawner : MonoBehaviour
{
    public List<SpawnableObject> objectsToSpawn; // Sửa thành list mới chứa GameObject và tỉ lệ
    public float width = 5; // Độ rộng phạm vi
    public float depth = 5; // Độ sâu phạm vi
    public int spawnCount = 10; // Số lượng object sinh ra tổng cộng
    public Vector2 scaleRange = new Vector2(0.5f, 2f); // Phạm vi scale của object


    [System.Serializable] // Đảm bảo Unity có thể hiển thị cấu trúc này trong Inspector
    public class SpawnableObject
    {
        public GameObject gameObject; // GameObject để spawn
        public float spawnRate = 1f; // Tỉ lệ spawn, cao hơn nghĩa là xuất hiện nhiều hơn
    }
    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // Lựa chọn ngẫu nhiên một object từ list dựa trên tỉ lệ
            float totalRate = 0;
            foreach (var obj in objectsToSpawn)
            {
                totalRate += obj.spawnRate;
            }

            float randomPoint = Random.value * totalRate;
            for (int j = 0; j < objectsToSpawn.Count; j++)
            {
                if (randomPoint < objectsToSpawn[j].spawnRate)
                {
                    SpawnObject(objectsToSpawn[j]);
                    break;
                }
                else
                {
                    randomPoint -= objectsToSpawn[j].spawnRate;
                }
            }
        }
    }

    void SpawnObject(SpawnableObject spawnableObject)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-width, width), 0, Random.Range(-depth, depth));
        Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        float scale = Random.Range(scaleRange.x, scaleRange.y);

        GameObject spawnedObject = Instantiate(spawnableObject.gameObject, spawnPosition, spawnRotation,this.transform);
        spawnedObject.transform.localScale = new Vector3(scale, scale, scale);
        //spawnableObject.gameObject.transform.SetParent(this.gameObject.transform);
    }
}
