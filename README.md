![image](https://github.com/bitnaughts/bitnaughts.assets/blob/master/images/banner.png)

# BitNaughts Unity Project

Interested in setting up the BitNaughts development environment? You're at the right place!

## Prerequisites

- [Unity 2018.4.2f1.exe](https://download.unity3d.com/download_unity/d6fb3630ea75/UnityDownloadAssistant-2018.4.2f1.exe?_ga=2.197447151.1368034446.1560221342-1747770899.1559956949)
  - [More download options here](https://unity3d.com/unity/qa/lts-releases)

## Installation

First, clone this repository and enter it:

```bash
git clone https://github.com/bitnaughts/bitnaughts.unity.git
cd bitnaughts.unity
```

Then, run the shell script to install the various BitNaughts modules. Simply double-click the ```init.sh``` script in the file explorer, or via bash:

```bash
./init.sh
```

Lastly, it is recommended to use GitHub Desktop to manage the various submodules. To allow GitHub Desktop to track them, open the application and go to ```File```, then ```Add Local Repository``` and navigate to each submodule (e.g. ```bitnaughts.ui.ux```) and press ```Add Repository```.

You're all set! Load a scene in ```bitnaughts/Assets/bitnaughts/Scenes/``` and mess around!