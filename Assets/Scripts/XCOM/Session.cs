using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace XCOM.Grid
{
    public class Session : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //
        protected UIManager UIManager
        {
            get
            {
                if (!_uiManager) _uiManager = FindObjectOfType<UIManager>();
                return _uiManager;
            }
        }

        protected UnitController UnitController
        {
            get
            {
                if (!_unitController) _unitController = FindObjectOfType<UnitController>();
                return _unitController;
            }
        }

        // Private Properties //
        [SerializeField] private int nPlayers;
        [SerializeField] private Player playerPrefab;

        private List<Player> players;

        // Cached Components //

        // Cached References //
        private UIManager _uiManager;
        private UnitController _unitController;

        // Public Methods //

        // Private Methods //
        private void Start()
        {
            InitialisePlayers();

            UnitController.OnUnitSelected += SelectUnit;
        }

        private void InitialisePlayers()
        {
            players = new List<Player>();

            for (int i = 0; i < nPlayers; i++)
            {
                Player newPlayer = Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity, transform);

                newPlayer.name = $"Player {i + 1}";

                players.Add(newPlayer);
            }
        }

        private void SelectUnit(object sender, Unit selectedUnit)
        {
            UIManager.ShowSelectedVisual();
            UIManager.FollowSelectedVisual(selectedUnit.transform);
        }
    }
}
