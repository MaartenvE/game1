using System;

public class HalfBlockColorComparer : IEqualityComparer<HalfBlockColor>
{
	public bool Equals(HalfBlockColor x, HalfBlockColor y){
		return x.Equals(y);
	}

	public int GetHashCode(IHalfBlockColor _this){
		return _this.GetColorName ().GetHashCode ();
	}
}