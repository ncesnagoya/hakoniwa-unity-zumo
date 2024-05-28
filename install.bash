#!/bin/bash


if [ -d plugin/plugin-srcs ]
then
    if [ ! -d plugin/plugin-srcs/Assets/Model/Zumo -a ! -d zumo/Assets/Model/Zumo ]
    then
        echo "ERROR can not found Zumo.zip"
        echo "please download here: https://github.com/ncesnagoya/hakoniwa-unity-zumo/releases/tag/v1.2.0"
        echo "and unzip Zumo.zip on the zumo/Assets/Model, "
        echo "after that, please remove Zumo.zip"
        exit 1
    fi
else
    echo "ERROR: can not find submodule plugin"
    echo "git pull"
    echo "git submodule update --init --recursive"
    exit 1
fi

cd plugin

bash install.bash
cd ..

cp -rp zumo/Assets/* plugin/plugin-srcs/Assets/
cp -rp zumo/Resources/* plugin/plugin-srcs/
