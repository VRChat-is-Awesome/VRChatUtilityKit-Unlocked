﻿using System;
using System.Collections.Generic;
using MelonLoader;
using UIExpansionKit.API.Controls;
using UnhollowerRuntimeLib;
using UnityEngine;
using UserInfoExtensions.Components;
using VRC.UI;
using VRChatUtilityKit.Ui;
using VRChatUtilityKit.Utilities;

namespace UserInfoExtensions.Modules
{
    public class BioButtons : ModuleBase
    {
        public static IMenuButton bioLinksUixButton;
        public static IMenuButton bioLanguagesUixButton;

        public static MelonPreferences_Entry<bool> BioLinksButton;
        public static MelonPreferences_Entry<bool> BioLanguagesButton;

        public static BioLinksPopup bioLinksPopup;
        public static BioLanguagesPopup bioLanguagesPopup;
        public static List<Uri> bioLinks = new List<Uri>();
        public static List<string> userLanguages = new List<string>();
        public readonly static Dictionary<string, string> languageLookup = new Dictionary<string, string>
        {
            { "eng", "[ eng ] English" },
            { "kor", "[ kor ] 한국어" },
            { "rus", "[ rus ] Русский" },
            { "spa", "[ spa ] Español" },
            { "por", "[ por ] Português" },
            { "zho", "[ zho ] 中文" },
            { "deu", "[ deu ] Deutsch" },
            { "jpn", "[ jpn ] 日本語" },
            { "fra", "[ fra ] Français" },
            { "swe", "[ swe ] Svenska" },
            { "nld", "[ nld ] Nederlands" },
            { "pol", "[ pol ] Polski" },
            { "dan", "[ dan ] Dansk" },
            { "nor", "[ nor ] Norsk" },
            { "ita", "[ ita ] Italiano" },
            { "tha", "[ tha ] ภาษาไทย" },
            { "fin", "[ fin ] Suomi" },
            { "hun", "[ hun ] Magyar" },
            { "ces", "[ ces ] Čeština" },
            { "tur", "[ tur ] Türkçe" },
            { "ara", "[ ara ] العربية" },
            { "ron", "[ ron ] Română" },
            { "vie", "[ vie ] Tiếng Việt" },
            { "ase", "[ ase ] American Sign Language" },
            { "bfi", "[ bfi ] British Sign Language" },
            { "dse", "[ dse ] Dutch Sign Language" },
            { "fsl", "[ fsl ] French Sign Language" },
            { "kvk", "[ kvk ] Korean Sign Language" },
        };

