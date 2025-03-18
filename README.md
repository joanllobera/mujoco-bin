# Mujoco Binaries for Unity

This repository contains binaries to simplify running MuJoCo in Unity.

The MuJoCo Binaries allows the Unity Editor and runtime to use the MuJoCo Unity plugin without need of adding the binaries to the project. To know more about the MuJoCo plugin for Unity, check [Unity Plug-in - MuJoCo Documentation](https://mujoco.readthedocs.io/en/latest/unity.html)

An important feature is that android support is also included. To compile the mujoco library for android, we use the procedure descring in the pdf in this fork: [GitHub - yvesantzero/mujoco: Multi-Joint dynamics with Contact. A general purpose physics simulator.](https://github.com/yvesantzero/mujoco)

To integrate this package in your project, just add a link to in the manifest file. Make sure you point to the same MuJoCo plugin that the binaries. Current mujoco release being used is **3.3.0**

To add it, the file  `manifest.json`in the packages folder should have two lines that look like this: 

```
"org.mujoco": "https://github.com/deepmind/mujoco.git?path=unity#3.3.0",
"bin.mujoco": "https://github.com/joanllobera/mujoco-bin.git#3.3.0",
```
