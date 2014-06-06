using UnityEngine;
using System.Collections;

public interface IHalfBlockColorBehaviour {
	AbstractHalfBlockColor CombineColor(AbstractHalfBlockColor first, AbstractHalfBlockColor second);
	void SetMapping();
}
