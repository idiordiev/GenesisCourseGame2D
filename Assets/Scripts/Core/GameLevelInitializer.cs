﻿using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Core
{
    public class GameLevelInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private GameUIInputView _gameUIInputView;

        private ExternalDevicesInputReader _externalDevicesInputReader;
        private PlayerBrain _playerBrain;

        private bool _onPause = false;
            
        private void Awake()
        {
            _externalDevicesInputReader = new ExternalDevicesInputReader();
            _playerBrain = new PlayerBrain(_playerEntity,
                new List<IEntityInputSource>() { _gameUIInputView, _externalDevicesInputReader });
        }

        private void Update()
        {
            if(_onPause)
                return;
            
            _externalDevicesInputReader.OnUpdate();
        }

        private void FixedUpdate()
        {
            if(_onPause)
                return;
            
            _playerBrain.OnFixedUpdate();
        }
    }
}