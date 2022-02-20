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
                img.gameObject.SetActive(false);
            }
            else
            {
                if (!img.gameObject.activeSelf)
                {
                    img.gameObject.SetActive(true);
                }
                Sprite sp = ResSvc.Instance().LoadSprite(path, true);
                img.sprite = sp;
            }
        }
        
        public static void SetBagItemSprite(this Image img,string imgPath)
        {
            if (string.IsNullOrEmpty(imgPath))
            {
                img.gameObject.SetActive(false);
            }
            else
            {
                if (!img.gameObject.activeSelf)
                {
                    img.gameObject.SetActive(true);
                }
                Sprite sp = ResSvc.Instance().LoadSprite($@"ResImages/ResItems/{imgPath}", true);
                img.sprite = sp;
            }
        }
        
    }
}