using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Define
{

	public enum UIEvent
	{
		Click,
		Pressed,
		PointerDown,
		PointerUp,
	}

	public enum Scene
	{
		Unknown,
		Loading,
		Main,
		AR,
	}

	public enum Sound
	{
		Bgm,
		Effect,
		Speech,
		Effect2,
		Effect3,
		Max,
	}

	public enum ContentType
	{
		Animation,
		Puzzle,
		Room,
		DragAndDrop
	}

	public enum CharacterState
	{
		Appear,
		Stop,
		Moving,
		Attack,
		Dead
	}
}
