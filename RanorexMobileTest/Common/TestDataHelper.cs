/*
 * 機能: テストデータ共通処理
 * ロジック: CSVファイルを読み込み、ランダムでテストデータを取得する
 * User: mo_ba
 * Date: 2026/09/08
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using Ranorex;


namespace RanorexMobileTest.Common
{
    /// <summary>
    /// CSVテストデータを管理する共通クラス。
    /// </summary>
   
    public static class TestDataHelper
    {
    	
    	private static readonly Random Random = new Random();
    	
    	        /// <summary>
        /// TestDataフォルダ内のCSVファイルパスを取得する。
        /// </summary>
        /// <param name="fileName">CSVファイル名</param>
        /// <returns>CSVファイルの絶対パス</returns>
        public static string GetTestDataPath(string fileName)
        {
        	return Path.GetFullPath(
        		Path.Combine(
        			AppDomain.CurrentDomain.BaseDirectory,
        			 @"..\..\TestData",
        			 fileName
        		)
        	);
        }
    	
        /// <summary>
        /// CSVファイルからランダムで1行のテストデータを取得する。
        /// </summary>
        /// <param name="filePath">CSVファイルパス</param>
        /// <returns>選択されたテストデータ</returns>
        public static string[] GetRandomCsvData(string filePath)
        {
        	if (!File.Exists(filePath))
        	{
        		throw new Exception(
        			$"Test data file was not found: {filePath}"
        		);
        	}
        	
        	string[] lines = File.ReadAllLines(filePath);
        	
        	// ヘッダー以外のデータが存在するか確認
        	if (lines.Length <= 1)
        	{
        		throw new Exception(
        			$"Test data is empty: {filePath}"
        		);
        	}
        	
        	// ヘッダーを除外してランダムに1行取得
        	int index = Random.Next(1, lines.Length);
        	string selectedLine = lines[index];
        	
        	Report.Info(
        		$"Test data selected from CSV. Row: {index + 1}"
        	);
        	
        	return selectedLine.Split(',');
            
        }
        
        
    }
}
