using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Project.ThirdParty.ScratchCard.Scripts.Tools
{
    public class MigrationHelper
    {
        private Object migratedObject;
        
        public void StartMigrate(ScratchCardManager scratchCardManager)
        {
            if (scratchCardManager == null)
            {
                return;
            }

            bool result = false;
            if (scratchCardManager.GetMeshRendererCard() == null)
            {
                FieldInfo field = scratchCardManager.GetType().GetField("MeshCard");
                object meshCardValue = field.GetValue(scratchCardManager);
                if (meshCardValue != null)
                {
                    GameObject meshCardGameObject = (GameObject)meshCardValue;
                    if (meshCardGameObject != null)
                    {
                        if (meshCardGameObject.TryGetComponent(out MeshRenderer meshRenderer))
                        {
                            scratchCardManager.SetMeshRendererCard(meshRenderer);
                            field.SetValue(scratchCardManager, null);
                            result = true;
                        }
                    }
                }
            }
            
            if (scratchCardManager.GetSpriteRendererCard() == null)
            {
                FieldInfo field = scratchCardManager.GetType().GetField("SpriteCard");
                object spriteCardValue = field.GetValue(scratchCardManager);
                if (spriteCardValue != null)
                {
                    GameObject spriteCardGameObject = (GameObject)spriteCardValue;
                    if (spriteCardGameObject != null)
                    {
                        if (spriteCardGameObject.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
                        {
                            scratchCardManager.SetSpriteRendererCard(spriteRenderer);
                            field.SetValue(scratchCardManager, null);
                            result = true;
                        }
                    }
                }
            }
            
            if (scratchCardManager.GetCanvasRendererCard() == null)
            {
                FieldInfo field = scratchCardManager.GetType().GetField("ImageCard");
                object imageCardValue = field.GetValue(scratchCardManager);
                if (imageCardValue != null)
                {
                    GameObject imageCardGameObject = (GameObject)imageCardValue;
                    if (imageCardGameObject != null)
                    {
                        if (imageCardGameObject.TryGetComponent(out Image image))
                        {
                            scratchCardManager.SetCanvasRendererCard(image);
                            field.SetValue(scratchCardManager, null);
                            result = true;
                        }
                    }
                }
            }

            if (result)
            {
                migratedObject = scratchCardManager;
                Debug.Log($"The migration for {scratchCardManager} was successful!", scratchCardManager);
            }
        }

        public void FinishMigrate()
        {
            if (migratedObject != null)
            {
                MarkAsDirty(migratedObject);
                migratedObject = null;
            }
        }

        private void MarkAsDirty(Object unityObject)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                UnityEditor.EditorUtility.SetDirty(unityObject);
                var component = unityObject as Component;
                if (component != null)
                {
                    UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(component.gameObject.scene);
                }
            }
#endif
        }
    }
}