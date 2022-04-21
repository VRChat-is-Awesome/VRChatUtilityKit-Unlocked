using System;
using MelonLoader;
using UIExpansionKit.API;
using UIExpansionKit.API.Controls;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRChatUtilityKit.Ui;

namespace CloningBeGone
{
    class UIXManager
    {
        public static MelonPreferences_Entry<bool> enableAlwaysOnButton;
        public static MelonPreferences_Entry<bool> enableAlwaysOffButton;

        public static IMenuToggle alwaysOnButton;
        public static IMenuToggle alwaysOffButton;

        private static bool ignore;

        public static void Init() 
        {
            enableAlwaysOnButton = CloningBeGoneMod.category.CreateEntry(nameof(enableAlwaysOnButton), true, "Enable/Disable the \"Always have cloning on with this avatar\" button");
            enableAlwaysOffButton = CloningBeGoneMod.category.CreateEntry(nameof(enableAlwaysOffButton), true, "Enable/Disable the \"Always have cloning off with this avatar\" button");
            enableAlwaysOnButton.OnValueChangedUntyped += OnPrefChanged;
            enableAlwaysOffButton.OnValueChangedUntyped += OnPrefChanged;

            UiManager.OnQuickMenuOpened += OnQuickMenuOpened;

            ICustomLayoutedMenu menu = ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu);
            alwaysOnButton = menu.AddToggleButton("Always have cloning on with this avatar", new Action<bool>((state) =>
            {
                if (ignore)
                    return;

                if (state)
                    CloningBeGoneMod.cloningOnAvatars.Value.Add(Player.prop_Player_0.prop_ApiAvatar_0.id);
                else
                    CloningBeGoneMod.cloningOnAvatars.Value.Remove(Player.prop_Player_0.prop_ApiAvatar_0.id);

                if (RoomManager.field_Internal_Static_ApiWorldInstance_0 != null)
                    CloningBeGoneMod.CheckAccessType(RoomManager.field_Internal_Static_ApiWorldInstance_0.type);
                CloningBeGoneMod.OnAvatarInstantiated(Player.prop_Player_0.prop_ApiAvatar_0);
            }), null);
            alwaysOnButton.SetVisible(enableAlwaysOnButton.Value);

            alwaysOffButton = menu.AddToggleButton("Always have cloning off with this avatar", new Action<bool>((state) =>
            {
                if (ignore)
                    return;

                if (state)
                    CloningBeGoneMod.cloningOffAvatars.Value.Add(Player.prop_Player_0.prop_ApiAvatar_0.id);
                else
                    CloningBeGoneMod.cloningOffAvatars.Value.Remove(Player.prop_Player_0.prop_ApiAvatar_0.id);

                if (RoomManager.field_Internal_Static_ApiWorldInstance_0 != null)
                    CloningBeGoneMod.CheckAccessType(RoomManager.field_Internal_Static_ApiWorldInstance_0.type);
                CloningBeGoneMod.OnAvatarInstantiated(Player.prop_Player_0.prop_ApiAvatar_0);
            }), null);
            alwaysOffButton.SetVisible(enableAlwaysOnButton.Value);
        }

        public static void OnQuickMenuOpened()
        {
            ignore = true;
            if (alwaysOnButton.Visible)
                alwaysOnButton.Selected = CloningBeGoneMod.cloningOnAvatars.Value.Contains(Player.prop_Player_0.prop_ApiAvatar_0.id);
            if (alwaysOffButton.Visible)
                alwaysOnButton.Selected = CloningBeGoneMod.cloningOffAvatars.Value.Contains(Player.prop_Player_0.prop_ApiAvatar_0.id);
            ignore = false;
        }

        private static void OnPrefChanged()
        {
            alwaysOnButton?.SetVisible(enableAlwaysOnButton.Value);
            alwaysOffButton?.SetVisible(enableAlwaysOffButton.Value);
        }
    }
}
