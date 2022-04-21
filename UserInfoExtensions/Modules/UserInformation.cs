using System;
using MelonLoader;
using UIExpansionKit.API.Controls;
using UnityEngine;
using UnityEngine.UI;
using VRC.Core;
using VRChatUtilityKit.Utilities;

namespace UserInfoExtensions.Modules
{
    class UserInformation : ModuleBase
    {
        public static MelonPreferences_Entry<bool> militaryTimeFormat;

        private static IMenuButton refreshButtonLabel;
        private static IMenuButton userNameLabel;
        private static IMenuButton platformLabel;
        private static IMenuButton lastLoginLabel;
        private static IMenuButton dateJoinedLabel;
        private static IMenuButton friendIndexLabel;

        public override void Init()
        {
            militaryTimeFormat = MelonPreferences.CreateEntry("UserInfoExtensionsSettings", nameof(militaryTimeFormat), true, "Display time in 24 hour time");

            UserInfoExtensionsMod.menu.AddLabel("General User Info");
            UserInfoExtensionsMod.menu.AddSpacer();
            refreshButtonLabel = UserInfoExtensionsMod.menu.AddSimpleButton("Pressing this button may be able to find unknown data", new Action(() => RefreshAPIUser()));
            userNameLabel = UserInfoExtensionsMod.menu.AddSimpleButton("", new Action(() => { }));
            platformLabel = UserInfoExtensionsMod.menu.AddSimpleButton("", new Action(() => { }));
            lastLoginLabel = UserInfoExtensionsMod.menu.AddSimpleButton("", new Action(() => { }));
            dateJoinedLabel = UserInfoExtensionsMod.menu.AddSimpleButton("", new Action(() => { }));
            friendIndexLabel = UserInfoExtensionsMod.menu.AddSimpleButton("", new Action(() => { }));
        }
        public override void OnUIXMenuOpen()
        {
            if (VRCUtils.ActiveUserInUserInfoMenu.username != null)
                userNameLabel.Text = "Username:\n" + VRCUtils.ActiveUserInUserInfoMenu.username;
            else
                userNameLabel.Text = "Username:\n" + VRCUtils.ActiveUserInUserInfoMenu.displayName.ToLower();

            switch (VRCUtils.ActiveUserInUserInfoMenu.last_platform)
            {
                case "standalonewindows":
                    platformLabel.Text = "Last Platform:\nPC";
                    break;
                case "android":
                    platformLabel.Text = "Last Platform:\nQuest";
                    break;
                default:
                    platformLabel.Text = "Last Platform:\nUnknown";
                    break;
            }

            try
            { 
                DateTime lastLogin = DateTime.Parse(VRCUtils.ActiveUserInUserInfoMenu.last_login);
                if (militaryTimeFormat.Value)
                    lastLoginLabel.Text = "Last Login:\n" + lastLogin.ToString("M/d/yyyy HH:mm");
                else
                    lastLoginLabel.Text = "Last Login:\n" + lastLogin.ToString("M/d/yyyy hh:mm tt");
            }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException || ex is FormatException)
                    lastLoginLabel.Text = "Last Login:\nUnknown";
            }

            try
            {
                if (Utils.ActiveUIEUser == null)
                {
                    dateJoinedLabel.Text = "Date Joined:\nUnknown";
                }
                else
                {
                    DateTime dateJoined = DateTime.Parse(Utils.ActiveUIEUser.DateJoined);
                    dateJoinedLabel.Text = "Date Joined:\n" + dateJoined.ToString("d");
                }
            }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException || ex is FormatException)
                    dateJoinedLabel.Text = "Date Joined:\nUnknown";
            }

            if (APIUser.CurrentUser != null)
            {
                if (VRCUtils.ActiveUserInUserInfoMenu.IsSelf)
                {
                    friendIndexLabel.Text = "Friend Number:\nIs Yourself";
                }
                else
                {
                    int friendIndex = APIUser.CurrentUser.friendIDs.IndexOf(VRCUtils.ActiveUserInUserInfoMenu.id);
                    if (friendIndex != -1)
                        friendIndexLabel.Text = "Friend Number:\n" + (friendIndex + 1).ToString();
                    else
                        friendIndexLabel.Text = "Friend Number:\nNot a Friend";
                }
            }
            else
            {
                friendIndexLabel.Text = "Friend Number:\nUnknown";
            }
        }

        public void RefreshAPIUser()
        {
            if (!Utils.StartRequestTimer(new Action(() => { if (refreshButtonLabel != null) refreshButtonLabel.Text = "Please wait between button presses"; }),
                new Action(() => { if (refreshButtonLabel != null) refreshButtonLabel.Text = "Pressing this button may be able to find unknown data"; })))
                return;

            if (Utils.ActiveUIEUser == null || !Utils.ActiveUIEUser.IsFullAPIUser) // Only bother to refresh if there's actually any invalid data // Still resets timer for consistent behavior on user's end
                APIUser.FetchUser(VRCUtils.ActiveUserInUserInfoMenu.id, new Action<APIUser>((user) => OnUIXMenuOpen()), null);
        }
    }
}
