using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{

    public bool held = false;

    public string keycode;

    // Start is called before the first frame update
    void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Console || SystemInfo.deviceType == DeviceType.Desktop)
        {
            transform.position += Vector3.up * 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool yeet = false;

        if (Input.GetKey(GetKeyCode(keycode.ToLower()[0])))
        {
            //Debug.Log(keycode + " pressed");
            held = true;
            yeet = true;
        }
        if (Input.GetKeyUp(GetKeyCode(keycode.ToLower()[0])))
        {
            //Debug.Log(keycode + "released");
            held = false;
        }
        if (Input.GetButton("Fire1"))
        {
            //Debug.Log("Click");
            if (GetComponent<BoxCollider2D>().bounds.Contains(new Vector3(Main.mouseWorldPosition.x, Main.mouseWorldPosition.y, 0))) {
                //Debug.Log(name + " pressed");
                held = true;
                yeet = true;
            }
        }
        if (!yeet)
            held = false;
    }

    private readonly Dictionary<char, KeyCode> _keycodeCache = new Dictionary<char, KeyCode>();
    private KeyCode GetKeyCode(char character)
    {
        // Get from cache if it was taken before to prevent unnecessary enum parse
        KeyCode code;
        if (_keycodeCache.TryGetValue(character, out code)) return code;
        // Cast to it's integer value
        int alphaValue = character;
        code = (KeyCode)System.Enum.Parse(typeof(KeyCode), alphaValue.ToString());
        _keycodeCache.Add(character, code);
        return code;
    }
    
}
