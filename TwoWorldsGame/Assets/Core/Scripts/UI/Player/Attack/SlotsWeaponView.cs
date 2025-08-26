using System.Collections.Generic;
using Core.Scripts.Player.Attacker;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Scripts.UI.Player.Attack
{
    public class SlotsWeaponView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<SlotWeaponButtonView> _slotWeaponButtonViews;
        
        private CompositeDisposable _compositeDisposable  = new CompositeDisposable();

        #endregion

        #region Inject

        [Inject]
        private void Construct(ISenderTypeAttack sender)
        {
            sender.Type.Subscribe(ChangeFocussedSlot).AddTo(_compositeDisposable);
        }

        #endregion

        private void ChangeFocussedSlot(TypePlayerAttack type)
        {
            foreach (var slot in _slotWeaponButtonViews)
            {
                slot.Button.SetIntractable(slot.Type == type);
            }
        }

        #region MonoBehaviour

        private void OnDestroy()
        {
            _compositeDisposable.Dispose();
        }

        #endregion
    }
}
