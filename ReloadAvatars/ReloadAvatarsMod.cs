﻿using System;
using MelonLoader;
using UIExpansionKit.API;
using UIExpansionKit.API.Controls;
using UnityEngine;
using VRChatUtilityKit.Utilities;

[assembly: MelonInfo(typeof(ReloadAvatars.ReloadAvatarsMod), "ReloadAvatars", "1.1.1", "Sleepers", "https://github.com/SleepyVRC/Mods")]
[assembly: MelonGame("VRChat", "VRChat")]

namespace ReloadAvatars
{
    public class ReloadAvatarsMod : MelonMod
    {
        public static IMenuButton reloadAvatarButton;
        public static IMenuButton reloadAllAvatarsButton;

        public static MelonPreferences_Entry<bool> reloadAvatarPref;
        public static MelonPreferences_Entry<bool> reloadAllAvatarsPref;

        public override void OnApplicationStart()
        {
            MelonPreferences_Category category = MelonPreferences.CreateCategory("ReloadAvatars", "ReloadAvatars Settings");
            reloadAvatarPref = category.CreateEntry("ReloadAvatar", true, "Enable/Disable Reload Avatar Button");
            reloadAllAvatarsPref = category.CreateEntry("ReloadAllAvatars", true, "Enable/Disable Reload All Avatars Button");

            foreach (MelonPreferences_Entry entry in category.Entries)
                entry.OnValueChangedUntyped += OnPrefChanged;

            reloadAvatarButton = ExpansionKitApi.GetExpandedMenu(ExpandedMenu.UserQuickMenu).AddSimpleButton("Reload Avatar", new Action(() =>
            {
                try
                {
                    VRCUtils.ReloadAvatar(VRCUtils.ActivePlayerInUserSelectMenu);
                }
                catch (Exception ex)
                {
                    LoggerInstance.Error("Error while reloading single avatar:\n" + ex.ToString());
                } // Ignore
            }));
            reloadAvatarButton.SetVisible(reloadAllAvatarsPref.Value);

            reloadAllAvatarsButton = ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Reload All Avatars", new Action(() =>
            {
                try
                {
                    VRCUtils.ReloadAllAvatars();
                }
                catch (Exception ex)
                {
                    LoggerInstance.Error("Error while reloading all avatars:\n" + ex.ToString());
                } // Ignore
            }));
            reloadAllAvatarsButton.SetVisible(reloadAvatarPref.Value);
            LoggerInstance.Msg("Initialized!");
        }
        public void OnPrefChanged()
        {
            reloadAvatarButton?.SetVisible(reloadAvatarPref.Value);
            reloadAllAvatarsButton?.SetVisible(reloadAllAvatarsPref.Value);
        }
    }
}
