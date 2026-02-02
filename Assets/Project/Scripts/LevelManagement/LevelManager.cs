using UnityEngine;

namespace Project.Scripts.LevelManagement
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelSettings _levelSettings;
        [SerializeField] private Transform _groundTileParent;
        [SerializeField] private Transform _fencePostParent;
        [SerializeField] private Transform _fenceHorizontalConnectionParent;
        [SerializeField] private Transform _fenceVerticalConnectionParent;

        private System.Random _rng;
        private int _seed;

        public void Generate()
        {
            _seed = _levelSettings.RandomSeed ? Random.Range(int.MinValue, int.MaxValue) : _levelSettings.Seed;
            _rng = new System.Random(_seed);

            ClearLevel();
            GenerateGround();
            GenerateFence();
        }

        private void GenerateGround()
        {
            for (int x = 0; x < _levelSettings.Width; x++)
            {
                for (int y = 0; y < _levelSettings.Height; y++)
                {
                    SpawnTile(PickWeightedGroundTile(), GridToWorld(x, y), _groundTileParent);
                }
            }
        }

        private void GenerateFence()
        {
            GeneratePosts();
            GenerateConnections();
        }

        private void GeneratePosts()
        {
            int w = _levelSettings.Width;
            int h = _levelSettings.Height;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    bool isBorder =
                        x == 0 ||
                        x == w - 1 ||
                        y == 0 ||
                        y == h - 1;

                    if (!isBorder)
                        continue;

                    SpawnTile(
                        _levelSettings.FenceSettings.FencePostPrefab,
                        GridToWorld(x, y),
                        _fencePostParent
                    );
                }
            }
        }

        private void GenerateConnections()
        {
            GenerateVerticalConnections();
            GenerateHorizontalConnections();
        }

        private void GenerateHorizontalConnections()
        {
        }

        private void GenerateVerticalConnections()
        {
        }

        private Vector3 GridToWorld(int x, int y)
        {
            float tileSize = _levelSettings.TileSize;

            Vector2 gridCenter = new Vector2(
                (_levelSettings.Width - 1) * tileSize * 0.5f,
                (_levelSettings.Height - 1) * tileSize * 0.5f);
            Vector2 worldPos = new Vector2(
                x * tileSize,
                y * tileSize);

            return worldPos - gridCenter + _levelSettings.Offset;
        }

        private void SpawnTile(GameObject prefab, Vector3 worldPos, Transform parent = null)
        {
            GameObject go = Instantiate(prefab,worldPos,Quaternion.identity,parent);
            go.transform.position = worldPos;
        }

        private GameObject PickWeightedGroundTile()
        {
            float total = 0f;
            foreach (var t in _levelSettings.GroundSettings.Tiles)
            {
                total += t.Weight;
            }

            float value = (float)(_rng.NextDouble() * total);

            foreach (GroundTile groundTile in _levelSettings.GroundSettings.Tiles)
            {
                value -= groundTile.Weight;
                if (value <= 0f)
                {
                    return groundTile.TilePrefab;
                }
            }

            return _levelSettings.GroundSettings.Tiles[0].TilePrefab;
        }

        public void ClearLevel()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}