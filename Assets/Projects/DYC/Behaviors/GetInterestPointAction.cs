using LastWizard;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniBT;

public class GetInterestPointAction : Action
{
    public int size;

    private Transform transform;
    private Hero hero;
    // public GameObject textMesh;

    // Start is called before the first frame update
    /*void Start()
    {
        //StartCoroutine(GetDensityTest(size, 1));
    }*/

    // Update is called once per frame
    /*void Update()
    {
        Debug.Log(GetDensity(size));
    }*/

    public override void Awake()
    {
        transform = gameObject.transform;
        hero = gameObject.GetComponent<Hero>();
    }

    protected override Status OnUpdate()
    {
        Vector3 dir = (Vector3)GetDensity(size) - transform.position;
        transform.position += dir.normalized * Time.deltaTime * 3;
        return Status.Success;
    }

    /*Vector2 FindDensityCenter(List<Enemy> enemies, float size)
    {
        Dictionary<Vector2Int, int> gridCounts = new Dictionary<Vector2Int, int>();
        foreach (Enemy enemy in enemies)
        {
            Vector2Int gridPos = new Vector2Int(
                Mathf.FloorToInt(enemy.transform.position.x / size), 
                Mathf.FloorToInt(enemy.transform.position.y / size));

            if (gridCounts.ContainsKey(gridPos)) 
                gridCounts[gridPos]++;
            else
                gridCounts[gridPos] = 1;
        }

        Vector2Int densityCenter = gridCounts.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

        return new Vector2(
            densityCenter.x * size + size / 2,
            densityCenter.y * size + size / 2);
    }*/

    /*IEnumerator GetDensityTest(float size, float freq)
    {
        while (true)
        {
            List<Enemy> enemies = GameObject.FindObjectsOfType<Enemy>().ToList();
            List<EXP> exps = GameObject.FindObjectsOfType<EXP>().ToList();

            // return if one of the list is empty
            if (exps.Count == 0 || enemies.Count == 0) yield return null;

            // the dictionary to store grid coordinate and density
            Dictionary<Vector2Int, int> grids = new Dictionary<Vector2Int, int>();

            // interate through all enemies and set the density
            foreach (Enemy enemy in enemies)
            {
                Vector2Int gridPos = new Vector2Int(
                    Mathf.FloorToInt(enemy.transform.position.x / size),
                    Mathf.FloorToInt(enemy.transform.position.y / size));

                if (grids.ContainsKey(gridPos))
                    grids[gridPos]--;
                else
                    grids[gridPos] = -1;
            }

            // interate through all exps and set the density
            foreach (EXP exp in exps)
            {
                Vector2Int gridPos = new Vector2Int(
                    Mathf.FloorToInt(exp.transform.position.x / size),
                    Mathf.FloorToInt(exp.transform.position.y / size));

                if (grids.ContainsKey(gridPos))
                    grids[gridPos]++;
                else
                    grids[gridPos] = 1;
            }

            foreach (var grid in grids)
            {
                Vector2 gridCenter = new Vector2(grid.Key.x * size + size / 2, grid.Key.y * size + size / 2);
                GameObject mesh = Instantiate(textMesh, (Vector3)gridCenter + new Vector3(0, 0, -10), Quaternion.identity);
                mesh.GetComponent<TextMesh>().text = grid.Value.ToString();
                Destroy(mesh, freq);
            }

            yield return new WaitForSeconds(freq);
        }
    }*/

    Vector2 GetDensity(float size)
    {
        Vector2 result = Vector2.zero;

        List<Enemy> enemies = GameObject.FindObjectsOfType<Enemy>().ToList();
        List<EXP> exps = GameObject.FindObjectsOfType<EXP>().ToList();
        List<HP> hps = GameObject.FindObjectsOfType<HP>().ToList();

        // return if one of the list is empty
        if (exps.Count == 0 || enemies.Count == 0) return result;

        // the dictionary to store grid coordinate and density
        Dictionary<Vector2Int, int> grids = new Dictionary<Vector2Int, int>();

        // interate through all enemies and set the density
        foreach (Enemy enemy in enemies)
        {
            Vector2Int gridPos = new Vector2Int(
                Mathf.FloorToInt(enemy.transform.position.x / size),
                Mathf.FloorToInt(enemy.transform.position.y / size));

            if (grids.ContainsKey(gridPos))
                grids[gridPos]--;
            else
                grids[gridPos] = -1;
        }

        // interate through all exps and set the density
        foreach (EXP exp in exps)
        {
            Vector2Int gridPos = new Vector2Int(
                Mathf.FloorToInt(exp.transform.position.x / size),
                Mathf.FloorToInt(exp.transform.position.y / size));

            if (grids.ContainsKey(gridPos))
                grids[gridPos]++;
            else
                grids[gridPos] = 1;
        }
       
        foreach (HP hp in hps)
        {
            float hpPrecent = Global.Hp.Value / Global.MaxHp.Value;
            if (hpPrecent >= 1) break;
            Vector2Int gridPos = new Vector2Int(
                Mathf.FloorToInt(hp.transform.position.x / size),
                Mathf.FloorToInt(hp.transform.position.y / size));

            if (grids.ContainsKey(gridPos))
                grids[gridPos]+= (hpPrecent > 0.5f) ? 2 : 99;
            else
                grids[gridPos] = (hpPrecent > 0.5f) ? 2 : 99 ;

        }

        int highestDensity = 0;
        foreach (var grid in grids)
        {
            if (grid.Value > highestDensity) 
            {
                highestDensity = grid.Value;
                result = new Vector2(grid.Key.x * size + size / 2, grid.Key.y * size + size / 2);
            }
        }

        return result;
    }
}
