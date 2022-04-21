﻿using System;
using MelonLoader;
using UIExpansionKit.API.Controls;
using UnityEngine;
using VRC;
using VRChatUtilityKit.Ui;
using VRChatUtilityKit.Utilities;

namespace UserInfoExtensions.Modules
{
    public class QuickMenuFromSocial : ModuleBase
    {
        public static MelonPreferences_Entry<bool> QuickMenuFromSocialButton;

        public static IMenuButton quickMenuFromSocialUixButton;

        public override void Init()
        {
            QuickMenuFromSocialButton = MelonPreferences.CreateEntry("UserInfoExtensionsSettings", nameof(QuickMenuFromSocialButton), false, "Show \"To Quick Menu\" button");
            quickMenuFromSocialUixButton = UserInfoExtensionsMod.userDetailsMenu.AddSimpleButton("To Quick Menu", ToQuickMenu);
            quickMenuFromSocialUixButton.SetVisible(QuickMenuFromSocialButton.Value);
            UserInfoExtensionsMod.menu.AddSimpleButton("To Quick Menu", ToQuickMenu);
        }
        public override void OnPreferencesSaved()
        {
            quickMenuFromSocialUixButton?.SetVisible(QuickMenuFromSocialButton.Value);
        }
        public static void ToQuickMenu()
        {
            UserInfoExtensionsMod.HideAllPopups();

            foreach (Player player in PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0)
            {
                if (player.prop_APIUser_0 == null) continue;
                if (player.prop_APIUser_0.id == VRCUtils.ActiveUserInUserInfoMenu.id)
                {
                    UiManager.CloseBigMenu();

                    UiManager.OpenQuickMenuPage("QuickMenuHere");
                    UiManager.OpenUserInQuickMenu(player.field_Private_APIUser_0);

                    return;
                }
            }
            UiManager.OpenSmallPopup("Notice:", "You cannot show this user on the Quick Menu because they are not in the same instance", "Close", new Action(UiManager.ClosePopup));
        }
    }
}
