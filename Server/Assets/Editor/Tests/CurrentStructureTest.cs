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
	public void SizeTest(int id, int MAXSIZE, Vector3[,,] data, float correctness){
		//setup test environment
		_MaxSize = MAXSIZE;
		_Data = data;
		_CurrentStructure = new CurrentStructure(MAXSIZE, data);

		//run the tests
		Assert.AreEqual (_MaxSize, _MaxSize);
	}

	[Test, TestCaseSource("CurrentStructureCases")]
	public void CorrectnessTest(int id, int MAXSIZE, Vector3[,,] data, float correctness){
		//setup test environment
		_MaxSize = MAXSIZE;
		_Data = data;
		_CurrentStructure = new CurrentStructure(MAXSIZE, data);

		//run the test
		Assert.AreEqual (correctness, _CurrentStructure.getCurrentCorrectnessFraction());

		//modify test environment
		if(_CurrentStructure.getColor(new Vector3(0,0,0))==new Vector3(0,0,0)){
			_CurrentStructure.updateCorrectness(new Vector3(0,0,0),new Vector3(0.5f,0.5f,0.5f));

		}
		else {
				_CurrentStructure.updateCorrectness(new Vector3(0,0,0),new Vector3(0f,0f,0f));
		}
		//since we just changed the correctness with negative 1 (except when already 0) we need to update this
		if (correctness != 0f) {
			correctness = (correctness*_MaxSize-1)/(_MaxSize);
		}

		//run the tests
		Assert.AreEqual (correctness, _CurrentStructure.getCurrentCorrectnessFraction());

	}


	//each case has format {int maxsize, Vector3[,,] data, float initialCorrectness}
	public static object[] CurrentStructureCases =
	{
		new object[] {1, 1, new Vector3[1, 1, 1] , 1f }
		, new object[] {2, 1, new Vector3[1, 1, 1] {{{new Vector3 (1f, 1f, 1f)}}}, 0f}
		, new object[] {3, 1, new Vector3[1, 1, 1] {{{new Vector3 (0.5f, 0.3f, 0.1f)}}}, 0f}
	};
}
