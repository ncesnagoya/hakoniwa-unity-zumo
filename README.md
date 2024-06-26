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

### 寸法について

Zumo ロボットの寸法は、以下の 3D モデル（Zumo.zip）を 10 倍しています。

https://github.com/ncesnagoya/hakoniwa-unity-zumo/releases

理由は、Unityの単位が、１ユニット＝１メートルでありますが、本物のzumoは 10cm 程度と小さいためです。

経験上、Unityで扱うオブジェクトのサイズが小さいとシミュレーションが適切に行われないケースがあり、Unityの１ユニットの単位で揃えておく方が良いと判断。

### 対応しているセンサとアクチュエータ

- [x] ラインフォロワーセンサー
- [X] ジャイロセンサー
- [X] 加速度センサー
- [X] 磁気センサー
- [X] モーター
- [X] LED

### ラインフォロワーセンサーの数と取付位置

| センサー | 実機の幅(mm) | Unityの幅(m)| Unityのオフセット(m)|
| ------- | ------- | ------- | ------- |
|右端| 0.00(0.00)  | 0.0   |0.330|
| 1 | 3.49(0.1375)   | 0.035   |0.295|
| 2 | 13.58(0.535)   | 0.136   |0.159|
| 3 | 11.17(0.44)   | 0.112   |0.048|
| 4 | 9.52(0.375)   | 0.095   |-0.048|
| 5 | 11.17(0.44)   | 0.112   |-0.159|
| 6 | 13.58(0.535)   | 0.136   |-0.295|
|左端| 3.49(0.1375)  | 0.035   |-0.330|
|sum|66(2.6)|0.66||

![image](https://github.com/ncesnagoya/hakoniwa-unity-zumo/assets/164193/c90a84df-eaf8-46b5-967e-d1114c60a127)

参考：https://www.pololu.com/docs/0J57/2.c


### ジャイロセンサー/加速度センサー/磁気センサーについて

Unityは左手系ですが、PDU通信データは右手系で計算した値を設定しています。

|指|座標|左手系（Unity）|←|右手系（ROS）|←|	
| ------- | ------- | ------- | ------- |  ------- |  ------- | 
|指|座標|自分中心|ロボット|自分中心|ロボット|
|親指|X|後|右|右|前方|
|人差し指|Y|上|上|前|左|
|中指|Z|右|前方|上|上|

### PDUデータ

#### センサ

* [ZumoPduSensor](https://github.com/ncesnagoya/hakoniwa-zumosim/blob/main/workspace/src/zumo/pdu/zumo_msgs/msg/ZumoPduSensor.msg)
  * line_sensors: ラインフォロワーセンサー（配列：６個）
    * [ZumoPduLineSensor](https://github.com/ncesnagoya/hakoniwa-zumosim/blob/main/workspace/src/zumo/pdu/zumo_msgs/msg/ZumoPduLineSensor.msg)
      * brightness: 0-1000 の値（0: 黒、1000: 白)
  * imu: ジャイロセンサー/加速度センサー/磁気センサー
    * [ZumoPduImu](https://github.com/ncesnagoya/hakoniwa-zumosim/blob/main/workspace/src/zumo/pdu/zumo_msgs/msg/ZumoPduImu.msg)
      * acc: 加速度センサー
      * gyro: ジャイロセンサ―
      * mag: 磁気センサー
      * 補足：
        * 角度はオイラー角（度）
        * 加速度の距離は実際の寸法に合わせています（単位：m)
  * 補足：
    * irs は定義のみで、未使用

#### アクチュエータ

* [ZumoPduActuator](https://github.com/ncesnagoya/hakoniwa-zumosim/blob/main/workspace/src/zumo/pdu/zumo_msgs/msg/ZumoPduActuator.msg)
  * motors: モーター（配列：２個）
    * [ZumoPduMotor](https://github.com/ncesnagoya/hakoniwa-zumosim/blob/main/workspace/src/zumo/pdu/zumo_msgs/msg/ZumoPduMotor.msg)
      * power: モーター回転指示値（値が大きいほど高い回転数が得られる。プラス値は正回転、マイナス値は逆回転）
  * led: LED
    * [ZumoPduLed ](https://github.com/ncesnagoya/hakoniwa-zumosim/blob/main/workspace/src/zumo/pdu/zumo_msgs/msg/ZumoPduLed.msg)
      * green: 車体先頭の２個のLEDのON/OFF指示。
        * ON: true
        * OFF: false
      * 補足：
        * red, blue, yellow は定義のみで、未使用









## 事前準備

２つのリポジトリをクローンします。

```
git clone --recursive git@github.com:ncesnagoya/hakoniwa-zumosim.git
```

```
git clone --recursive git@github.com:ncesnagoya/hakoniwa-unity-zumo.git
```

`Zumo.zip` をダウンロードし解凍して、`Zumo`フォルダ以下を `plugin/plugin-srcs/Assets/Model` にコピーします。

https://github.com/ncesnagoya/hakoniwa-unity-zumo/releases/download/v1.2.0/Zumo.zip

## インストール手順


クローンが終わったら、以下のようにディレクトリ移動します。

```
cd hakoniwa-unity-zumo/
```

そして、必要なUnityモジュール類をインストールします。

```
 bash install.bash 
```

unzipがないというエラーが出た場合は場合はインストールをしてください

```
sudo apt-get install -y unzip 
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

なお、以下のダイアログが出ますので、`TMP Importer` の`Import TMP Essentials` をクリックして、クローズしましょう。

![image](https://github.com/ncesnagoya/hakoniwa-unity-zumo/assets/164193/b81f796c-41a4-4f80-adac-bd353c354f8c)


このまま、`Window/Hakoniwa/GenerateDebug` をクリックしましょう。

![image](https://github.com/toppers/hakoniwa-openel-cpp/assets/164193/8be12b93-48d8-4fee-bac0-4e02ca0e6a9d)

コンフィグファイルの出力先は、`Assets/Resources`配下です。

補足：Resources配下のファイルは、起動時にUnityエディタがキャッシュしているため、Generateした場合は、再起動してください。

シミュレーション開始ボタンを押下し、以下の画面が出力され、コンソールにエラーメッセージが出力されていなければ成功です。

![image](https://github.com/ncesnagoya/hakoniwa-unity-zumo/assets/164193/995687a6-b9bc-4723-bde2-819ceb107181)


## hakoniwa-zumosim と連携して開発する場合

Unityエディタ起動後、プロジェクトビューの　`Scenes/Zumo/Hakoniwa` をダブルクリックしてください。

![image](https://github.com/ncesnagoya/hakoniwa-unity-zumo/assets/164193/98550577-e69c-414e-966d-69abdfb1c10f)


このまま、`Window/Hakoniwa/Generate` をクリックしましょう。

![image](https://github.com/ncesnagoya/hakoniwa-unity-zumo/assets/164193/08c1a452-f8be-4c3b-b062-09577e368e3d)

コンフィグファイルの出力先は、`Assets/Resources`配下です。

補足：Resources配下のファイルは、起動時にUnityエディタがキャッシュしているため、Generateした場合は、再起動してください。

シミュレーションを開始する場合は、`hakoniwa-zumosim`配下で、以下のコマンドを実行してください。

```
bash docker/run.bash
```

その後、シミュレーション開始ボタンを押下し、コンソールにエラーメッセージが出力されていなければ成功です。

シミュレーションを停止する場合は、Unityのシミュレーションを停止後に、`hakoniwa-zumosim`の端末を `CTRL+C` してください。
