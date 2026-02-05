using UnityEditor;
using UnityEngine;

namespace Project.Scripts.Bootstrap
{
    public static class Bootstrapper
    {
        /// <summary>
        /// Order of initialization
        /// 1 : Initialize On Load Method
        /// 2 : After Assemblies Loaded  // WARNING ! "UniTask" actions should be called after this step if not async operations not completed
        /// 3 : Before Splash Screen
        /// 4 : Before Scene Load
        /// 5 : After Scene Load
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void SubsystemRegistration()
        {
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void AfterAssembliesLoaded()
        {
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void BeforeSplashScreen()
        {
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void BeforeSceneLoad()
        {
#if UNITY_EDITOR
            IBootstrapper bootstrapper = new GameBootstrapper();
            bool status = EditorPrefs.GetBool("EDITOR_BOOTSTRAP_STATUS");
            if (!status) return;
#else
            IBootstrapper bootstrapper = new PlayableAdBootstrapper();
#endif
            bootstrapper.Bootstrap();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AfterSceneLoad()
        {
        }
    }
}
