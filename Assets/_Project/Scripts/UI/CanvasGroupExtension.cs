using UnityEngine;

namespace Sensorama.Core
{
    public static class CanvasGroupExtension
    {
        public static void CanvasGroupEnable(this CanvasGroup canvas, bool enable)
        {
            canvas.alpha = enable ? 1 : 0;
            canvas.interactable = enable;
            canvas.blocksRaycasts = enable;
        }
    }
}
