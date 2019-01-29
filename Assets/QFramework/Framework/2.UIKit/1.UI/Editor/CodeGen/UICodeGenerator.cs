﻿/****************************************************************************
 * Copyright (c) 2017 xiaojun、imagicbell
 * Copyright (c) 2017 ~ 2019.1 liangxie 
 * 
 * http://qframework.io
 * https://github.com/liangxiegame/QFramework
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 ****************************************************************************/

namespace QFramework
{
	using UnityEngine;
	using UnityEditor;
	using System.IO;

	public class UICodeGenerator
	{
		[MenuItem("Assets/@UI Kit - Create UICode")]
		public static void CreateUICode()
		{
			var objs = Selection.GetFiltered(typeof(GameObject), SelectionMode.Assets | SelectionMode.TopLevel);
			var displayProgress = objs.Length > 1;
			if (displayProgress) EditorUtility.DisplayProgressBar("", "Create UIPrefab Code...", 0);
			for (var i = 0; i < objs.Length; i++)
			{
				mInstance.CreateCode(objs[i] as GameObject, AssetDatabase.GetAssetPath(objs[i]));
				if (displayProgress)
					EditorUtility.DisplayProgressBar("", "Create UIPrefab Code...", (float) (i + 1) / objs.Length);
			}

			AssetDatabase.Refresh();
			if (displayProgress) EditorUtility.ClearProgressBar();
		}

		private void CreateCode(GameObject obj, string uiPrefabPath)
		{
			if (obj.IsNotNull())
			{
				var prefabType = PrefabUtility.GetPrefabType(obj);
				if (PrefabType.Prefab != prefabType)
				{
					return;
				}

				var clone = PrefabUtility.InstantiatePrefab(obj) as GameObject;
				if (null == clone)
				{
					return;
				}

				UIMarkCollector.mPanelCodeData = new PanelCodeData();
				Debug.Log(clone.name);
				UIMarkCollector.mPanelCodeData.PanelName = clone.name.Replace("(clone)", string.Empty);
				UIMarkCollector.FindAllMarkTrans(clone.transform, "");
				CreateUIPanelCode(obj, uiPrefabPath);

				UISerializer.AddSerializeUIPrefab(obj);

				Object.DestroyImmediate(clone);
			}
		}

		private void CreateUIPanelCode(GameObject uiPrefab, string uiPrefabPath)
		{
			if (null == uiPrefab)
				return;

			var behaviourName = uiPrefab.name;

			var strFilePath = string.Empty;

			var prefabDirPattern = UIKitSettingData.Load().UIPrefabDir;

			if (uiPrefabPath.Contains(prefabDirPattern))
			{
				strFilePath = uiPrefabPath.Replace(prefabDirPattern, UIKitSettingData.GetScriptsPath());

			}
			else if (uiPrefabPath.Contains("/Resources"))
			{
				strFilePath = uiPrefabPath.Replace("/Resources", UIKitSettingData.GetScriptsPath());
			}
			else
			{
				strFilePath = uiPrefabPath.Replace("/" + CodeGenUtil.GetLastDirName(uiPrefabPath), UIKitSettingData.GetScriptsPath());
			}

			strFilePath.Replace(uiPrefab.name + ".prefab", string.Empty).CreateDirIfNotExists();

			strFilePath = strFilePath.Replace(".prefab", ".cs");

			if (File.Exists(strFilePath) == false)
			{
				UIPanelCodeTemplate.Generate(strFilePath, behaviourName, UIKitSettingData.GetProjectNamespace());
			}

			CreateUIPanelDesignerCode(behaviourName, strFilePath);
			Debug.Log(">>>>>>>Success Create UIPrefab Code: " + behaviourName);
		}
		
		private void CreateUIPanelDesignerCode(string behaviourName, string uiUIPanelfilePath)
		{
			var dir = uiUIPanelfilePath.Replace(behaviourName + ".cs", "");
			var generateFilePath = dir + behaviourName + ".Designer.cs";

			UIPanelComponentsCodeTemplate.Generate(generateFilePath, behaviourName, UIKitSettingData.GetProjectNamespace(), UIMarkCollector.mPanelCodeData);

			foreach (var elementCodeData in UIMarkCollector.mPanelCodeData.ElementCodeDatas)
			{
				var elementDir = string.Empty;
				elementDir = elementCodeData.MarkedObjInfo.MarkObj.GetUIMarkType() == UIMarkType.Element
					? (dir + behaviourName + "/").CreateDirIfNotExists()
					: (Application.dataPath + "/" + UIKitSettingData.GetScriptsPath() + "/Components/").CreateDirIfNotExists();
				CreateUIElementCode(elementDir, elementCodeData);
			}
		}

		private static void CreateUIElementCode(string generateDirPath, ElementCodeData elementCodeData)
		{
			if (File.Exists(generateDirPath + elementCodeData.BehaviourName + ".cs") == false)
			{
				UIElementCodeTemplate.Generate(generateDirPath + elementCodeData.BehaviourName + ".cs",
					elementCodeData.BehaviourName, UIKitSettingData.GetProjectNamespace(), elementCodeData);
			}
			
			UIElementCodeComponentTemplate.Generate(generateDirPath + elementCodeData.BehaviourName + ".Designer.cs",
				elementCodeData.BehaviourName, UIKitSettingData.GetProjectNamespace(), elementCodeData);

			foreach (var childElementCodeData in elementCodeData.ElementCodeDatas)
			{
				var elementDir = (generateDirPath + elementCodeData.BehaviourName + "/").CreateDirIfNotExists();
				CreateUIElementCode(elementDir, childElementCodeData);
			}
		}

		private static readonly UICodeGenerator mInstance = new UICodeGenerator();
	}
}