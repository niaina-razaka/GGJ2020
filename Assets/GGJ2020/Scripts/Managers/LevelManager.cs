using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : GameManager
{
    [Header("Level Manager")]
    public float animationDelay = 0.15f;
    public float blockSpacing = 1.28f;
    [Range(0,1)]
    public float highBlockPercent = 0.85f;
    public int blockDistanceDestroyer = 50;
    public bool linearPopBlock = true;
    public TextAsset matrixAsset;
    public Camera cam;
    public Transform parentBlocks;
    public Transform startBlock;
    public Transform endBlock;
    public WorldCube prefabBlock;
    public List<WorldCube> cubes;
    
    public BlockMatrix blockMatrix = new BlockMatrix();
    public BlockMatrix0 blockMatrix0 = new BlockMatrix0();

    private Vector3 startPos;
    private float t_waitPopBlock = 1;
    private List<int[]> wallEndLevel = new List<int[]>()
        {
            new int[]{ 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            new int[]{ 0, 1, 1, 1, 0, 0, 0, 0, 0 },
            new int[]{ 0, 0, 1, 1, 0, 0, 0, 0, 0 },
            new int[]{ 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            new int[]{ 0, 0, 0, 0, 1, 0, 0, 0, 0 },
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[]{ 0, 0, 0, 1, 1, 0, 0, 0, 0 },
            new int[]{ 0, 0, 0, 1, 1, 0, 0, 0, 0 },
            new int[]{ 0, 0, 0, 1, 1, 0, 0, 0, 0 }
        };

    private List<List<int[]>> Blocks;

    new private void Start()
    {
        base.Start();
        cubes = FindObjectsOfType<WorldCube>().ToList();
        startPos = startBlock.transform.position;
        Blocks = blockMatrix.Blocks.ToList();
        Blocks.AddRange(blockMatrix0.Blocks);
        BossDefeated();
    }

    new private void Update()
    {
        base.Update();
        t_waitPopBlock -= Time.deltaTime;
        //if (t_waitPopBlock <= 0)
        {
            GenerateLevel();
            t_waitPopBlock = 0;
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 110, 25), "BONE"))
        {
            ChangeElement(HumanPart.BONE);
        }
        if (GUI.Button(new Rect(0, 25, 110, 25), "BLOOD_VESSEL"))
        {
            ChangeElement(HumanPart.BLOOD_VESSEL);
        }
        if (GUI.Button(new Rect(0, 50, 110, 25), "BRAIN"))
        {
            ChangeElement(HumanPart.BRAIN);
        }
        if (GUI.Button(new Rect(0, 75, 110, 25), "HEART"))
        {
            ChangeElement(HumanPart.HEART);
        }
        if (GUI.Button(new Rect(0, 110, 200, 25), "BOOS DEFEATED"))
        {
            BossDefeated();
            //comment mety ve?
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 from = endBlock.transform.position;
        from.x = endBlock.transform.position.x - blockDistanceDestroyer;
        from.z = 0;
        Vector3 to = endBlock.transform.position;
        to.z = 0;
        float k = 0.2f;
        for(int i = 0; i <= 6; i++)
        {
            Gizmos.DrawLine(from + new Vector3(0, i*k), to + new Vector3(0, i * k));
        }
    }

    private void GenerateLevel()
    {
        bool mustPopBlock = false;
        //destroy unranged blocks
        foreach (WorldCube c in cubes)
        {
            float distance = Vector2.Distance(c.transform.position, endBlock.position);
            if (distance >= blockDistanceDestroyer)
            {
                cubes.Remove(c);
                DestroyImmediate(c.gameObject);
                break;
            }
        }
        //check mindistance to popup
        if(cubes.Count == 0)
        {
            mustPopBlock = true;
        }
        else
        {
            WorldCube rightBlock = cubes.OrderBy(x => x.transform.position.x).Last();
            if (Vector2.Distance(rightBlock.transform.position, endBlock.position) >= 1 && 
                rightBlock.transform.position.x <= endBlock.position.x)
            {
                mustPopBlock = true;
            }
        }
        if (mustPopBlock)
        {
            if (linearPopBlock)
            {
                WorldCube clone = Instantiate(prefabBlock);
                clone.Element = humanPart;
                clone.name = humanPart.ToString();
                clone.transform.localScale = Vector3.one;
                clone.transform.parent = parentBlocks;
                clone.transform.position = startPos;
                cubes.Add(clone);
                if (Random.value >= highBlockPercent)
                {
                    WorldCube u_clone = Instantiate(prefabBlock);
                    u_clone.Element = humanPart;
                    u_clone.name = humanPart.ToString();
                    u_clone.transform.localScale = Vector3.one;
                    u_clone.transform.parent = parentBlocks;
                    u_clone.transform.position = new Vector3(startPos.x, startPos.y + blockSpacing, 0);
                    cubes.Add(u_clone);
                }
                startPos += new Vector3(blockSpacing, 0, 0);
            }
            else
            {
                PopBlock();
            }
        }
    }

    private void PopBlock()
    {
        int random = Random.Range(0, Blocks.Count);
        List<int[]> matrix = Blocks[random];
        Vector3 pos = new Vector3(startPos.x, startPos.y, startPos.z);
        float elevation = 0;
        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                if (matrix[i][j] == 0)
                {
                    pos += new Vector3(blockSpacing, 0, 0);
                }
                else if (matrix[i][j] == 1)
                {
                    WorldCube clone = Instantiate(prefabBlock);
                    clone.Element = humanPart;
                    clone.name = humanPart.ToString();
                    clone.transform.localScale = Vector3.one;
                    clone.transform.parent = parentBlocks;
                    clone.transform.position = pos;
                    cubes.Add(clone);
                    pos += new Vector3(blockSpacing, 0, 0);
                }
            }
            elevation += blockSpacing;
            pos = new Vector3(startPos.x, elevation, 0);
        }
        startPos += new Vector3(blockSpacing * matrix[0].Length, 0, 0);
    }

    public void BossDefeated()
    {
        Vector3 pos = new Vector3(startPos.x, startPos.y, startPos.z);
        float elevation = 0;
        for(int i = 0; i < wallEndLevel.Count; i++)
        {
            for(int j=0; j < wallEndLevel[i].Length; j++)
            {
                if(wallEndLevel[i][j] == 0)
                {
                    pos += new Vector3(blockSpacing, 0, 0);
                }
                else if (wallEndLevel[i][j] == 1)
                {
                    WorldCube clone = Instantiate(prefabBlock);
                    clone.Element = humanPart;
                    clone.name = humanPart.ToString();
                    clone.transform.localScale = Vector3.one;
                    clone.transform.parent = parentBlocks;
                    clone.transform.position = pos;
                    cubes.Add(clone);
                    pos += new Vector3(blockSpacing, 0, 0);
                }
            }
            elevation += blockSpacing;
            pos = new Vector3(startPos.x, elevation, 0);
        }
        startPos += new Vector3(blockSpacing * wallEndLevel[0].Length, 0, 0);
    }

    public void ChangeElement(HumanPart element)
    {
        humanPart = element;
        //StartCoroutine(DynamicAnimateCubes(humanPart));
    }

    IEnumerator DynamicAnimateCubes(HumanPart element)
    {
        int count = 0;
        while (count < cubes.Count)
        {
            yield return new WaitForSeconds(animationDelay);
            cubes[count].Element = element;
            count++;
        }
    }
}
