#!/bin/bash
cd Assets
# BitNaughts Core Modules
git clone --submodule https://github.com/bitnaughts/bitnaughts.git
git clone --submodule https://github.com/bitnaughts/bitnaughts.assets.git
git clone --submodule https://github.com/bitnaughts/bitnaughts.multiplayer.git
git clone --submodule https://github.com/bitnaughts/bitnaughts.world.git
git clone --submodule https://github.com/bitnaughts/bitnaughts.ui.ux.git
git clone --submodule https://github.com/bitnaughts/csharp.interpreter.git
git clone --submodule https://github.com/bitnaughts/sql.interpreter.git
# Async/Await Integration Library
git clone https://github.com/bitnaughts/unity.async.git
