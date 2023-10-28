using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public partial class ButtonWithText : MonoBehaviour
    {
        [SerializeField, PublicAccessor] Button button;
        [SerializeField, PublicAccessor] TMP_Text buttonText;
    }
}