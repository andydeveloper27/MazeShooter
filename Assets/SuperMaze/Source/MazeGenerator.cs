using UnityEngine;
using System.Collections;

namespace SuperMaze {

    public class MazeGenerator : MonoBehaviour
    {

        // public variables
        public int cellsWide = 5;
        public int cellsDeep = 5;
        public float cellSize = 1.0f;
        public float wallThickness = 0.2f;
        public float wallHeight = 1.0f;
        public bool floorOn = true;
        public bool ceilingOn = false;
        public Material wallMaterial;
        public Material floorMaterial;
        public Material ceilingMaterial;
        public float textureScale = 1.0f;

        // private variables
        private int[,] grid;
        private bool[,] visited;
        private int width;
        private int depth;

        // Initialise
        void Start()
        {
            // optional seed value for the random generation
            // if seed == 0, then the seed will be randomised
            int seed = 0;
            // calling main Maze Generation Function
            GenerateMaze(seed);
        }

        // Main Maze Generation Function
        public void GenerateMaze(int seed)
        {
            if (seed != 0) Random.seed = seed;
            width = Mathf.Max(width, 1);
            depth = Mathf.Max(depth, 1);
            width = cellsWide * 2 + 1;
            depth = cellsDeep * 2 + 1;
            grid = new int[width, depth];
            visited = new bool[width, depth];
            // arraylist of cells that can be returned to
            ArrayList validCells = new ArrayList();
            // generate
            int currentX = 1;
            int currentZ = 1;
            grid[currentX, currentZ] = 1;
            // drunken walk algorithm
            int its = 0;
            while (!AllVisited())
            {
                its++;
                // set current cell as visited
                visited[currentX, currentZ] = true;
                // end walk if deadend
                if (UnvisitedNeighbours(currentX, currentZ) > 0)
                {
                    // add current cell to list of cells that still have unvisited neighbours
                    validCells.Add(new Vector2(currentX, currentZ));
                }
                else
                {
                    for (int i = validCells.Count - 1; i >= 0; i--)
                    {
                        Vector2 cell = (Vector2)validCells[i];
                        // jump current position to a cell that still has unvisited neighbours
                        if (UnvisitedNeighbours(Mathf.RoundToInt(cell.x), Mathf.RoundToInt(cell.y)) > 0)
                        {
                            currentX = Mathf.RoundToInt(cell.x);
                            currentZ = Mathf.RoundToInt(cell.y);
                        }
                        else
                        {
                            validCells.RemoveAt(i);
                        }
                    }
                }
                // store 'old' position
                int oldX = currentX;
                int oldZ = currentZ;
                // determine where to go next
                if (Random.value < 0.5f)
                {
                    int stepX = 2;
                    if (Random.value < 0.5f) stepX = -2;
                    currentX += stepX;
                }
                else
                {
                    int stepZ = 2;
                    if (Random.value < 0.5f) stepZ = -2;
                    currentZ += stepZ;
                }
                currentX = Mathf.Clamp(currentX, 1, width - 2);
                currentZ = Mathf.Clamp(currentZ, 1, depth - 2);
                // destroy current cell
                grid[currentX, currentZ] = 1;
                // destroy wall
                if (!visited[currentX, currentZ])
                {
                    int wallX = (oldX + currentX) / 2;
                    int wallZ = (oldZ + currentZ) / 2;
                    grid[wallX, wallZ] = 1;
                }
            }

            // build
            if (floorOn)
            {
                GameObject floor = MakeFloor();
            }
            if (ceilingOn)
            {
                GameObject ceiling = MakeFloor();
                ceiling.transform.position += new Vector3(0, wallHeight + 0.2f, 0);
                if (ceilingMaterial != null) ceiling.GetComponent<Renderer>().material = ceilingMaterial;
            }
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                {
                    // calc centre of cell
                    float xPos = x * 0.5f * cellSize;
                    float zPos = z * 0.5f * cellSize;
                    // create block dimensions
                    float blockWidth = cellSize - wallThickness;
                    if (x % 2 == 0) blockWidth = wallThickness;
                    float blockDepth = cellSize - wallThickness;
                    if (z % 2 == 0) blockDepth = wallThickness;
                    // make block
                    if (grid[x, z] == 0)
                    {
                        MakeBlock(new Vector3(xPos, wallHeight * 0.5f, zPos), new Vector3(blockWidth, wallHeight, blockDepth));
                    }
                }

            }
        }


