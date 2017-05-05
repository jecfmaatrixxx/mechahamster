﻿// Copyright 2017 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Firebase.Unity.Editor;

namespace Hamster.States {
  // Test loop class, for automated testing.
  // Loads up a level + replay data, plays through it,
  // and then exits the app.
  class TestLoop : BaseState {
    public TestLoop() { }

    // When we get back from the gameplay state, exit the app:
    public override void Resume(StateExitValue results) {
      Debug.Log("Shutting down!");
      Application.Quit();
    }

    public override void Initialize() {
      TextAsset json = Resources.Load(StringConstants.TestLoopLevel) as TextAsset;
      LevelMap currentLevel = JsonUtility.FromJson<LevelMap>(json.ToString());
      currentLevel.DatabasePath = null;
      CommonData.gameWorld.DisposeWorld();
      CommonData.gameWorld.SpawnWorld(currentLevel);

      manager.PushState(new Gameplay(Gameplay.GameplayMode.TestLoop));
    }

  }

}
