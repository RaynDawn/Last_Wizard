using UnityEngine;
using QFramework;
using UnityEngine.Tilemaps;

namespace LastWizard
{
	public partial class RepeatTileController : ViewController
	{
		private Tilemap mUp;
        private Tilemap mDown;
        private Tilemap mLeft;
        private Tilemap mRight;
        private Tilemap mUpLeft;
		private Tilemap mDownLeft;
        private Tilemap mUpRight;
		private Tilemap mDownRight;
        private Tilemap mCenter;

        private int AreaX = 0;
        private int AreaY = 0;
        void Start()
		{
            // Code Here
            Tilemap.CompressBounds();
            CreateTilemaps();
            UpdatePosition();
		}

        private void Update()
        {
            if(Player.Default && Time.frameCount % 60 == 0)
            {
                var cellPos = Tilemap.layoutGrid.WorldToCell(Player.Default.transform.Position());
                AreaX = cellPos.x / Tilemap.size.x;
                AreaY = cellPos.y / Tilemap.size.y;
                UpdatePosition();
            }
        }

        void CreateTilemaps()
		{
			mUp = Tilemap.InstantiateWithParent(transform);
            mDown = Tilemap.InstantiateWithParent(transform);
            mLeft = Tilemap.InstantiateWithParent(transform);
            mRight = Tilemap.InstantiateWithParent(transform);
            mUpLeft = Tilemap.InstantiateWithParent(transform);
            mUpRight = Tilemap.InstantiateWithParent(transform);
            mDownLeft = Tilemap.InstantiateWithParent(transform);
            mDownRight = Tilemap.InstantiateWithParent(transform);
            mCenter = Tilemap;
        }

        void UpdatePosition()
        {
            mUp.Position(new Vector2 (AreaX * Tilemap.size.x, (AreaY + 1) * Tilemap.size.y));
            mDown.Position(new Vector2(AreaX * Tilemap.size.x, (AreaY - 1) * Tilemap.size.y));
            mLeft.Position(new Vector2((AreaX - 1) * Tilemap.size.x, AreaY * Tilemap.size.y));
            mRight.Position(new Vector2((AreaX + 1) * Tilemap.size.x, AreaY * Tilemap.size.y));
            mUpLeft.Position(new Vector2((AreaX - 1) * Tilemap.size.x, (AreaY + 1) * Tilemap.size.y));
            mDownLeft.Position(new Vector2((AreaX - 1) * Tilemap.size.x, (AreaY - 1) * Tilemap.size.y));
            mUpRight.Position(new Vector2((AreaX + 1) * Tilemap.size.x, (AreaY + 1) * Tilemap.size.y));
            mDownRight.Position(new Vector2((AreaX + 1) * Tilemap.size.x, (AreaY - 1) * Tilemap.size.y));
            mCenter.Position(new Vector2(AreaX * Tilemap.size.x, AreaY * Tilemap.size.y));
        }

	}
}