        public override void Init()
        {
            BioLinksButton = MelonPreferences.CreateEntry("UserInfoExtensionsSettings", nameof(BioLinksButton), false, "Show \"Bio Links\" button");
            BioLanguagesButton = MelonPreferences.CreateEntry("UserInfoExtensionsSettings", nameof(BioLanguagesButton), false, "Show \"Bio Languages\" button");

            bioLinksUixButton = UserInfoExtensionsMod.userDetailsMenu.AddSimpleButton("Bio Links", ShowBioLinksPopup);
            bioLinksUixButton.SetVisible(BioLinksButton.Value);
            bioLanguagesUixButton = UserInfoExtensionsMod.userDetailsMenu.AddSimpleButton("Bio Languages", ShowBioLanguagesPopup);
            bioLanguagesUixButton.SetVisible(BioLanguagesButton.Value);

            UserInfoExtensionsMod.menu.AddLabel("Bio Related Things");
            UserInfoExtensionsMod.menu.AddSpacer();
            UserInfoExtensionsMod.menu.AddSpacer();
            UserInfoExtensionsMod.menu.AddSimpleButton("Bio Links", ShowBioLinksPopup);
            UserInfoExtensionsMod.menu.AddSimpleButton("Bio Languages", ShowBioLanguagesPopup);
            UserInfoExtensionsMod.menu.AddSpacer();
        }
        public override void UiInit() //This is a shit show but it works so shshshhhshh
        {
            ClassInjector.RegisterTypeInIl2Cpp<BioLinksPopup>();
            GameObject popupGameObject = GameObject.Find("UserInterface/MenuContent/Popups/UpdateStatusPopup");
            popupGameObject = UnityEngine.Object.Instantiate(popupGameObject, popupGameObject.transform.parent);
            UnityEngine.Object.DestroyImmediate(popupGameObject.GetComponent<PopupUpdateStatus>());
            UnityEngine.Object.DestroyImmediate(popupGameObject.transform.Find("Popup/StatusSettings/DoNotDisturbStatus").gameObject);
            UnityEngine.Object.DestroyImmediate(popupGameObject.transform.Find("Popup/StatusSettings/OfflineStatus").gameObject);
            UnityEngine.Object.DestroyImmediate(popupGameObject.transform.Find("Popup/StatusHistoryButton").gameObject);
            UnityEngine.Object.DestroyImmediate(popupGameObject.transform.Find("Popup/InputFieldStatus").gameObject);
            foreach (I2.Loc.Localize component in popupGameObject.GetComponentsInChildren<I2.Loc.Localize>()) UnityEngine.Object.Destroy(component);

            bioLinksPopup = popupGameObject.AddComponent<BioLinksPopup>();

            bioLinksPopup.field_Public_String_0 = "LINKS_POPUP"; //Required to make popup work

            bioLinksPopup.field_Public_Button_0 = popupGameObject.transform.Find("Popup/ExitButton").GetComponent<UnityEngine.UI.Button>();
            bioLinksPopup.field_Public_Button_0.onClick.AddListener((UnityEngine.Events.UnityAction)(() => bioLinksPopup.Close()));

            bioLinksPopup.toggleGroup = popupGameObject.GetComponent<UnityEngine.UI.ToggleGroup>();

            bioLinksPopup.openLinkButton = popupGameObject.transform.Find("Popup/Buttons/UpdateButton").GetComponent<UnityEngine.UI.Button>();
            bioLinksPopup.openLinkButton.onClick = new UnityEngine.UI.Button.ButtonClickedEvent();
            bioLinksPopup.openLinkButton.onClick.AddListener(new Action(() => bioLinksPopup.OnOpenLink()));
            bioLinksPopup.openLinkButton.gameObject.name = "OpenLinkButton";
            bioLinksPopup.openLinkButton.transform.GetComponentInChildren<UnityEngine.UI.Text>().text = "Open Link";

            popupGameObject.transform.Find("Popup/UpdateStatusTitleText").GetComponent<UnityEngine.UI.Text>().text = "Open Bio Link";

            bioLinksPopup.linkTexts = new UnityEngine.UI.Text[3];
            bioLinksPopup.icons = new UnityEngine.UI.RawImage[3];
            bioLinksPopup.linkStates = new GameObject[3];
            Transform statusSettings = popupGameObject.transform.Find("Popup/StatusSettings");
            for (int i = 0; i < 3; i++)
            {
                UnityEngine.UI.Toggle toggle = statusSettings.GetChild(i).GetComponent<UnityEngine.UI.Toggle>();
                bioLinksPopup.linkStates[i] = toggle.gameObject;

                UnityEngine.Object.DestroyImmediate(toggle.transform.FindChild("StatusIcon").GetComponent<UiStatusIcon>());
                bioLinksPopup.icons[i] = toggle.transform.FindChild("StatusIcon").GetComponent<UnityEngine.UI.RawImage>();

                toggle.transform.FindChild("StatusIcon").name = "WebsiteIcon";

                bioLinksPopup.toggleGroup.RegisterToggle(toggle);
                toggle.group = bioLinksPopup.toggleGroup;
                toggle.onValueChanged.AddListener(new Action<bool>((state) =>
                {
                    if (!state)
                    {
                        toggle.transform.FindChild("Description").GetComponent<UnityEngine.UI.Text>().color = new Color(0.5f, 0.5f, 0.5f);
                    }
                    else
                    {
                        toggle.transform.FindChild("Description").GetComponent<UnityEngine.UI.Text>().color = Color.white;

                        bioLinksPopup.currentLink = bioLinks[toggle.transform.GetSiblingIndex()];
                    }
                }));

                toggle.transform.FindChild("Description").GetComponent<UnityEngine.UI.Text>().color = new Color(0.5f, 0.5f, 0.5f);

                bioLinksPopup.linkTexts[i] = toggle.transform.FindChild("Description").GetComponent<UnityEngine.UI.Text>();

                toggle.transform.gameObject.name = $"BioLink{i + 1}";
            }

            bioLinksPopup.gameObject.name = "BioLinksPopup";

            ClassInjector.RegisterTypeInIl2Cpp<BioLanguagesPopup>();
            popupGameObject = GameObject.Find("UserInterface/MenuContent/Popups/UpdateStatusPopup");
            popupGameObject = UnityEngine.Object.Instantiate(popupGameObject, popupGameObject.transform.parent);
            UnityEngine.Object.DestroyImmediate(popupGameObject.GetComponent<PopupUpdateStatus>());
            UnityEngine.Object.DestroyImmediate(popupGameObject.transform.Find("Popup/StatusSettings/DoNotDisturbStatus").gameObject);
            UnityEngine.Object.DestroyImmediate(popupGameObject.transform.Find("Popup/StatusSettings/OfflineStatus").gameObject);
            UnityEngine.Object.DestroyImmediate(popupGameObject.transform.Find("Popup/InputFieldStatus").gameObject);
            UnityEngine.Object.DestroyImmediate(popupGameObject.transform.Find("Popup/StatusHistoryButton").gameObject);
            foreach (I2.Loc.Localize component in popupGameObject.GetComponentsInChildren<I2.Loc.Localize>()) UnityEngine.Object.Destroy(component);

            bioLanguagesPopup = popupGameObject.AddComponent<BioLanguagesPopup>();

            bioLanguagesPopup.field_Public_String_0 = "LANGUAGES_POPUP"; //Required to make popup work

            bioLanguagesPopup.field_Public_Button_0 = popupGameObject.transform.Find("Popup/ExitButton").GetComponent<UnityEngine.UI.Button>();
            bioLanguagesPopup.field_Public_Button_0.onClick.AddListener((UnityEngine.Events.UnityAction)(() => bioLanguagesPopup.Close()));

            bioLanguagesPopup.closeButton = popupGameObject.transform.Find("Popup/Buttons/UpdateButton").GetComponent<UnityEngine.UI.Button>();
            bioLanguagesPopup.closeButton.onClick = new UnityEngine.UI.Button.ButtonClickedEvent();
            bioLanguagesPopup.closeButton.onClick.AddListener(new Action(() => bioLanguagesPopup.Close()));
            bioLanguagesPopup.closeButton.gameObject.name = "CloseButton";
            bioLanguagesPopup.closeButton.transform.GetComponentInChildren<UnityEngine.UI.Text>().text = "Close";

            popupGameObject.transform.Find("Popup/UpdateStatusTitleText").GetComponent<UnityEngine.UI.Text>().text = "Bio Languages";

            bioLanguagesPopup.languageTexts = new UnityEngine.UI.Text[3];
            bioLanguagesPopup.languageStates = new GameObject[3];
            statusSettings = popupGameObject.transform.Find("Popup/StatusSettings");
            for (int i = 0; i < 3; i++)
            {
                UnityEngine.Object.Destroy(statusSettings.GetChild(i).GetComponent<UnityEngine.UI.Toggle>());
                bioLanguagesPopup.languageStates[i] = statusSettings.GetChild(i).gameObject;

                UnityEngine.Object.DestroyImmediate(statusSettings.GetChild(i).transform.FindChild("StatusIcon").GetComponent<UiStatusIcon>());

                statusSettings.GetChild(i).transform.FindChild("StatusIcon").GetComponent<UnityEngine.UI.RawImage>().color = Color.white;
                UnityEngine.Object.DestroyImmediate(statusSettings.GetChild(i).transform.FindChild("Highlight").gameObject);

                statusSettings.GetChild(i).transform.FindChild("Description").GetComponent<UnityEngine.UI.Text>().color = Color.white;

                bioLanguagesPopup.languageTexts[i] = statusSettings.GetChild(i).transform.FindChild("Description").GetComponent<UnityEngine.UI.Text>();

                statusSettings.GetChild(i).transform.gameObject.name = $"BioLanguage{i + 1}";
            }

            bioLanguagesPopup.gameObject.name = "BioLanguagesPopup";
        }
        public override void OnPreferencesSaved()
        {
            bioLinksUixButton?.SetVisible(BioLinksButton.Value);
            bioLanguagesUixButton?.SetVisible(BioLanguagesButton.Value);
        }
        public override void OnUserInfoOpen()
        {
            userLanguages.Clear();
            if (VRCUtils.ActiveUserInUserInfoMenu != null)
            {
                foreach (string tag in VRCUtils.ActiveUserInUserInfoMenu.tags) //Cant use where here because Il2Cpp List and regular List
                {
                    if (tag.StartsWith("language_")) userLanguages.Add(languageLookup[tag.Substring(9)]);
                }
            }
        }

