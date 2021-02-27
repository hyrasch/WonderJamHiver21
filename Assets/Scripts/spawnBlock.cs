using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class spawnBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> blocksPrefabs; //contient les prefabs des différents blocs
    public float respawnTime = 1.0f;
    private Vector2 screenBound;
    public float pixelPerUnit = 32f;
    void Start()
    {
        screenBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(waveBlocks());
    }

    private void spawnARandomBlock()
    {
        System.Random rnd = new System.Random();
        int index = rnd.Next(blocksPrefabs.Count);
        GameObject block = Instantiate(blocksPrefabs[index]) as GameObject;

        //Random position sur x, dans le futur faire en sorte que ça soit dans une colonne qui spawn
        float positionX = Random.Range(-screenBound.x * 0.9f, screenBound.x * 0.9f);

        block.transform.position = new Vector2(positionX, screenBound.y * 1.5f);

        //random de la rotation
        int rotationFactor = rnd.Next(-3, 3);
        block.transform.Rotate(new Vector3(0, 0, 1), rotationFactor * 90);
        //block.transform.localScale.Scale(new Vector3(0.01f, 0.01f, 0.01f));
        //block.transform.localScale.Scale(new Vector3(0.15f, 0.15f, 1));
    }

   IEnumerator waveBlocks()
    {
        while(true) // à changer en while(inGame)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnARandomBlock();
        }

    }


}
