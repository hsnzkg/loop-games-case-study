using DG.Tweening;
using Project.Scripts.Entity.Player.Movement;
using UnityEngine;

namespace Project.Scripts.Entity.Player.Animation
{
    public class AnimationSystem : MonoBehaviour
    {
        [SerializeField] private Transform m_renderParent;
        [SerializeField] private AnimationSettings m_animationSettings;
        [SerializeField] private SpriteRenderer m_playerSprite;
        [SerializeField] private SpriteRenderer m_playerBottomSpriteL;
        [SerializeField] private SpriteRenderer m_playerBottomSpriteR;
        
        private IInputProvider  m_inputProvider;
        private MaterialPropertyBlock m_blockPlayer;
        private MaterialPropertyBlock m_blockBottomL;
        private MaterialPropertyBlock m_blockBottomR;

        private Tween m_hurtTween;
        private float m_hurtValue;
        
        private Tween m_deathTween;
        
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
            m_inputProvider = GetComponent<IInputProvider>();
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
            Vector2 input = m_inputProvider.GetInput();
            bool isMoving = m_inputProvider.GetHasInput();

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
        
        public void Hurt()
        {
            CompleteHurtTweenIfRunning();

            m_hurtValue = 0f;
            ApplyHurtValue();

            m_hurtTween = CreateHurtTween();
            m_hurtTween.Play();
        }

        public void Dead(Vector2 direction)
        {
            CompleteDeathTweenIfRunning();
            m_deathTween = CreateDeathTween(direction);
            m_deathTween.Play();
        }
        
        private Tween CreateDeathTween(Vector2 direction)
        {
            Sequence seq = DOTween.Sequence();

            seq.Join(CreateDeathMoveTween(direction));
            seq.Join(CreateDeathRotateTween());
            seq.Join(CreateDeathScaleTween());

            seq.OnComplete(OnDeathTweenComplete);

            return seq;
        }
        
        private Tween CreateDeathMoveTween(Vector2 direction)
        {
            Vector3 start = m_renderParent.localPosition;
            Vector3 target = CalculateDeathTargetPosition(start, direction);

            Vector3[] path = GenerateDeathPath(start, target);

            return m_renderParent
                .DOLocalPath(path, m_animationSettings.DeathMoveDuration, PathType.CatmullRom)
                .SetEase(m_animationSettings.DeathMoveEase);
        }
        
        private Vector3 CalculateDeathTargetPosition(Vector3 start, Vector2 direction)
        {
            Vector3 dir = direction.normalized;
            float dist = m_animationSettings.DeathMoveDistance;

            return start + dir * dist;
        }
        
        private Vector3[] GenerateDeathPath(Vector3 start, Vector3 end)
        {
            Vector3 mid = (start + end) * 0.5f;

            Vector2 randomCircle = UnityEngine.Random.insideUnitCircle * m_animationSettings.DeathRandomRadius;

            Vector3 midOffset = new Vector3(randomCircle.x, randomCircle.y, 0f);

            return new[]
            {
                start,
                mid + midOffset,
                end
            };
        }
        
        private Tween CreateDeathRotateTween()
        {
            float randomSpin = UnityEngine.Random.Range(-360f, 360f);

            return m_renderParent
                .DOLocalRotate(
                    new Vector3(0f, 0f, randomSpin),
                    m_animationSettings.DeathRotateDuration,
                    RotateMode.LocalAxisAdd
                )
                .SetEase(m_animationSettings.DeathRotateEase);
        }
        
        private Tween CreateDeathScaleTween()
        {
            Vector3 targetScale = Vector3.one * m_animationSettings.DeathEndScale;

            return m_renderParent
                .DOScale(targetScale, m_animationSettings.DeathScaleDuration)
                .SetEase(m_animationSettings.DeathScaleEase);
        }
        
        private void OnDeathTweenComplete()
        {
            
        }
        
        private void CompleteDeathTweenIfRunning()
        {
            if (m_deathTween == null) return;
            if (!m_deathTween.IsActive()) return;

            m_deathTween.Complete();
        }
        
        private Tween CreateHurtTween()
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(CreateFadeInTween());
            seq.Append(CreateFadeOutTween());

            return seq;
        }
        
        private Tween CreateFadeInTween()
        {
            return DOTween
                .To(GetHurtValue, SetHurtValue, 1f, m_animationSettings.HurtFadeIn)
                .SetEase(m_animationSettings.HurtEase);
        }

        private Tween CreateFadeOutTween()
        {
            return DOTween
                .To(GetHurtValue, SetHurtValue, 0f, m_animationSettings.HurtFadeOut)
                .SetEase(m_animationSettings.HurtEase);
        }
        
        private float GetHurtValue()
        {
            return m_hurtValue;
        }

        private void SetHurtValue(float value)
        {
            m_hurtValue = value;
            ApplyHurtValue();
        }
        
        private void ApplyHurtValue()
        {
            m_blockPlayer.SetFloat("_Hurt", m_hurtValue);
            m_playerSprite.SetPropertyBlock(m_blockPlayer);

            m_blockBottomL.SetFloat("_Hurt", m_hurtValue);
            m_playerBottomSpriteL.SetPropertyBlock(m_blockBottomL);

            m_blockBottomR.SetFloat("_Hurt", m_hurtValue);
            m_playerBottomSpriteR.SetPropertyBlock(m_blockBottomR);
        }
        
        private void CompleteHurtTweenIfRunning()
        {
            if (m_hurtTween == null) return;
            if (!m_hurtTween.IsActive()) return;

            m_hurtTween.Complete();
        }
    }
}
