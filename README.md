# hakoniwa-unity-zumo

## 前提とする環境

* OS
  * Windows 10/11 WSL2 Ubuntu 22.04.1 LTS
* Unity Hub
  * Unity Hub 3.5.2
* Unity
  * Unity 2022.3.8f1

なお、Unityおよび Unity Hub はインストールされていることを前提として解説します。

## Zumo ロボット情報

### 寸法と寸法

Zumo ロボットの

## 事前準備

２つのリポジトリをクローンします。

```
git clone --recursive git@github.com:ncesnagoya/hakoniwa-zumosim.git
```

```
git clone --recursive git@github.com:ncesnagoya/hakoniwa-unity-zumo.git
```

## インストール手順


クローンが終わったら、以下のようにディレクトリ移動します。

```
cd hakoniwa-unity-zumo/
```

そして、必要なUnityモジュール類をインストールします。

```
 bash install.bash 
```

この状態で Unity Hub で当該プロジェクトを開きましょう。

対象フォルダ：`hakoniwa-unity-zumo\plugin\plugin-srcs`

途中、バージョンが異なる旨のメッセージが出ますが、`Continue` してください。

![image](https://github.com/ncesnagoya/hakoniwa-unity-zumo/assets/164193/20658510-6990-4630-80c7-42620f6dfb55)

もし、以下のダイアログが出た場合は、`Ignore` してください。

![image](https://github.com/ncesnagoya/hakoniwa-unity-zumo/assets/164193/1ac1a546-adb3-4a97-936a-9c9f11959dd7)

成功するとこうなります。

![image](https://github.com/ncesnagoya/hakoniwa-unity-zumo/assets/164193/43b9162f-7c5f-4011-9805-568d567dea69)


次に、`Newtonsoft.Json` が不足している場合は、以下で対応してください。

PackageManager左上の+ボタンから、「Add package from git URL...」で `com.unity.nuget.newtonsoft-json` を追加します。

## Unity単独で開発する場合

Unityエディタ起動後、プロジェクトビューの　`Scenes/Zumo/Work` をダブルクリックしてください。

![image](https://github.com/ncesnagoya/hakoniwa-unity-zumo/assets/164193/b454bb9e-013a-44ec-a863-3b5eff34f769)

以下のダイアログが出ますので、`TMP Importer` の`Import TMP Essentials` をクリックしてください。

![image](https://github.com/ncesnagoya/hakoniwa-unity-zumo/assets/164193/dda63a23-18c2-4f2e-aa00-968773c03a1d)

このまま、`Window/Hakoniwa/GenerateDebug` をクリックしましょう。

![image](https://github.com/toppers/hakoniwa-openel-cpp/assets/164193/8be12b93-48d8-4fee-bac0-4e02ca0e6a9d)


## hakoniwa-zumosim と連携して開発する場合

Unityエディタ起動後、プロジェクトビューの　`Scenes/Zumo/Hakoniwa` をダブルクリックしてください。

![image](https://github.com/ncesnagoya/hakoniwa-unity-zumo/assets/164193/98550577-e69c-414e-966d-69abdfb1c10f)


このまま、`Window/Hakoniwa/Generate` をクリックしましょう。

![image](https://github.com/ncesnagoya/hakoniwa-unity-zumo/assets/164193/08c1a452-f8be-4c3b-b062-09577e368e3d)
