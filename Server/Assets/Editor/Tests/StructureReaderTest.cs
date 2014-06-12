using UnityEngine;
using System.Collections;
using NUnit.Framework;


[TestFixture]
public class StructureReaderTest {

	Color[][][] parsedResult;
	

	[Test, TestCaseSource("CorrectStructureReaderCases")]
	public void CheckStructureTest(string mapLocation, Color[][][] expectedResult){
		parsedResult = StructureReader.loadLevel (mapLocation);

		//Assert.Fail ("not implemented");
		int size = expectedResult.Length;
		for (int x = 0; x<size; x++) {
			for (int y = 0; y<size; y++) {
				for (int z = 0; z<size; z++) {
					string errorMessage = "at "+x+" "+y+" "+z;
					//Assert.Fail ("not implemented");
					Assert.AreEqual(parsedResult[x][y][z], expectedResult[x][y][z], errorMessage);
				}
			}
		}
	}



	//each case has format {Color[][][] parsedResult, Color[][[][] expectedResult, int size}
	public static object[] CorrectStructureReaderCases =
	{
		//new object[] {null, null}
		new object[] {Application.dataPath+"/maps/testmaps/test1.txt", new Color[2][][]{
				new Color[2][]{new Color[2] {ColorModel.RED, ColorModel.NONE}
				 			 , new Color[2]{ColorModel.NONE, ColorModel.PURPLE}}
				, new Color[2][]{new Color[2] {ColorModel.BLUE, ColorModel.NONE}
	     					 , new Color[2]{ColorModel.NONE, ColorModel.GREEN}}}}
	};
}
