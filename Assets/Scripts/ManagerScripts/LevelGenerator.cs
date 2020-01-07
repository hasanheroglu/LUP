using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour {

    public Texture2D map;
    public ColorToPrefab[] colorMappings;

    private Camera main;
    private GameManager gameManager;
    GameObject go;
    private string level;
    private bool colliderInitiated = false;
    private int cubeCount = 0;
    private float colliderScale = 0.5f;


    // Use this for initialization
    private void Awake () {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        map = Resources.Load("LevelTextures/Level" + SceneManager.GetActiveScene().buildIndex.ToString(), typeof(Texture2D)) as Texture2D;
        colorMappings[2].prefab = Resources.Load("Prefabs/Balls/" + gameManager.selectedBall, typeof(GameObject)) as GameObject;
        main = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        //main.orthographicSize = (Mathf.Floor((map.width / 2) - 1) * 2)*1f;
        //main.orthographicSize = 16f;
        Debug.Log(main.aspect);
        main.orthographicSize = ((9f/16f)*14f)/main.aspect;
        if(main.orthographicSize < 13.5f){
            main.orthographicSize = 13.5f;
        }
        main.transform.position = new Vector3(7.5f, 7.5f, -16f);
        main.transform.Rotate(-90, 0, 0);
        InstantiateLight();
        GenerateLevel();
	}

    void GenerateLevel(){
        for (int x = 0; x < map.width; x++){
            for (int y = 0; y < map.height; y++){
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y){
        Color pixelColor = map.GetPixel(x, y);
        foreach (ColorToPrefab colorMapping in colorMappings){
            if(colorMapping.color.Equals(pixelColor)){
                
                if(pixelColor == Color.black){
                    cubeCount++;
                    if(!colliderInitiated){
                        go = new GameObject();
                        go.AddComponent<PolygonCollider2D>();
                        go.AddComponent<Rigidbody2D>();
                        go.GetComponent<Rigidbody2D>().gravityScale = 0;
                        go.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

                        go.transform.position = new Vector3(0, 0, 0);
                        colliderInitiated = true;
                    }
                    go.GetComponent<PolygonCollider2D>().pathCount = cubeCount;
                    Vector2[] cube = new Vector2[4];
                    cube[0] = new Vector2(x - colliderScale, y - colliderScale);
                    cube[1] = new Vector2(x + colliderScale, y - colliderScale);
                    cube[2] = new Vector2(x + colliderScale, y + colliderScale);
                    cube[3] = new Vector2(x - colliderScale, y + colliderScale);
                    go.GetComponent<PolygonCollider2D>().SetPath(cubeCount - 1, cube);
                }


                Instantiate(colorMapping.prefab, new Vector3(x, y, 0), colorMapping.prefab.transform.rotation);

            }
        }

    }

    private void InstantiateLight(){
        GameObject lightPrefab;
        lightPrefab = Resources.Load("Prefabs/LevelItems/Directional Light", typeof(GameObject)) as GameObject;
        Instantiate(lightPrefab, lightPrefab.transform.position, lightPrefab.transform.rotation);
    }
}
