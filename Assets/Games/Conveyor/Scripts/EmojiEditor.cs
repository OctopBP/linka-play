using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Emoji))]
public class EmojiDrawerUIE : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var newFont = AssetDatabase.LoadAssetAtPath<Font>("Assets/Other/Fonts/Apple Color Emoji.ttc");
        var container = new VisualElement();
        var codeField = new PropertyField(property.FindPropertyRelative("code"), "Emoji")
        {
            style =
            {
                unityFontDefinition = new FontDefinition
                {
                    fontAsset = FontAsset.CreateFontAsset(newFont)
                }
            }
        };

        container.Add(codeField);
        return container;
    }
}