# UniLerp
UniLerp is a small Unity library for smoothly transitioning game objects between position, scale and rotation.

## What is this?
This is a (very) small library for LERPing game objects in unity between positions, scale and rotation.

## How do I use it?
Simply include somewhere in your C# project, then use the StartLerp method to register a game object for a transition. Then, use the Tick method somewhere on an update method on a Unity Behaviour.

#### This should looks somewhat like this:

	using UnityEngine;

	public class FooBehaviour : MonoBehaviour
	{
	    public GameObject Goal;

	    void Start()
	    {
	        UniLerp.StartLerp(Goal,
	         new Vector3(-30.83f, 1.0f, 1.0f),
	         new Vector3(100f, 90f, 90f),
	         new Vector3(1.0f, 1.0f, 1.0f)
	        );
	    }

	    void Update()
	    {
	        UniLerp.Tick();
		}
	}

With Goal being passed in through the Unity editor as a parameter.

## When will it update?

If I need further features or capabilities, I'll be adding them. If you have anything extra you need from the library, or any improvements you can offer, please make a pull request!

## Who did this?
[That would be me.](http://jordanrobinson.co.uk)
