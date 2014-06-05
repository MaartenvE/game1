using UnityEngine;
using System.Collections;

public class CurrentStructure {

	private Vector3[,,] _GoalStructure; 
	private bool[,,] _CorrectStructure;

	private int currentCorrectness = 0;
	private int fullyCorrect;

	private int MAX_SIZE;

	public CurrentStructure(int MaxSize, Vector3[,,] goalStructure){
		MAX_SIZE = MaxSize;
		fullyCorrect = MaxSize * MaxSize * MaxSize; //3 dimensions of maxsize gives the total amount of correctness

		_GoalStructure = goalStructure;
		_CorrectStructure = new bool[MAX_SIZE, MAX_SIZE, MAX_SIZE];

		//calculate currentcorrectness (which, since the currentstructure is empty, is equal to every null in goalstructure)
		for(int i = 0; i<MaxSize; i++){
			for(int j = 0; j<MaxSize; j++){
				for(int k = 0; k<MaxSize; k++){
					if(_GoalStructure[i,j,k] == null){
						currentCorrectness++;
						_CorrectStructure[i,j,k] = true;
					}
				}
			}
		}
	}

	//checks if the current correctness is correct
	public void updateCorrectness(Vector3 location, Vector3 color){
		if(getColor (location)==color){
			if(!getCorrectness (location)){
				setCorrectness(location, true);
				currentCorrectness++;
			}
			//else the value does not change
		}
		else{
			if(getCorrectness(location)){
				setCorrectness (location, false);
				currentCorrectness--;
			}
			//else the value does not change
		}
	}

	public Vector3 getColor(Vector3 location){
		return _GoalStructure [(int)location.x, (int)location.y, (int)location.z];
	}
	public bool getCorrectness(Vector3 location){
		return _CorrectStructure [(int)location.x, (int)location.y, (int)location.z];
	}
	public void setCorrectness(Vector3 location, bool newCorrectness){
		_CorrectStructure [(int)location.x, (int)location.y, (int)location.z] = newCorrectness;
	}

	public float getCurrentCorrectnessPercentage(){
		return currentCorrectness / fullyCorrect;
	}

	public bool isCorrect(){
		return fullyCorrect==currentCorrectness;
	}
}
