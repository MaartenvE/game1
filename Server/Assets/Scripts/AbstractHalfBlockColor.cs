using System.Collections;
using System;
using UnityEngine;

public abstract class AbstractHalfBlockColor : IEquatable<AbstractHalfBlockColor>
{
	private string _color;

	public string color {
		get{ return _color;}
		protected set{ _color = value;}
	}

	public override int GetHashCode() {
		return _color.GetHashCode ();
	}
	
	public bool Equals(AbstractHalfBlockColor other){
		return this.color.Equals(other.color);
	}

	public abstract AbstractHalfBlockColor CombineColor(AbstractHalfBlockColor other);

	//IHalfBlockColor CombineColor(IHalfBlockColor other);
}
