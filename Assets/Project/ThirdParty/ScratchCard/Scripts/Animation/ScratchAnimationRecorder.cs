/*
using System.Collections.Generic;
using Project.ThirdParty.ScratchCard.Scripts.Core;
using Project.ThirdParty.ScratchCard.Scripts.Core.InputData;
using UnityEngine;

namespace Project.ThirdParty.ScratchCard.Scripts.Animation
{
    public class ScratchAnimationRecorder : MonoBehaviour
    {
        public ScratchCard ScratchCard;
        public ScratchAnimation ScratchAnimation;
        public ScratchAnimationSpace AnimationSpace;
        public bool FlushOnDestroy = true;

        [SerializeReference] private List<BaseScratch> scratches = new List<BaseScratch>();
        
        private void Start()
        {
            ScratchCard.GetInput().OnScratchHoleExtended += OnScratchHole;
            ScratchCard.GetInput().OnScratchLineExtended += OnScratchLine;
        }

        private void OnDestroy()
        {
            ScratchCard.GetInput().OnScratchHoleExtended -= OnScratchHole;
            ScratchCard.GetInput().OnScratchLineExtended -= OnScratchLine;
            if (FlushOnDestroy)
            {
                Flush();
            }
        }
        
        private void OnValidate()
        {
            if (ScratchCard == null)
            {
                if (TryGetComponent(out ScratchCard))
                {
                    return;
                }
                
                if (TryGetComponent<ScratchCardManager>(out var scratchCardManager))
                {
                    if (scratchCardManager.Card != null)
                    {
                        ScratchCard = scratchCardManager.Card;
                    }
                }
            }
        }

        [ContextMenu("Flush")]
        public void Flush()
        {
            if (scratches.Count > 0)
            {
                float firstTime = scratches[0].Time;
                for (int i = 1; i < scratches.Count; i++)
                {
                    scratches[i].Time -= firstTime;
                    if (scratches[i] is LineScratch line)
                    {
                        line.TimeEnd -= firstTime;
                    }
                }
                scratches[0].Time = 0f;
                if (scratches[0] is LineScratch firstLine)
                {
                    firstLine.TimeEnd -= firstTime;
                }

                ScratchAnimation.ScratchSpace = AnimationSpace;
                ScratchAnimation.Scratches.Clear();
                ScratchAnimation.Scratches.AddRange(scratches);
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(ScratchAnimation);
#endif
                string scratchAnimationJson = JsonUtility.ToJson(ScratchAnimation);
                Debug.Log(scratchAnimationJson);
                scratches.Clear();
            }
        }

        private void OnScratchHole(ScratchCardInputData hole)
        {
            if (enabled)
            {
                Vector2 imageSize = Vector2.one;
                if (AnimationSpace == ScratchAnimationSpace.UV)
                {
                    imageSize = ScratchCard.GetScratchData().GetTextureSize();
                }
                
                BaseScratch scratch = new BaseScratch();
                scratch.Position = hole.Position / imageSize;
                scratch.BrushScale = hole.Pressure * ScratchCard.BrushSize;
                scratch.Time = hole.Time;
                
                scratches.Add(scratch);
            }
        }

        private void OnScratchLine(ScratchCardInputData lineStart, ScratchCardInputData lineEnd)
        {
            if (enabled)
            {
                Vector2 imageSize = Vector2.one;
                if (AnimationSpace == ScratchAnimationSpace.UV)
                {
                    imageSize = ScratchCard.GetScratchData().GetTextureSize();
                }

                LineScratch scratch = new LineScratch();
                scratch.Position = lineStart.Position / imageSize;
                scratch.BrushScale = lineStart.Pressure * ScratchCard.BrushSize;
                scratch.Time = lineStart.Time;
                scratch.PositionEnd = lineEnd.Position / imageSize;
                scratch.BrushScaleEnd = lineEnd.Pressure * ScratchCard.BrushSize;
                scratch.TimeEnd = lineEnd.Time;
                scratches.Add(scratch);
            }
        }
    }
}
*/