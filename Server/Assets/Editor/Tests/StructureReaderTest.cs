using UnityEngine;
using System.Collections;
using NUnit.Framework;


[TestFixture]
public class StructureReaderTest {

	Color?[,,] parsedResult;
	

	[Test, TestCaseSource("CorrectStructureReaderCases")]
	public void CheckStructureTest(string mapLocation, Color?[][][] expectedResult){


		parsedResult = StructureReader.loadLevel (mapLocation);


		//Assert.Fail ("not implemented");
		int size = expectedResult.Length;

		for (int x = 0; x<size; x++) {
			for (int y = 0; y<size; y++) {
				for (int z = 0; z<size; z++) {
					string errorMessage = "at "+x+" "+y+" "+z;
					Assert.AreEqual(parsedResult[x,y,z], expectedResult[y][x][z], errorMessage);
				}
			}
		}
	}


	private string adress = "/maps/testmaps/test1.structure";
	//each case has format {Color[][][] parsedResult, Color[][[][] expectedResult, int size}
	private static object[] CorrectStructureReaderCases =
	{
		new object[] {Application.dataPath+adress, new Color?[2][][]{
				new Color?[2][]{new Color?[2] {ColorModel.RED, null}
				 			 , new Color?[2]{null, ColorModel.PURPLE}}
				, new Color?[2][]{new Color?[2] {ColorModel.BLUE, null}
	     					 , new Color?[2]{null, ColorModel.GREEN}}}}
	};
}
