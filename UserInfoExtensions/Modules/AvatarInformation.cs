using System;
using MelonLoader;
using UIExpansionKit.API;
using UIExpansionKit.API.Controls;
using UIExpansionKit.Components;
using VRC.Core;
using VRChatUtilityKit.Ui;

namespace UserInfoExtensions.Modules
{
    class AvatarInformation : ModuleBase
    {
        public static MelonPreferences_Entry<bool> AvatarInfoMenuButton;
        private static IMenuButton avatarInfoMenuUixButton;

        public static ICustomShowableLayoutedMenu avatarInfoMenu;

        private static IMenuButton authorNameLabel;
        private static IMenuButton avatarNameLabel;
        private static IMenuButton platformLabel;
        private static IMenuButton releaseTypeLabel;
        private static IMenuButton lastUpdatedLabel;
        private static IMenuButton VersionLabel;

        private static ApiAvatar avatar;

        public override void Init()
        {
            AvatarInfoMenuButton = MelonPreferences.CreateEntry("UserInfoExtensionsSettings", nameof(AvatarInfoMenuButton), true, "Show \"Avatar Info Menu\" button in Avatar Menu");

            avatarInfoMenuUixButton = ExpansionKitApi.GetExpandedMenu(ExpandedMenu.AvatarMenu).AddSimpleButton("Avatar Info Menu", new Action(() => { avatarInfoMenu?.Show(); OnMenuShown(); }));
            avatarInfoMenuUixButton.OnInstanceCreated += new Action<UnityEngine.GameObject>(delegate
            {
                GetAvatarAuthor.avatarPage.GetComponent<EnableDisableListener>().OnDisabled += new Action(() => avatarInfoMenu?.Hide());
            });

            avatarInfoMenu = ExpansionKitApi.CreateCustomFullMenuPopup(new LayoutDescription()
            {
                RowHeight = 80,
                NumColumns = 3,
                NumRows = 5
            });
            avatarInfoMenu.AddLabel("Avatar information:");
            avatarInfoMenu.AddSpacer();
            avatarInfoMenu.AddSimpleButton("Back", new Action(() => avatarInfoMenu.Hide()));
            authorNameLabel = avatarInfoMenu.AddSimpleButton("", new Action(() => { }));
            avatarInfoMenu.AddSimpleButton("Show Avatar Description", new Action(() => { avatarInfoMenu.Hide(); UiManager.OpenSmallPopup("Description:", avatar.description ?? "", "Close", UiManager.ClosePopup); }));
            avatarNameLabel = avatarInfoMenu.AddSimpleButton("", new Action(() => { }));
            platformLabel = avatarInfoMenu.AddSimpleButton("", new Action(() => { }));
            releaseTypeLabel = avatarInfoMenu.AddSimpleButton("", new Action(() => { }));
            lastUpdatedLabel = avatarInfoMenu.AddSimpleButton("", new Action(() => { }));
            VersionLabel = avatarInfoMenu.AddSimpleButton("", new Action(() => { }));
        }

        private static void OnMenuShown()
        {
            avatar = GetAvatarAuthor.avatarPage.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0;

            if (avatar == null)
            {
                authorNameLabel.Text = "Author Name:\nUnknown";
                avatarNameLabel.Text = "Avatar Name:\nUnknown";
                platformLabel.Text = "Platform:\nUnknown";
                releaseTypeLabel.Text = "Release Type:\nUnknown";
                lastUpdatedLabel.Text = "Last Updated At:\nUnknown";
                VersionLabel.Text = "Version:\nUnknown";
            }
            else
            {
                if (string.IsNullOrEmpty(avatar.authorName))
                    authorNameLabel.Text = "Author Name:\nUnknown";
                else
                    authorNameLabel.Text = $"Author Name:\n{avatar.authorName}";

                if (string.IsNullOrEmpty(avatar.name))
                    avatarNameLabel.Text = "Avatar Name:\nUnknown";
                else
                    avatarNameLabel.Text = $"Avatar Name:\n{avatar.name}";

                string supportedPlatforms = avatar.supportedPlatforms.ToString();
                switch (supportedPlatforms)
                {
                    case "StandaloneWindows":
                        supportedPlatforms = "PC";
                        break;
                    case "Android":
                        supportedPlatforms = "Quest";
                        break;
                }
                platformLabel.Text = "Platform:\n" + supportedPlatforms;

                if (string.IsNullOrEmpty(avatar.releaseStatus))
                    releaseTypeLabel.Text = "Release Type:\nUnknown";
                else
                    releaseTypeLabel.Text = "Release Type:\n" + char.ToUpper(avatar.releaseStatus[0]) + avatar.releaseStatus.Substring(1);

                if (avatar.updated_at == null)
                {
                    lastUpdatedLabel.Text = "Last Updated At:\nUnknown";
                }
                else
                {
                    if (UserInformation.militaryTimeFormat.Value)
                        lastUpdatedLabel.Text = "Last Updated At:\n" + avatar.updated_at.ToString("M/d/yyyy HH:mm");
                    else
                        lastUpdatedLabel.Text = "Last Updated At:\n" + avatar.updated_at.ToString("M/d/yyyy hh:mm tt");
                }

                if (avatar.version < 1)
                    VersionLabel.Text = "Version:\nUnknown";
                else
                    VersionLabel.Text = $"Version:\n{avatar.version}";
            }
        }

        public override void OnPreferencesSaved()
        {
            avatarInfoMenuUixButton?.SetVisible(AvatarInfoMenuButton.Value);
        }
    }
}
