using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.ThirdParty.ScratchCard.Scripts.Core.InputData
{
    public class RaycastController
    {
        private GameObject scratchSurface;
        private List<CanvasGraphicRaycaster> raycasters;

        public RaycastController(GameObject surfaceObject, Canvas[] canvasesForRaycastsBlocking)
        {
            scratchSurface = surfaceObject;
            raycasters = new List<CanvasGraphicRaycaster>();
            
            if (surfaceObject.TryGetComponent(out Image image) && image.canvas != null)
            {
                if (!image.canvas.TryGetComponent(out CanvasGraphicRaycaster raycaster))
                {
                    raycaster = image.canvas.gameObject.AddComponent<CanvasGraphicRaycaster>();
                }
                if (raycaster != null)
                {
                    raycasters.Add(raycaster);
                }
            }

            if (canvasesForRaycastsBlocking != null)
            {
                foreach (var canvas in canvasesForRaycastsBlocking)
                {
                    if (canvas != null)
                    {
                        if (!canvas.TryGetComponent(out CanvasGraphicRaycaster raycaster))
                        {
                            raycaster = canvas.gameObject.AddComponent<CanvasGraphicRaycaster>();
                        }

                        if (raycaster != null)
                        {
                            raycasters.Add(raycaster);
                        }
                    }
                }
            }
        }

        public bool IsBlock(Vector3 position)
        {
            bool isBlock = false;
            foreach (CanvasGraphicRaycaster raycaster in raycasters)
            {
                if (raycaster == null)
                {
                    continue;
                }
                
                List<RaycastResult> result = raycaster.GetRaycasts(position);
                if (result.Count == 0 || result.Count > 0 && result[0].gameObject == scratchSurface)
                {
                    continue;
                }
                
                isBlock = true;
                break;
            }
            return isBlock;
        }
    }
}