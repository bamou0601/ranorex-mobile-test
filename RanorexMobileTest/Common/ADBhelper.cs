/*
 * Created by Ranorex
 * 機能: Android ADB共通操作
 * ロジック: ADBコマンドを使用してAndroid Emulatorの操作を共通化する
 * User: mo_ba
 * Date: 2026/09/08
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Diagnostics;
using Ranorex;


namespace RanorexMobileTest.Common
{
	/// Android Emulatorに対するADB操作を提供する共通クラス。
    public static class ADBhelper
    {
     	// ADB実行ファイルのパス
     	private const string AdbPath =
     		@"C:\Users\mo_ba\AppData\Local\Android\Sdk\platform-tools\adb.exe";
    	
     	// テスト対象Android Emulator
     	private const string DeviceId = "emulator-5554";
     	
     	/// 指定した座標をADBでタップする。
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        public static void Tap(int x, int y)
        {
        	Report.Info($"ADBで画面をタップします。座標: ({x}, {y})");
        	
        	ExecuteCommand($"shell input tap {x} {y}");
        	
        	Report.Info($"ADBによるタップが完了しました。座標: ({x}, {y})");    	
        	
        }
        
        /// <summary>
        /// Android画面のUI階層XMLを取得する。
        /// </summary>
        /// <returns>UI階層XML</returns>
        public static string GetUiHierarchy()
        {
        	// Android端末上にUI hierarchyを出力
        	ExecuteCommand(
        		"shell uiautomator dump /sdcard/window.xml"
        	);
        	
        	// XML内容を取得
        	return ExecuteCommand(
        		"shell cat /sdcard/window.xml"
        	);
        }
          
        
        /// <summary>
        /// ADBコマンドを実行する。
        /// </summary>
        /// <param name="arguments">ADBコマンド引数</param>
        /// <returns>ADBコマンドの標準出力</returns>
        public static string ExecuteCommand(string arguments)
        {
        	ProcessStartInfo processInfo = new ProcessStartInfo
        	{
        		FileName = AdbPath,
        		Arguments = $"-s {DeviceId} {arguments}",
        		UseShellExecute = false,
        		RedirectStandardOutput = true,
        		RedirectStandardError = true,
        		CreateNoWindow = true
        	};
        	
        	using (Process process = new Process())
        	{	
        		process.StartInfo = processInfo;
        		process.Start();
        		
        		string output = process.StandardOutput.ReadToEnd();
        		string error = process.StandardError.ReadToEnd();
        		
        		process.WaitForExit();
        		
        		if (process.ExitCode != 0)
        		{
        			throw new Exception(
        				$"ADB command failed. " +
        				$"Command={arguments}, " +
                        $"ExitCode={process.ExitCode}, " +
                        $"Error={error}"
        			);        		
        		}
        		
        		return output;
        	}
        }

    }
}
