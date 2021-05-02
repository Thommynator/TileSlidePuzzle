using UnityEngine;
using System.Collections.Generic;

public class RasterScript : MonoBehaviour
{
    public static RasterScript current;
    public int dimension;
    public GameObject tilePrefab;
    public GameObject emptyTilePrefab;
    public GameObject winPanel;
    private Vector2 emptyTileCoordinates;
    private List<List<GameObject>> raster;
    private GameObject rasterObject;

    void Awake()
    {
        current = this;
        rasterObject = GameObject.Find("Raster");
        raster = new List<List<GameObject>>();

        for (int y = 0; y < dimension; y++)
        {
            raster.Add(new List<GameObject>());

            for (int x = 0; x < dimension; x++)
            {
                bool isLastTile = (x + 1) * (y + 1) == dimension * dimension;
                GameObject tile;
                if (!isLastTile)
                {
                    tile = Instantiate<GameObject>(tilePrefab, new Vector3(x, -y, 0), Quaternion.identity);
                    tile.transform.SetParent(rasterObject.transform);
                    tile.GetComponent<TileScript>().Initalize(x + y * dimension + 1, new Vector3(x, -y, 0));
                }
                else
                {
                    tile = Instantiate<GameObject>(emptyTilePrefab, new Vector3(x, -y, 0), Quaternion.identity);
                    tile.transform.SetParent(rasterObject.transform);
                    emptyTileCoordinates = new Vector2(x, -y);
                }
                raster[y].Add(tile);
            }
        }
    }

    void Update()
    {
        // if (CheckWinCondition())
        // {
        //     Time.timeScale = 0.0f;
        //     winPanel.SetActive(true);
        // }
    }

    public bool MoveDown()
    {
        return MoveTile(Vector2.down);
    }

    public bool MoveUp()
    {
        return MoveTile(Vector2.up);
    }

    public bool MoveLeft()
    {
        return MoveTile(Vector2.left);

    }

    public bool MoveRight()
    {
        return MoveTile(Vector2.right);
    }

    private bool MoveTile(Vector2 direction)
    {
        try
        {
            // invert, because we're moving the "empty tile", i.e. the dircetion is opposite
            Vector2 targetTileCoordinates = emptyTileCoordinates - direction;
            SwapTiles(targetTileCoordinates, emptyTileCoordinates);
            emptyTileCoordinates -= direction;
        }
        catch (System.Exception)
        {
            Debug.Log("Could not move tile!");
            return false;
        }
        return true;
    }

    private void SwapTiles(Vector2 from, Vector2 to)
    {
        GameObject fromTile = GetTile(from);
        GameObject toTile = GetTile(to);

        SetTile(to, fromTile);
        SetTile(from, toTile);

        float time = 0.4f;
        LeanTween
            .move(fromTile, new Vector3(to.x, to.y, 0), time)
            .setEaseInOutBack();

        LeanTween
            .move(toTile, new Vector3(from.x, from.y, 0), time)
            .setEaseInOutBack();
    }

    private GameObject GetTile(Vector2 coordinateInRaster)
    {
        if (coordinateInRaster.x < 0 || coordinateInRaster.x > dimension || coordinateInRaster.y > 0 || coordinateInRaster.y < -dimension)
        {
            throw new System.Exception("Index out of raster.");
        }
        coordinateInRaster = AbsVector2(coordinateInRaster);
        return raster[Mathf.FloorToInt(coordinateInRaster.y)][Mathf.FloorToInt(coordinateInRaster.x)];
    }

    private void SetTile(Vector2 coordinateInRaster, GameObject tileObject)
    {
        if (coordinateInRaster.x < 0 || coordinateInRaster.x > dimension || coordinateInRaster.y > 0 || coordinateInRaster.y < -dimension)
        {
            throw new System.Exception("Index out of raster.");
        }
        coordinateInRaster = AbsVector2(coordinateInRaster);
        raster[Mathf.FloorToInt(coordinateInRaster.y)][Mathf.FloorToInt(coordinateInRaster.x)] = tileObject;
    }

    private Vector2 AbsVector2(Vector2 vector)
    {
        return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
    }

    public bool CheckWinCondition()
    {
        bool hasWon = true;
        raster.ForEach(row => row.ForEach(tile =>
        {
            if (tile.GetComponent<TileScript>() != null && !tile.GetComponent<TileScript>().IsAtCorrectPosition())
            {
                hasWon = false;
            }
        }));

        Debug.Log("Win Status: " + hasWon);
        return hasWon;
    }

}