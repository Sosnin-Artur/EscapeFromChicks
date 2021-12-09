using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControllerMobileFPS : MonoBehaviour
{
    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float gravity = -9.8f;    
    [SerializeField] private float rechargeTime = 1.0f;    
    [SerializeField] private float weaponTime = 5.0f;    

    [SerializeField] private CharacterController charController;
    [SerializeField] private Camera camera;    
    [SerializeField] private Joystick joystick;    
    [SerializeField] private GameObject projectale;    
    [SerializeField] private GameObject fireball;    
    
    private bool _isRecharge;
    private bool _isChanged;

    private Vector3 _offset;
    
    public void Shoot()
    {               
        if (!_isRecharge)
        {            
            _offset = new Vector3(0, 1, 0);
            Vector3 mousePos = ConvertMousePosition();
            
            if (mousePos == Vector3.zero) return;
            
            Vector3 difference = mousePos - transform.position;
            difference.Normalize();        
            float angleY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;                
            float angleX = Mathf.Asin(-difference.y) * Mathf.Rad2Deg;                

            _offset.x *= difference.x;
            _offset.z *= difference.z;
            
            GameObject obj = Instantiate(projectale, transform.position + _offset, Quaternion.Euler(angleX, angleY, 0));            
            _isRecharge = true;
            StartCoroutine(Recharge());
        }          
    }
    
    private void Awake()
    {
        Messenger.AddListener(GameEvent.WEAPON_CHANGED, ChangeProjectile);
    }
        
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.WEAPON_CHANGED, ChangeProjectile);
    }
        
    private void Update()
    {
        Move();                 
    }    

    private void Move()
    {
        float deltaX = joystick.Horizontal * speed;
        float deltaZ = joystick.Vertical * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        charController.Move(movement);
    }    
    
    private IEnumerator Recharge()
    {
        yield return new WaitForSeconds(rechargeTime);
        _isRecharge = false;
    }
    
    private IEnumerator ChangeWeapon()
    {
        if (!_isChanged)
        {
            GameObject temp = projectale;
            projectale = fireball;
            _isChanged = true;
            yield return new WaitForSeconds(5.0f);
            projectale = temp;
            _isChanged = false;
            
        }        
    }
    
    private void ChangeProjectile()
    {
        StartCoroutine(ChangeWeapon());
    }

    private Vector3 ConvertMousePosition()
    {
        Vector3 target = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2));
        RaycastHit mouseHit;
        
        if (Physics.Raycast(ray, out mouseHit))
        {
            target = mouseHit.point;            
        }

        return target - _offset;
    }

    private void OnGUI()
    {
        int size = 12;
        float posX = camera.pixelWidth / 2 - size / 4;
        float posY = camera.pixelHeight / 2 - size / 2;

        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}