        // check to see if all cells have been visited
        bool AllVisited()
        {
            int visitedCells = 0;
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                {
                    if (x % 2 == 0 || z % 2 == 0) visited[x, z] = true;
                    if (!visited[x, z])
                    {
                        return false;
                    }
                    visitedCells++;
                }
            }
            return true;
        }


        // count unvisited neighbours
        int UnvisitedNeighbours(int x, int z)
        {
            int unvisitedNeighbours = 0;
            if (x > 1)
            {
                if (!visited[x - 2, z]) unvisitedNeighbours++;
            }
            if (x < width - 2)
            {
                if (!visited[x + 2, z]) unvisitedNeighbours++;
            }
            if (z > 1)
            {
                if (!visited[x, z - 2]) unvisitedNeighbours++;
            }
            if (z < depth - 2)
            {
                if (!visited[x, z + 2]) unvisitedNeighbours++;
            }
            return unvisitedNeighbours;
        }


        // make a floor
        GameObject MakeFloor()
        {
            // make the floor
            GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.transform.parent = transform;
            float sizeX = (cellSize * (width - 1));
            float sizeZ = (cellSize * (depth - 1));
            float posY = -0.1f;
            floor.transform.localScale = new Vector3(sizeX * 0.5f, 0.2f, sizeZ * 0.5f);
            //floor.transform.rotation = Quaternion.Euler(90, 0, 0);
            if (floorMaterial != null) floor.GetComponent<Renderer>().material = floorMaterial;
            floor.transform.localPosition = new Vector3(sizeX * 0.25f, posY, sizeZ * 0.25f);
            // UV map
            WorldUvs(floor);
            // return
            return floor;
        }


        // make a wall
        void MakeBlock(Vector3 pos, Vector3 size)
        {
            // make the block
            GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
            block.transform.parent = transform;
            block.transform.localPosition = pos;
            block.transform.localScale = size;
            if (wallMaterial != null) block.GetComponent<Renderer>().material = wallMaterial;

            WorldUvs(block);
        }

        // remap UVs of a mesh to world coordinates
        void WorldUvs(GameObject go)
        {

            Mesh mesh = go.transform.GetComponent<MeshFilter>().mesh;
            Vector2[] uvs = new Vector2[mesh.uv.Length];
            uvs = mesh.uv;
            Vector2[] newsUv = new Vector2[mesh.uv.Length];
            Vector3 size = go.transform.localScale;
            Vector3 pos = go.transform.position;

            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                // up or down
                if (Mathf.Abs(mesh.normals[i].y) > 0.5f)
                {
                    newsUv[i] = new Vector2(textureScale * (mesh.vertices[i].x * size.x + pos.x), textureScale * (mesh.vertices[i].z * size.z + pos.z));
                }
                // left or right
                if (Mathf.Abs(mesh.normals[i].x) > 0.5f)
                {
                    newsUv[i] = new Vector2(textureScale * (mesh.vertices[i].z * size.z + pos.z), textureScale * (mesh.vertices[i].y * size.y + pos.y));
                }
                // front or back
                if (Mathf.Abs(mesh.normals[i].z) > 0.5f)
                {
                    newsUv[i] = new Vector2(textureScale * (mesh.vertices[i].x * size.x + pos.z), textureScale * (mesh.vertices[i].y * size.y + pos.y));
                }
            }

            mesh.uv = newsUv;
        }

        // destroy - call to delete the entire maze
        public void DestroyMaze()
        {
            for (int i = 0; i < transform.childCount; i++) {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

    }

}