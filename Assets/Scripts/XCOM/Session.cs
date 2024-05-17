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

        // Private Properties //
        [SerializeField] private int nPlayers;
        [SerializeField] private Player playerPrefab;

        private List<Player> players;

        // Cached Components //

        // Cached References //

        // Public Methods //

        // Private Methods //
        private void Awake()
        {
            InitialisePlayers();
        }

        private void InitialisePlayers()
        {
            players = new List<Player>();

            for (int i = 0; i < nPlayers; i++)
            {
                Player newPlayer = Instantiate(playerPrefab, transform.position, Quaternion.identity, transform);

                newPlayer.name = $"Player {i + 1}";

                players.Add(newPlayer);
            }
        }
    }
}
