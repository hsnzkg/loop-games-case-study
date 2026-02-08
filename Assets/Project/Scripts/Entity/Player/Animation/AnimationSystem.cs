using Project.Scripts.Input;
using UnityEngine;

namespace Project.Scripts.Entity.Player.Animation
{
    public class AnimationSystem : MonoBehaviour
    {
        [SerializeField] private AnimationSettings m_animationSettings;
        [SerializeField] private SpriteRenderer m_playerSprite;
        [SerializeField] private SpriteRenderer m_playerBottomSpriteL;
        [SerializeField] private SpriteRenderer m_playerBottomSpriteR;

        private MaterialPropertyBlock m_blockPlayer;
        private MaterialPropertyBlock m_blockBottomL;
        private MaterialPropertyBlock m_blockBottomR;

        private int m_lastFacing = 1;
        private int m_cachedFacing = 1;
        private bool m_cachedMoving;

        private bool m_facingChanged;
        private bool m_movingChanged;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            m_playerSprite.material = new Material(m_playerSprite.material);
            m_playerBottomSpriteL.material = new Material(m_playerBottomSpriteL.material);
            m_playerBottomSpriteR.material = new Material(m_playerBottomSpriteR.material);
            InitializeBodyLayer();
            InitializeBottomLayer();
      
        }

        private void InitializeBodyLayer()
        {
            m_blockPlayer = new MaterialPropertyBlock();
            m_blockPlayer.SetFloat("_Facing", 1);
            m_blockPlayer.SetVector("_WobbleDir",m_animationSettings.BodyWobbleDir);
            m_blockPlayer.SetFloat("_WobbleAmount",m_animationSettings.BodyWobbleAmount);
            m_blockPlayer.SetFloat("_WobbleSpeed",m_animationSettings.BodyWobbleIdleSpeed);
            m_blockPlayer.SetFloat("_SquashAmount",m_animationSettings.SquashAmount);
            m_blockPlayer.SetTexture("_MainTex",m_animationSettings.BodyTexture);
            m_playerSprite.SetPropertyBlock(m_blockPlayer);
        }

        private void InitializeBottomLayer()
        {
            m_blockBottomL = new MaterialPropertyBlock();
            m_blockBottomR = new MaterialPropertyBlock();
            
            m_blockBottomL.SetFloat("_Facing", 1);
            m_blockBottomR.SetFloat("_Facing", 1);
            
            m_blockBottomL.SetVector("_WobbleDir",m_animationSettings.FootWobbleDir);
            m_blockBottomL.SetFloat("_WobbleAmount",m_animationSettings.FootWobbleAmount);
            m_blockBottomL.SetFloat("_WobbleSpeed",m_animationSettings.FootWobbleIdleSpeed);
            m_blockBottomL.SetTexture("_MainTex",m_animationSettings.FootLTexture);
            
            m_blockBottomR.SetVector("_WobbleDir",-m_animationSettings.FootWobbleDir);
            m_blockBottomR.SetFloat("_WobbleAmount",m_animationSettings.FootWobbleAmount);
            m_blockBottomR.SetFloat("_WobbleSpeed",m_animationSettings.FootWobbleIdleSpeed);
            m_blockBottomR.SetTexture("_MainTex",m_animationSettings.FootRTexture);

            m_playerBottomSpriteL.SetPropertyBlock(m_blockBottomL);
            m_playerBottomSpriteR.SetPropertyBlock(m_blockBottomR);
        }

        private void Update()
        {
            CheckState();

            if (m_facingChanged)
            {
                UpdateFacing();
            }

            if (m_movingChanged)
            {
                UpdateMoving();
            }
        }

        private void CheckState()
        {
            PlayerInputData data = InputManager.GetData();

            Vector2 input = data.GetMovementInputAxisVec2();
            bool isMoving = data.GetHasMovementInput();

            int facing = m_lastFacing;
            if (input.x > 0f)
            {
                facing = 1;
            }
            else if (input.x < 0f)
            {
                facing = -1;
            }

            if (isMoving)
            {
                m_lastFacing = facing;
            }
            else
            {
                facing = m_lastFacing;
            }

            m_facingChanged = facing != m_cachedFacing;
            if (m_facingChanged)
            {
                m_cachedFacing = facing;
            }
            
            m_movingChanged = isMoving != m_cachedMoving;
            if (m_movingChanged)
            {
                m_cachedMoving = isMoving;
            }
        }

        private void UpdateFacing()
        {
            m_blockPlayer.SetFloat("_Facing", m_cachedFacing);
            m_playerSprite.SetPropertyBlock(m_blockPlayer);
        }

        private void UpdateMoving()
        {
            m_blockPlayer.SetFloat("_WobbleSpeed", m_cachedMoving ? m_animationSettings.BodyWobbleWalkSpeed : m_animationSettings.BodyWobbleIdleSpeed);
            m_playerSprite.SetPropertyBlock(m_blockPlayer);
            
            m_blockBottomL.SetFloat("_WobbleSpeed", m_cachedMoving ? m_animationSettings.FootWobbleWalkSpeed : m_animationSettings.FootWobbleIdleSpeed);
            m_playerBottomSpriteL.SetPropertyBlock(m_blockBottomL);
            
            m_blockBottomR.SetFloat("_WobbleSpeed", m_cachedMoving ? m_animationSettings.FootWobbleWalkSpeed : m_animationSettings.FootWobbleIdleSpeed);
            m_playerBottomSpriteR.SetPropertyBlock(m_blockBottomR);
        }
    }
}
