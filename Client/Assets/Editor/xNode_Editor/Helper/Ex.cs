using UnityEngine;

namespace Editor.xNode_Editor
{
    public static class Ex
    {
        public static int GetIndex(this string fieldName)
        {
            if (int.TryParse(fieldName.Substring(fieldName.LastIndexOf(" ")),out int index))
            {
                return index;
            }
            else
            {
                return -1;
            }
        }
    }
}