using System.Linq;
using MelonLoader;
using VRChatUtilityKit.Utilities;

[assembly: MelonInfo(typeof(UserHistory.UserHistoryMod), "UserHistory", "1.1.1", "Sleepers", "https://github.com/SleepyVRC/Mods")]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonAdditionalDependencies("UIExpansionKit")]

namespace UserHistory
{
    public class UserHistoryMod : MelonMod
    {
        internal static bool HasUIX { get { return MelonHandler.Mods.Any(x => x.Info.Name.Equals("UI Expansion Kit")); } }

        public static UserHistoryMod Instance { get; private set; }
        public override void OnApplicationStart()
        {
            Instance = this;
            Config.Init();
            UserManager.Init();

            if (HasUIX)
                typeof(UIXManager).GetMethod("AddMethodToUIInit").Invoke(null, null);
            else
                MelonCoroutines.Start(StartUiManagerInitIEnumerator());
        }

        private System.Collections.IEnumerator StartUiManagerInitIEnumerator()
        {
            while (VRCUiManager.prop_VRCUiManager_0 == null)
                yield return null;

            OnUiManagerInit();
        }

        public void OnUiManagerInit()
        {
            MenuManager.UiInit();
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (buildIndex != -1)
                return;

            UserManager.cachedPlayers.Clear();
        }
    }
}