        public static void ShowBioLinksPopup()
        {
            UserInfoExtensionsMod.HideAllPopups();

            if (VRCUtils.ActiveUserInUserInfoMenu.bioLinks == null)
            {
                UiManager.OpenSmallPopup("Notice:", "Cannot get user's links", "Close", new Action(UiManager.ClosePopup));
            }
            else
            {
                CheckLinks(VRCUtils.ActiveUserInUserInfoMenu.bioLinks);
                if (VRCUtils.ActiveUserInUserInfoMenu.bioLinks.Count == 0)
                {
                    UiManager.OpenSmallPopup("Notice:", "This user has no bio links", "Close", new Action(UiManager.ClosePopup));
                }
                else if (bioLinks.Count == 0)
                {
                    UiManager.OpenSmallPopup("Notice:", "This user has invalid links", "Close", new Action(UiManager.ClosePopup));
                }
                else
                {
                    VRCUiManager.prop_VRCUiManager_0.Method_Public_Void_String_Boolean_0("UserInterface/MenuContent/Popups/BioLinksPopup");
                }
            }
        }
        public static void CheckLinks(Il2CppSystem.Collections.Generic.List<string> checkLinks)
        {
            bioLinks = new List<Uri>();
            foreach (string link in checkLinks)
            {
                Uri checkedLink;
                try
                {
                    checkedLink = new Uri(link);
                }
                catch
                {
                    continue;
                }
                bioLinks.Add(checkedLink);
            }
        }

        public static void ShowBioLanguagesPopup()
        {
            UserInfoExtensionsMod.HideAllPopups();

            if (userLanguages == null || userLanguages.Count == 0)
            {
                UiManager.OpenSmallPopup("Notice:", "This user has no bio languages", "Close", new Action(UiManager.ClosePopup));
            }
            else
            {
                VRCUiManager.prop_VRCUiManager_0.Method_Public_Void_String_Boolean_0("UserInterface/MenuContent/Popups/BioLanguagesPopup");
            }
        }
    }
}
