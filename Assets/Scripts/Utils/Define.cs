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
		Host,
		Auth,
		Lobby,
		Game,
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

	public enum SkillType
	{
		Attack,
		Buff,
		Heal,
		Move
	}

	public enum EnemyAttackType
	{
		Melee,
		Projectile,
		Laser
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

	public enum ItemType
	{
		None,
		Heal,
		Attack,
		MoveSpeed,
		AttackSpeed,
		RotateSpeed,
		BulletSpeed
	}
}
