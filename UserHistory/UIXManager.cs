using System;
using UIExpansionKit.API;
using UIExpansionKit.API.Controls;
using UnityEngine;

namespace UserHistory
{
    class UIXManager
    {
        public static void AddMethodToUIInit()
        {
            ExpansionKitApi.OnUiManagerInit += UserHistoryMod.Instance.OnUiManagerInit;
        }

        private static IMenuButton openButton;

        public static void AddOpenButtonToUIX()
        {
            openButton = ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Open User History", new Action(MenuManager.OpenUserHistoryMenu));
            openButton.SetVisible(Config.useUIX.Value);
            Config.useUIX.OnValueChanged += OnUseUIXChange;
        }
        public static void OnUseUIXChange(bool oldValue, bool newValue)
        {
            if (oldValue == newValue) return;

            openButton?.SetVisible(newValue);
            MenuManager.openButton.gameObject.SetActive(!newValue);
        }
    }
}
