using Project.Scripts.Level.Settings;
using Project.ThirdParty.ScratchCard.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelSettings m_levelSettings;
        [SerializeField] private Transform m_levelParent;
        [SerializeField] private SpriteRenderer m_levelOverlay;
        [SerializeField] private SpriteRenderer m_levelShadowOverlay;
        [SerializeField] private ScratchCardManager m_scratchCardManager;
        private System.Random m_rng;
        private int m_seed;

        private void OnEnable()
        {
            m_scratchCardManager.Card.OnInitialized += ResizeLevelScratchOverlay;
        }

        private void OnDisable()
        {
            m_scratchCardManager.Card.OnInitialized -= ResizeLevelScratchOverlay;
        }

        public void Generate()
        {
            m_seed = m_levelSettings.RandomSeed ? Random.Range(int.MinValue, int.MaxValue) : m_levelSettings.Seed;
            m_rng = new System.Random(m_seed);

            ClearLevel();
            GenerateGround();
            GenerateFence();
        }

        private void GenerateGround()
        {
            int bw = m_levelSettings.BlankSpace;
            int w = m_levelSettings.Width;
            int h = m_levelSettings.Height;

            int totalW = w + bw * 2;
            int totalH = h + bw * 2;

            for (int x = 0; x < totalW; x++)
            {
                for (int y = 0; y < totalH; y++)
                {
                    bool insideLevel = x >= bw && x < bw + w && y >= bw && y < bw + h;

                    if (!insideLevel)
                    {
                        SpawnTile(m_levelSettings.GroundSettings.Tiles[0].TilePrefab,
                            GridToWorld(x, y, totalW, totalH));
                        continue;
                    }

                    bool isLevelBorder =
                        x == bw ||
                        x == bw + w - 1 ||
                        y == bw ||
                        y == bw + h - 1;

                    SpawnTile(
                        isLevelBorder ? m_levelSettings.GroundSettings.Tiles[0].TilePrefab : PickWeightedGroundTile(),
                        GridToWorld(x, y, totalW, totalH));
                }
            }
        }

        private void GenerateFence()
        {
            GeneratePosts();
            GenerateConnections();
        }

        private void ResizeLevelScratchOverlay(ScratchCard scratchCard)
        {
            float tileSize = m_levelSettings.TileSize;

            int bw = m_levelSettings.BlankSpace;
            int totalW = m_levelSettings.Width + bw * 2;
            int totalH = m_levelSettings.Height + bw * 2;

            float worldW = totalW * tileSize;
            float worldH = totalH * tileSize;

            Vector2 spriteSize = m_levelOverlay.sprite.bounds.size;

            m_levelOverlay.transform.localScale = new Vector3(
                worldW / spriteSize.x,
                worldH / spriteSize.y,
                1);
        }

        private void GeneratePosts()
        {
            int w = m_levelSettings.Width;
            int h = m_levelSettings.Height;

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
                        m_levelSettings.FenceSettings.FencePostPrefab,
                        GridToWorld(x, y)
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
            int w = m_levelSettings.Width;
            int h = m_levelSettings.Height;

            for (int x = 0; x < w - 1; x++)
            {
                Vector3 a = GridToWorld(x, 0);
                Vector3 b = GridToWorld(x + 1, 0);
                SpawnTile(
                    m_levelSettings.FenceSettings.HorizontalConnectionPrefab,
                    (a + b) * 0.5f
                );
            }

            for (int x = 0; x < w - 1; x++)
            {
                Vector3 a = GridToWorld(x, h - 1);
                Vector3 b = GridToWorld(x + 1, h - 1);
                SpawnTile(
                    m_levelSettings.FenceSettings.HorizontalConnectionPrefab,
                    (a + b) * 0.5f
                );
            }
        }

        private void GenerateVerticalConnections()
        {
            int w = m_levelSettings.Width;
            int h = m_levelSettings.Height;

            for (int y = 0; y < h - 1; y++)
            {
                Vector3 a = GridToWorld(0, y);
                Vector3 b = GridToWorld(0, y + 1);
                SpawnTile(
                    m_levelSettings.FenceSettings.VerticalConnectionPrefab,
                    (a + b) * 0.5f
                );
            }

            for (int y = 0; y < h - 1; y++)
            {
                Vector3 a = GridToWorld(w - 1, y);
                Vector3 b = GridToWorld(w - 1, y + 1);
                SpawnTile(
                    m_levelSettings.FenceSettings.VerticalConnectionPrefab,
                    (a + b) * 0.5f
                );
            }
        }

        private Vector3 GridToWorld(int x, int y)
        {
            float tileSize = m_levelSettings.TileSize;

            Vector2 gridCenter = new Vector2(
                (m_levelSettings.Width - 1) * tileSize * 0.5f,
                (m_levelSettings.Height - 1) * tileSize * 0.5f);
            Vector2 worldPos = new Vector2(
                x * tileSize,
                y * tileSize);

            return worldPos - gridCenter + m_levelSettings.Offset;
        }

        private Vector3 GridToWorld(int x, int y, int gridW, int gridH)
        {
            float tileSize = m_levelSettings.TileSize;

            Vector2 gridCenter = new Vector2(
                (gridW - 1) * tileSize * 0.5f,
                (gridH - 1) * tileSize * 0.5f
            );

            Vector2 worldPos = new Vector2(
                x * tileSize,
                y * tileSize
            );

            return worldPos - gridCenter + m_levelSettings.Offset;
        }

        private void SpawnTile(GameObject prefab, Vector3 worldPos)
        {
            GameObject go = Instantiate(prefab, worldPos, Quaternion.identity, m_levelParent);
            go.transform.position = worldPos;
        }

        private GameObject PickWeightedGroundTile()
        {
            float total = 0f;
            foreach (GroundTile t in m_levelSettings.GroundSettings.Tiles)
            {
                total += t.Weight;
            }

            float value = (float)(m_rng.NextDouble() * total);

            foreach (GroundTile groundTile in m_levelSettings.GroundSettings.Tiles)
            {
                value -= groundTile.Weight;
                if (value <= 0f)
                {
                    return groundTile.TilePrefab;
                }
            }

            return m_levelSettings.GroundSettings.Tiles[0].TilePrefab;
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