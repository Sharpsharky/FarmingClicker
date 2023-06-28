namespace FarmingClicker.Generic
{
    using System.Collections.Generic;
    using UnityEngine;

    public class GameObjectPool : MonoBehaviour
    {
        [SerializeField]
        private GameObject gameObjectPrefab;
        [SerializeField]
        private int countToSpawnOnInit = 50;

        private Queue<GameObject> gameObjects = new Queue<GameObject>();

        private void Awake()
        {
            AddGameObject(countToSpawnOnInit);
        }


        public GameObject Get()
        {
            if (gameObjects.Count == 0)
            {
                AddGameObject(1);
            }
            GameObject gameObj = gameObjects.Dequeue();
            gameObj.transform.parent = null;
            gameObj.SetActive(true);
            return gameObj;
        }

        private void AddGameObject(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject gameObj = Instantiate(gameObjectPrefab);
                gameObj.SetActive(false);
                gameObj.transform.parent = transform;
                gameObj.GetComponent<IGameObjectPooled>().Pool = this;
                gameObjects.Enqueue(gameObj);

            }
        }

        public void ReturnToPool(GameObject gameObj)
        {
            gameObj.SetActive(false);
            gameObj.transform.position = new Vector3(0,0,0);
            gameObj.transform.parent = transform;
            gameObjects.Enqueue(gameObj);
        }


    }

    internal interface IGameObjectPooled
    {
        GameObjectPool Pool { get; set; }
    }
}