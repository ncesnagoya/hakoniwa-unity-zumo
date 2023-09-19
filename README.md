# hakoniwa-unity-zumo

## 前提とする環境

* OS
  * Windows 10/11 WSL2 Ubuntu 22.04.1 LTS
* Unity Hub
  * Unity Hub 3.5.2
* Unity
  * Unity 2022.3.8f1

なお、Unityおよび Unity Hub はインストールされていることを前提として解説します。

## 事前準備

２つのリポジトリをクローンします。

```
git clone --recursive https://github.com/ncesnagoya/hakoniwa-zumosim.git
```

```
git clone --recursive https://github.com/ncesnagoya/hakoniwa-unity-zumo.git
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

成功するとこうなります。

TODO

なお、Unityエディタのバージョンによっては、起動中にエラーとなる場合があります。
その場合、途中、ダイアログがポップアップされて警告されますが、気にせず起動しましょう。

原因は、`Newtonsoft.Json` が不足しているためです。
対応方法は、下記記事にある通り、Unityのパッケージマネージャから `Newtonsoft.Json`をインストールすることで解消できます。

https://qiita.com/sakano/items/6fa16af5ceab2617fc0f

## Unity単独で開発する場合

Unityエディタ起動後、プロジェクトビューの　`Scenes/TraninModel/Work` をダブルクリックしてください。

![image](https://github.com/toppers/hakoniwa-unity-ev3model/assets/164193/af772f9c-a79f-4712-8ad5-bbc24a874d24)


このまま、`Window/Hakoniwa/GenerateDebug` をクリックしましょう。

![image](https://github.com/toppers/hakoniwa-openel-cpp/assets/164193/8be12b93-48d8-4fee-bac0-4e02ca0e6a9d)


## hakoniwa-zumosim と連携して開発する場合

TODO
