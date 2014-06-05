using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Moq;

[TestFixture]
public class CurrentStructureTest {

	private CurrentStructure _CurrentStructure;
	private int _MaxSize;
	private Vector3[,,] _Data;


	/*[SetUp]
	[TestCaseSource("CurrentStructureCases")]
	public void SetUpTesting(int MAXSIZE, Vector3[,,] data) {
		//setup test environment
		_MaxSize = MAXSIZE;
		_Data = data;
		_CurrentStructure = new CurrentStructure(MAXSIZE, data);

		//run the tests

	}*/




	[Test, TestCaseSource("CurrentStructureCases")]
	public void SizeTest(int MAXSIZE, Vector3[,,] data){
		//setup test environment
		_MaxSize = MAXSIZE;
		_Data = data;
		_CurrentStructure = new CurrentStructure(MAXSIZE, data);

		//run the tests
		Assert.AreEqual (_MaxSize, _MaxSize);
	}

	[Test, TestCaseSource("CurrentStructureCases")]
	public void CorrectnessTest(int MAXSIZE, Vector3[,,] data){
		//setup test environment
		_MaxSize = MAXSIZE;
		_Data = data;
		_CurrentStructure = new CurrentStructure(MAXSIZE, data);

		//run the tests

	}


	//each case has format {int maxsize, Vector3[,,] data}
	public static object[] CurrentStructureCases =
	{
		new object[] {1,new Vector3[1,1,1] },
		new object[] {1, new Vector3[1, 1, 1] {{{new Vector3(0f,0f,0f)}}}}
	};
}
