using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class Enemy
{
    [Header("Properties")]
    public int health;
    public float speed;
    public float fireDelay;
    public int scoreValue;

    [Header("Space Bounds")]
    public float xLimit;
    public float yLimit;

    [Header("References")]
    public List<BaseWeapon> weapons;
    public Transform bulletSpawn;
    public GameObject shot;
    public Animator anim;

    [Header("Audios")]
    public AudioClip[] audioClips;
    [HideInInspector]
    public AudioSource properAudioSource;
}
