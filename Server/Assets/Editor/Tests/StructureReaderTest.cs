using UnityEngine;
using System.Collections;
using NUnit.Framework;


[TestFixture]
public class StructureReaderTest {

	//Color?[,,] parsedResult;
	
	//functions directly implemented instead of parameterized due to failure of unity jenkins.
	[Test]
	public void CheckStructureTest(){
		string mapLocation = Application.dataPath+"/maps/testmaps/test1.structure";
		Color?[][][] expectedResult = new Color?[2][][]{
			new Color?[2][]{new Color?[2] {ColorModel.RED, null}
				, new Color?[2]{null, ColorModel.PURPLE}}
			, new Color?[2][]{new Color?[2] {ColorModel.BLUE, null}
				, new Color?[2]{null, ColorModel.GREEN}}};

		Color?[,,] parsedResult = StructureReader.loadLevel (mapLocation);


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
}
