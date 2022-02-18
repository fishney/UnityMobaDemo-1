using UnityEngine;
using UnityEngine.UI;

namespace HOK.Expansion
{
    public static class CodingK_Expansion
    {
        public static void SetSprite(this Image img,string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                img.sprite = null;
            }
            else
            {
                Sprite sp = ResSvc.Instance().LoadSprite(path, true);
                img.sprite = sp;
            }
        }
        
    }
}