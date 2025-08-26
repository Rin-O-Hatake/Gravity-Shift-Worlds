using System;
using Plugins.AltoCityUIPack.Scripts.Button;

namespace Core.Scripts.Player.Attacker
{
    public enum TypePlayerAttack
    {
        Ranged,
        Melee,
    }

    [Serializable]
    public class SlotWeaponButtonView
    {
        public UIButtonManagerCustom Button;
        public TypePlayerAttack Type;
    }
}
