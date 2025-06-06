﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
	protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

	protected bool _init = false;

	public virtual bool Init()
	{
		if (_init)
			return false;

		return _init = true;
	}

	private void OnEnable()
	{
		Init();
	}

	protected void Bind<T>(Type type) where T : UnityEngine.Object
	{
		string[] names = Enum.GetNames(type);
		UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
		_objects.Add(typeof(T), objects);

		for (int i = 0; i < names.Length; i++)
		{
			if (typeof(T) == typeof(GameObject))
				objects[i] = Utils.FindChild(gameObject, names[i], true);
			else
				objects[i] = Utils.FindChild<T>(gameObject, names[i], true);
			
			if (objects[i] == null)
				Debug.Log($"Failed to bind({names[i]})");
		}
	}
	
	protected void BindWithSound<T>(Type type) where T : UnityEngine.Object
	{
		string[] names = Enum.GetNames(type);
		UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
		_objects.Add(typeof(T), objects);

		for (int i = 0; i < names.Length; i++)
		{
			if (typeof(T) == typeof(GameObject))
				objects[i] = Utils.FindChild(gameObject, names[i], true);
			else
				objects[i] = Utils.FindChild<T>(gameObject, names[i], true);
			
			if (objects[i] == null)
				Debug.Log($"Failed to bind({names[i]})");
		}
	}


	protected void BindObject(Type type) { Bind<GameObject>(type);  }
	protected void BindImage(Type type) { Bind<Image>(type);  }
	protected void BindText(Type type) { Bind<TextMeshProUGUI>(type);  }
	protected void BindButton(Type type) { Bind<Button>(type);  }
	
	protected T Get<T>(int idx) where T : UnityEngine.Object
	{
		UnityEngine.Object[] objects = null;
		if (_objects.TryGetValue(typeof(T), out objects) == false)
			return null;

		return objects[idx] as T;
	}
	
	protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
	protected TextMeshProUGUI GetText(int idx) { return Get<TextMeshProUGUI>(idx); }
	protected Button GetButton(int idx) { return Get<Button>(idx); }
	protected Image GetImage(int idx) { return Get<Image>(idx); }

	public static void BindEvent(GameObject go, Action action, String clipName, Define.UIEvent type = Define.UIEvent.Click)
	{  
		UI_EventHandler evt = Utils.GetOrAddComponent<UI_EventHandler>(go);

		evt.OnClickHandler += () =>
		{
			Managers.UIAnimation.ButtonClickAnimationA(go);
			Managers.Sound.Play(Define.Sound.Effect3, AudioDefine.BUTTON_CLICK_EFFECT);
		};

		/*if (go.TryGetComponent<ButtonSound>(out ButtonSound buttonSound) && buttonSound.GetClip() != null)
		{
			evt.OnPointerDownHandler += () =>
			{
				Managers.Sound.PlayAudioClip(Define.Sound.Effect, buttonSound.GetClip());
			};	
		}
		else
		{
			evt.OnPointerDownHandler += () =>
			{
				Managers.Sound.PlayAudioClip(Define.Sound.Effect, Managers.Sound.clips["UI"]);
			};	
		}*/


		switch (type)
		{
			case Define.UIEvent.Click:
				evt.OnClickHandler -= action;
				evt.OnClickHandler += action;
				break;
			case Define.UIEvent.Pressed:
				evt.OnPressedHandler -= action;
				evt.OnPressedHandler += action;
				break;
			case Define.UIEvent.PointerDown:
				evt.OnPointerDownHandler -= action;
				evt.OnPointerDownHandler += action;
				break;
			case Define.UIEvent.PointerUp:
				evt.OnPointerUpHandler -= action;
				evt.OnPointerUpHandler += action;
				break;
		}
	}
}
