# augmented-reality-project
&gt; 小组 AR 项目 Unity 协作仓库

## 环境
- Unity **2022.3.55f1c1**  
- Vuforia Engine **11.3.4**  

## 首次运行
1. 安装 Unity 2022.3.55f1c1（或2022.3.62f2c1）  
2. `git clone` 本仓库  
3. 用 **Unity Hub → Open 项目根目录**
4. Unity 会自动读取 `Packages/manifest.json` 并下载 Vuforia 等依赖； 

## 协作规范
- 主分支 `main` 保护，合并需 PR + 1 人 review  
- 大文件（&gt;50 MB）用 Git LFS 
- 不要提交 `Library/`, `Temp/`, `*.tgz`, `.vs/`, 见 `.gitignore`

## 功能
- [x] 用户选择3d模型与追踪目标图像
- [x] 用户调整3d模型位置姿态与缩放
- [ ] 除.obj之外，其他格式3d模型的解析导入
- [ ] ……

## 第三方许可
- Vuforia Engine © PTC Inc. 免费开发许可，详见 [vuforia.com/legal](https://vuforia.com/legal)
