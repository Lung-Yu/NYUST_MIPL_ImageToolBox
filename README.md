# NYUST MIPL ImageToolBox

Image Processing Tools Collection developed by NYUST MIPL (National Yunlin University of Science and Technology - Medical Image Processing Laboratory)

## Overview

這個專案包含多個影像處理工具，主要分為以下幾個子專案：

1. **ImageProcessToolBox** - 主要的影像處理工具集，包含多種濾波器和影像處理方法
2. **ImageProcessEfficacy** - 影像處理效能評估工具
3. **EulerFormula** - 歐拉公式相關的影像處理應用
4. **CuttingImage** - 影像切割工具
5. **ImageSimilarity** - 影像相似度比較工具

## 功能模組

影像處理工具主要包含：

1. **像素點的轉換 (Point Processing)**  
   - 灰階化 (Grayscale)
   - 負片效果 (Negative)
   - 亮度調整 (Brightness Adjustment)
   - 對比度調整 (Contrast Enhancement)
   - 閾值處理 (Thresholding)

2. **空間卷積 (Space Filter)**  
   - 低通濾波器 (Low Pass Filters)
   - 高通濾波器 (High Pass Filters)
   - 均值濾波器 (Mean Filter)
   - Sobel 濾波器 (Edge Detection)

3. **區域分割 (Segmentation)**  
   - 閾值分割 (Threshold Segmentation)
   - 邊緣檢測 (Edge Detection)

4. **雜訊添加 (Noise)**  
   - 高斯雜訊 (Gaussian Noise)
   - 鹽和胡椒雜訊 (Salt and Pepper Noise)

5. **特徵擷取 (Feature Extraction)**  
   - 邊緣檢測 (Edge Detection)
   - 角點檢測 (Corner Detection)

6. **風格轉換 (Style Space Filter)**  
   - 油畫效果 (Oil Painting Effect)
   - 素描效果 (Sketch Effect)

7. **頻率域轉換 (Frequency Domain)**  
   - 傅立葉轉換 (Fourier Transform)
   - 頻率域濾波 (Frequency Domain Filtering)

8. **形態學處理 (Morphology)**  
   - 膨脹 (Dilation)
   - 侵蝕 (Erosion)
   - 開運算 (Opening)
   - 閉運算 (Closing)

## 程式實作上使用之 Design Pattern 技巧

1. **Template Pattern** - 將 Point Processing 與 Space Filter 的處理抽象化為 Template
2. **Strategy Pattern** - 使用不同的演算法策略處理不同類型的影像處理需求
3. **Factory Pattern** - 用於創建不同類型的濾波器和處理器

## 開發環境

- 開發工具：Visual Studio 2013
- 程式語言：C#
- 平台：.NET Framework

## 如何使用

1. 使用 Visual Studio 開啟 `ImageProcessToolBox.sln` 解決方案
2. 選擇你想使用的專案，設為啟動專案
3. 編譯並執行程式
