# An Immersive Virtual Reality Learning Environment for Handheld Mobile Laser Scanner Operation

Official repository for the paper **"An Immersive Virtual Reality Learning Environment for Handheld Mobile Laser Scanner Operation"** presented at ISPRS / PHEDCS 2026.

**Authors:** Clara Garcia-Moll, Alba Gilsanz-Lorenzo, Pedro Arias, and Jesus Balado  
**Affiliation:** GeoTECH, CINTECX, Universidade de Vigo, Spain  

---

## 📌 Overview

Handheld Mobile Laser Scanners (**HMLS**) provide a fast and flexible solution for indoor 3D reality capture. However, point cloud quality is heavily operator-dependent. Inappropriate scanning practices—such as excessive walking pace, sudden turns, scanning too close to walls, or operating near highly reflective/featureless surfaces—frequently cause SLAM registration errors and incomplete point clouds.

This repository contains the standalone **Virtual Reality (VR) learning framework** developed for **Meta Quest 3**. Designed as an introductory familiarization tool prior to operating real HMLS hardware, the application guides users through a simulated 3D scanning experience with real-time, context-aware guidance.

<img width="426" height="240" alt="Proyecto de vídeo 2" src="https://github.com/user-attachments/assets/0fba4716-cd54-4605-92af-da04d7b0f747" />


---

## ✨ Key Features

- **Standalone VR Execution**: Natively targeted for Meta Quest 3 via **Unity** and **OpenXR** (no external PC required).
- **Realistic Indoor Scenario**: Recreates a 10×7 m residential apartment based on a real Building Information Model (BIM) from Svalbard, Norway.
- **Real-Time Contextual Feedback**:
  - **Behavior-Based Warnings**: Real-time detection of excessive walking speed, abrupt head/scanner rotation, and hand instability.
  - **Location-Based Warnings**: Triggers upon entering challenging areas (reflective surfaces like refrigerators/glass/mirrors, featureless walls, low-light areas).
- **Non-Intrusive Guidance & Reports**: Contextual UI warning panels with optional "More Info" deep-dives, followed by an end-of-session summary report reviewing all 10 core acquisition factors.

---

## 🏗 Framework Architecture & Workflow

```
[ Introductory Area ] ──> [ Apartment Exploration ] ──> [ Contextual Warning Detection ]
                                                                   │
                                                                   ▼
[ Post-Training Report ] <── [ Behavioral Correction ] <── [ Warning Explanation ]
```

1. **Exploration**: Free 6DoF movement and joystick navigation across domestic spaces (living room, kitchen, bedroom, bathroom, storage).
2. **Detection**: Continuous tracking of user head pose and HMLS controller position.
3. **Feedback**: Instant real-time warning pop-ups with concise explanations.
4. **Summary**: A final checklist report displaying user performance across all 10 acquisition principles.

---

## 🛠 Project Requirements & Setup

### Hardware Requirements
- **Meta Quest 3** HMD
- Touch Controllers

### Software Requirements
- **Unity Engine** (`2022.3 LTS` or newer recommended)
- **OpenXR Plugin** configured for Android / Meta Quest build target
- **XR Interaction Toolkit**

### Setup & Deployment
1. Clone this repository to your local computer.
2. Open the project folder in **Unity Hub**.
3. Set the build platform to **Android** in `File > Build Settings`.
4. Connect your Meta Quest 3 via USB (with Developer Mode enabled) and click **Build and Run**.

---

## 📊 User Evaluation Summary

The framework was evaluated through a user study ($N=8$) assessing usability and educational knowledge transfer:

| Metric / Dimension | Score / Result |
| :--- | :--- |
| **Ease of Use** | **4.75 / 5.00** ($\pm 0.43$) |
| **Visual Information Amount** | **4.25 / 5.00** ($\pm 0.66$) |
| **Clarity of Information** | **4.00 / 5.00** ($\pm 0.00$) |
| **Coverage of Fundamental Principles** | **87.5%** agreed fundamental principles were covered |
| **Perceived Knowledge Gain** | **62.5%** learned new factors affecting scan quality |

---

## 📄 Citation

If you find this software or framework useful in your research, please cite our paper:

```bibtex
@inproceedings{garciamoll2025hmls_vr,
  title     = {An Immersive Virtual Reality Learning Environment for Handheld Mobile Laser Scanner Operation},
  author    = {Garcia-Moll, Clara and Gilsanz-Lorenzo, Alba and Arias, Pedro and Balado, Jesus},
  booktitle = {The International Archives of the Photogrammetry, Remote Sensing and Spatial Information Sciences (PHEDCS 2025)},
  year      = {2026},
  address   = {Tashkent, Uzbekistan},
  publisher = {ISPRS}
}
```

---

## 🤝 Acknowledgements

This research was conducted within the framework of the **SUM4Re project** (*Creating materials banks from digital urban mining*), receiving funding from the European Union's Horizon Europe research and innovation program under Grant Agreement No. 101129961. Additional funding was provided by the Government of Spain (RYC2022-038100-I / MCIN/AEI/10.13039/501100011033 and FSE+) and Xunta de Galicia—GAIN (EDC431C 2024/30).
