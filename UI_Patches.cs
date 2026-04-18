using HarmonyLib;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

namespace sebdalf
{
    [HarmonyPatch(typeof(UIStationWindow))]
    public static class UIStationWindow_Scrolling_Patch
    {
        [HarmonyPatch(typeof(UIStationWindow), "_OnOpen")]
        [HarmonyPostfix]
        public static void OnOpenPostfix(UIStationWindow __instance)
        {
            Transform storageGroup = __instance.transform.Find("storage-group");
            if (storageGroup == null) return;

            ScrollRect scroll = storageGroup.GetComponent<ScrollRect>();
            if (scroll == null)
            {
                scroll = storageGroup.gameObject.AddComponent<ScrollRect>();

                scroll.gameObject.AddComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 0.01f);
                storageGroup.gameObject.AddComponent<Mask>().showMaskGraphic = false;

                scroll.content = storageGroup.GetComponent<RectTransform>();
                scroll.horizontal = false;
                scroll.vertical = true;
                scroll.movementType = ScrollRect.MovementType.Clamped;
                scroll.scrollSensitivity = 20f;
            }

            RectTransform rect = storageGroup.GetComponent<RectTransform>();
            float slotHeight = 80f;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, BetterLogisticsStation.InterstellarLogisticsStationMaxItemKinds.Value * slotHeight);
        }
    }
}