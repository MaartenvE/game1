
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtractiveHalfBlockColorBehaviour : IHalfBlockColorBehaviour {
	private Dictionary<ColorPair, AbstractHalfBlockColor>
				_subtractiveColorMap;

	public SubtractiveHalfBlockColorBehaviour(){
		_subtractiveColorMap = new Dictionary<ColorPair,
		AbstractHalfBlockColor>();


	}	

	public void SetMapping(){
		AddMapping (new ColorPair (CreateColor ("yellow"), CreateColor ("blue")), CreateColor ("green"));
	}

	private HalfBlockColor CreateColor(string _color){
		return new HalfBlockColor (_color);
	}

	private void AddMapping(ColorPair _pair, AbstractHalfBlockColor _result){
		_subtractiveColorMap.Add (_pair, _result);
	}

	public AbstractHalfBlockColor CombineColor(AbstractHalfBlockColor first, AbstractHalfBlockColor second){
		ColorPair key = new ColorPair (first, second);
		AbstractHalfBlockColor _result;
		_subtractiveColorMap.TryGetValue (key, out _result);
		return _result;
	}
}
