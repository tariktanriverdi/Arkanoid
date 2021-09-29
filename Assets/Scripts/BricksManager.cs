using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksManager : MonoBehaviour
{
    #region Singleton

    private static BricksManager _instance;
    public static BricksManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
        {

            Destroy(gameObject);

        }
        else
        {
            _instance = this;
        }
    }
    #endregion
    public Sprite[] Sprites;
    public List<int[,]> LevelData { get; set; }
    public int maxCols = 12;
    public int maxRows = 17;
    private GameObject bricksContainer;
    public List<Brick> RemainingBricks { get; set; }
    public int InitialBricksCount { get;  set; }

    public int CurrentLevel = 0;
    public float initialBrickSpawnPositionX = -1.96f;
    public float initialBrickSpawnPositionY = 3.325f;
    public Brick brickPrefab;
    public Color[] BricksColler;
    public float shiftAmountX=0.365F;
    public float shiftAmountY=0.365F;

    private void Start()
    {
        this.bricksContainer = new GameObject("BricksContainer");
        this.RemainingBricks = new List<Brick>();
        this.LevelData = this.LoadLevelData();
        this.GenerateBricks();

    }

    private void GenerateBricks()
    {
        int[,] currentLevelData = this.LevelData[this.CurrentLevel];
        float curretnSpawnX = initialBrickSpawnPositionX;
        float curretnSpawnY = initialBrickSpawnPositionY;
        float zShift = 0;
        for (int row = 0; row < maxRows; row++)
        {
            for (int col = 0; col < maxCols; col++)
            {
                int brickType = currentLevelData[row, col];
                if (brickType > 0)
                {
                    Brick newBrick = Instantiate(brickPrefab, new Vector3(curretnSpawnX, curretnSpawnY, 0.0f - zShift), Quaternion.identity) as Brick;
                    newBrick.Init(bricksContainer.transform,this.Sprites[brickType-1],this.BricksColler[brickType],brickType);
                    this.RemainingBricks.Add(newBrick);
                    zShift+=0.0001f;

                }
                curretnSpawnX+=shiftAmountX;
                if(col+1==this.maxCols){
                 curretnSpawnX=initialBrickSpawnPositionX;
                 
                }


            }
            curretnSpawnY-=shiftAmountY;
        }
        this.InitialBricksCount=this.RemainingBricks.Count;

    }

    private List<int[,]> LoadLevelData()
    {
        TextAsset text = Resources.Load("levels") as TextAsset;
        string[] rows = text.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        List<int[,]> levelsData = new List<int[,]>();
        int[,] currentLevel = new int[maxRows, maxCols];
        int currentRow = 0;
        for (int row = 0; row < rows.Length; row++)
        {
            string line = rows[row];
            if (line.IndexOf("--") == -1)
            {
                string[] bricks = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //Debug.Log(bricks.ToString());
                for (int col = 0; col < bricks.Length; col++)
                {
                    currentLevel[currentRow, col] = int.Parse(bricks[col]);
                }
                currentRow++;
            }
            else
            {
                currentRow = 0;
                levelsData.Add(currentLevel);
                currentLevel = new int[maxRows, maxCols];

            }
        }

        return levelsData;

    }


}


